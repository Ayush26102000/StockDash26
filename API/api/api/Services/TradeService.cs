
using Microsoft.EntityFrameworkCore;
using api.Data;
using api.Models;
using api.Services;

namespace api.Services
{
    public class TradeService : ITradeService
    {
        private readonly TradeJournalDbContext _context;

        public TradeService(TradeJournalDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Trade>> GetAllTradesAsync()
        {
            return await _context.Trades.ToListAsync();
        }

        public async Task<Trade> GetTradeByIdAsync(int id)
        {
            return await _context.Trades.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Trade> CreateTradeAsync(Trade trade)
        {
            _context.Trades.Add(trade);
            await _context.SaveChangesAsync();
            return trade;
        }

        public async Task<Trade> UpdateTradeAsync(int id, Trade trade)
        {
            var existingTrade = await _context.Trades.FindAsync(id);
            if (existingTrade == null) return null;

            existingTrade.Asset = trade.Asset;
            existingTrade.EntryPrice = trade.EntryPrice;
            existingTrade.ExitPrice = trade.ExitPrice;
            existingTrade.Quantity = trade.Quantity;
            existingTrade.TradeDate = trade.TradeDate;

            await _context.SaveChangesAsync();
            return existingTrade;
        }

        public async Task<bool> DeleteTradeAsync(int id)
        {
            var trade = await _context.Trades.FindAsync(id);
            if (trade == null) return false;

            _context.Trades.Remove(trade);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<TradePerformance> GetTradePerformanceAsync(int id)
        {
            var trade = await _context.Trades.FindAsync(id);
            if (trade == null) return null;

            var profitLoss = (trade.ExitPrice - trade.EntryPrice) * trade.Quantity;
            var roi = (profitLoss / (trade.EntryPrice * trade.Quantity)) * 100;
            var riskRewardRatio = CalculateRiskRewardRatio(trade.EntryPrice, trade.ExitPrice);

            var performance = new TradePerformance
            {
                TradeId = trade.Id,
                EntryPrice = trade.EntryPrice,
                ExitPrice = trade.ExitPrice,
                Quantity = trade.Quantity,
                ProfitLoss = profitLoss,
                ROI = roi,
                RiskRewardRatio = riskRewardRatio
            };

            return performance;
        }

        private decimal CalculateRiskRewardRatio(decimal entryPrice, decimal exitPrice)
        {
            // Example logic: Risk/Reward = (ExitPrice - EntryPrice) / (EntryPrice * 0.02) 
            // where 2% stop loss is considered for risk calculation.
            return (exitPrice - entryPrice) / (entryPrice * 0.02m);
        }

        public async Task<IEnumerable<TradePerformance>> GenerateTradePerformanceReportAsync()
        {
            var trades = await _context.Trades.ToListAsync();
            var performanceReport = new List<TradePerformance>();

            foreach (var trade in trades)
            {
                var performance = await GetTradePerformanceAsync(trade.Id);
                if (performance != null)
                {
                    performanceReport.Add(performance);
                }
            }

            return performanceReport;
        }

    }
}
