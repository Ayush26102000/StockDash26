using api.Services;
using Microsoft.AspNetCore.Mvc;
using api.Services;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly ITradeService _tradeService;

        public ReportsController(ITradeService tradeService)
        {
            _tradeService = tradeService;
        }

        // GET: api/Reports/TradePerformance
        [HttpGet("TradePerformance")]
        public async Task<ActionResult> GetTradePerformanceReport()
        {
            var report = await _tradeService.GenerateTradePerformanceReportAsync();
            if (report == null || !report.Any())
            {
                return NoContent();
            }

            return Ok(report);
        }
    }
}
