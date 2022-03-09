using SyncingSyllabi.Data.Models.Request.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Data.Models.Request
{
    public class UserRequestModel : BaseRequestImageModel
    {
        public long UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string School { get; set; }
        public string Major { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool? IsEmailConfirm { get; set; }
        public bool? IsResetPassword { get; set; }
        public bool? IsGoogle { get; set; }
        public bool? IsActive { get; set; }
    }

    public class UserPasswordRequestModel 
    {
        public long UserId { get; set; }
        public string CurrentPassword { get; set; }
        public string UpdatedPassword { get; set; }
    }
}
