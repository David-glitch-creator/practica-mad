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

        void DeleteImage(long imageId);

        List<ImageEntity> SearchImages(String keywords);

        List<ImageEntity> SearchImages(string keywords, long categoryId);

        List<ImageEntity> SearchImages(long categoryId);

        long LikeImage(long userId, long imageId);

        long DislikeImage(long userId, long imageId);

        int GetNumberOfLikes(long imageId);
    }
}