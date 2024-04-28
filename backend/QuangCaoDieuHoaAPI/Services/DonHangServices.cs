using AutoMapper;
using QuangCaoDieuHoaAPI.Common;
using QuangCaoDieuHoaAPI.Database;
using QuangCaoDieuHoaAPI.Repositories;

namespace QuangCaoDieuHoaAPI.Services
{
    public class DonHangServices
    {
        private readonly DonHangRepository _donHangRepository;
        private readonly ApiOption _apiOption;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHost;

        public DonHangServices(ApiOption apiOption, DatabaseContext databaseContext, IMapper mapper, IWebHostEnvironment webHost)
        {
            _donHangRepository = new DonHangRepository(apiOption, databaseContext, mapper);
            _apiOption = apiOption;
            _mapper = mapper;
            _webHost = webHost;
        }

        public object GetAllOrder()
        {
            try
            {
                var ods = _donHangRepository.FindAll();
                if (ods == null)
                {
                    return null;
                }
                else
                {
                    return ods.ToList();
                }
            }
            catch
            {
                return null;
            }
        }
        public object GetOrderByID(int orderid)
        {
            try
            {
                var sodr = _donHangRepository.FindByCondition(p => p.ID == orderid);
                if(sodr == null)
                {
                    return null;
                }    
                else
                {
                    return sodr;
                }    
            }
            catch
            {
                return null;
            }
        }
    }
}
