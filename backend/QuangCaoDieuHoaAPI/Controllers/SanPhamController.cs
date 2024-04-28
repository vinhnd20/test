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
    public class SanPhamController : BaseApiController<SanPhamController>
    {
        private readonly SanPhamServices _sanPhamServices;
        private readonly IMapper _mapper;
        public SanPhamController(DatabaseContext databaseContext, IMapper mapper, ApiOption apiConfig, IWebHostEnvironment webHost)
        {
            _mapper = mapper;
            _sanPhamServices = new SanPhamServices(apiConfig, databaseContext, mapper, webHost);
        }

        // tim kiem tat ca san pham se hien thi ca san pham bi an

        // truong hop status = 1, de gia san pham la lien he

        [HttpGet]
        [Route("GetAllProduct")]
        [AllowAnonymous]
        public MessageData GetAllProduct()
        {
            try
            {
                var allsp = _sanPhamServices.FindAllProduct();
                return new MessageData { Data = allsp, Status = 1 };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }

        [HttpGet]
        [Route("GetProductIntop10")]
        [AllowAnonymous]
        public MessageData GetProductIntop10()
        {
            try
            {
                var allsp = _sanPhamServices.FindProductInTop10();
                return new MessageData { Data = allsp, Status = 1 };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }

        // tim kiem theo ten san pham 

        [HttpGet]
        [Route("GetProductByName")]
        [AllowAnonymous]
        public MessageData GetProductByName(string name)
        {
            try
            {
                var sp = _sanPhamServices.FindByName(name);
                return new MessageData { Data = sp, Status = 1, Des = $"Get product {name} success" };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }


        // tim kiem thoeo ten hang
        
        [HttpGet]
        [AllowAnonymous]
        [Route("GetProductBrand")]
        public MessageData GetByBrand(string brand)
        {
            try
            {
                //VerifyClient();
                
                var sp = _sanPhamServices.GetProductByBrand(brand);
                return new MessageData { Status = 1, Data = sp };
            }
            catch
            {
                return new MessageData { Status = 0, Data = null };
            }
        }

        //  tim kiem nhung san pham da bi an hoac con hien thi

        [HttpGet]
        [Route("GetProductBysatus")]
        [AllowAnonymous]
        public MessageData GetProductByStatus(int status)
        {
            try
            {
                var sp = _sanPhamServices.GetProductByStatus(status);
                return new MessageData { Status = 1, Data = sp, Des = "get product by status success" };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }

        [HttpGet]
        [Route("GetProductById")]
        [AllowAnonymous]
        public MessageData GetProductById(int Id)
        {
            try
            {
                var sp = _sanPhamServices.GetProductById(Id);
                return new MessageData { Status = 1, Data = sp, Des = "get product by id success" };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }

        // tim kiem theo bo loc tong hop
        [HttpPost]
        [Route("FindByFilter")]
        [AllowAnonymous]
        public MessageData GetProductFilter(FilterProductRequest request)
        {
            try
            {
                var res = _sanPhamServices.FindProductByFilter(request);
                return new MessageData { Data = res, Des = "Get by filter successfully" };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }

        // them moi san pham

        [HttpPost]
        [Route("AddProduct")]
        //[Authorize(Roles = "Admin")]
        public MessageData AddProduct([FromForm] ProductRequest request)
        {
            try
            {
                var res = _sanPhamServices.AddNewProduct(request, UserId);
                return new MessageData { Status = 1, Data = res };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }


        // xoa san pham
        [HttpDelete]
        [Route("DeleteProduct")]
        //[Authorize(Roles = "Admin")]
        public MessageData HideProduct(int ProductID)
        {
            try
            {
                var res = _sanPhamServices.DeleteProduct(ProductID);
                return new MessageData { Des = "Hide success", Data = res };
            }
            catch(Exception ex)
            {
                return NG(ex);
            }
        }

        // cap nhat san pham

        [HttpPut]
        [Route("UpdateProduct")]
        //[Authorize(Roles = "Admin")]
        public MessageData UpdateProduct([FromForm] UpdateProduct request)
        {
            try
            {
                var res = _sanPhamServices.EditProduct(request);

                return new MessageData { Des = "Update succesfully", Data = res };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }

        [HttpGet]
        [Route("GetProductStatisticskInWeek")]
        //[Authorize(Roles = "Admin")]
        public MessageData GetProductStatisticskInWeek()
        {
            try
            {
                var res = _sanPhamServices.GetProductStatisticskInWeek();

                return new MessageData { Des = "Update succesfully", Data = res };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }
    }
}
