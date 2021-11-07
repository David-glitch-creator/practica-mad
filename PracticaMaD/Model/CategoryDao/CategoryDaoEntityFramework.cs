using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System;
using System.Data;

using System.Linq;

using System.Data.Entity;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.CategoryDao
{
    public class CategoryDaoEntityFramework :
        GenericDaoEntityFramework<Category, Int64>, ICategoryDao
    {
        #region Public Constructors

        public CategoryDaoEntityFramework()
        {
        }

        #endregion Public Constructors

        #region ICategoryDao Members. Specific Operations

        public Category FindByName(string name)
        {
            Category category = null;

            DbSet<Category> categories = Context.Set<Category>();

            var result =
                (from c in categories
                 where c.categoryName == name
                 select c);

            category = result.FirstOrDefault();

            if (category == null)
                throw new InstanceNotFoundException(name,
                    typeof(Category).FullName);

            return category;
        }

        public List<Category> FindAll()
        {
            DbSet<Category> categories = Context.Set<Category>();

            var result =
                (from c in categories
                 select c).ToList();

            return result;
        }

        #endregion ICategoryDao Members
    }
}
