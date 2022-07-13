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
using Es.Udc.DotNet.PracticaMaD.Model.TagDao;
using Es.Udc.DotNet.PracticaMaD.Model.TagService;

namespace Es.Udc.DotNet.PracticaMaD.Test
{
    [TestClass]
    public class TagServiceTest
    {
        private static IKernel kernel;
        private static IUserProfileDao userProfileDao;
        private static ICategoryDao categoryDao;
        private static IImageEntityDao imageEntityDao;
        private static ITagDao tagDao;
        private static Category category;
        private static Category category2;
        private static IUserService userService;
        private static IImageService imageService;
        private static ITagService tagService;

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
        private const String aperture = "2.00";
        private const String exposureTime = "1/659 s";
        private const String iso = "ISO 100/21";
        private const String whiteBalance = "4";
        private const String imageFile = "..\\..\\..\\foto.jpg";

        private const String tagName = "etiqueta";

        private TransactionScope transaction;

        public TestContext TestContext { get; set; }

        [TestMethod]
        public void Add_RemoveTagTest()
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

                tagService.AddTagToImage(tagName, imageId);

                Assert.IsTrue(tagService.GetTagsFromImage(imageId).Count == 1);

                Tag actual = tagDao.FindByName(tagName);
                Tag expected = new Tag();
                expected.tagId = actual.tagId;
                expected.tagName = tagName;
                expected.taggedImagesNumber = 1;

                Assert.AreEqual(expected, actual);

                tagService.RemoveTagFromImage(tagName, imageId);
                expected.taggedImagesNumber = 0;

                Assert.IsTrue(tagService.GetTagsFromImage(imageId).Count == 0);

                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod]
        public void GetByPopularityTest()
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

                var image2Id = imageService.UploadImage(userId, title2, imageDescription,
                    new ExifDetails(aperture, exposureTime, iso, whiteBalance),
                    category.categoryId, imageFile);

                tagService.AddTagToImage(tagName, imageId);
                tagService.AddTagToImage("etiqueta2", imageId);
                tagService.AddTagToImage(tagName, image2Id);

                Assert.IsTrue(tagService.GetTagsFromImage(imageId).Count == 2);
                Assert.IsTrue(tagService.GetTagsFromImage(image2Id).Count == 1);

                List<Tag> actual = tagService.GetByPopularity();
                List<Tag> expected = new List<Tag>();

                Tag expected1 = new Tag();
                expected1.tagId = tagDao.FindByName(tagName).tagId;
                expected1.tagName = tagName;
                expected1.taggedImagesNumber = 2;

                Tag expected2 = new Tag();
                expected2.tagId = tagDao.FindByName("etiqueta2").tagId;
                expected2.tagName = "etiqueta2";
                expected2.taggedImagesNumber = 1;

                expected.Add(expected1);
                expected.Add(expected2);

                for (int i = 0; i < expected.Count; i++)
                {
                    Assert.AreEqual(expected[i], actual[i], "Tag found does not correspond with the original one.");
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
            tagDao = kernel.Get<ITagDao>();
            userService = kernel.Get<IUserService>();
            imageService = kernel.Get<IImageService>();
            tagService = kernel.Get<ITagService>();
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