using BW2_Team6.Models;
using BW2_Team6.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BW2_Team6.Controllers
{
    public class SellController : Controller
    {
        private readonly ISellService _sellService;
        private readonly IPharmacyService _pharmacyService;
        private readonly IOwnerService _ownerService;

        public SellController(ISellService sellService, IPharmacyService pharmacyService, IOwnerService ownerService)
        {
            _sellService = sellService;
            _pharmacyService = pharmacyService;
            _ownerService = ownerService;
        }

        public async Task<IActionResult> AllSells()
        {
            var sells = await _sellService.GetAll();
            return View(sells);
        }

        [HttpPost]
        public async Task<IActionResult> SellProducts (int[] selectedProducts)
        {
            if(selectedProducts != null && selectedProducts.Length > 0)
            {
                foreach(var productId in selectedProducts)
                {
                    var product = await _pharmacyService.GetProductById(productId);
                    var owner = await _ownerService.Read(2);
                    if(product != null)
                    {
                        var sell = new Sell
                        {
                            Owner = owner,
                            Product = product
                        };
                        await _sellService.Create(sell);
                    }
                }
            }

            return RedirectToAction("AllSells");
        }
    }
}

