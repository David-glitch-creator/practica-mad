using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;
using System;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.CategoryService
{
    public interface ICategoryService
    {
        ICategoryDao CategoryDao { set; }

        /// <exception cref="InstanceNotFoundException"/>
        CategoryDto FindByName(String name);

        List<CategoryDto> FindAll();
    }
}