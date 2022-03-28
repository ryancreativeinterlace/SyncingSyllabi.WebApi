using AutoMapper;
using SyncingSyllabi.Data.Dtos.Core;
using SyncingSyllabi.Data.Models.Core;
using SyncingSyllabi.Data.Models.Request;
using SyncingSyllabi.Repositories.Interfaces;
using SyncingSyllabi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Services.Services
{
    public class SyllabusService : ISyllabusService
    {
        private readonly IMapper _mapper;
        private readonly ISyllabusBaseRepository _syllabusBaseRepository;

        public SyllabusService
        (
            IMapper mapper,
            ISyllabusBaseRepository syllabusBaseRepository
        )
        {
            _mapper = mapper;
            _syllabusBaseRepository = syllabusBaseRepository;
        }

        public SyllabusDto CreateSyllabus(SyllabusRequestModel syllabusRequestModel)
        {
            SyllabusDto createSyllabusResult = null;

            var syllabusModel = new SyllabusModel();
            syllabusModel.UserId = syllabusRequestModel.UserId;
            syllabusModel.ClassCode = !string.IsNullOrEmpty(syllabusRequestModel.ClassCode) ? syllabusRequestModel.ClassCode.Trim() : string.Empty;
            syllabusModel.ClassName = !string.IsNullOrEmpty(syllabusRequestModel.ClassName) ? syllabusRequestModel.ClassName.Trim() : string.Empty;
            syllabusModel.TeacherName = !string.IsNullOrEmpty(syllabusRequestModel.TeacherName) ? syllabusRequestModel.TeacherName.Trim() : string.Empty;
            syllabusModel.ColorInHex = !string.IsNullOrEmpty(syllabusRequestModel.ColorInHex) ? syllabusRequestModel.ColorInHex.Trim() : string.Empty;
            syllabusModel.ClassSchedule = syllabusRequestModel.ClassSchedule;
            syllabusModel.IsActive = true;

            SyllabusDto syllabus = _mapper.Map<SyllabusDto>(syllabusModel);

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
            syllabusModel.ClassSchedule = syllabusRequestModel.ClassSchedule ?? null;
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
    }
}
