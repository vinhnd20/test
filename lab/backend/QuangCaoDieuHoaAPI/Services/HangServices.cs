using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
    public class HangServices
    {
        private readonly HangRepository _hangRepository;
        private readonly TrangThaiRepository _trangThaiRepository;
        private readonly ApiOption _apiOption;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHost;

        public HangServices(ApiOption apiOption, DatabaseContext databaseContext, IMapper mapper, IWebHostEnvironment webHost)
        {
            _hangRepository = new HangRepository(apiOption, databaseContext, mapper);
            _trangThaiRepository = new TrangThaiRepository(apiOption, databaseContext, mapper);
            _apiOption = apiOption;
            _mapper = mapper;
            _webHost = webHost;
        }

        public object FindAllBrand()
        {
            try
            {
                return _hangRepository.FindByCondition(row => row.status == 1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object FindBrandById(int Id)
        {
            try
            {
                var brand = _hangRepository.FindByCondition(row => row.Id == Id).FirstOrDefault();
                if (brand == null) {
                    throw new Exception("Can't find brand");
                }
                return brand;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object FindBrandByName(string name)
        {
            try
            {
                var brand = _hangRepository.FindByCondition(row => row.tenHang == name).FirstOrDefault();
                if (brand == null)
                {
                    throw new Exception("Can't find brand");
                }
                return brand;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object AddNewBrand(BrandRequest request)
        {
            try
            {
                var checkBrand = _hangRepository.FindByCondition(row => row.tenHang == request.tenHang).FirstOrDefault();
                if (checkBrand != null)
                {
                    throw new Exception("Brand is existed!");
                }
                var newBrand = _mapper.Map<Hang>(request);
                newBrand.image = "";
                if (request.image != null)
                {
                    var date = DateTime.UtcNow.ToString("yyyy_MM_dd_HH_mm");
                    using (FileStream fileStream = File.Create(_webHost.WebRootPath + "\\brands\\images\\" + date + request.image.FileName))
                    {
                        request.image.CopyTo(fileStream);
                        fileStream.Flush();
                    }
                    newBrand.image = "brands/images/" + date + request.image.FileName;
                }
                newBrand.status = 1;
                _hangRepository.Create(newBrand);
                _hangRepository.SaveChange();
                return newBrand;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object EditBrand(UpdateBrandRequest request)
        {
            try
            {
                var y = _hangRepository.FindByCondition(p => p.Id == request.Id).FirstOrDefault();
                if (y == null)
                {
                    throw new Exception("Can't not find brand to edit");
                }
                y.tenHang = request.ten;
                y.moTa = request.moTa;
                y.UpdatedDate = DateTime.Now;
                if (request.image != null && request.image.FileName != y.image)
                {
                    var date = DateTime.UtcNow.ToString("yyyy_MM_dd_HH_mm");
                    using (FileStream fileStream = File.Create(_webHost.WebRootPath + "\\brands\\images\\" + date + request.image.FileName))
                    {
                        request.image.CopyTo(fileStream);
                        fileStream.Flush();
                    }
                    y.image = "brands/images/" + date + request.image.FileName;
                }

                _hangRepository.UpdateByEntity(y);
                _hangRepository.SaveChange();
                return y;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public object DeleteBrand(int HangID)
        {
            try
            {
                var checkBrandName = _hangRepository.FindByCondition(p => p.Id == HangID).FirstOrDefault();
                if (checkBrandName != null)
                {
                    checkBrandName.status = 0;
                    _hangRepository.UpdateByEntity(checkBrandName);
                    _hangRepository.SaveChange();
                    return checkBrandName;
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
    }
}
