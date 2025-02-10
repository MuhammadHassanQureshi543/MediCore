using Application.Interface.Reports;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MediCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class ReportsController : ControllerBase
    {
        private readonly IReports _reportsServices;
        public ReportsController(IReports reportsServices)
        {
            _reportsServices = reportsServices;
        }

        [HttpGet]
        [Route("patient-count")]
        public async Task<ActionResult> GetPatientCount()
        {
            var result = await _reportsServices.GetPatientCount();
            return Ok(result);
        }

        [HttpGet]
        [Route("financials")]
        public async Task<ActionResult> GetFinancialsReport()
        {
            var result = await _reportsServices.GetFinancialReport();
            return Ok(result);
        }
    }
}
