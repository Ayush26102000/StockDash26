using api.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using api.Models;

namespace api.Data
{
    public class TradeJournalDbContext : DbContext
    {
        public TradeJournalDbContext(DbContextOptions<TradeJournalDbContext> options) : base(options) { }

        public DbSet<Trade> Trades { get; set; }
    }
}
