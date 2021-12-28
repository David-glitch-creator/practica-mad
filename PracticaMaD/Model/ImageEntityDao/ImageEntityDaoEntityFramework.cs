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

        public List<ImageEntity> FindByCategory(long categoryId)
        {
            DbSet<ImageEntity> images = Context.Set<ImageEntity>();

            var result =
                (from i in images
                 where i.categoryId == categoryId
                 select i).ToList();

            return result;
        }

        public List<ImageEntity> FindAll()
        {
            DbSet<ImageEntity> images = Context.Set<ImageEntity>();

            var result =
                (from c in images
                 select c).ToList();

            return result;
        }

        public List<ImageEntity> FindByKeywords(string keywords)
        {
            DbSet<ImageEntity> images = Context.Set<ImageEntity>();

            var result =
                (from c in images
                 where c.title.Contains(keywords) || c.imageDescription.Contains(keywords)
                 select c).ToList();

            return result;
        }

        public List<ImageEntity> FindByCategoryKeywords(long categoryId, string keywords)
        {
            DbSet<ImageEntity> images = Context.Set<ImageEntity>();

            var result =
                (from c in images
                 where c.categoryId == categoryId && (c.title.Contains(keywords) || c.imageDescription.Contains(keywords))
                 select c).ToList();

            return result;
        }


        public long Like(UserProfile user, ImageEntity image)
        {
            if (!image.UserProfile1.Contains(user))
            {
                image.UserProfile1.Add(user);
            }

            Update(image);

            return image.imageId;
        }

        public long Dislike(UserProfile user, ImageEntity image)
        {
            if (image.UserProfile1.Contains(user))
            {
                image.UserProfile1.Remove(user);
            }

            Update(image);

            return image.imageId;
        }

        public int GetNumberOfLikes(ImageEntity image)
        {
            return image.UserProfile1.Count;
        }

        #endregion IImageEntityDao Members
    }
}
