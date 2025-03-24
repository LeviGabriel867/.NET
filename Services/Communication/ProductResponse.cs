using Dws.Note_one.Api.Domain.Models; 
using Dws.Note_one.Api.Services.Communication;
namespace Dws.Note_one.Api.Services.Communication 
{
    public class ProductResponse : BaseResponse
    {
        public Product Product { get; private set; }

        private ProductResponse(bool success, string message, Product product) : base(success, message)
        {
            Product = product;
        }

        public ProductResponse(Product product) : this(true, string.Empty, product) { }

        public ProductResponse(string message) : this(false, message, null) { }
    }
}