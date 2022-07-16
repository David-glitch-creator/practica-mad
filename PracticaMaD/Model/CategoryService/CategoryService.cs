using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;
using Ninject;
using System;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.CategoryService
{
    public class CategoryService : ICategoryService
    {
        [Inject]
        public ICategoryDao CategoryDao { private get; set; }

        /// <exception cref="InstanceNotFoundException"/>
        public CategoryDto FindByName(string name)
        {
            Category category = CategoryDao.FindByName(name);

            return new CategoryDto(category.categoryId, category.categoryName);
        }

        public List<CategoryDto> FindAll()
        {
            List<Category> categories = CategoryDao.FindAll();

            List<CategoryDto> categoryDtos = new List<CategoryDto>();

            foreach (Category category in categories)
            {
                categoryDtos.Add(new CategoryDto(category.categoryId, category.categoryName));
            }

            return categoryDtos;
        }
    }
}