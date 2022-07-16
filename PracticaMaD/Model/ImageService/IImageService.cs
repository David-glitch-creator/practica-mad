using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Ninject;
using System;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.UserService.Util;
using Es.Udc.DotNet.PracticaMaD.Model.UserService.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.ImageEntityDao;
using System.Collections.Generic;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;

namespace Es.Udc.DotNet.PracticaMaD.Model.ImageService
{
    public interface IImageService
    {
        [Inject]
        IUserProfileDao UserProfileDao { set; }

        [Inject]
        ICategoryDao CategoryDao { set; }

        [Inject]
        IImageEntityDao ImageEntityDao { set; }

        long UploadImage(long userProfileId, string title, string imageDescription,
            ExifDetails details, long categoryId, string imageFile);

        long UploadImage(long userProfileId, string title, string imageDescription,
            ExifDetails details, long categoryId, byte[] imageFile);

        /// <exception cref="InstanceNotFoundException"/>
        void DeleteImage(long imageId);

        /// <exception cref="InstanceNotFoundException"/>
        ImageDto GetImageById(long imageId);

        ImageBlock GetAllImages(int startIndex, int count);

        ImageBlock GetImagesByUser(long userId, int startIndex, int count);

        ImageBlock SearchImages(String keywords, int startIndex, int count);

        ImageBlock SearchImages(string keywords, long categoryId, int startIndex, int count);

        ImageBlock SearchImages(long categoryId, int startIndex, int count);

        /// <exception cref="InstanceNotFoundException"/>
        long LikeImage(long userId, long imageId);

        /// <exception cref="InstanceNotFoundException"/>
        long DislikeImage(long userId, long imageId);

        /// <exception cref="InstanceNotFoundException"/>
        int GetNumberOfLikes(long imageId);

        /// <exception cref="InstanceNotFoundException"/>
        bool DoesLike(long userId, long imageId);

        /// <exception cref="InstanceNotFoundException"/>
        ImageBlock GetImagesByTag(long tagId, int startIndex, int count);
    }
}