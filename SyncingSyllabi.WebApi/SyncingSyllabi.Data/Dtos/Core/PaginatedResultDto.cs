using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SyncingSyllabi.Data.Dtos.Core
{
    public class PaginatedResultDto<TDto>
    {
        public PaginatedResultDto()
        {
            Items = Enumerable.Empty<TDto>();
        }

        public IEnumerable<TDto> Items { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public int TotalCount { get; set; }

        public PaginatedResultDto<TSelectDTO> Select<TSelectDTO>(Func<TDto, TSelectDTO> selector)
        {
            var newPagedDTO = new PaginatedResultDto<TSelectDTO>();
            newPagedDTO.Items = this.Items
                                        .Select(selector)
                                        .ToList();
            newPagedDTO.Skip = this.Skip;
            newPagedDTO.Take = this.Take;
            newPagedDTO.TotalCount = this.TotalCount;

            return newPagedDTO;
        }

        public static PaginatedResultDto<TDto> FromPaginatedItems(IEnumerable<TDto> items, int totalCount, int skip = 0, int take = 0)
        {
            var newPagedDTO = new PaginatedResultDto<TDto>();
            newPagedDTO.Items = items;
            newPagedDTO.Skip = skip;
            newPagedDTO.Take = take;
            newPagedDTO.TotalCount = totalCount;

            return newPagedDTO;
        }
    }
}
