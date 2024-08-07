using BW2_Team6.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BW2_Team6.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecoverApiController : ControllerBase
    {
        private readonly IRecoverService _recoverSvc;

        public RecoverApiController(IRecoverService recoverService)
        {
            _recoverSvc = recoverService;
        }

        [HttpGet("{microchip}")]
        public async Task<IActionResult> SearchAnimalByMicrochip(string microchip)
        {
            var recover = await _recoverSvc.SearchAnimalByMicrochip(microchip);
            return Ok(recover);
        }
    }
}

    