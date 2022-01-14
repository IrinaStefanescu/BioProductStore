using BioProductStore.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BioProductStore.Services.CategoryService
{
    public interface ICategoryService
    {
        public List<CategoryResponseDTO> GetAllCategories();

        CategoryResponseDTO GetCategoryByCategoryId(Guid Id);

        void CreateCategory(RegisterCategoryDTO entity);

        void DeleteCategoryById(Guid id);

        void UpdateCategory(RegisterCategoryDTO category, Guid id);
    }
}
