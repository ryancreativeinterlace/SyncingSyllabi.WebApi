using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Data.Models.Core
{
    public class PaginationModel
    {
        public bool IncludeTotal { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }

        public bool IsEmpty()
        {
            return Skip == 0 && Take == 0;
        }

        public static PaginationModel Default
        {
            get
            {
                return new PaginationModel()
                {
                    IncludeTotal = true,
                    Skip = 0,
                    Take = 6
                };
            }
        }
    }
}
