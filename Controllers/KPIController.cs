using GeekGarden.API.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace GeekGarden.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KPIController : ControllerBase
    {
        private readonly AppDbContext _context;
        public KPIController(AppDbContext context) => _context = context;

        [HttpGet("summary")]
        public IActionResult GetSummary()
        {
            var result = _context.KPIs
                .GroupBy(k => k.CompanyId)
                .Select(g => new
                {
                    Company = _context.Companies.First(c => c.Id == g.Key).Name,
                    AverageScore = g.Average(x => x.KpiScore)
                }).ToList();

            return Ok(result);
        }
    }
}
