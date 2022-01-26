using BioProductStore.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BioProductStore.Services.ProductService
{
    public interface IProductService
    {
        public List<ProductResponseDTO> GetAllProducts();

        List<ProductResponseDTO> GetAllProductsByCategoryId(Guid id);

        ProductResponseDTO GetProductByProductId(Guid Id);

        void CreateProduct(RegisterProductDTO entity);

        void DeleteProductById(Guid id);

        void UpdateProduct(UpdateProductDTO product, Guid id);
    }
}
