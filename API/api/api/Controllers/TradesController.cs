using api.Models;
using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TradesController : ControllerBase
    {
        private readonly ITradeService _tradeService;

        public TradesController(ITradeService tradeService)
        {
            _tradeService = tradeService;
        }

        // GET: api/Trades
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Trade>>> GetTrades()
        {
            var trades = await _tradeService.GetAllTradesAsync();
            return Ok(trades);
        }

        // GET: api/Trades/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Trade>> GetTrade(int id)
        {
            var trade = await _tradeService.GetTradeByIdAsync(id);
            if (trade == null)
            {
                return NotFound();
            }
            return Ok(trade);
        }

        // POST: api/Trades
        [HttpPost]
        public async Task<ActionResult<Trade>> PostTrade(Trade trade)
        {
            var createdTrade = await _tradeService.CreateTradeAsync(trade);
            return CreatedAtAction(nameof(GetTrade), new { id = createdTrade.Id }, createdTrade);
        }

        // PUT: api/Trades/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Trade>> PutTrade(int id, Trade trade)
        {
            var updatedTrade = await _tradeService.UpdateTradeAsync(id, trade);
            if (updatedTrade == null)
            {
                return NotFound();
            }
            return Ok(updatedTrade);
        }

        // DELETE: api/Trades/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTrade(int id)
        {
            var success = await _tradeService.DeleteTradeAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
