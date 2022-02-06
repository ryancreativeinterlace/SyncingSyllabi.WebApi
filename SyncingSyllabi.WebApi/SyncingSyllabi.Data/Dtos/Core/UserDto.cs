using SyncingSyllabi.Data.Dtos.Base;

namespace SyncingSyllabi.Data.Dtos.Core
{
    public class UserDto : BaseTrackedDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
