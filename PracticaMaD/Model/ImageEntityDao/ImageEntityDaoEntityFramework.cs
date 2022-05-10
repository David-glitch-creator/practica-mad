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

        public List<ImageEntity> FindByAuthor(long userId, int startIndex,
            int count)
        {
            DbSet<ImageEntity> images = Context.Set<ImageEntity>();

            var result =
                (from i in images
                 where i.author == userId
                 orderby i.uploadDate descending
                 select i).Skip(startIndex).Take(count).ToList();

            return result;
        }

        public List<ImageEntity> FindByCategory(long categoryId, int startIndex, int count)
        {
            DbSet<ImageEntity> images = Context.Set<ImageEntity>();

            var result =
                (from i in images
                 where i.categoryId == categoryId
                 orderby i.uploadDate descending
                 select i).Skip(startIndex).Take(count).ToList();

            return result;
        }

        public List<ImageEntity> FindAll(int startIndex, int count)
        {
            DbSet<ImageEntity> images = Context.Set<ImageEntity>();

            var result =
                (from i in images
                 orderby i.uploadDate descending
                 select i).Skip(startIndex).Take(count).ToList();

            return result;
        }

        public List<ImageEntity> FindByKeywords(string keywords, int startIndex, int count)
        {
            DbSet<ImageEntity> images = Context.Set<ImageEntity>();

            var result =
                (from i in images
                 where i.title.Contains(keywords) || i.imageDescription.Contains(keywords)
                 orderby i.uploadDate descending
                 select i).Skip(startIndex).Take(count).ToList();

            return result;
        }

        public List<ImageEntity> FindByCategoryKeywords(long categoryId, string keywords, int startIndex, int count)
        {
            DbSet<ImageEntity> images = Context.Set<ImageEntity>();

            var result =
                (from i in images
                 where i.categoryId == categoryId && (i.title.Contains(keywords) || i.imageDescription.Contains(keywords))
                 orderby i.uploadDate descending
                 select i).Skip(startIndex).Take(count).ToList();

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
