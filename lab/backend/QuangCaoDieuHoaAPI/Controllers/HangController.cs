using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuangCaoDieuHoaAPI.Common;
using QuangCaoDieuHoaAPI.Database;
using QuangCaoDieuHoaAPI.Dto;
using QuangCaoDieuHoaAPI.Models;
using QuangCaoDieuHoaAPI.Request;
using QuangCaoDieuHoaAPI.Services;

namespace QuangCaoDieuHoaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HangController : BaseApiController<HangController>
    {
        private readonly HangServices _hangServices;
        private readonly IMapper _mapper;
        public HangController(DatabaseContext databaseContext, IMapper mapper, ApiOption apiConfig, IWebHostEnvironment webHost)
        {
            _mapper = mapper;
            _hangServices = new HangServices(apiConfig, databaseContext, mapper, webHost);
        }

        // tim kiem tat ca san pham se hien thi ca san pham bi an

        // truong hop status = 1, de gia san pham la lien he

        [HttpGet]
        [Route("GetAllBrand")]
        [AllowAnonymous]
        public MessageData GetAllBrand()
        {
            try
            {
                var res = _hangServices.FindAllBrand();
                return new MessageData { Data = res, Status = 1 };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }

        [HttpGet]
        [Route("GetBrandById")]
        public MessageData GetBrandById(int Id)
        {
            try
            {
                var res = _hangServices.FindBrandById(Id);
                return new MessageData { Data = res, Status = 1 };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("GetBrandByName")]
        public MessageData GetBrandByName(string name)
        {
            try
            {
                var res = _hangServices.FindBrandByName(name);
                return new MessageData { Data = res, Status = 1 };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }

        [HttpPost]
        [Route("AddBrand")]
        //[Authorize(Roles = "Admin")]
        public MessageData AddBrand([FromForm] BrandRequest request)
        {
            try
            {
                var res = _hangServices.AddNewBrand(request);
                return new MessageData { Status = 1, Data = res };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }


        [HttpDelete]
        [Route("HideBrand")]
        //[Authorize(Roles = "Admin")]
        public MessageData HideBrand(int HangId)
        {
            try
            {
                var res = _hangServices.DeleteBrand(HangId);
                return new MessageData { Des = "Hide success", Data = res };
            }
            catch(Exception ex)
            {
                return NG(ex);
            }
        }

        // cap nhat san pham

        [HttpPut]
        [Route("UpdateBrand")]
        //[Authorize(Roles = "Admin")]
        public MessageData UpdateBrand([FromForm] UpdateBrandRequest request)
        {
            try
            {
                var res = _hangServices.EditBrand(request);

                return new MessageData { Des = "Update succesfully", Data = res };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }
    }
}
