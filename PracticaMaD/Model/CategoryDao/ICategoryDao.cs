using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.CategoryDao
{
    public interface ICategoryDao :IGenericDao<Category, Int64>
    {
        Category FindByName(String name);

        List<Category> FindAll();
    }
}