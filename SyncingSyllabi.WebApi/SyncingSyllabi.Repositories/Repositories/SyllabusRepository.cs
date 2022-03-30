using SyncingSyllabi.Contexts.Entities;
using SyncingSyllabi.Data.Dtos.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace SyncingSyllabi.Repositories.Repositories
{
    public partial class SyllabusBaseRepository
    {
        public SyllabusDto CreateSyllabus(SyllabusDto syllabusDto)
        {
            SyllabusDto result = null;

            var syllabus = _mapper.Map<SyllabusEntity>(syllabusDto);

            UseDataContext(ctx =>
            {
                var getSyllabus = ctx.Syllabus
                                 .AsNoTracking()
                                 .Where(w =>
                                        (w.ClassCode.ToLower() == syllabus.ClassCode.ToLower() ||
                                        w.ClassName.ToLower() == syllabus.ClassName.ToLower()) &&
                                        w.IsActive.Value)
                                 .Select(s => _mapper.Map<SyllabusEntity>(s))
                                 .FirstOrDefault();

                if (getSyllabus == null)
                {
                    syllabus.FillCreated(syllabus.UserId);
                    syllabus.FillUpdated(syllabus.UserId);

                    ctx.Syllabus.Add(syllabus);

                    ctx.SaveChanges();

                    result = _mapper.Map<SyllabusDto>(syllabus);
                }
            });

            return result;
        }

        public SyllabusDto UpdateSyllabus(SyllabusDto syllabusDto)
        {
            SyllabusDto result = null;

            var syllabus = _mapper.Map<SyllabusEntity>(syllabusDto);

            UseDataContext(ctx =>
            {
                var getSyllabus = ctx.Syllabus
                                 .AsNoTracking()
                                 .Where(w => w.Id == syllabus.Id && w.UserId == syllabus.UserId && w.IsActive.Value)
                                 .Select(s => _mapper.Map<SyllabusEntity>(s))
                                 .FirstOrDefault();

                if (getSyllabus != null)
                {
                    getSyllabus.ClassCode = !string.IsNullOrEmpty(syllabus.ClassCode) ? syllabus.ClassCode : getSyllabus.ClassCode;
                    getSyllabus.ClassName = !string.IsNullOrEmpty(syllabus.ClassName) ? syllabus.ClassName : getSyllabus.ClassName;
                    getSyllabus.TeacherName = !string.IsNullOrEmpty(syllabus.TeacherName) ? syllabus.TeacherName : getSyllabus.TeacherName;
                    getSyllabus.ColorInHex = !string.IsNullOrEmpty(syllabus.ColorInHex) ? syllabus.ColorInHex : getSyllabus.ColorInHex;
                    getSyllabus.ClassSchedule = !string.IsNullOrEmpty(syllabus.ClassSchedule) ? syllabus.ClassSchedule : getSyllabus.ClassSchedule;
                    getSyllabus.IsActive = syllabus.IsActive ?? getSyllabus.IsActive;

                    getSyllabus.FillCreated(getSyllabus.UserId);
                    getSyllabus.FillUpdated(getSyllabus.UserId);

                    ctx.Syllabus.Update(getSyllabus);

                    ctx.SaveChanges();

                    result = _mapper.Map<SyllabusDto>(getSyllabus);
                }
            });

            return result;
        }

        public SyllabusDto GetSyllabus(long syllabusId, long userId)
        {
            SyllabusDto result = null;

            UseDataContext(ctx =>
            {

                var getSyllabus = ctx.Syllabus
                                 .AsNoTracking()
                                 .Where(w => w.Id == syllabusId && w.UserId == userId && w.IsActive.Value)
                                 .Select(s => _mapper.Map<SyllabusEntity>(s))
                                 .FirstOrDefault();

                result = _mapper.Map<SyllabusDto>(getSyllabus);

            });

            return result;
        }
    }
}
