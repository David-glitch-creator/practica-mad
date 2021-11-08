using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;
using Es.Udc.DotNet.PracticaMaD.Model.ImageEntityDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using Ninject;
using System;
using System.Collections.Generic;
using System.IO;

namespace Es.Udc.DotNet.PracticaMaD.Model.ImageService
{
    public class ImageService : IImageService
    {

        public IUserProfileDao UserProfileDao { private get; set ; }
        public ICategoryDao CategoryDao { private get; set ; }
        public IImageEntityDao ImageEntityDao { private get; set ; }
        
        public long UploadImage(long userProfileId, string title,
            ExifDetails details, long categoryId, string imageFile)
        {
            ImageEntity image = new ImageEntity();

            image.title = title;
            image.uploadDate = DateTime.Now;
            image.aperture = details.Aperture;
            image.exposureTime = details.ExposureTime;
            image.iso = details.Iso;
            image.whiteBalance = details.WhiteBalance;
            image.author = userProfileId;
            image.categoryId = categoryId;
            image.imageFile = File.ReadAllBytes(imageFile);

            ImageEntityDao.Create(image);

            return image.imageId;
        }
        public void DeleteImage(long imageId)
        {
            ImageEntityDao.Remove(imageId);
        }

        public List<ImageEntity> SearchImages(string keywords)
        {
            return ImageEntityDao.FindByKeywords(keywords);
        }

        public List<ImageEntity> SearchImages(string keywords, long categoryId)
        {
            return ImageEntityDao.FindByCategoryKeywords(categoryId, keywords);
        }

        public List<ImageEntity> SearchImages(long categoryId)
        {
            return ImageEntityDao.FindByCategory(categoryId);
        }

        public long LikeImage(long userId, long imageId)
        {
            throw new System.NotImplementedException();
        }

        public long DislikeImage(long userId, long imageId)
        {
            throw new System.NotImplementedException();
        }

        public int GetNumberOfLikes(long imageId)
        {
            throw new System.NotImplementedException();
        }
    }
}