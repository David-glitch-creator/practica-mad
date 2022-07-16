using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;
using Es.Udc.DotNet.PracticaMaD.Model.ImageEntityDao;
using Es.Udc.DotNet.PracticaMaD.Model.TagDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using Ninject;
using System;
using System.Collections.Generic;
using System.IO;

namespace Es.Udc.DotNet.PracticaMaD.Model.ImageService
{
    public class ImageService : IImageService
    {
        [Inject]
        public IUserProfileDao UserProfileDao { private get; set ; }
        [Inject]
        public ICategoryDao CategoryDao { private get; set ; }
        [Inject]
        public IImageEntityDao ImageEntityDao { private get; set ; }
        [Inject]
        public ITagDao TagDao { private get; set ; }
        
        public long UploadImage(long userProfileId, string title, string imageDescription,
            ExifDetails details, long categoryId, string imageFile)
        {
            ImageEntity image = new ImageEntity();

            image.title = title;
            image.imageDescription = imageDescription;
            image.uploadDate = DateTime.Now;
            image.aperture = details.Aperture;
            image.exposureTime = details.ExposureTime;
            image.iso = details.Iso;
            image.whiteBalance = details.WhiteBalance;
            image.author = userProfileId;
            image.categoryId = categoryId;
            image.imageFile = File.ReadAllBytes(@imageFile);

            ImageEntityDao.Create(image);

            return image.imageId;
        }

        public long UploadImage(long userProfileId, string title, string imageDescription,
            ExifDetails details, long categoryId, byte[] imageFile)
        {
            ImageEntity image = new ImageEntity();

            image.title = title;
            image.imageDescription = imageDescription;
            image.uploadDate = DateTime.Now;
            image.aperture = details.Aperture;
            image.exposureTime = details.ExposureTime;
            image.iso = details.Iso;
            image.whiteBalance = details.WhiteBalance;
            image.author = userProfileId;
            image.categoryId = categoryId;
            image.imageFile = imageFile;

            ImageEntityDao.Create(image);

            return image.imageId;
        }

        /// <exception cref="InstanceNotFoundException"/>
        public void DeleteImage(long imageId)
        {
            ImageEntityDao.Remove(imageId);
        }

        public ImageBlock GetAllImages(int startIndex, int count)
        {
            List<ImageEntity> images = ImageEntityDao.FindAll(startIndex, count + 1);

            bool existMoreImages = (images.Count == count + 1);

            if (existMoreImages)
                images.RemoveAt(count);

            List<ImageDto> imageDtos = new List<ImageDto>();

            foreach (ImageEntity image in images)
            {
                String loginName = UserProfileDao.Find(image.author).loginName;

                imageDtos.Add(new ImageDto(image.imageId, image.title, image.uploadDate,
                    image.aperture, image.exposureTime, image.iso, image.whiteBalance,
                    image.author, loginName, image.categoryId, image.imageFile, image.imageDescription));
            }

            return new ImageBlock(imageDtos, existMoreImages);
        }

        public ImageBlock GetImagesByUser(long userId, int startIndex, int count)
        {
            List<ImageEntity> images = ImageEntityDao.FindByAuthor(userId, startIndex, count + 1);

            bool existMoreImages = (images.Count == count + 1);

            if (existMoreImages)
                images.RemoveAt(count);

            List<ImageDto> imageDtos = new List<ImageDto>();

            foreach(ImageEntity image in images)
            {
                String loginName = UserProfileDao.Find(image.author).loginName;

                imageDtos.Add(new ImageDto(image.imageId, image.title, image.uploadDate, 
                    image.aperture, image.exposureTime, image.iso, image.whiteBalance, 
                    image.author, loginName, image.categoryId, image.imageFile, image.imageDescription));
            }

            return new ImageBlock(imageDtos, existMoreImages);
        }

        public ImageBlock SearchImages(string keywords, int startIndex, int count)
        {
            List<ImageEntity> images = ImageEntityDao.FindByKeywords(keywords, startIndex, count + 1);

            bool existMoreImages = (images.Count == count + 1);

            if (existMoreImages)
                images.RemoveAt(count);

            List<ImageDto> imageDtos = new List<ImageDto>();

            foreach (ImageEntity image in images)
            {
                String loginName = UserProfileDao.Find(image.author).loginName;

                imageDtos.Add(new ImageDto(image.imageId, image.title, image.uploadDate,
                    image.aperture, image.exposureTime, image.iso, image.whiteBalance,
                    image.author, loginName, image.categoryId, image.imageFile, image.imageDescription));
            }

            return new ImageBlock(imageDtos, existMoreImages);
        }

