using Dws.Note_one.Api.Domain.Models; 
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dws.Note_one.Api.Persistence.Repositories.IRepositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> ListAsync();
        Task AddAsync(Product product);
        Task<IEnumerable<Product>> ListByCategoryIdAsync(int categoryId);
        Task<Product>FindByIdAsync(int id);
        Task DeleteByIdAsync(int id);   
    }
}