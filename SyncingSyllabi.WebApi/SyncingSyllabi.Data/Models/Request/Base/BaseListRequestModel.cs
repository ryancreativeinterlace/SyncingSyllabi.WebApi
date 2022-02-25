using SyncingSyllabi.Data.Models.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SyncingSyllabi.Data.Models.Request.Base
{
    public class BaseListRequestModel
    {
        public BaseListRequestModel()
        {
            Pagination = new PaginationModel();
            Sort = Enumerable.Empty<SortColumnModel>();

            //FilterCriteria = Enumerable.Empty<FilterCriteriaModel>();
        }

        public PaginationModel Pagination { get; set; }
        public IEnumerable<SortColumnModel> Sort { get; set; }

        //public IEnumerable<FilterCriteriaModel> FilterCriteria { get; set; }
    }
}
