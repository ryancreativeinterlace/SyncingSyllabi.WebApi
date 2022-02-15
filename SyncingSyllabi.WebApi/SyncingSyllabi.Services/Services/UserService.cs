using AutoMapper;
using SyncingSyllabi.Data.Dtos.Core;
using SyncingSyllabi.Data.Models.Core;
using SyncingSyllabi.Repositories.Interfaces;
using SyncingSyllabi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Services.Services
{

    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserBaseRepository _userBaseRepository;

        public UserService
        (
            IMapper mapper,
            IUserBaseRepository userBaseRepository
        )
        {
            _mapper = mapper;
            _userBaseRepository = userBaseRepository;
        }

        public UserDto CreateUser(UserModel userModel)
        {
            UserDto userResult = null;

            UserDto user = _mapper.Map<UserDto>(userModel);
            
            if(user != null)
            {
                user.Active = true;
                userResult = _userBaseRepository.CreateUser(user);
            }

            return userResult;
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
