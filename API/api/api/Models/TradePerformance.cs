namespace api.Models
{
    public class TradePerformance
    {
        public int TradeId { get; set; }
        public decimal EntryPrice { get; set; }
        public decimal ExitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal ProfitLoss { get; set; }
        public decimal ROI { get; set; }
        public decimal RiskRewardRatio { get; set; }
    }
}
