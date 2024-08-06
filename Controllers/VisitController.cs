using BW2_Team6.Models;
using BW2_Team6.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BW2_Team6.Controllers
{
    public class VisitController : Controller
    {
        private readonly IVisitService _visitSvc;
        public VisitController(IVisitService visitService)
        {
            _visitSvc = visitService;
        }
        public async Task<IActionResult> AllVisits()
        {
            var visits = await _visitSvc.GetAll();
            return View(visits);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int animalId, VisitViewModel visit)
        {
            if (ModelState.IsValid)
            {
                await _visitSvc.Create(animalId, visit);
                return RedirectToAction("AllVisits", "Visit");
            }
            return View(visit);
        }

        public async Task<IActionResult> Update(int id)
        {
            var visit = await _visitSvc.GetById(id);
            return View(visit);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, VisitViewModel visit)
        {
            if (ModelState.IsValid)
            {
                await _visitSvc.Update(id, visit);
                return RedirectToAction("AllVisits", "Visit");
            }
            return View(visit);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var visit = await _visitSvc.GetById(id);
            return View(visit);
        }

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _visitSvc.Delete(id);
            return RedirectToAction("AllVisits", "Visit");

        }
    }
}
