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

        private const String title = "foto";
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

                var imageId = imageService.UploadImage(userId, title, imageDescription,
                    new ExifDetails(aperture, exposureTime, iso, whiteBalance), 
                    category.categoryId, imageFile);

                var image = imageEntityDao.Find(imageId);

                // Check data
                Assert.AreEqual(imageId, image.imageId);
                Assert.AreEqual(uploadDate, image.uploadDate);
                Assert.AreEqual(aperture, image.aperture);
                Assert.AreEqual(exposureTime, image.exposureTime);
                Assert.AreEqual(iso, image.iso);
                Assert.AreEqual(whiteBalance, image.whiteBalance);
                Assert.AreEqual(File.ReadAllBytes(@imageFile), image.imageFile);

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

                var imageId = imageService.UploadImage(userId, title, imageDescription,
                    new ExifDetails(aperture, exposureTime, iso, whiteBalance),
                    category.categoryId, imageFile);

                var image = imageEntityDao.Find(imageId);

                var imageId2 = imageService.UploadImage(userId, "photo", imageDescription,
                    new ExifDetails(aperture, exposureTime, iso, whiteBalance),
                    category.categoryId, imageFile);

                var image2 = imageEntityDao.Find(imageId);

                List<ImageEntity> expected = new List<ImageEntity>();
                expected.Add(image);
                expected.Add(image2);

                List<ImageEntity> actual = imageService.SearchImages("foto");

                for(int i = 0; i<expected.Count; i++)
                {
                    Assert.AreEqual(expected[i], actual[i]);
                }

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