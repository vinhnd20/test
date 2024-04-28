using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Index.HPRtree;
using QuangCaoDieuHoaAPI.Common;
using QuangCaoDieuHoaAPI.Database;
using QuangCaoDieuHoaAPI.Dto;
using QuangCaoDieuHoaAPI.Models;
using QuangCaoDieuHoaAPI.Repositories;
using QuangCaoDieuHoaAPI.Request;
using System.Collections;
using System.Drawing;
using System.Net.WebSockets;

namespace QuangCaoDieuHoaAPI.Services
{
    public class SanPhamServices
    {
        private readonly SanPhamRepository _sanPhamRepository;
        private readonly HangRepository _hangRepository;
        private readonly TrangThaiRepository _trangThaiRepository;
        private readonly ApiOption _apiOption;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHost;

        public SanPhamServices(ApiOption apiOption, DatabaseContext databaseContext, IMapper mapper, IWebHostEnvironment webHost)
        {
            _sanPhamRepository = new SanPhamRepository(apiOption, databaseContext, mapper);
            _hangRepository = new HangRepository(apiOption, databaseContext, mapper);
            _trangThaiRepository = new TrangThaiRepository(apiOption, databaseContext, mapper);
            _apiOption = apiOption;
            _mapper = mapper;
            _webHost = webHost;
        }

        public object FindAllProduct()
        {
            try
            {
                return _sanPhamRepository.FindByCondition(row => row.Status == 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object FindProductInTop10()
        {
            try
            {
                var recentProducts = _sanPhamRepository.FindByCondition(row => row.Status == 1)
                                                      .OrderByDescending(row => row.CreatedDate)
                                                      .Take(10)
                                                      .ToList();
                return recentProducts;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object FindByName(string name)
        {
            try
            {
                var sp = _sanPhamRepository.FindByCondition(p => p.ten.Contains(name) && p.Status == 1);
                if (sp != null && sp.Any())
                {
                    return sp;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public object AddNewProduct(ProductRequest request, int userID)
        {
            try
            {
                var checkBrand = _hangRepository.FindByCondition(row => row.Id == request.HangID).FirstOrDefault();
                if (checkBrand == null)
                {
                    throw new Exception("Can't find brand");
                }
                var newSP = _mapper.Map<SanPham>(request);
                newSP.Image = "";
                if (request.Image != null)
                {
                    var date = DateTime.UtcNow.ToString("yyyy_MM_dd_HH_mm");
                    using (FileStream fileStream = File.Create(_webHost.WebRootPath + "\\products\\images\\" + date + request.Image.FileName))
                    {
                        request.Image.CopyTo(fileStream);
                        fileStream.Flush();
                    }
                    newSP.Image = "products/images/" + date + request.Image.FileName;
                }
                newSP.UserID = userID;
                newSP.Status = 1;
                _sanPhamRepository.Create(newSP);
                _sanPhamRepository.SaveChange();
                return newSP;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object EditProduct(UpdateProduct request)
        {
            try
            {
                var y = _sanPhamRepository.FindByCondition(p => p.ID == request.id).FirstOrDefault();
                if (y == null)
                {
                    throw new Exception("Can't not find product to edit");
                }
                var checkBrand = _hangRepository.FindByCondition(row => row.Id == request.HangID).FirstOrDefault();
                if (checkBrand == null)
                {
                    throw new Exception("Can't find brand");
                }
                y.ten = request.ten;
                y.giaTien = request.giaTien;
                y.soLuong = request.soLuong;
                y.model = request.model;
                y.moTa = request.moTa;
                y.congSuat = request.congSuat;
                y.soChieu = request.soChieu;
                y.kieuDang = request.kieuDang;
                y.HangID = request.HangID;
                y.TrangThai = request.TrangThai;
                y.UpdatedDate = DateTime.Now;
                if (request.Image != null && request.Image.FileName != y.Image)
                {
                    var date = DateTime.UtcNow.ToString("yyyy_MM_dd_HH_mm");
                    using (FileStream fileStream = File.Create(_webHost.WebRootPath + "\\products\\images\\" + date + request.Image.FileName))
                    {
                        request.Image.CopyTo(fileStream);
                        fileStream.Flush();
                    }
                    y.Image = "products/images/" + date + request.Image.FileName;
                }

                _sanPhamRepository.UpdateByEntity(y);
                _sanPhamRepository.SaveChange();
                return y;
                //else
                //{
                //    throw new Exception("Brand id is not already in database");
                //}    
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public object GetProductByBrand(string ten)
        {
            try
            {
                var brand = _hangRepository.FindByCondition(row => row.tenHang == ten).FirstOrDefault();
                if (brand != null)
                {
                    var sp = _sanPhamRepository.FindByCondition(p => p.HangID == brand.Id && p.Status == 1);

                    if (sp == null)
                    {
                        return null;
                    }
                    else
                    {
                        return sp.ToList();
                    }
                }
                else
                {
                    throw new Exception("Brand id is not already in database");
                }

            }
            catch (Exception ex)
            {
                throw ex; ;
            }
        }
        public object GetProductByStatus(int status)
        {
            try
            {
                var sp = _sanPhamRepository.FindByCondition(p => p.TrangThai == status);
                if (sp == null)
                {
                    throw new Exception("can't not find any product");
                }
                else
                {
                    return sp.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object GetProductById(int Id)
        {
            try
            {
                var sp = _sanPhamRepository.FindByCondition(p => p.ID == Id && p.Status == 1).FirstOrDefault();
                if (sp == null)
                {
                    throw new Exception("can't not find any product");
                }
                else
                {
                    return sp;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public object FindProductByFilter(FilterProductRequest request)
        {
            try
            {
                var filterProduct = _sanPhamRepository.FindByCondition(row => row.Status == 1);
                if (request.hangId != 0)
                {
                    filterProduct = filterProduct.Where(f => f.HangID == request.hangId);
                }
                if (request.congSuat != "")
                {
                    filterProduct = filterProduct.Where(f => f.congSuat == request.congSuat);
                }
                if (request.kieuDang != "")
                {
                    filterProduct = filterProduct.Where(f => f.kieuDang == request.kieuDang);
                }
                if (request.soChieu != "")
                {
                    filterProduct = filterProduct.Where(f => f.soChieu == request.soChieu);
                }
                if (request.GiaDau != 0 && request.GiaCuoi != 0)
                {
                    filterProduct = filterProduct.Where(f => f.giaTien >= request.GiaDau && f.giaTien <= request.GiaCuoi);
                }
                return filterProduct.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        // sửa thành xóa
        public object DeleteProduct(int idSP)
        {
            try
            {
                var SpXoa = _sanPhamRepository.FindByCondition(p => p.ID == idSP).FirstOrDefault();
                if (SpXoa == null)
                {
                    throw new Exception("product id is not already in database");
                }
                else
                {
                    SpXoa.Status = 0;
                    _sanPhamRepository.UpdateByEntity(SpXoa);
                    _sanPhamRepository.SaveChange();
                    return SpXoa;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object GetProductStatisticskInWeek()
        {
            try
            {
                var viewListInt = _sanPhamRepository.GetProductStatisticsInWeek();
                return viewListInt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
