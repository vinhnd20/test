using AutoMapper;
using BanVeXemPhimApi.Common;
using QuangCaoDieuHoaAPI.Common;
using QuangCaoDieuHoaAPI.Database;
using QuangCaoDieuHoaAPI.Models;
using QuangCaoDieuHoaAPI.Repositories;
using QuangCaoDieuHoaAPI.Request;
using QuangCaoDieuHoaAPI.Utility;
using System.Linq.Expressions;
using System.Net.WebSockets;

namespace QuangCaoDieuHoaAPI.Services
{
    public class AccountService
    {
        private readonly UserRepository _userRepository;
        private readonly ApiOption _apiOption;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHost;

        public AccountService(ApiOption apiOption, DatabaseContext databaseContext, IMapper mapper, IWebHostEnvironment webHost)
        {
            _userRepository = new UserRepository(apiOption, databaseContext, mapper);
            _apiOption = apiOption;
            _mapper = mapper;
            _webHost = webHost;
        }

        /// <summary>
        /// Get profile
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public object GetProfile(int userId)
        {
            try
            {
                return _userRepository.FindOrFail(userId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get profile
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public object UpdateProfile(int userId, UpdateProfileRequest request)
        {
            try
            {
                var user = _userRepository.FindOrFail(userId);
                if(user == null)
                {
                    throw new Exception("User doesn't exist!");
                }
                user.FirstName = request.FirstName;
                user.LastName = request.LastName;
                user.Birthday = request.Birthday;
                user.UpdatedDate = DateTime.UtcNow;
                _userRepository.UpdateByEntity(user);
                _userRepository.SaveChange();
                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
