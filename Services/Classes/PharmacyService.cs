using BW2_Team6.Context;
using BW2_Team6.Models;
using BW2_Team6.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BW2_Team6.Services
{
    public class PharmacyService : IPharmacyService
    {
        private readonly DataContext _db; 

        public PharmacyService(DataContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _db.Products.ToListAsync();
        }

        public async Task<Product> GetProductById(int id)
        {
            return await _db.Products.FindAsync(id);
        }

        public async Task<Product> CreateProduct(Product product)
        {
            await _db.Products.AddAsync(product);
            await _db.SaveChangesAsync();
            return product;
        }

        public async Task<Product> UpdateProduct(int id, Product product)
        {
            var existingProduct = await _db.Products.FindAsync(id);
            if (existingProduct == null)
            {
                return null;
            }

            existingProduct.Name = product.Name;
            existingProduct.TypeOfProduct = product.TypeOfProduct;
            existingProduct.TypeOfUse = product.TypeOfUse;
            existingProduct.Company = product.Company;

            _db.Products.Update(existingProduct);
            await _db.SaveChangesAsync();
            return existingProduct;
        }

        public async Task<bool> DeleteProduct(int id)
        {
            var product = await _db.Products.FindAsync(id);
            if (product == null)
            {
                return false;
            }

            _db.Products.Remove(product);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Sell>> GetSellsByCustomer(int id)
        {
            return await _db.Sells.Where(s => s.Owner.Id == id).ToListAsync();
        }

        public async Task<IEnumerable<Sell>> GetSellsByFiscalCode (string fiscalcode)
        {
            return await _db.Sells.Where(s => s.Owner.FiscalCode == fiscalcode).ToListAsync();
        }

    }
}
