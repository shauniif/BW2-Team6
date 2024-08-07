using BW2_Team6.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BW2_Team6.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellApiController : ControllerBase
    {
        private readonly IPharmacyService _pharmacySvc;

        public SellApiController(IPharmacyService pharmacySvc)
        {
            _pharmacySvc = pharmacySvc;
        }

        [HttpGet("{fiscalcode}")]
        public async Task<IActionResult> SellsByFiscalCode(string fiscalcode)
        {
            var visit = await _pharmacySvc.GetSellsByFiscalCode(fiscalcode);
            return Ok(visit);
        }
    }
}
