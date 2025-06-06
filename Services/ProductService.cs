using Dws.Note_one.Api.Domain.Models;
using Dws.Note_one.Api.Services;
using Dws.Note_one.Api.Services.Communication;
using Dws.Note_one.Api.Persistence.Repositories.IRepositories;
using Dws.Note_one.Api.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dws.Note_one.Api.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICategoryRepository _categoryRepository; 

        public ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _categoryRepository = categoryRepository; 
        }

        public async Task<ProductResponse> DeleteAsync(int id)
        {
            try
            {
                var existProduct = await _productRepository.FindByIdAsync(id);

                if (existProduct == null)
                {
                    return new ProductResponse($"Product with ID {id} not found"); 
                }

                _productRepository.DeleteByIdAsync(existProduct.Id);

                await _unitOfWork.SaveChangesAsync();

                return new ProductResponse(existProduct);
            }
            catch (Exception ex)
            {
               
                return new ProductResponse($"An error occurred when deleting the product: {ex.Message}");
            }
        }
        public async Task DeleteByIdAsync(int id)
        {
            try
            {
                var existProduct = await _productRepository.FindByIdAsync(id);
                if (existProduct == null)
                {
                    throw new KeyNotFoundException($"Product with ID {id} not found");
                }

                await _productRepository.DeleteByIdAsync(existProduct.Id);  
                await _unitOfWork.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in DeleteByIdAsync: {ex.Message}");
                throw; 
            }
        }
    

        public async Task<ProductResponse> AddAsync(Product product)
        {
            try
            {
                var existingCategory = await _categoryRepository.FindByIdAsync(product.CategoryId);
                if (existingCategory == null)
                {
                    return new ProductResponse($"Category with ID {product.CategoryId} not found.");
                }

                await _productRepository.AddAsync(product);
                await _unitOfWork.CompleteAsync();
                return new ProductResponse(product);
            }
            catch (Exception ex)
            {
                return new ProductResponse($"An error occurred when saving the product: {ex.Message}");
            }
        }

        public async Task<IEnumerable<Product>> ListByCategoryIdAsync(int categoryId)
        {
            return await _productRepository.ListByCategoryIdAsync(categoryId);
        }

        public async Task<IEnumerable<Product>> ListAsync()
        {
            return await _productRepository.ListAsync();
        }

        public async Task<IEnumerable<Product>> ListByCategoryNameAsync(string categoryName)
        {
            var category = await _categoryRepository.FindByNameAsync(categoryName);
            if (category == null)
            {
                return new List<Product>();
            }

            return await _productRepository.ListByCategoryIdAsync(category.Id);
        }

        public async Task<ProductResponse> SaveAsync(Product product, string categoryName)
        {
            try
            {
                var existingCategory = await _categoryRepository.FindByNameAsync(categoryName);
                if (existingCategory == null)
                {
                    return new ProductResponse($"Category with name '{categoryName}' not found.");
                }

                product.Category = existingCategory;
                product.CategoryId = existingCategory.Id;


                await _productRepository.AddAsync(product);
                await _unitOfWork.CompleteAsync();
                return new ProductResponse(product);
            }
            catch (Exception ex)
            {
                return new ProductResponse($"An error occurred when saving the product: {ex.Message}");
            }
        }       
    }
}