using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using QuangCaoDieuHoaAPI.Common;
using QuangCaoDieuHoaAPI.Database;
using QuangCaoDieuHoaAPI.Dto;
using QuangCaoDieuHoaAPI.Models;
using QuangCaoDieuHoaAPI.Repositories;
using QuangCaoDieuHoaAPI.Request;
using System.Xml.Linq;

namespace QuangCaoDieuHoaAPI.Services
{
    public class QuanLyFormServices
    {
        private readonly Repositories.QuanLyFormRespository _quanLyRepository;
        private readonly Repositories.SanPhamRepository _sanPhamRepository;
        private readonly ApiOption _apiOption;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHost;

        public QuanLyFormServices(ApiOption apiOption, DatabaseContext databaseContext, IMapper mapper, IWebHostEnvironment webHost)
        {
            _quanLyRepository = new Repositories.QuanLyFormRespository(apiOption, databaseContext, mapper);
            _sanPhamRepository = new Repositories.SanPhamRepository(apiOption, databaseContext, mapper);
            _apiOption = apiOption;
            _mapper = mapper;
            _webHost = webHost;
        }

        public object GetAllUserForm()
        {
            try
            {
                var allForm = _quanLyRepository.FindByCondition(row => row.Status == 1);
                List<FormKhachHangDto> listFormDto = new List<FormKhachHangDto>();
                foreach (var form in allForm)
                {
                    var formDto = new FormKhachHangDto();
                    formDto.ID = form.ID;
                    formDto.email = form.email;
                    formDto.hoTen = form.hoTen;
                    formDto.soDienThoai = form.soDienThoai;
                    formDto.diaChi = form.diaChi;
                    formDto.productId = form.productId;
                    formDto.CreatedDate = form.CreatedDate;
                    var checkProductName = _sanPhamRepository.FindByCondition(row => row.ID == form.productId).FirstOrDefault();
                    if (checkProductName != null)
                    {
                        formDto.productName = checkProductName.ten;

                    }
                    else
                    {
                        formDto.productName = "";
                    }
                    listFormDto.Add(formDto);
                }
                listFormDto.Reverse();
                return listFormDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object FindUserByName(string name)
        {
            try
            {
                var sp = _quanLyRepository.FindByCondition(p => p.hoTen.Contains(name) && p.Status == 1);
                if (sp != null && sp.Any())
                {
                    List<FormKhachHangDto> listFormDto = new List<FormKhachHangDto>();
                    foreach (var form in sp)
                    {
                        var formDto = new FormKhachHangDto();
                        formDto.ID = form.ID;
                        formDto.email = form.email;
                        formDto.hoTen = form.hoTen;
                        formDto.soDienThoai = form.soDienThoai;
                        formDto.diaChi = form.diaChi;
                        formDto.productId = form.productId;
                        formDto.CreatedDate = form.CreatedDate;
                        var checkProductName = _sanPhamRepository.FindByCondition(row => row.ID == form.productId).FirstOrDefault();
                        if (checkProductName != null)
                        {
                            formDto.productName = checkProductName.ten;

                        }
                        else
                        {
                            formDto.productName = "";
                        }
                        listFormDto.Add(formDto);
                    }
                    return listFormDto;
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

        public object FindFormByFilter(FilterFormRequest request)
        {
            try
            {
                var filterForm = _quanLyRepository.FindByCondition(row => row.Status == 1);
                if (request.hoTen != "")
                {
                    filterForm = filterForm.Where(f => f.hoTen.Contains(request.hoTen));
                }
                if (request.soDienThoai != "")
                {
                    filterForm = filterForm.Where(f => f.soDienThoai.Contains(request.soDienThoai));
                }
                if (request.TuNgay.HasValue && request.DenNgay.HasValue && request.TuNgay < request.DenNgay)
                {
                    filterForm = filterForm.Where(f => f.CreatedDate >= request.TuNgay && f.CreatedDate <= request.DenNgay);
                }
                return filterForm.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public object GetUserFromByID(int id)
        {
            try
            {
                var sus = _quanLyRepository.FindByCondition(u => u.ID == id && u.Status == 1).FirstOrDefault();
                if (sus != null)
                {
                    var formDto = new FormKhachHangDto();
                    formDto.ID = sus.ID;
                    formDto.email = sus.email;
                    formDto.hoTen = sus.hoTen;
                    formDto.soDienThoai = sus.soDienThoai;
                    formDto.diaChi = sus.diaChi;
                    formDto.CreatedDate = sus.CreatedDate;
                    formDto.productId = sus.productId;
                    var checkProductName = _sanPhamRepository.FindByCondition(row => row.ID == sus.productId).FirstOrDefault();
                    if (checkProductName != null)
                    {
                        formDto.productName = checkProductName.ten;

                    }
                    else
                    {
                        formDto.productName = "";
                    }
                    if (checkProductName != null)
                    {
                        formDto.productName = checkProductName.ten;

                    }
                    else
                    {
                        formDto.productName = "";
                    }
                    return formDto;
                }
                else
                {
                    throw new Exception("Can't find form");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object CreateForm(QuanLyFormRequest request)
        {
            try
            {
                var checkProductName = _sanPhamRepository.FindByCondition(row => row.ID == request.productId).FirstOrDefault();
                if (checkProductName == null)
                {

                    throw new Exception("Can't find product");
                }
                var newForm = _mapper.Map<FormKhachHang>(request);
                newForm.Status = 1;
                _quanLyRepository.Create(newForm);
                _quanLyRepository.SaveChange();
                return newForm;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object UpdateFormByID(UpdateForm request)
        {
            try
            {
                var checkProductName = _sanPhamRepository.FindByCondition(row => row.ID == request.productId).FirstOrDefault();
                if (checkProductName == null)
                {

                    throw new Exception("Can't find product");
                }
                var res = _quanLyRepository.FindByCondition(f => f.ID == request.ID).FirstOrDefault();
                if (res != null)
                {
                    res.email = request.email;
                    res.productId = request.productId;
                    res.soDienThoai = request.soDienThoai;
                    res.hoTen = request.hoTen;
                    res.diaChi = request.diaChi;
                    res.Status = 1;
                    res.UpdatedDate = DateTime.Now;
                    _quanLyRepository.UpdateByEntity(res);
                    _quanLyRepository.SaveChange();
                    return res;
                }
                else
                {
                    throw new Exception("Can't find Form");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object DeleteFormByID(int id)
        {
            try
            {
                var res = _quanLyRepository.FindByCondition(f => f.ID == id && f.Status == 1).FirstOrDefault();
                if (res == null)
                {
                    throw new Exception("Can't find Form");
                }
                else
                {
                    //res.Status = 0;
                    _quanLyRepository.DeleteByEntity(res);
                    _quanLyRepository.SaveChange();
                    return res;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
