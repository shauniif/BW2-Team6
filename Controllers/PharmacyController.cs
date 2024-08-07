﻿using BW2_Team6.Models;
using BW2_Team6.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BW2_Team6.Controllers
{
    public class PharmacyController : Controller
    {
        private readonly IPharmacyService _pharmacyService;
        private readonly ICompaniesService _companiesSvc;

        public PharmacyController(IPharmacyService pharmacyService, ICompaniesService companiesService)
        {
            _pharmacyService = pharmacyService;
            _companiesSvc = companiesService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> AllProducts()
        {
            var products = await _pharmacyService.GetAllProducts();
            return View(products);
        }


        public async Task<IActionResult> CreateProduct()
        {
            var companies = await _companiesSvc.GetAll();
            ViewBag.Companies = new SelectList(companies, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProduct(Product product, int companyId)
        {
            if (companyId == 0)
            {
                ModelState.AddModelError("Company", "Please select a company.");
            }

            if (ModelState.IsValid)
            {
                product.Company = await _companiesSvc.Read(companyId);
                await _pharmacyService.CreateProduct(product);
                return RedirectToAction("AllProducts");
            }

            var companies = await _companiesSvc.GetAll();
            ViewBag.Companies = new SelectList(companies, "Id", "Name");
            return View(product);
        }

        [HttpGet]
        public async Task<IActionResult> EditProduct(int id)
        {
            var product = await _pharmacyService.GetProductById(id);
            return View(product);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> EditProduct(int id, Product product)
        {
            var updatedProduct = await _pharmacyService.UpdateProduct(id, product);
            if (updatedProduct == null)
            {
                return NotFound();
            }
            return RedirectToAction("AllProducts", "Pharmacy");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _pharmacyService.GetProductById(id);
            return View(product);
        }

        [HttpPost, ActionName("DeleteProduct")]
        public async Task<ActionResult> DeleteConfirmedProduct(int id)
        {
            await _pharmacyService.DeleteProduct(id);
            return RedirectToAction("AllProducts", "Pharmacy");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sell>>> SellsByCustomer(int id)
        {
            var sales = await _pharmacyService.GetSellsByCustomer(id);
            return View(sales);
        }
    }
}
