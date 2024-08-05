using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BW2_Team6.Models;
using System.Threading.Tasks;
using BW2_Team6.Services.Classes;

namespace BW2_Team6.Controllers
{
    public class AnimalsController : Controller
    {
        private readonly AnimalService _animalService;
        private readonly ApplicationDbContext _context;

        public AnimalsController(AnimalService animalService, ApplicationDbContext context)
        {
            _animalService = animalService;
            _context = context;
        }

        // GET: Animals
        public async Task<IActionResult> AllAnimals()
        {
            return View(await _animalService.GetAllAsync());
        }

        // GET: Animals/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var animal = await _animalService.GetByIdAsync(id);
            if (animal == null)
            {
                return NotFound();
            }
            return View(animal);
        }

        // GET: Animals/Create
        public IActionResult Create()
        {
            ViewData["Owners"] = new SelectList(_context.Owners, "Id", "Name");
            return View();
        }

        // POST: Animals/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DateRegister,Name,Type,Fur,DateBirth,Microchip,OwnerId")] Animal animal)
        {
            if (ModelState.IsValid)
            {
                await _animalService.CreateAsync(animal);
                return RedirectToAction(nameof(Index));
            }
            ViewData["Owners"] = new SelectList(_context.Owners, "Id", "Name", animal.OwnerId);
            return View(animal);
        }

        // GET: Animals/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var animal = await _animalService.GetByIdAsync(id);
            if (animal == null)
            {
                return NotFound();
            }
            ViewData["Owners"] = new SelectList(_context.Owners, "Id", "Name", animal.OwnerId);
            return View(animal);
        }

        // POST: Animals/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DateRegister,Name,Type,Fur,DateBirth,Microchip,OwnerId")] Animal animal)
        {
            if (id != animal.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _animalService.UpdateAsync(id, animal);
                }
                catch (Exception)
                {
                    if (!AnimalExists(animal.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Owners"] = new SelectList(_context.Owners, "Id", "Name", animal.OwnerId);
            return View(animal);
        }

        // GET: Animals/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var animal = await _animalService.GetByIdAsync(id);
            if (animal == null)
            {
                return NotFound();
            }
            return View(animal);
        }

        // POST: Animals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _animalService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private bool AnimalExists(int id)
        {
            return _animalService.GetByIdAsync(id) != null;
        }
    }
}


