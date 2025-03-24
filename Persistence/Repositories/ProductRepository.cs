using Microsoft.EntityFrameworkCore;
using Dws.Note_one.Api.Domain.Models;
using Dws.Note_one.Api.Persistence.Repositories;
using Dws.Note_one.Api.Persistence.Context; 
using Dws.Note_one.Api.Persistence.Repositories.IRepositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Dws.Note_one.Api.Persistence.Repositories
{
    public class ProductRepository : BaseRepository, IProductRepository
    {
        private readonly AppDbContext _context; 

        public ProductRepository(AppDbContext context) : base(context)
        {
            _context = context; 
        }

        public async Task AddAsync(Product product) 
        {
            await _context.Products.AddAsync(product);
        }

        public async Task<IEnumerable<Product>> ListByCategoryIdAsync(int categoryId)
        {
            return await _context.Products
                .Where(p => p.CategoryId == categoryId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> ListAsync() 
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> FindByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }
        public async Task DeleteByIdAsync(int id)
        {
            var productToDelete = await _context.Products.FindAsync(id);
            if (productToDelete != null)
            {
                _context.Products.Remove(productToDelete);
            }
        }
    }
}