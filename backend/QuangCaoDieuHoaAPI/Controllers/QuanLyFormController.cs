using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuangCaoDieuHoaAPI.Common;
using QuangCaoDieuHoaAPI.Database;
using QuangCaoDieuHoaAPI.Dto;
using QuangCaoDieuHoaAPI.Services;
using QuangCaoDieuHoaAPI.Models;
using QuangCaoDieuHoaAPI.Request;
using Microsoft.AspNetCore.Authorization;

namespace QuangCaoDieuHoaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuanLyFormController : BaseApiController<QuanLyFormController>
    {
        private readonly QuanLyFormServices _quanLyServices;
        public QuanLyFormController(DatabaseContext databaseContext, IMapper mapper, ApiOption apiConfig, IWebHostEnvironment webHost)
        {
            _quanLyServices = new QuanLyFormServices(apiConfig, databaseContext, mapper, webHost);
        }
        [HttpGet]
        [Route("GetAllUser")]
        public MessageData GetUer()
        {
            try
            {
               var s = _quanLyServices.GetAllUserForm();
                return new MessageData { Status = 1, Des = "Successfully", Data = s };
            }
            catch(Exception ex)
            {
                return NG(ex);
            }
        }
        [HttpGet]
        [Route("GetUserByID")]
        public MessageData GetUserByID(int id)
        {
            try
            {
                var su = _quanLyServices.GetUserFromByID(id);
                return new MessageData { Status = 1, Data = su, Des = "Successfully" };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }

        [HttpGet]
        [Route("GetUserByName")]
        public MessageData GetUserByName(string name)
        {
            try
            {
                var su = _quanLyServices.FindUserByName(name);
                return new MessageData { Status = 1, Data = su, Des = "Successfully" };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }

        [HttpPost]
        [Route("CreateNewForm")]
        [AllowAnonymous]
        public MessageData CreateNewForm(QuanLyFormRequest request)
        {
            try
            {
                var res = _quanLyServices.CreateForm(request);
                return new MessageData { Data = res, Des = "Update form success" };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }

        [HttpPost]
        [Route("FindFormByFilter")]
        public MessageData FindFormByFilter(FilterFormRequest request)
        {
            try
            {
                var res = _quanLyServices.FindFormByFilter(request);
                return new MessageData { Data = res, Des = "Update form success" };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }

        [HttpPut]
        [Route("UpdateUserForm")]
        public MessageData UpdateUserForm(UpdateForm request)
        {
            try
            {
                var res = _quanLyServices.UpdateFormByID(request);
                return new MessageData { Data = res, Des = "Update form success" };
            }
            catch(Exception ex)
            {
                return NG(ex);
            }
        }

        [HttpDelete]
        [Route("DeleteForm")]
        public MessageData DeleteUserForm(int id)
        {
            try
            {
                var res = _quanLyServices.DeleteFormByID(id);
                return new MessageData { Data = res, Des = "Delete success" };
            }
            catch(Exception ex)
            {
                return NG(ex);
            }
        }
    }
}
