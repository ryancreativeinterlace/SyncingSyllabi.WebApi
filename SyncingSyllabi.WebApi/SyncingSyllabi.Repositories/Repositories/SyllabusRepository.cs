using SyncingSyllabi.Common.Tools.Extensions;
using SyncingSyllabi.Contexts.Entities;
using SyncingSyllabi.Data.Dtos.Core;
using SyncingSyllabi.Data.Enums;
using SyncingSyllabi.Data.Models.Core;
using SyncingSyllabi.Data.Models.Response;
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

        public PaginatedResultDto<SyllabusDataOutputModel> GetSyllabusDetailsList(long userId, IEnumerable<SortColumnDto> sortColumn, PaginationDto pagination)
        {
            IEnumerable<SyllabusDataOutputModel> getSyllabusListResult = Enumerable.Empty<SyllabusDataOutputModel>();

            var outputList = new List<SyllabusDataOutputModel>();

            UseDataContext(ctx =>
            {
                var getSyllabusList = ctx.Syllabus
                                     .AsNoTracking()
                                     .Where(w => 
                                            w.UserId == userId &&
                                            w.IsActive.Value)
                                     .Select(s => _mapper.Map<SyllabusDto>(s))
                                     .ToList();

                if (getSyllabusList.Count() > 0)
                {
                    var getSyllabusData = _mapper.Map<IEnumerable<SyllabusModel>>(getSyllabusList);

                    if(getSyllabusData.Count() > 0)
                    {
                        foreach (var item in getSyllabusData)
                        {
                            var syllabusItem = new SyllabusDataOutputModel()
                            {
                                Id = item.Id,
                                UserId = item.UserId,
                                ClassCode = item.ClassCode,
                                ClassName = item.ClassName,
                                TeacherName = item.TeacherName,
                                ClassSchedule = !string.IsNullOrEmpty(item.ClassSchedule) ? item.ClassSchedule.Split("|").ToList() : null,
                                ColorInHex = item.ColorInHex,
                                CreatedBy = item.CreatedBy,
                                DateCreated = item.DateCreated,
                                UpdatedBy = item.UpdatedBy,
                                DateUpdated = item.DateUpdated,
                                IsActive = item.IsActive

                            };

                            outputList.Add(syllabusItem);
                        }
                    }

                    getSyllabusListResult = outputList;
                }

                if (sortColumn.Count() > 0)
                {
                    getSyllabusListResult = getSyllabusListResult.MultipleSort<SyllabusDataOutputModel>(sortColumn.ToList(), SortTypeEnum.Syllabus).ToList();
                }
            });

            return getSyllabusListResult.Page(pagination);
        }

        public bool DeleteSyllabus(long syllabusId, long userId)
        {
            bool result = false;

            UseDataContext(ctx =>
            {

                var getSyllabus = ctx.Syllabus
                                  .AsNoTracking()
                                  .Where(w => w.Id == syllabusId && w.UserId == userId && w.IsActive.Value)
                                  .Select(s => _mapper.Map<SyllabusEntity>(s))
                                  .FirstOrDefault();

                if (getSyllabus != null)
                {
                    getSyllabus.IsActive = false;

                    getSyllabus.FillCreated(getSyllabus.UserId);
                    getSyllabus.FillUpdated(getSyllabus.UserId);

                    ctx.Syllabus.Update(getSyllabus);

                    ctx.SaveChanges();

                    result = true;
                }
            });

            return result;
        }
    }
}
