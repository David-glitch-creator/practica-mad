using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserService;
using Es.Udc.DotNet.PracticaMaD.Model.UserService.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.UserService.Util;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System;
using System.Transactions;
using Es.Udc.DotNet.PracticaMaD.Test;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;
using Es.Udc.DotNet.PracticaMaD.Model.ImageEntityDao;
using Es.Udc.DotNet.PracticaMaD.Model;
using System.IO;
using Es.Udc.DotNet.PracticaMaD.Model.ImageService;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Test
{
    [TestClass]
    public class ImageServiceTest
    {
        private static IKernel kernel;
        private static IUserProfileDao userProfileDao;
        private static ICategoryDao categoryDao;
        private static IImageEntityDao imageEntityDao;
        private static Category category;
        private static Category category2;
        private static IUserService userService;
        private static IImageService imageService;

        // Variables used in several tests are initialized here
        private const string loginName = "loginNameTest";
        private const string clearPassword = "password";
        private const string firstName = "name";
        private const string lastName = "lastName";
        private const string email = "user@udc.es";
        private const string language = "es";
        private const string country = "ES";
        private const long NON_EXISTENT_USER_ID = -1;

        private const string loginName2 = "loginNameTest2";
        private const string email2 = "user2@udc.es";

        private const String categoryName = "Categoria1";
        private const String category2Name = "Categoria2";

        private const String title = "foto";
        private const String title2 = "photo";
        private const String imageDescription = "foto que saqué en Pontevedra";
        private DateTime uploadDate = DateTime.Now;
        private const String aperture = "2.00";
        private const String exposureTime = "1/659 s";
        private const String iso = "ISO 100/21";
        private const String whiteBalance = "4";
        private const String imageFile = "..\\..\\..\\foto.jpg";

        private TransactionScope transaction;

        public TestContext TestContext { get; set; }

        [TestMethod]
        public void UploadImageTest()
        {
            using (var scope = new TransactionScope())
            {
                category = new Category();
                category.categoryName = categoryName;
                categoryDao.Create(category);

                var userId =
                    userService.RegisterUser(loginName, clearPassword,
                        new UserProfileDetails(firstName, lastName, email, language, country));

                uploadDate = DateTime.Now;

                var imageId = imageService.UploadImage(userId, title, imageDescription,
                    new ExifDetails(aperture, exposureTime, iso, whiteBalance), 
                    category.categoryId, imageFile);

                var image = imageService.GetImageById(imageId);

                // Check data
                Assert.AreEqual(imageId, image.ImageId);
                Assert.AreEqual(title, image.Title);
                Assert.AreEqual(imageDescription, image.ImageDescription);
                Assert.IsTrue(Math.Abs((uploadDate - image.UploadDate).TotalSeconds) < 1);
                Assert.AreEqual(aperture, image.Aperture);
                Assert.AreEqual(exposureTime, image.ExposureTime);
                Assert.AreEqual(iso, image.Iso);
                Assert.AreEqual(whiteBalance, image.WhiteBalance);
                Assert.AreEqual(userId, image.AuthorId);
                Assert.AreEqual(loginName, image.AuthorLogin);

                byte[] expected = File.ReadAllBytes(@imageFile);
                for (int i = 0; i < expected.Length; i++)
                {
                    Assert.AreEqual(expected[i], image.ImageFile[i]);
                }

            }
        }

        [TestMethod]
        [ExpectedException(typeof(InstanceNotFoundException))]
        public void DeleteImageTest()
        {
            using (var scope = new TransactionScope())
            {
                category = new Category();
                category.categoryName = categoryName;
                categoryDao.Create(category);

                var userId =
                    userService.RegisterUser(loginName, clearPassword,
                        new UserProfileDetails(firstName, lastName, email, language, country));

                var imageId = imageService.UploadImage(userId, title, imageDescription,
                    new ExifDetails(aperture, exposureTime, iso, whiteBalance),
                    category.categoryId, imageFile);

                var image = imageService.GetImageById(imageId);

                imageService.DeleteImage(imageId);

                imageService.GetImageById(imageId);

            }
        }

        [TestMethod]
        public void GetAllImagesTest()
        {
            using (var scope = new TransactionScope())
            {
                category = new Category();
                category.categoryName = categoryName;
                categoryDao.Create(category);

                category2 = new Category();
                category2.categoryName = category2Name;
                categoryDao.Create(category2);

                var userId =
                    userService.RegisterUser(loginName, clearPassword,
                        new UserProfileDetails(firstName, lastName, email, language, country));

                var user2Id =
                    userService.RegisterUser(loginName2, clearPassword,
                        new UserProfileDetails(firstName, lastName, email2, language, country));

                var imageId = imageService.UploadImage(userId, title, imageDescription,
                    new ExifDetails(aperture, exposureTime, iso, whiteBalance),
                    category.categoryId, imageFile);

                var imageId2 = imageService.UploadImage(user2Id, title2, imageDescription,
                    new ExifDetails(aperture, exposureTime, iso, whiteBalance),
                    category.categoryId, imageFile);

                var imageId3 = imageService.UploadImage(userId, title, imageDescription,
                    new ExifDetails(aperture, exposureTime, iso, whiteBalance),
                    category.categoryId, imageFile);

                var imageId4 = imageService.UploadImage(user2Id, title2, imageDescription,
                    new ExifDetails(aperture, exposureTime, iso, whiteBalance),
                    category.categoryId, imageFile);

                ImageBlock actual = imageService.GetAllImages(0, 2);
                Assert.IsTrue(actual.Images.Count == 2);
                Assert.IsTrue(actual.ExistMoreImages);

                actual = imageService.GetAllImages(2, 3);
                Assert.IsTrue(actual.Images.Count == 2);
                Assert.IsFalse(actual.ExistMoreImages);
            }
        }

        [TestMethod]
        public void GetImagesByUserTest()
        {
            using (var scope = new TransactionScope())
            {
                category = new Category();
                category.categoryName = categoryName;
                categoryDao.Create(category);

                var userId =
                    userService.RegisterUser(loginName, clearPassword,
                        new UserProfileDetails(firstName, lastName, email, language, country));

                var user2Id =
                    userService.RegisterUser(loginName2, clearPassword,
                        new UserProfileDetails(firstName, lastName, email2, language, country));

                uploadDate = DateTime.Now;

                var imageId = imageService.UploadImage(userId, title, imageDescription,
                    new ExifDetails(aperture, exposureTime, iso, whiteBalance),
                    category.categoryId, imageFile);

                var imageId2 = imageService.UploadImage(user2Id, title2, imageDescription,
                    new ExifDetails(aperture, exposureTime, iso, whiteBalance),
                    category.categoryId, imageFile);

                ImageBlock actual = imageService.GetImagesByUser(userId, 0, 9);
                List<ImageDto> images = actual.Images;

                Assert.IsTrue(images.Count == 1);

                // Check data
                Assert.AreEqual(imageId, images[0].ImageId);
                Assert.AreEqual(title, images[0].Title);
                Assert.AreEqual(imageDescription, images[0].ImageDescription);
                Assert.IsTrue(Math.Abs((uploadDate - images[0].UploadDate).TotalSeconds) < 1);
                Assert.AreEqual(aperture, images[0].Aperture);
                Assert.AreEqual(exposureTime, images[0].ExposureTime);
                Assert.AreEqual(iso, images[0].Iso);
                Assert.AreEqual(whiteBalance, images[0].WhiteBalance);
                Assert.AreEqual(userId, images[0].AuthorId);
                Assert.AreEqual(loginName, images[0].AuthorLogin);

                byte[] expectedImage = File.ReadAllBytes(@imageFile);
                for (int i = 0; i < expectedImage.Length; i++)
                {
                    Assert.AreEqual(expectedImage[i], images[0].ImageFile[i]);
                }

                Assert.IsFalse(actual.ExistMoreImages);

            }
        }

        [TestMethod]
        public void SearchImagesByKeywordsTest()
        {
            using (var scope = new TransactionScope())
            {
                category = new Category();
                category.categoryName = categoryName;
                categoryDao.Create(category);

                var userId =
                    userService.RegisterUser(loginName, clearPassword,
                        new UserProfileDetails(firstName, lastName, email, language, country));

                uploadDate = DateTime.Now;

                var imageId = imageService.UploadImage(userId, title, imageDescription,
                    new ExifDetails(aperture, exposureTime, iso, whiteBalance),
                    category.categoryId, imageFile);

                DateTime uploadDate2 = DateTime.Now;

                var imageId2 = imageService.UploadImage(userId, title2, imageDescription,
                    new ExifDetails(aperture, exposureTime, iso, whiteBalance),
                    category.categoryId, imageFile);

                ImageBlock actual = imageService.SearchImages("foto", 0, 9);
                List<ImageDto> images = actual.Images;

                Assert.IsTrue(images.Count == 2);

                // Check data
                Assert.AreEqual(imageId2, images[0].ImageId);
                Assert.AreEqual(title2, images[0].Title);
                Assert.AreEqual(imageDescription, images[0].ImageDescription);
                Assert.IsTrue(Math.Abs((uploadDate - images[0].UploadDate).TotalSeconds) < 1);
                Assert.AreEqual(aperture, images[0].Aperture);
                Assert.AreEqual(exposureTime, images[0].ExposureTime);
                Assert.AreEqual(iso, images[0].Iso);
                Assert.AreEqual(whiteBalance, images[0].WhiteBalance);
                Assert.AreEqual(userId, images[0].AuthorId);
                Assert.AreEqual(loginName, images[0].AuthorLogin);

                byte[] expectedImage = File.ReadAllBytes(@imageFile);
                for (int i = 0; i < expectedImage.Length; i++)
                {
                    Assert.AreEqual(expectedImage[i], images[0].ImageFile[i]);
                }

                // Check data
                Assert.AreEqual(imageId, images[1].ImageId);
                Assert.AreEqual(title, images[1].Title);
                Assert.AreEqual(imageDescription, images[1].ImageDescription);
                Assert.IsTrue(Math.Abs((uploadDate2 - images[1].UploadDate).TotalSeconds) < 1);
                Assert.AreEqual(aperture, images[1].Aperture);
                Assert.AreEqual(exposureTime, images[1].ExposureTime);
                Assert.AreEqual(iso, images[1].Iso);
                Assert.AreEqual(whiteBalance, images[1].WhiteBalance);
                Assert.AreEqual(userId, images[1].AuthorId);
                Assert.AreEqual(loginName, images[1].AuthorLogin);

                for (int i = 0; i < expectedImage.Length; i++)
                {
                    Assert.AreEqual(expectedImage[i], images[1].ImageFile[i]);
                }

                Assert.IsFalse(actual.ExistMoreImages);

            }
        }

        [TestMethod]
        public void SearchImagesByKeywordsAndCategoryTest()
        {
            using (var scope = new TransactionScope())
            {
                category = new Category();
                category.categoryName = categoryName;
                categoryDao.Create(category);

                category2 = new Category();
                category2.categoryName = category2Name;
                categoryDao.Create(category2);

                var userId =
                    userService.RegisterUser(loginName, clearPassword,
                        new UserProfileDetails(firstName, lastName, email, language, country));

                uploadDate = DateTime.Now;

                var imageId = imageService.UploadImage(userId, title, imageDescription,
                    new ExifDetails(aperture, exposureTime, iso, whiteBalance),
                    category.categoryId, imageFile);

                var imageId2 = imageService.UploadImage(userId, title2, imageDescription,
                    new ExifDetails(aperture, exposureTime, iso, whiteBalance),
                    category2.categoryId, imageFile);

                ImageBlock actualBlock = imageService.SearchImages("foto", category.categoryId, 0, 9);
                List<ImageDto> images = actualBlock.Images;

                Assert.IsTrue(images.Count == 1);

                // Check data
                Assert.AreEqual(imageId, images[0].ImageId);
                Assert.AreEqual(title, images[0].Title);
                Assert.AreEqual(imageDescription, images[0].ImageDescription);
                Assert.IsTrue(Math.Abs((uploadDate - images[0].UploadDate).TotalSeconds) < 1);
                Assert.AreEqual(aperture, images[0].Aperture);
                Assert.AreEqual(exposureTime, images[0].ExposureTime);
                Assert.AreEqual(iso, images[0].Iso);
                Assert.AreEqual(whiteBalance, images[0].WhiteBalance);
                Assert.AreEqual(userId, images[0].AuthorId);
                Assert.AreEqual(loginName, images[0].AuthorLogin);

                byte[] expectedImage = File.ReadAllBytes(@imageFile);
                for (int i = 0; i < expectedImage.Length; i++)
                {
                    Assert.AreEqual(expectedImage[i], images[0].ImageFile[i]);
                }

                Assert.IsFalse(actualBlock.ExistMoreImages);

            }
        }

        [TestMethod]
        public void SearchImagesByCategoryTest()
        {
            using (var scope = new TransactionScope())
            {
                category = new Category();
                category.categoryName = categoryName;
                categoryDao.Create(category);

                category2 = new Category();
                category2.categoryName = category2Name;
                categoryDao.Create(category2);

                var userId =
                    userService.RegisterUser(loginName, clearPassword,
                        new UserProfileDetails(firstName, lastName, email, language, country));

                var imageId = imageService.UploadImage(userId, title, imageDescription,
                    new ExifDetails(aperture, exposureTime, iso, whiteBalance),
                    category.categoryId, imageFile);

                uploadDate = DateTime.Now;

                var imageId2 = imageService.UploadImage(userId, title2, imageDescription,
                    new ExifDetails(aperture, exposureTime, iso, whiteBalance),
                    category2.categoryId, imageFile);

                ImageBlock actualBlock = imageService.SearchImages(category2.categoryId, 0, 9);
                List<ImageDto> images = actualBlock.Images;

                Assert.IsTrue(images.Count == 1);

                // Check data
                Assert.AreEqual(imageId2, images[0].ImageId);
                Assert.AreEqual(title2, images[0].Title);
                Assert.AreEqual(imageDescription, images[0].ImageDescription);
                Assert.IsTrue(Math.Abs((uploadDate - images[0].UploadDate).TotalSeconds) < 1);
                Assert.AreEqual(aperture, images[0].Aperture);
                Assert.AreEqual(exposureTime, images[0].ExposureTime);
                Assert.AreEqual(iso, images[0].Iso);
                Assert.AreEqual(whiteBalance, images[0].WhiteBalance);
                Assert.AreEqual(userId, images[0].AuthorId);
                Assert.AreEqual(loginName, images[0].AuthorLogin);

                byte[] expectedImage = File.ReadAllBytes(@imageFile);
                for (int i = 0; i < expectedImage.Length; i++)
                {
                    Assert.AreEqual(expectedImage[i], images[0].ImageFile[i]);
                }

                Assert.IsFalse(actualBlock.ExistMoreImages);

            }
        }

        [TestMethod]
        public void LikeImage()
        {
            using (var scope = new TransactionScope())
            {
                category = new Category();
                category.categoryName = categoryName;
                categoryDao.Create(category);

                var userId = userService.RegisterUser(loginName, clearPassword,
                    new UserProfileDetails(firstName, lastName, email, language, country));

                var imageId = imageService.UploadImage(userId, title, imageDescription,
                    new ExifDetails(aperture, exposureTime, iso, whiteBalance),
                    category.categoryId, imageFile);

                Assert.IsTrue(imageService.GetNumberOfLikes(imageId) == 0);

                imageService.LikeImage(userId, imageId);

                Assert.IsTrue(imageService.GetNumberOfLikes(imageId) == 1);

                imageService.DislikeImage(userId, imageId);

                Assert.IsTrue(imageService.GetNumberOfLikes(imageId) == 0);
            }
        }

        #region Additional test attributes

        //Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize]
        public static void MyClassInitialize(TestContext testContext)
        {
            kernel = TestManager.ConfigureNInjectKernel();

            userProfileDao = kernel.Get<IUserProfileDao>();
            categoryDao = kernel.Get<ICategoryDao>();
            imageEntityDao = kernel.Get<IImageEntityDao>();
            userService = kernel.Get<IUserService>();
            imageService = kernel.Get<IImageService>();
        }

        //Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup]
        public static void MyClassCleanup()
        {
            TestManager.ClearNInjectKernel(kernel);
        }

        //Use TestInitialize to run code before running each test
        [TestInitialize]
        public void MyTestInitialize()
        {
        }

        //Use TestCleanup to run code after each test has run
        [TestCleanup]
        public void MyTestCleanup()
        {
        }

        #endregion Additional test attributes
    }
}