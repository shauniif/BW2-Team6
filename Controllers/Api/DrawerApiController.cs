using BW2_Team6.Models;
using BW2_Team6.Services.Classes;
using BW2_Team6.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BW2_Team6.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrawerApiController : ControllerBase
    {
        private readonly IDrawerService _drawerService;

        public DrawerApiController(IDrawerService drawerService) { 
           _drawerService = drawerService;
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> SearchProduct(int id)
        {
           var drawer = await _drawerService.SearchProduct(id);
            return Ok(drawer);
        } 


    }
    
}
