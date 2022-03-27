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
            SyllabusDto creteSyllabusResult = null;

            var syllabusModel = new SyllabusModel();
            syllabusModel.ClassCode = !string.IsNullOrEmpty(syllabusRequestModel.ClassCode) ? syllabusRequestModel.ClassCode.Trim() : string.Empty;
            syllabusModel.ClassName = !string.IsNullOrEmpty(syllabusRequestModel.ClassName) ? syllabusRequestModel.ClassName.Trim() : string.Empty;
            syllabusModel.TeacherName = !string.IsNullOrEmpty(syllabusRequestModel.TeacherName) ? syllabusRequestModel.TeacherName.Trim() : string.Empty;
            syllabusModel.IsActive = true;

            SyllabusDto syllabus = _mapper.Map<SyllabusDto>(syllabusModel);

            if (syllabus != null)
            {
                creteSyllabusResult = _syllabusBaseRepository.CreateSyllabus(syllabus);
            }

            return creteSyllabusResult;
        }

        public SyllabusDto UpdateSyllabus(SyllabusRequestModel syllabusRequestModel)
        {
            SyllabusDto updateSyllabusResult = null;

            var syllabusModel = new SyllabusModel();
            syllabusModel.Id = syllabusRequestModel.SyllabusId;
            syllabusModel.ClassCode = !string.IsNullOrEmpty(syllabusRequestModel.ClassCode) ? syllabusRequestModel.ClassCode.Trim() : string.Empty;
            syllabusModel.ClassName = !string.IsNullOrEmpty(syllabusRequestModel.ClassName) ? syllabusRequestModel.ClassName.Trim() : string.Empty;
            syllabusModel.TeacherName = !string.IsNullOrEmpty(syllabusRequestModel.TeacherName) ? syllabusRequestModel.TeacherName.Trim() : string.Empty;
            syllabusModel.IsActive = syllabusRequestModel.IsActive ?? null;

            SyllabusDto syllabus = _mapper.Map<SyllabusDto>(syllabusModel);

            if (syllabus != null)
            {
                updateSyllabusResult = _syllabusBaseRepository.UpdateSyllabus(syllabus);
            }

            return updateSyllabusResult;
        }
    }
}
