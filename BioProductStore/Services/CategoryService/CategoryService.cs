using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BioProductStore.DataAccess;
using BioProductStore.DTOs;
using BioProductStore.Models;
using Microsoft.Data.SqlClient;

namespace BioProductStore.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private UnitOfWork _uow;
        private readonly IMapper _mapper;

        public CategoryService(UnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public List<CategoryResponseDTO> GetAllCategories()
        {
            var categoriesList = _uow.Category.GetAllAsQueryable();

            if (categoriesList.Count() == 0)
                throw new Exception("There are no categories");

            List<CategoryResponseDTO> categoryRespondDTO = _mapper.Map<List<CategoryResponseDTO>>(categoriesList);
            return categoryRespondDTO;
        }

        public CategoryResponseDTO GetCategoryByCategoryId(Guid Id)
        {
            Category category = _uow.Category.FindById(Id);

            if (category == null)
                throw new Exception("Category not found");

            CategoryResponseDTO categoryRespondDTO = _mapper.Map<CategoryResponseDTO>(category);
            return categoryRespondDTO;
        }

        public void CreateCategory(RegisterCategoryDTO entity)
        {
            if (_uow.Category.FindBy(e => e.Name == entity.Name) != null)
                throw new Exception("Category already exists");

            var categoryToCreate = _mapper.Map<Category>(entity);
            categoryToCreate.DateCreated = DateTime.Now;
            categoryToCreate.DateModified = DateTime.Now;

            _uow.Category.Create(categoryToCreate);
            _uow.SaveChanges();
        }

        public void DeleteCategoryById(Guid id)
        {
            Category category = _uow.Category.FindById(id);

            if (category == null)
                throw new Exception("Category not found");

            _uow.Category.Delete(category);
            _uow.SaveChanges();
        }

        public void UpdateCategory(RegisterCategoryDTO category, Guid id)
        {
            Category categoryToUpdate = _uow.Category.FindById(id);

            if (categoryToUpdate == null)
                throw new Exception("Category not found");

            categoryToUpdate = _mapper.Map<RegisterCategoryDTO, Category>(category, categoryToUpdate);
            categoryToUpdate.DateModified = DateTime.Now;

            try
            {
                _uow.Category.Update(categoryToUpdate);
                _uow.SaveChanges();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
