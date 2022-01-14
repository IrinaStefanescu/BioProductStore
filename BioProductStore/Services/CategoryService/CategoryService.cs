using BioProductStore.DTOs;
using BioProductStore.Models;
using BioProductStore.Repositories.CategoryRepository;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BioProductStore.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        public ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public List<CategoryResponseDTO> GetAllCategories()
        {
            List<Category> categoriesList = _categoryRepository.GetAllCategories();

            if (categoriesList.Count == 0)
                throw new Exception("There are no categories");

            List<CategoryResponseDTO> categoryRespondDTO = _mapper.Map<List<CategoryResponseDTO>>(categoriesList);
            return categoryRespondDTO;
        }

        public CategoryResponseDTO GetCategoryByCategoryId(Guid Id)
        {
            Category category = _categoryRepository.FindById(Id);

            if (category == null)
                throw new Exception("Category not found");

            CategoryResponseDTO categoryRespondDTO = _mapper.Map<CategoryResponseDTO>(category);
            return categoryRespondDTO;
        }

        public void CreateCategory(RegisterCategoryDTO entity)
        {
            if (_categoryRepository.GetByName(entity.Name) != null)
                throw new Exception("Category already exists");

            var categoryToCreate = _mapper.Map<Category>(entity);
            categoryToCreate.DateCreated = DateTime.Now;
            categoryToCreate.DateModified = DateTime.Now;

            _categoryRepository.Create(categoryToCreate);
            _categoryRepository.Save();
        }

        public void DeleteCategoryById(Guid id)
        {
            Category category = _categoryRepository.FindById(id);

            if (category == null)
                throw new Exception("Category not found");

            _categoryRepository.Delete(category);
            _categoryRepository.Save();
        }

        public void UpdateCategory(RegisterCategoryDTO category, Guid id)
        {
            Category categoryToUpdate = _categoryRepository.FindById(id);

            if (categoryToUpdate == null)
                throw new Exception("Category not found");

            categoryToUpdate = _mapper.Map<RegisterCategoryDTO, Category>(category, categoryToUpdate);
            categoryToUpdate.DateModified = DateTime.Now;

            try
            {
                _categoryRepository.Update(categoryToUpdate);
                _categoryRepository.Save();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
