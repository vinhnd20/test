using AutoMapper;
using QuangCaoDieuHoaAPI.Common;
using QuangCaoDieuHoaAPI.Database;
using QuangCaoDieuHoaAPI.Models;
using QuangCaoDieuHoaAPI.Respositories;

namespace QuangCaoDieuHoaAPI.Repositories
{
    public class QuanLyFormRespository : BaseRespository<FormKhachHang>
    {
        private IMapper _mapper;


        public QuanLyFormRespository(ApiOption apiConfig, DatabaseContext databaseContext, IMapper mapper) : base(apiConfig, databaseContext)
        {
            this._mapper = mapper;
        }
    }
}
