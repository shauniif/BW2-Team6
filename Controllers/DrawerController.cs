using BW2_Team6.Services.Classes;
using BW2_Team6.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BW2_Team6.Controllers
{
    public class DrawerController : Controller
    {
        private readonly IDrawerService _drawerSvc;
        public DrawerController(IDrawerService drawerSvc)
        {
            _drawerSvc = drawerSvc;
        }
        public async Task<IActionResult> AllDrawers()
        {
            var drawers = await _drawerSvc.GetAll();
            return View(drawers);
        }
    }
}
