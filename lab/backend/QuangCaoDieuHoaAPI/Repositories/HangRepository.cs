using AutoMapper;
using QuangCaoDieuHoaAPI.Common;
using QuangCaoDieuHoaAPI.Database;
using QuangCaoDieuHoaAPI.Models;
using QuangCaoDieuHoaAPI.Respositories;

namespace QuangCaoDieuHoaAPI.Repositories
{
    public class HangRepository : BaseRespository<Hang>
    {
        private IMapper _mapper;
        public HangRepository(ApiOption apiConfig, DatabaseContext databaseContext, IMapper mapper) : base(apiConfig, databaseContext)
        {
            this._mapper = mapper;
        }
    }
}
