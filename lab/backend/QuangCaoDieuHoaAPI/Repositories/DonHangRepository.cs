using AutoMapper;
using QuangCaoDieuHoaAPI.Common;
using QuangCaoDieuHoaAPI.Database;
using QuangCaoDieuHoaAPI.Models;
using QuangCaoDieuHoaAPI.Request;
using QuangCaoDieuHoaAPI.Respositories;
using QuangCaoDieuHoaAPI.Utility;


namespace QuangCaoDieuHoaAPI.Repositories
{
    public class DonHangRepository : BaseRespository<DonHang>
    {
        private IMapper _mapper;
        public DonHangRepository(ApiOption apiConfig, DatabaseContext databaseContext, IMapper mapper) : base(apiConfig, databaseContext)
        {
            this._mapper = mapper;
        }
    }
}
