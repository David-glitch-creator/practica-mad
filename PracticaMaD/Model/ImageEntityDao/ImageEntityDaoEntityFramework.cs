using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System;
using System.Data;

using System.Linq;

using System.Data.Entity;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.ImageEntityDao
{
    public class ImageEntityDaoEntityFramework : 
        GenericDaoEntityFramework<ImageEntity, Int64>, IImageEntityDao
    {
        #region Public Constructors

        public ImageEntityDaoEntityFramework()
        {
        }

        #endregion Public Constructors


        #region IImageEntityDao Members. Specific Operations

        public List<ImageEntity> FindByAuthor(long userId)
        {
            DbSet<ImageEntity> images = Context.Set<ImageEntity>();

            var result =
                (from i in images
                 where i.author == userId
                 select i).ToList();

            return result;
        }
        
        #endregion IImageEntityDao Members
    }
}
