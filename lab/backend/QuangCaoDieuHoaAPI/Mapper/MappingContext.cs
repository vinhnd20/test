using AutoMapper;
using QuangCaoDieuHoaAPI.Dto;
using QuangCaoDieuHoaAPI.Models;
using QuangCaoDieuHoaAPI.Request;

namespace QuangCaoDieuHoaAPI.Mapper
{
    public class MappingContext : Profile
    {
        public MappingContext()
        {
            CreateMap<FormKhachHangDto, SanPham>();
            CreateMap<UpdateProduct, SanPham>();
            CreateMap<ProductRequest, SanPham>();
            CreateMap<QuanLyFormRequest, FormKhachHang>();
            CreateMap<UpdateProduct, SanPham>();
            CreateMap<BrandRequest, Hang>();
            CreateMap<QuanLyFormRequest, FormKhachHang>();
        }
    }
}
