﻿using Amazon;
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
using System.Collections.Generic;
using SyncingSyllabi.Data.Models.Core;
using System.Linq;
using SyncingSyllabi.Common.Tools.Extensions;
using SyncingSyllabi.Data.Models.Response;
using Microsoft.AspNetCore.Http;
using Aspose;
using Aspose.Pdf.Devices;
using SyncingSyllabi.Data.Enums;

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

        public async Task<OcrScanReponseDataModel> SyllabusDetectAsync(IFormFile imageFile, IEnumerable<string> pdfFile, IEnumerable<int> pages, OcrTypeEnum ocrTypeEnum)
        {
            var fileByte = new List<byte[]>();

            var result = new MemoryStream();

            var assignmentDetail = new OcrAssignmentResponseModel();
            var syllabusDetail = new OcrSyllabusResponseModel();

            var syllabusModel = new OcrScanReponseDataModel();

            var teacherList = new List<TeacherNameModel>();
            var classCodeList = new List<ClassCodeModel>();
            var classNameList = new List<ClassNameModel>();
            var classScheduleList = new List<ClassScheduleModel>();
            var assignmentTitleList = new List<AssignmentTitleModel>();
            var table = new TableModel();
            var tableDetails = new List<TableDetailModel>();
            var assgnList = new List<OcrAssignmentResponseModel>();

            var awsCredentials = new Amazon.Runtime.BasicAWSCredentials(EncryptionHelper.DecryptString(AwsConstants.AKI), EncryptionHelper.DecryptString(AwsConstants.SAK));

            RegionEndpoint region = RegionEndpoint.GetBySystemName(_s3Settings.Region);

            var textractClient = new AmazonTextractClient(awsCredentials, region);

            if(imageFile != null)
            {
                using (var ms = new MemoryStream())
                {
                    imageFile.CopyTo(ms);
                    var fileData = Convert.ToBase64String(ms.ToArray());
                    fileByte.Add(Convert.FromBase64String(fileData));
                    //Aspose.Pdf.Document pdfDocument = new Aspose.Pdf.Document(ms);
                    //var filterPdf = pdfDocument.Pages.Where(w => pages.Any(a => a == w.Number)).ToList();

                    //foreach (var page in filterPdf)
                    //{
                    //    //// Define Resolution
                    //    //Resolution resolution = new Resolution(400);

                    //    //// Create Jpeg device with specified attributes
                    //    //// Width, Height, Resolution
                    //    //JpegDevice JpegDevice = new JpegDevice(600, 800, resolution);

                    //    //// Convert a particular page and save the image to stream
                    //    //JpegDevice.Process(pdfDocument.Pages[page.Number], result);
                    //    //var fileData = Convert.ToBase64String(result.ToArray());

                    //    if(imageFile != null)
                    //    {
                    //        fileByte = Convert.FromBase64String(fileData);
                    //    }
                    //    else
                    //    {
                    //        fileByte = Convert.FromBase64String(pdfFile);
                    //    }
                    //}
                }
            }
            else
            {
                if(pdfFile.Count() > 0)
                {
                    foreach(var file in pdfFile)
                    {

                        fileByte.Add(Convert.FromBase64String(file));
                    }
                }
            }

            foreach(var byteData in fileByte)
            {
                var featureType = new List<string>()
                {
                    "QUERIES",
                    "TABLES",
                    "FORMS"
                };

                var queryConfig = new QueriesConfig();

                if (ocrTypeEnum == OcrTypeEnum.Syllabus)
                {
                    // Syllabi

                    var instrucorQueries = new List<Query>()
                        {
                            new Query()
                            {
                                Text = "Professor Name",
                                Alias = "Professor"
                            },
                            new Query()
                            {
                                Text = "Instructor Name",
                                Alias = "Instructor"
                            },
                             new Query()
                            {
                                Text = "Teacher Name",
                                Alias = "Teacher"
                            },
                            new Query()
                            {
                                Text = "Lecturer Name",
                                Alias = "Lecturer"
                            },
                            new Query()
                            {
                                Text = "Doctor",
                                Alias = "Dr."
                            },
                            new Query()
                            {
                                Text = "Tutor Name",
                                Alias = "Tutor"
                            }
                        };

                    queryConfig.Queries = instrucorQueries;

                    var instructorAnalyze = await textractClient.AnalyzeDocumentAsync(new AnalyzeDocumentRequest
                    {
                        Document = new Document
                        {
                            Bytes = new MemoryStream(byteData)
                        },
                        FeatureTypes = featureType,
                        QueriesConfig = queryConfig
                    });

                    var instructor = instructorAnalyze.Blocks.Where(w => w.Text != null && w.BlockType != null && (w.BlockType == "QUERY_RESULT" || w.BlockType == "TABLE" || w.BlockType == "CELL"))
                                                             .GroupBy(gp => gp.Text)
                                                             .Select(s => s.FirstOrDefault())
                                                             .ToList();

                    if (instructor.Count() > 0)
                    {
                        teacherList.AddRange(instructor
                                            .Select(s => new
                                            TeacherNameModel
                                            {
                                                Name = s.Text,
                                                ConfidenceScore = s.Confidence
                                            }));
                    }

                    var classCodeQueries = new List<Query>()
                        {
                            new Query()
                            {
                                Text = "Syllabus"
                            },
                            new Query()
                            {
                                Text = "Class Number"
                            },
                            new Query()
                            {
                                Text = "Class Code"
                            },
                            new Query()
                            {
                                Text = "Course Number"
                            },
                            new Query()
                            {
                                Text = "Subject Code"
                            },
                            new Query()
                            {
                                Text = "Subject Number"
                            },
                            new Query()
                            {
                                Text = "Course Code"
                            },
                            new Query()
                            {
                                Text = "Code"
                            }
                        };

                    queryConfig.Queries = classCodeQueries;

                    var classCodeAnalyze = await textractClient.AnalyzeDocumentAsync(new AnalyzeDocumentRequest
                    {
                        Document = new Document
                        {
                            Bytes = new MemoryStream(byteData)
                        },
                        FeatureTypes = featureType,
                        QueriesConfig = queryConfig
                    });

                    var classCode = classCodeAnalyze.Blocks.Where(w => w.Text != null && w.BlockType != null && (w.BlockType == "QUERY_RESULT" || w.BlockType == "TABLE" || w.BlockType == "CELL"))
                                                           .GroupBy(gp => gp.Text)
                                                           .Select(s => s.FirstOrDefault())
                                                           .ToList();

                    if (classCode.Count() > 0)
                    {
                        classCodeList.AddRange(classCode
                                                .Select(s => new
                                                ClassCodeModel
                                                {
                                                    Name = s.Text,
                                                    ConfidenceScore = s.Confidence
                                                }));

                    }

                    var classNameQueries = new List<Query>()
                        {
                            new Query()
                            {
                                Text = "Class Name",
                                Alias = "Class"
                            },
                            new Query()
                            {
                                Text = "Course Name",
                                Alias = "Course"
                            },
                            new Query()
                            {
                                Text = "Subject Name",
                                Alias = "Subject"
                            },
                            new Query()
                            {
                                Text = "Syllabus"
                            }

                        };

                    queryConfig.Queries = classNameQueries;

                    var classNameAnalyze = await textractClient.AnalyzeDocumentAsync(new AnalyzeDocumentRequest
                    {
                        Document = new Document
                        {
                            Bytes = new MemoryStream(byteData)
                        },
                        FeatureTypes = featureType,
                        QueriesConfig = queryConfig
                    });

                    var className = classNameAnalyze.Blocks.Where(w => w.Text != null && w.BlockType != null && (w.BlockType == "QUERY_RESULT" || w.BlockType == "TABLE" || w.BlockType == "CELL"))
                                                           .GroupBy(gp => gp.Text)
                                                           .Select(s => s.FirstOrDefault())
                                                           .ToList();

                    if (className.Count() > 0)
                    {
                        classNameList.AddRange(classCode
                                                .Select(s => new
                                                ClassNameModel
                                                {
                                                    Name = s.Text,
                                                    ConfidenceScore = s.Confidence

                                                }));
                    }

                    var scheduleQueries = new List<Query>()
                        {
                            new Query()
                            {
                                Text = "Schedule Time"
                            },
                            new Query()
                            {
                                Text = "Lec Time"
                            },
                            new Query()
                            {
                                Text = "Lab Time"
                            },
                            new Query()
                            {
                                Text = "Time"
                            },
                            new Query()
                            {
                                Text = "Date"
                            },
                            new Query()
                            {
                                Text = "Day"
                            },
                            new Query()
                            {
                                Text = "Class Time"
                            },
                            new Query()
                            {
                                Text = "Monday",
                                Alias = "Mon"
                            },
                            new Query()
                            {
                                Text = "Tuesday",
                                Alias = "Tue"
                            },
                            new Query()
                            {
                                Text = "Wednesday",
                                Alias = "Wed"
                            },
                            new Query()
                            {
                                Text = "Thursday",
                                Alias = "Thur"
                            },
                             new Query()
                            {
                                Text = "Friday",
                                Alias = "Fri"
                            },

                        };

                    queryConfig.Queries = scheduleQueries;

                    var scheduleAnalyze = await textractClient.AnalyzeDocumentAsync(new AnalyzeDocumentRequest
                    {
                        Document = new Document
                        {
                            Bytes = new MemoryStream(byteData)
                        },
                        FeatureTypes = featureType,
                        QueriesConfig = queryConfig
                    });

                    var schedule = scheduleAnalyze.Blocks.Where(w => w.Text != null && w.BlockType != null && (w.BlockType == "QUERY_RESULT" || w.BlockType == "TABLE" || w.BlockType == "CELL"))
                                                         .GroupBy(gp => gp.Text)
                                                         .Select(s => s.FirstOrDefault())
                                                         .ToList();

                    if (schedule.Count() > 0)
                    {
                        classScheduleList.AddRange(schedule
                                                    .Select(s => new
                                                    ClassScheduleModel
                                                    {
                                                        Name = s.Text,
                                                        ConfidenceScore = s.Confidence
                                                    }));
                    }

                    syllabusModel.OcrSyllabusModel = syllabusDetail;

                    syllabusModel.OcrSyllabusModel.TeacherName = teacherList.GroupBy(gp => gp.Name).Select(s => s.FirstOrDefault()).ToList();
                    syllabusModel.OcrSyllabusModel.ClassCode = classCodeList.GroupBy(gp => gp.Name).Select(s => s.FirstOrDefault()).ToList();
                    syllabusModel.OcrSyllabusModel.ClassName = classNameList.GroupBy(gp => gp.Name).Select(s => s.FirstOrDefault()).ToList();
                    syllabusModel.OcrSyllabusModel.ClassSchedule = classScheduleList;

                }
                else
                {

                    // Assignement
                    var assignmentQueries = new List<Query>()
                        {
                            new Query()
                            {
                                Text = "Assignments",
                                Alias = "Assignment"
                            },
                            new Query()
                            {
                                Text = "Assignment Title"
                            },
                            new Query()
                            {
                                Text = "Title"
                            }
                        };


                    queryConfig.Queries = assignmentQueries;

                    var assignmentAnalyze = await textractClient.AnalyzeDocumentAsync(new AnalyzeDocumentRequest
                    {
                        Document = new Document
                        {
                            Bytes = new MemoryStream(byteData)
                        },
                        FeatureTypes = featureType,
                        QueriesConfig = queryConfig
                    });

                    // Get The Cell First and Local the Row and ColumnIndex to get the Specific Words on a TABLE format

                    var cell = assignmentAnalyze.Blocks.Where(w => w.BlockType.Value == "CELL").ToList();
                    var mergecell = assignmentAnalyze.Blocks.Where(w => w.BlockType.Value == "MERGED_CELL").ToList();
                    var query = assignmentAnalyze.Blocks.Where(w => w.BlockType.Value == "QUERY_RESULT").ToList();
                    var line = assignmentAnalyze.Blocks.Where(w => w.BlockType.Value == "LINE").ToList();

                    // Loop each row
                    var counter = assignmentAnalyze.Blocks.Where(w => w.BlockType.Value == "CELL").Select(s => s.RowIndex).GroupBy(gb => new { gb }).Count() + 1;

                    var deadlineFilter = new List<string>()
                    {
                        "week of",
                        "due date",
                        "deadline"
                    };

                    var assignmentFilter = new List<string>()
                    {
                        "assignment",
                        "assignments",
                        "homework",
                        "homeworks"
                    };

                    if(counter > 0)
                    {

                        for (int tableCounter = 1; tableCounter < counter; tableCounter++)
                        {
                            var columnDetails = new List<Block>();

                            if (tableCounter == 1)
                            {
                                // Get column header for table format
                                columnDetails = assignmentAnalyze
                                                .Blocks
                                                .Where(w =>
                                                      (w.BlockType.Value == "CELL" ||
                                                       w.BlockType.Value == "MERGED_CELL") &&
                                                       w.EntityTypes.Contains("COLUMN_HEADER") &&
                                                       w.RowIndex == 1).ToList();
                            }
                            else
                            {

                                // Get column data for table format
                                columnDetails = assignmentAnalyze
                                                .Blocks
                                                .Where(w => 
                                                      (w.BlockType.Value == "CELL" ||
                                                       w.BlockType.Value == "MERGED_CELL") &&
                                                       w.RowIndex == tableCounter)
                                                .ToList();

                            }

                            foreach (var data in columnDetails)
                            {
                                var tableData = new TableDetailModel();
                                var childId = new List<string>();

                                tableData.TableId = data.Id;
                                tableData.RowIndex = data.RowIndex;
                                tableData.ColumnIndex = data.ColumnIndex;
                                tableData.ConfidenceScore = data.Confidence;

                                if(tableCounter == 1)
                                {
                                    tableData.IsHeader = true;
                                }
                                else
                                {
                                    tableData.IsHeader = false;
                                }

                                if (data.Relationships.Count > 0)
                                {
                                    foreach (var child in data.Relationships)
                                    {
                                        child.Ids.ForEach(
                                        f =>
                                        {
                                            childId.Add(f);
                                        });
                                    }
                                }

                                tableData.ChildIds = childId;

                                tableDetails.Add(tableData);
                            }

                            foreach (var details in tableDetails)
                            {
                                var columnValue = assignmentAnalyze.Blocks
                                                                    .Where(w => details.ChildIds
                                                                    .Any(dc => w.Relationships
                                                                    .Any(ra => ra.Ids
                                                                    .Contains(dc)) && w.Text != null))
                                                                    .ToList();

                                if(columnValue.Count > 0)
                                {
                                    details.Value = columnValue.FirstOrDefault().Text;
                                }
                            }
                        }
                    }

                    //var getColumnHeaderValue = assignmentAnalyze.Blocks
                    //                           .Where(w => columnHeader
                    //                           .Any(ch => ch.Relationships
                    //                           .Any(ra => ra.Ids
                    //                           .Contains(w.Id))) && w.BlockType == "WORD")
                    //                           .ToList();

                    //var assignment = assignmentAnalyze.Blocks.Where(w => w.Text != null && w.BlockType != null && (w.BlockType.Value == "QUERY_RESULT" || w.BlockType.Value == "TABLE" || w.BlockType.Value == "CELL" || w.BlockType.Value == "LINE"))
                    //                                         .GroupBy(gp => gp.Text)
                    //                                         .Select(s => s.FirstOrDefault())
                    //                                         .ToList();

                    var assignmentResponseModel = new List<OcrAssignmentResponseModel>();

                    if(tableDetails.Count > 0)
                    {
                        var getAssignmentHeader = tableDetails
                                                   .Where(w => w.Value != null && assignmentFilter
                                                   .Any(a => a
                                                   .Contains(w.Value.ToLower())))
                                                   .Select(s => new { s.RowIndex, s.ColumnIndex })
                                                   .FirstOrDefault();

                        var deadlineHeader = tableDetails
                                               .Where(w => w.Value != null && deadlineFilter
                                               .Any(a => a
                                               .Contains(w.Value.ToLower())))
                                               .Select(s => new { s.RowIndex, s.ColumnIndex })
                                               .FirstOrDefault();

                        for (int i = 2; i < counter; i++)
                        {
                            var assgn = new OcrAssignmentResponseModel();

                            // Skip header
                            foreach (var item in tableDetails.Where(w => w.RowIndex == i && w.ColumnIndex == getAssignmentHeader.ColumnIndex))
                            {
                                if (getAssignmentHeader != null && item.ColumnIndex == getAssignmentHeader.ColumnIndex && item.Value != null)
                                {
                                    var title = new AssignmentTitleModel()
                                    {
                                        Name = item.Value,
                                        ConfidenceScore = item.ConfidenceScore
                                    };

                                    assgn.AssignmentTitle = title;
                                }
                            }

                            foreach (var item in tableDetails.Where(w => w.RowIndex == i && w.ColumnIndex == deadlineHeader.ColumnIndex))
                            {
                                if (getAssignmentHeader != null && item.ColumnIndex == deadlineHeader.ColumnIndex && item.Value != null)
                                {
                                    var dateStart = new AssignmentStartDateModel();
                                    var dateEnd = new AssignmentEndDateModel();

                                    var deadline = item.Value.Split("-");

                                    if (deadline.Count() == 0 || deadline.Count() == 1)
                                    {
                                        dateStart.Name = DateTime.Now.ToShortDateString();
                                        dateStart.ConfidenceScore = item.ConfidenceScore;

                                        dateEnd.Name = deadline.First();
                                        dateEnd.ConfidenceScore = item.ConfidenceScore;
                                    }

                                    if (deadline.Count() > 1)
                                    {
                                        dateStart.Name = deadline[0];
                                        dateStart.ConfidenceScore = item.ConfidenceScore;

                                        dateEnd.Name = deadline[1];
                                        dateEnd.ConfidenceScore = item.ConfidenceScore;
                                    }

                                    assgn.AssignmentDateStart = dateStart;
                                    assgn.AssignmentDateEnd = dateEnd;
                                }
                            }

                            assgnList.Add(assgn);
                        }
                    }

                    //if (assignment.Count > 0)
                    //{
                    //    assignmentTitleList.AddRange(assignment
                    //                                .Select(s => new
                    //                                AssignmentTitleModel
                    //                                {
                    //                                    Name = s.Text,
                    //                                    ConfidenceScore = s.Confidence
                    //                                }));
                    //}

                    //syllabusModel.OcrAssignmentModel.AssignmentTitle = assignmentTitleList.GroupBy(gp => gp.Name).Select(s => s.FirstOrDefault()).ToList();
                }
            }

            syllabusModel.OcrAssignmentModel = assgnList;

            //await this.UploadFile(directory, externalKey, buffer);

            return syllabusModel;
        }
    }
}
