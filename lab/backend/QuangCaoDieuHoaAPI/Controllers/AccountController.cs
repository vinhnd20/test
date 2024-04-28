using AutoMapper;
using QuangCaoDieuHoaAPI.Common;
using QuangCaoDieuHoaAPI.Database;
using QuangCaoDieuHoaAPI.Dto;
using QuangCaoDieuHoaAPI.Request;
using QuangCaoDieuHoaAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using QuangCaoDieuHoaAPI.Models;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace QuangCaoDieuHoaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : BaseApiController<AccountController>
    {
        private readonly AccountService _accountService;
        public AccountController(DatabaseContext databaseContext, IMapper mapper, ApiOption apiConfig, IWebHostEnvironment webHost)
        {
            _accountService = new AccountService(apiConfig, databaseContext, mapper, webHost);
        }   

        /// <summary>
        /// get profile
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetProfile")]
        public MessageData GetProfile()
        {
            try
            {
                var res = _accountService.GetProfile(UserId);
                return new MessageData { Data = res, Status = 1 };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }

        /// <summary>
        /// get profile
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("UpdateProfile")]
        public MessageData UpdateProfile(UpdateProfileRequest request)
        {
            try
            {
                var res = _accountService.UpdateProfile(UserId, request);
                return new MessageData { Data = res, Status = 1 };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }
    }
}
