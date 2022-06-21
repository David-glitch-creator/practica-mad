using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;
using System;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.CategoryService
{
    public interface ICategoryService
    {
        ICategoryDao CategoryDao { set; }

        CategoryDto FindByName(String name);

        List<CategoryDto> FindAll();
    }
}