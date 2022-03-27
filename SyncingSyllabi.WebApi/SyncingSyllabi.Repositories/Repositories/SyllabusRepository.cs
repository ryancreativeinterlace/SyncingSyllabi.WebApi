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
                                        (w.ClassCode.ToLower() == syllabus.ClassCode || 
                                        w.ClassName.ToLower() == syllabus.ClassName) &&
                                        w.IsActive.Value)
                                 .Select(s => _mapper.Map<SyllabusEntity>(s))
                                 .FirstOrDefault();

                if (getSyllabus == null)
                {
                    syllabus.FillCreated(1);
                    syllabus.FillUpdated(1);

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
                    getSyllabus.IsActive = syllabus.IsActive ?? getSyllabus.IsActive;

                    getSyllabus.FillCreated(1);
                    getSyllabus.FillUpdated(1);

                    ctx.Syllabus.Update(getSyllabus);

                    ctx.SaveChanges();

                    result = _mapper.Map<SyllabusDto>(getSyllabus);
                }
            });

            return result;
        }
    }
}
