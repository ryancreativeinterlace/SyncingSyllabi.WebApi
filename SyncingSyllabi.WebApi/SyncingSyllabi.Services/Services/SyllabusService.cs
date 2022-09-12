using AutoMapper;
using SyncingSyllabi.Common.Tools.Helpers;
using SyncingSyllabi.Data.Dtos.Core;
using SyncingSyllabi.Data.Enums;
using SyncingSyllabi.Data.Models.Core;
using SyncingSyllabi.Data.Models.Request;
using SyncingSyllabi.Data.Models.Response;
using SyncingSyllabi.Data.Settings;
using SyncingSyllabi.Repositories.Interfaces;
using SyncingSyllabi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SyncingSyllabi.Services.Services
{
    public class SyllabusService : ISyllabusService
    {
        private readonly S3Settings _s3Settings;
        private readonly IMapper _mapper;
        private readonly IS3FileRepository _s3FileRepository;
        private readonly ISyllabusBaseRepository _syllabusBaseRepository;

        public SyllabusService
        (
            S3Settings s3Settings,
            IMapper mapper,
            IS3FileRepository s3FileRepository,
            ISyllabusBaseRepository syllabusBaseRepository
        )
        {
            _s3Settings = s3Settings;
            _mapper = mapper;
            _s3FileRepository = s3FileRepository;
            _syllabusBaseRepository = syllabusBaseRepository;
        }

        public SyllabusResponseModel CreateSyllabus(SyllabusRequestModel syllabusRequestModel)
        {
            var createSyllabusResult = new SyllabusResponseModel();

            var syllabusModel = new SyllabusModel();
            syllabusModel.UserId = syllabusRequestModel.UserId;
            syllabusModel.ClassCode = !string.IsNullOrEmpty(syllabusRequestModel.ClassCode) ? syllabusRequestModel.ClassCode.Trim() : string.Empty;
            syllabusModel.ClassName = !string.IsNullOrEmpty(syllabusRequestModel.ClassName) ? syllabusRequestModel.ClassName.Trim() : string.Empty;
            syllabusModel.TeacherName = !string.IsNullOrEmpty(syllabusRequestModel.TeacherName) ? syllabusRequestModel.TeacherName.Trim() : string.Empty;
            syllabusModel.ColorInHex = !string.IsNullOrEmpty(syllabusRequestModel.ColorInHex) ? syllabusRequestModel.ColorInHex.Trim() : string.Empty;
            syllabusModel.ClassSchedule = !string.IsNullOrEmpty(syllabusRequestModel.ClassSchedule) ? syllabusRequestModel.ClassSchedule.Trim() : string.Empty;
            syllabusModel.IsActive = true;

            SyllabusDto syllabus = _mapper.Map<SyllabusDto>(syllabusModel);

            if (syllabusRequestModel.File != null)
            {
                var fileName = Guid.NewGuid().ToString();

                //var fileBytes = FileHelper.ConvertPdfToImage(syllabusRequestModel.File);

                //if (fileBytes.Length > 0)
                //{
                //    _s3FileRepository.SyllabusDetectAsync(_s3Settings.SyllabusFilesDirectory, fileName, fileBytes).GetAwaiter().GetResult();
                //}
            }

            if (syllabus != null)
            {
                createSyllabusResult = _syllabusBaseRepository.CreateSyllabus(syllabus);
            }

            return createSyllabusResult;
        }

        public SyllabusDto UpdateSyllabus(SyllabusRequestModel syllabusRequestModel)
        {
            SyllabusDto updateSyllabusResult = null;

            var syllabusModel = new SyllabusModel();
            syllabusModel.Id = syllabusRequestModel.SyllabusId;
            syllabusModel.UserId = syllabusRequestModel.UserId;
            syllabusModel.ClassCode = !string.IsNullOrEmpty(syllabusRequestModel.ClassCode) ? syllabusRequestModel.ClassCode.Trim() : string.Empty;
            syllabusModel.ClassName = !string.IsNullOrEmpty(syllabusRequestModel.ClassName) ? syllabusRequestModel.ClassName.Trim() : string.Empty;
            syllabusModel.TeacherName = !string.IsNullOrEmpty(syllabusRequestModel.TeacherName) ? syllabusRequestModel.TeacherName.Trim() : string.Empty;
            syllabusModel.ColorInHex = !string.IsNullOrEmpty(syllabusRequestModel.ColorInHex) ? syllabusRequestModel.ColorInHex.Trim() : string.Empty;
            syllabusModel.ClassSchedule = !string.IsNullOrEmpty(syllabusRequestModel.ClassSchedule) ? syllabusRequestModel.ClassSchedule.Trim() : string.Empty;
            syllabusModel.IsActive = syllabusRequestModel.IsActive ?? null;

            SyllabusDto syllabus = _mapper.Map<SyllabusDto>(syllabusModel);

            if (syllabus != null)
            {
                updateSyllabusResult = _syllabusBaseRepository.UpdateSyllabus(syllabus);
            }

            return updateSyllabusResult;
        }

        public SyllabusDto GetSyllabus(long syllabusId, long userId)
        {
            SyllabusDto getSyllabusResult = null;

            getSyllabusResult = _syllabusBaseRepository.GetSyllabus(syllabusId, userId);

            return getSyllabusResult;
        }

        public PaginatedResultDto<SyllabusDataOutputModel> GetSyllabusDetailsList(SyllabusRequestModel syllabusRequestModel)
        {
            var paginationDto = syllabusRequestModel.Pagination != null ? _mapper.Map<PaginationDto>(syllabusRequestModel.Pagination) : null;
            var sortColumnDto = syllabusRequestModel.Sort?.Select(f => _mapper.Map<SortColumnDto>(f));

            return _syllabusBaseRepository.GetSyllabusDetailsList(syllabusRequestModel.UserId, sortColumnDto, paginationDto);
        }

        public bool DeleteSyllabus(long syllabusId, long userId)
        {
            return _syllabusBaseRepository.DeleteSyllabus(syllabusId, userId);
        }

        public OcrScanReponseDataModel OcrScan(OcrScanRequestModel syllabusRequestModel)
        {
            OcrScanReponseDataModel syllabusResult = null;

            if (syllabusRequestModel.ImageFile != null || syllabusRequestModel.PdfFile.Count() > 0)
            {
                var fileName = Guid.NewGuid().ToString();

                //var fileBytes = FileHelper.ConvertPdfToImage(syllabusRequestModel.File, syllabusRequestModel.Pages);

                //if (fileBytes.Length > 0)
                //{
                //    syllabusResult = _s3FileRepository.SyllabusDetectAsync(_s3Settings.SyllabusFilesDirectory, fileName, fileBytes).GetAwaiter().GetResult();
                //}

                if(syllabusRequestModel.OcrUploadTypeEnum == OcrUploadTypeEnum.Pdf)
                {
                    syllabusResult = _s3FileRepository.SyllabusDetectAsync(null, syllabusRequestModel.PdfFile, syllabusRequestModel.Pages, syllabusRequestModel.OcrTypeEnum).GetAwaiter().GetResult();
                }
                else
                {
                    syllabusResult = _s3FileRepository.SyllabusDetectAsync(syllabusRequestModel.ImageFile, null, syllabusRequestModel.Pages, syllabusRequestModel.OcrTypeEnum).GetAwaiter().GetResult();
                }
            }

            return syllabusResult;
        }
    }
}
