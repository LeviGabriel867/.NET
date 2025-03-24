using System.Collections.Generic;
using System.Threading.Tasks;
using Dws.Note_one.Api.Domain.Models;
using Dws.Note_one.Api.Services.Communication;

namespace Dws.Note_one.Api.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> ListAsync();
        Task<IEnumerable<Product>> ListByCategoryNameAsync(string categoryName); 
        Task<ProductResponse> SaveAsync(Product product, string categoryName);
        Task<ProductResponse> DeleteAsync(int id); 
        Task DeleteByIdAsync(int id); 
    }
}