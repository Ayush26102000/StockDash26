using api.Models;
using System.Collections.Generic;
using api.Models;

namespace   api.Services
{
    public interface ITradeService
    {
        Task<IEnumerable<Trade>> GetAllTradesAsync();
        Task<Trade> GetTradeByIdAsync(int id);
        Task<Trade> CreateTradeAsync(Trade trade);
        Task<Trade> UpdateTradeAsync(int id, Trade trade);
        Task<bool> DeleteTradeAsync(int id);
        Task<TradePerformance> GetTradePerformanceAsync(int id);
        Task<IEnumerable<TradePerformance>> GenerateTradePerformanceReportAsync();
    }
}