        public ImageBlock SearchImages(string keywords, long categoryId, int startIndex, int count)
        {
            List<ImageEntity> images = ImageEntityDao.FindByCategoryKeywords(categoryId, keywords, startIndex, count + 1);

            bool existMoreImages = (images.Count == count + 1);

            if (existMoreImages)
                images.RemoveAt(count);

            List<ImageDto> imageDtos = new List<ImageDto>();

            foreach (ImageEntity image in images)
            {
                String loginName = UserProfileDao.Find(image.author).loginName;

                imageDtos.Add(new ImageDto(image.imageId, image.title, image.uploadDate,
                    image.aperture, image.exposureTime, image.iso, image.whiteBalance,
                    image.author, loginName, image.categoryId, image.imageFile, image.imageDescription));
            }

            return new ImageBlock(imageDtos, existMoreImages);
        }

        public ImageBlock SearchImages(long categoryId, int startIndex, int count)
        {
            List<ImageEntity> images = ImageEntityDao.FindByCategory(categoryId, startIndex, count + 1);

            bool existMoreImages = (images.Count == count + 1);

            if (existMoreImages)
                images.RemoveAt(count);

            List<ImageDto> imageDtos = new List<ImageDto>();

            foreach (ImageEntity image in images)
            {
                String loginName = UserProfileDao.Find(image.author).loginName;

                imageDtos.Add(new ImageDto(image.imageId, image.title, image.uploadDate,
                    image.aperture, image.exposureTime, image.iso, image.whiteBalance,
                    image.author, loginName, image.categoryId, image.imageFile, image.imageDescription));
            }

            return new ImageBlock(imageDtos, existMoreImages);
        }

        /// <exception cref="InstanceNotFoundException"/>
        public long LikeImage(long userId, long imageId)
        {
            UserProfile userProfile = UserProfileDao.Find(userId);
            ImageEntity image = ImageEntityDao.Find(imageId);

            return ImageEntityDao.Like(userProfile, image);
        }

        /// <exception cref="InstanceNotFoundException"/>
        public long DislikeImage(long userId, long imageId)
        {
            UserProfile userProfile = UserProfileDao.Find(userId);
            ImageEntity image = ImageEntityDao.Find(imageId);

            return ImageEntityDao.Dislike(userProfile, image);
        }

        /// <exception cref="InstanceNotFoundException"/>
        public int GetNumberOfLikes(long imageId)
        {
            ImageEntity image = ImageEntityDao.Find(imageId);

            return ImageEntityDao.GetNumberOfLikes(image);
        }

        /// <exception cref="InstanceNotFoundException"/>
        public ImageDto GetImageById(long imageId)
        {
            ImageEntity image = ImageEntityDao.Find(imageId);
            String loginName = UserProfileDao.Find(image.author).loginName;

            return new ImageDto(image.imageId, image.title, image.uploadDate,
                    image.aperture, image.exposureTime, image.iso, image.whiteBalance,
                    image.author, loginName, image.categoryId, image.imageFile, image.imageDescription);
        }

        /// <exception cref="InstanceNotFoundException"/>
        public bool DoesLike(long userId, long imageId)
        {
            UserProfile userProfile = UserProfileDao.Find(userId);
            ImageEntity image = ImageEntityDao.Find(imageId);

            return ImageEntityDao.DoesLike(userProfile, image);
        }

        /// <exception cref="InstanceNotFoundException"/>
        public ImageBlock GetImagesByTag(long tagId, int startIndex, int count)
        {
            Tag tag = TagDao.Find(tagId);

            List<ImageEntity> images = ImageEntityDao.GetImagesWithTag(tag, startIndex, count + 1);

            bool existMoreImages = (images.Count == count + 1);

            if (existMoreImages)
                images.RemoveAt(count);

            List<ImageDto> imageDtos = new List<ImageDto>();

            foreach (ImageEntity image in images)
            {
                String loginName = UserProfileDao.Find(image.author).loginName;

                imageDtos.Add(new ImageDto(image.imageId, image.title, image.uploadDate,
                    image.aperture, image.exposureTime, image.iso, image.whiteBalance,
                    image.author, loginName, image.categoryId, image.imageFile, image.imageDescription));
            }

            return new ImageBlock(imageDtos, existMoreImages);
        }
    }
}