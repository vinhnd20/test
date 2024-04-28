using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuangCaoDieuHoaAPI.Common;
using QuangCaoDieuHoaAPI.Database;
using QuangCaoDieuHoaAPI.Dto;
using QuangCaoDieuHoaAPI.Services;

namespace QuangCaoDieuHoaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonHangController : BaseApiController<DonHangController>
    {
        private readonly DonHangServices _donHangServices;
        public DonHangController(DatabaseContext databaseContext, IMapper mapper, ApiOption apiConfig, IWebHostEnvironment webHost)
        {
            _donHangServices = new DonHangServices(apiConfig, databaseContext, mapper, webHost);
        }
        [HttpGet]
        [Route("getallOrder")]
        public MessageData GetAllProduct()
        {
            try
            {
                var sp = _donHangServices.GetAllOrder();
                return new MessageData { Status = 1, Data = sp };
            }
            catch(Exception ex)
            {
                return NG(ex);
            }
        }
        [HttpGet]
        [Route("GetOderById")]
        public MessageData GetOrderByID(int id)
        {
            try
            {
                var sodr = _donHangServices.GetOrderByID(id);
                return new MessageData { Status = 1, Data = sodr};
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }
        [HttpPost]
        [Route("AddNewOrder")]
        public MessageData Addorder(int id)
        {
            try
            {
                var sodr = _donHangServices.GetOrderByID(id);
                return new MessageData { Status = 1, Data = sodr };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }
        [HttpPut]
        [Route("updateOrder")]
        public MessageData UpdateOrder(int id)
        {
            try
            {
                var sodr = _donHangServices.GetOrderByID(id);
                return new MessageData { Status = 1, Data = sodr };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }
    }
}
