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

                var image = imageEntityDao.Find(imageId);

                // Check data
                Assert.AreEqual(imageId, image.imageId);
                Assert.AreEqual(title, image.title);
                Assert.AreEqual(imageDescription, image.imageDescription);
                Assert.IsTrue(Math.Abs((uploadDate - image.uploadDate).TotalSeconds) < 1);
                Assert.AreEqual(aperture, image.aperture);
                Assert.AreEqual(exposureTime, image.exposureTime);
                Assert.AreEqual(iso, image.iso);
                Assert.AreEqual(whiteBalance, image.whiteBalance);
                Assert.AreEqual(userId, image.author);

                byte[] expected = File.ReadAllBytes(@imageFile);
                for (int i = 0; i < expected.Length; i++)
                {
                    Assert.AreEqual(expected[i], image.imageFile[i]);
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

                var image = imageEntityDao.Find(imageId);

                imageService.DeleteImage(imageId);

                Assert.IsNull(imageEntityDao.Find(imageId));

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

                List<ImageEntity> actual = imageService.SearchImages("foto");

                Assert.IsTrue(actual.Count == 2);

                // Check data
                Assert.AreEqual(imageId, actual[0].imageId);
                Assert.AreEqual(title, actual[0].title);
                Assert.AreEqual(imageDescription, actual[0].imageDescription);
                Assert.IsTrue(Math.Abs((uploadDate - actual[0].uploadDate).TotalSeconds) < 1);
                Assert.AreEqual(aperture, actual[0].aperture);
                Assert.AreEqual(exposureTime, actual[0].exposureTime);
                Assert.AreEqual(iso, actual[0].iso);
                Assert.AreEqual(whiteBalance, actual[0].whiteBalance);
                Assert.AreEqual(userId, actual[0].author);

                byte[] expectedImage = File.ReadAllBytes(@imageFile);
                for (int i = 0; i < expectedImage.Length; i++)
                {
                    Assert.AreEqual(expectedImage[i], actual[0].imageFile[i]);
                }

                // Check data
                Assert.AreEqual(imageId2, actual[1].imageId);
                Assert.AreEqual(title2, actual[1].title);
                Assert.AreEqual(imageDescription, actual[1].imageDescription);
                Assert.IsTrue(Math.Abs((uploadDate2 - actual[1].uploadDate).TotalSeconds) < 1);
                Assert.AreEqual(aperture, actual[1].aperture);
                Assert.AreEqual(exposureTime, actual[1].exposureTime);
                Assert.AreEqual(iso, actual[1].iso);
                Assert.AreEqual(whiteBalance, actual[1].whiteBalance);
                Assert.AreEqual(userId, actual[1].author);

                for (int i = 0; i < expectedImage.Length; i++)
                {
                    Assert.AreEqual(expectedImage[i], actual[1].imageFile[i]);
                }

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

                List<ImageEntity> actual = imageService.SearchImages("foto", category.categoryId);

                Assert.IsTrue(actual.Count == 1);

                // Check data
                Assert.AreEqual(imageId, actual[0].imageId);
                Assert.AreEqual(title, actual[0].title);
                Assert.AreEqual(imageDescription, actual[0].imageDescription);
                Assert.IsTrue(Math.Abs((uploadDate - actual[0].uploadDate).TotalSeconds) < 1);
                Assert.AreEqual(aperture, actual[0].aperture);
                Assert.AreEqual(exposureTime, actual[0].exposureTime);
                Assert.AreEqual(iso, actual[0].iso);
                Assert.AreEqual(whiteBalance, actual[0].whiteBalance);
                Assert.AreEqual(userId, actual[0].author);

                byte[] expectedImage = File.ReadAllBytes(@imageFile);
                for (int i = 0; i < expectedImage.Length; i++)
                {
                    Assert.AreEqual(expectedImage[i], actual[0].imageFile[i]);
                }

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

                List<ImageEntity> actual = imageService.SearchImages(category2.categoryId);

                Assert.IsTrue(actual.Count == 1);

                // Check data
                Assert.AreEqual(imageId2, actual[0].imageId);
                Assert.AreEqual(title2, actual[0].title);
                Assert.AreEqual(imageDescription, actual[0].imageDescription);
                Assert.IsTrue(Math.Abs((uploadDate - actual[0].uploadDate).TotalSeconds) < 1);
                Assert.AreEqual(aperture, actual[0].aperture);
                Assert.AreEqual(exposureTime, actual[0].exposureTime);
                Assert.AreEqual(iso, actual[0].iso);
                Assert.AreEqual(whiteBalance, actual[0].whiteBalance);
                Assert.AreEqual(userId, actual[0].author);

                byte[] expectedImage = File.ReadAllBytes(@imageFile);
                for (int i = 0; i < expectedImage.Length; i++)
                {
                    Assert.AreEqual(expectedImage[i], actual[0].imageFile[i]);
                }

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