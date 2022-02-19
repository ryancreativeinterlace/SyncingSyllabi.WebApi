using SyncingSyllabi.Data.Models.Base;
using SyncingSyllabi.Data.Models.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Data.Models.Response
{
    public class GoalResponseModel : BaseResponseModel<GoalResponseDataModel>
    {
        public GoalResponseModel()
        {
            Data = new GoalResponseDataModel();
        }
    }

    public class GoalResponseDataModel
    {
        public GoalModel Item { get; set; }
        public bool Success { get; set; } = true;
    }
}
