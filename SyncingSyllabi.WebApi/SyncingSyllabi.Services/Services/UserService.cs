using SyncingSyllabi.Data.Dtos.Core;
using SyncingSyllabi.Repositories.Interfaces;
using SyncingSyllabi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Services.Services
{

    public class UserService : IUserService
    {

        private readonly IUserBaseRepository _userBaseRepository;

        public UserService
        (
            IUserBaseRepository userBaseRepository
        )
        {
            _userBaseRepository = userBaseRepository;
        }

        public UserDto GetUserById(long userId)
        {
            var userDetail = _userBaseRepository.GetUserById(userId);

            return userDetail;
        }

        public UserDto GetUserByEmail(string email)
        {
            var userDetail = _userBaseRepository.GetUserByEmail(email);

            return userDetail;
        }
    }
}
