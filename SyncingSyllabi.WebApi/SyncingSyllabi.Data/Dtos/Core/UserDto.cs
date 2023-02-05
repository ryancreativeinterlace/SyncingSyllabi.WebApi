using SyncingSyllabi.Data.Dtos.Base;
using System;

namespace SyncingSyllabi.Data.Dtos.Core
{
    public class UserDto : BaseTrackedDto
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
        public string TimeZone { get; set; }
    }
}
