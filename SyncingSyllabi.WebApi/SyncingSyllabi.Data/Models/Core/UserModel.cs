using SyncingSyllabi.Data.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Data.Models.Core
{
    public class UserModel : BaseTrackedModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string School { get; set; }
        public string Major { get; set; }
        public string ImageName { get; set; }
        public string ImageUrl { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool? IsEmailConfirm { get; set; }
        public bool? IsResetPassword { get; set; }
        public bool? IsGoogle { get; set; }
        public string NotificationToken { get; set; }
    }
}
