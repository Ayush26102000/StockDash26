using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    public class Trade
    {
        public int Id { get; set; }

        [Required]
        public string Asset { get; set; }  // Stock, Crypto, Forex pair

        [Required]
        public decimal EntryPrice { get; set; }

        [Required]
        public decimal ExitPrice { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public DateTime TradeDate { get; set; } = DateTime.UtcNow;

        public decimal ProfitOrLoss => (ExitPrice - EntryPrice) * Quantity;
    }
}
