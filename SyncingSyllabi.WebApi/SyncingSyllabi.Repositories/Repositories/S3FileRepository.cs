using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Amazon.Util;
using SyncingSyllabi.Common.Tools.Helpers;
using SyncingSyllabi.Data.Constants;
using SyncingSyllabi.Data.Settings;
using SyncingSyllabi.Repositories.Interfaces;
using System;
using Amazon.Textract;
using Amazon.Textract.Model;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace SyncingSyllabi.Repositories.Repositories
{
    public partial class S3FileRepository : IS3FileRepository
    {
        private readonly S3Settings _s3Settings;

        private static readonly Encoding ContentDispositionHeaderEncoding = Encoding.GetEncoding("ISO-8859-1");

        public S3FileRepository
        (
            S3Settings s3Settings
        )
        {
            _s3Settings = s3Settings;
        }

        private IAmazonS3 CreateS3Client()
        {
            RegionEndpoint region = RegionEndpoint.GetBySystemName(_s3Settings.Region);
            IAmazonS3 client = null;

            if (!string.IsNullOrWhiteSpace(AwsConstants.AKI) &&
                !string.IsNullOrWhiteSpace(AwsConstants.SAK))
            {
                client = new AmazonS3Client(EncryptionHelper.DecryptString(AwsConstants.AKI), EncryptionHelper.DecryptString(AwsConstants.SAK), region);
            }
            else
            {
                client = new AmazonS3Client(region);
            }

            return client;
        }

        private async Task EnsureBucket(IAmazonS3 s3client, string bucketName)
        {
            // NOTE: regions other than east-1 will throw an error if an attempt to recreate existing bucket
            // https://stackoverflow.com/questions/35356838/getting-error-bucketalreadyownedbyyou-your-previous-request-to-create-the-nam
            // https://docs.aws.amazon.com/AmazonS3/latest/API/ErrorResponses.html            
            if (!await s3client.DoesS3BucketExistAsync(bucketName))
            {
                await s3client.PutBucketAsync(bucketName);
            }
        }

        public async Task UploadFile(string directory, string externalKey, byte[] buffer)
        {
            try
            {
                //XrayUtils.AddXRayAnnotation(XrayAnnotationConstants.ExternalKey, $"{_s3Settings.BucketName}{directory}/{externalKey}");

                var request = new TransferUtilityUploadRequest();
                request.BucketName = $"{_s3Settings.BucketName}{directory}";

                request.Key = externalKey;

                request.InputStream = new MemoryStream(buffer);

                IAmazonS3 client = CreateS3Client();
                TransferUtility utility = new TransferUtility(client);
                await EnsureBucket(utility.S3Client, _s3Settings.BucketName);
                await utility.UploadAsync(request);
            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                if
                    (
                        amazonS3Exception.ErrorCode != null &&
                        (
                            amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId") ||
                            amazonS3Exception.ErrorCode.Equals("InvalidSecurity")
                        )
                    )
                {
                    throw new Exception("Please check the provided AWS Credentials.");
                }
                else
                {
                    throw new Exception("An error occurred with the message " + amazonS3Exception.Message + " when writing an object");
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public async Task<byte[]> DownloadFile(string directory, string externalKey)
        {
            try
            {
                GetObjectRequest request = new GetObjectRequest();
                request.BucketName = $"{_s3Settings.BucketName}{directory}";
                request.Key = externalKey;

                IAmazonS3 client = CreateS3Client();
                GetObjectResponse response = await client.GetObjectAsync(request);

                using (Stream responseStream = response.ResponseStream)
                {
                    byte[] buffer = new byte[16 * 1024];

                    using (MemoryStream ms = new MemoryStream())
                    {
                        int read;
                        while ((read = responseStream.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            ms.Write(buffer, 0, read);
                        }

                        return ms.ToArray();
                    }
                }
            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                if
                    (
                        amazonS3Exception.ErrorCode != null &&
                        (
                            amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId") ||
                            amazonS3Exception.ErrorCode.Equals("InvalidSecurity")
                        )
                    )
                {
                    throw new Exception("Please check the provided AWS Credentials.");
                }
                else
                {
                    throw new Exception("An error occurred with the message " + amazonS3Exception.Message + " when writing an object");
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public string GetPreSignedUrl(string directory, string externalKey, string contentType, string fileName, DateTime expiry)
        {
            try
            {
                var request = new GetPreSignedUrlRequest();
                request.BucketName = $"{_s3Settings.BucketName}{directory}";
                request.Key = externalKey;
                
                if(!string.IsNullOrEmpty(contentType))
                {
                    string safeFileName = GetWebSafeFileName(fileName);
                    request.ResponseHeaderOverrides.ContentType = contentType;
                    request.ResponseHeaderOverrides.ContentDisposition = $"attachment; filename={safeFileName}";
                }
              
                request.Expires = expiry;

                IAmazonS3 client = CreateS3Client();
                string url = client.GetPreSignedURL(request);
                return url;
            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                if
                    (
                        amazonS3Exception.ErrorCode != null &&
                        (
                            amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId") ||
                            amazonS3Exception.ErrorCode.Equals("InvalidSecurity")
                        )
                    )
                {
                    throw new Exception("Please check the provided AWS Credentials.");
                }
                else
                {
                    throw new Exception("An error occurred with the message " + amazonS3Exception.Message + " when writing an object");
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public static string GetWebSafeFileName(string fileName)
        {
            fileName = AWSSDKUtils.UrlEncode(fileName, false);
            // We need to convert the file name to ISO-8859-1 due to browser compatibility problems with the Content-Disposition Header (see: https://stackoverflow.com/a/216777/1038611)
            var webSafeFileName = Encoding.Convert(Encoding.Unicode, ContentDispositionHeaderEncoding, Encoding.Unicode.GetBytes(fileName));

            // Furthermore, any characters not supported by ISO-8859-1 will be replaced by « ? », which is not an acceptable file name character. So we replace these as well.
            return ContentDispositionHeaderEncoding.GetString(webSafeFileName).Replace('?', '-');
        }

        public async Task CopyObject(string sourceBucketName, string sourceDirectory, string sourceExternalKey, string destinationBucketName, string destinationDirectory, string destinationExternalKey)
        {
            try
            {
                IAmazonS3 client = CreateS3Client();

                await client.CopyObjectAsync($"{sourceBucketName}{sourceDirectory}", sourceExternalKey, $"{destinationBucketName}{destinationDirectory}", destinationExternalKey);
            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                if
                    (
                        amazonS3Exception.ErrorCode != null &&
                        (
                            amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId") ||
                            amazonS3Exception.ErrorCode.Equals("InvalidSecurity")
                        )
                    )
                {
                    throw new Exception("Please check the provided AWS Credentials.");
                }
                else
                {
                    throw new Exception("An error occurred with the message " + amazonS3Exception.Message + " when writing an object");
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public async Task StartDetectAsync(string directory, string externalKey, byte[] buffer)
        {

            var awsCredentials = new Amazon.Runtime.BasicAWSCredentials(EncryptionHelper.DecryptString(AwsConstants.AKI), EncryptionHelper.DecryptString(AwsConstants.SAK));

            RegionEndpoint region = RegionEndpoint.GetBySystemName(_s3Settings.Region);

            var textractClient = new AmazonTextractClient(awsCredentials, region);
            
            var detectResponse = await textractClient.DetectDocumentTextAsync(new DetectDocumentTextRequest
            {
                Document = new Document
                {
                    Bytes = new MemoryStream(buffer)
                }
            });

            foreach (var block in detectResponse.Blocks)
            {
                Console.WriteLine($"Type {block.BlockType}, Text: {block.Text}");
            }

            await this.UploadFile(directory, externalKey, buffer);
        }
    }
}
