using BioProductStore.DTOs;
using BioProductStore.Models;
using BioProductStore.Repositories.ProductRepository;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BioProductStore.Services.ProductService
{
    public class ProductService
    {
        public IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public List<ProductResponseDTO> GetAllProducts()
        {
            List<Product> productsList = _productRepository.GetAllProducts();

            if (productsList.Count == 0)
                throw new Exception("There are no products");

            List<ProductResponseDTO> productRespondDto = _mapper.Map<List<ProductResponseDTO>>(productsList);
            return productRespondDto;

        }

        public ProductResponseDTO GetProductByProductId(Guid Id)
        {
            Product product = _productRepository.FindById(Id);

            if (product == null)
                throw new Exception("Product not found");

            ProductResponseDTO productRespondDto = _mapper.Map<ProductResponseDTO>(product);
            return productRespondDto;
        }

        public void CreateProduct(RegisterProductDTO entity)
        {
            // verific ca numele produsului sa fie unic
            if (_productRepository.GetByName(entity.Name) != null)
                throw new Exception("Product already exists");

            var productToCreate = _mapper.Map<Product>(entity);
            productToCreate.DateCreated = DateTime.Now;
            productToCreate.DateModified = DateTime.Now;

            _productRepository.Create(productToCreate);
            _productRepository.Save();
        }

        public void DeleteProductById(Guid id)
        {
            Product product = _productRepository.FindById(id);

            if (product == null)
                throw new Exception("Product not found");

            _productRepository.Delete(product);
            _productRepository.Save();
        }

        public void UpdateProduct(UpdateProductDTO newproduct, Guid id)
        {
            Product productToUpdate = _productRepository.FindById(id);

            if (productToUpdate == null)
                throw new Exception("Product not found");

            productToUpdate = _mapper.Map<UpdateProductDTO, Product>(newproduct, productToUpdate);
            productToUpdate.DateModified = DateTime.Now;

            try
            {
                _productRepository.Update(productToUpdate);
                _productRepository.Save();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
