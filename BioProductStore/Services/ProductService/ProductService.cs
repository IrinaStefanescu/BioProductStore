using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BioProductStore.DataAccess;
using BioProductStore.DTOs;
using BioProductStore.Models;
using Microsoft.Data.SqlClient;

namespace BioProductStore.Services.ProductService
{
    public class ProductService : IProductService
    {
        public UnitOfWork _uow;
        private readonly IMapper _mapper;

        public ProductService(UnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public List<ProductResponseDTO> GetAllProducts()
        {
            var productsList = _uow.Product.GetAllAsQueryable();

            if (productsList.Count() == 0)
                throw new Exception("There are no products");

            List<ProductResponseDTO> productRespondDto = _mapper.Map<List<ProductResponseDTO>>(productsList);
            return productRespondDto;

        }

        public ProductResponseDTO GetProductByProductId(Guid Id)
        {
            Product product = _uow.Product.FindById(Id);

            if (product == null)
                throw new Exception("Product not found");

            ProductResponseDTO productRespondDto = _mapper.Map<ProductResponseDTO>(product);
            return productRespondDto;
        }

        public void CreateProduct(RegisterProductDTO entity)
        {
            // verific ca numele produsului sa fie unic
            if (_uow.Product.FindBy(e => e.Name == entity.Name) != null)
                throw new Exception("Product already exists");

            var productToCreate = _mapper.Map<Product>(entity);
            productToCreate.DateCreated = DateTime.Now;
            productToCreate.DateModified = DateTime.Now;

            _uow.Product.Create(productToCreate);
            _uow.SaveChanges();
        }

        public void DeleteProductById(Guid id)
        {
            Product product = _uow.Product.FindById(id);

            if (product == null)
                throw new Exception("Product not found");

            _uow.Product.Delete(product);
            _uow.SaveChanges();
        }

        public void UpdateProduct(UpdateProductDTO newproduct, Guid id)
        {
            Product productToUpdate = _uow.Product.FindById(id);

            if (productToUpdate == null)
                throw new Exception("Product not found");

            productToUpdate = _mapper.Map<UpdateProductDTO, Product>(newproduct, productToUpdate);
            productToUpdate.DateModified = DateTime.Now;

            try
            {
                _uow.Product.Update(productToUpdate);
                _uow.SaveChanges();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
