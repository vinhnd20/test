using AutoMapper;
using QuangCaoDieuHoaAPI.Common;
using QuangCaoDieuHoaAPI.Database;
using QuangCaoDieuHoaAPI.Models;
using QuangCaoDieuHoaAPI.Respositories;

namespace QuangCaoDieuHoaAPI.Repositories
{
    public class TrangThaiRepository : BaseRespository<TrangThai>
    {
        private readonly IMapper _mapper;
        public TrangThaiRepository(ApiOption apiConfig,DatabaseContext dbContext,IMapper mapper) : base(apiConfig, dbContext) 
        { 
            this._mapper = mapper;
        }

    }
}
