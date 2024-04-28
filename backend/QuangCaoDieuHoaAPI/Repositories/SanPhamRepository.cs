using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuangCaoDieuHoaAPI.Common;
using QuangCaoDieuHoaAPI.Database;
using QuangCaoDieuHoaAPI.Models;
using QuangCaoDieuHoaAPI.Respositories;
using static QuangCaoDieuHoaAPI.Repositories.SanPhamRepository;

namespace QuangCaoDieuHoaAPI.Repositories
{
    public class SanPhamRepository : BaseRespository<SanPham>
    {
        private IMapper _mapper;
        public SanPhamRepository(ApiOption apiConfig, DatabaseContext databaseContext, IMapper mapper) : base(apiConfig, databaseContext)
        {
            this._mapper = mapper;
        }
        public List<object> GetProductStatisticsInWeek()
        {
            try
            {
                DateTime startOfWeek = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek);

                List<object> weeklyStatistics = new List<object>();

                for (int i = 0; i < 7; i++)
                {
                    DateTime currentDate = startOfWeek.AddDays(i);

                    int productCount = DbContext.tblSanPhams
                        .Count(p => p.CreatedDate.Date == currentDate.Date);

                    var dailyStatistics = new
                    {
                        Date = currentDate,
                        ProductCount = productCount
                    };

                    weeklyStatistics.Add(dailyStatistics);
                }

                return weeklyStatistics;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
