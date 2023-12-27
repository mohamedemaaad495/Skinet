using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext _context;

        public ProductRepository(StoreContext context)
        {
            _context = context;
        }
        public async Task<IReadOnlyList<Product>> GetAllProductsAsync()
        {
            var products = await _context.products
                .Include(p=>p.ProductType)
                .Include(p=>p.ProductBrand)
                .ToListAsync();
            return products;
        }

        public async Task<IReadOnlyList<ProductBrand>> GetAllProductsBrandsAsync()
        {
            var productBrands = await _context.productBrands.ToListAsync();
            return productBrands;
        }

        public async Task<IReadOnlyList<ProductType>> GetAllProductsTypesAsync()
        {
            var productTypes = await _context.productTypes.ToListAsync();
            return productTypes;
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            var product = await _context.products
                .Include(p=>p.ProductType)
                .Include(p=>p.ProductBrand)
                .FirstOrDefaultAsync(p => p.Id == id);
            return product;
        }
    }
}
