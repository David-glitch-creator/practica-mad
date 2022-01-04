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
using Es.Udc.DotNet.PracticaMaD.Model.CommentService;
using Es.Udc.DotNet.PracticaMaD.Model.CommentDao;

namespace Es.Udc.DotNet.PracticaMaD.Test
{
    [TestClass]
    public class CommentServiceTest
    {
        private static IKernel kernel;
        private static IUserProfileDao userProfileDao;
        private static ICategoryDao categoryDao;
        private static IImageEntityDao imageEntityDao;
        private static ICommentDao commentDao;
        private static Category category;
        private static Category category2;
        private static IUserService userService;
        private static IImageService imageService;
        private static ICommentService commentService;

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

        private const String commentText = "Bonita foto";
        private const String newCommentText = "Muy bonita foto";

        private TransactionScope transaction;

        public TestContext TestContext { get; set; }

        [TestMethod]
        public void CommentImageAndUpdate()
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

                Assert.IsFalse(commentService.HasComments(imageId));

                var expectedDate = DateTime.Now;

                var commentId = commentService.CommentImage(userId, imageId, commentText);
                
                Assert.IsTrue(commentService.HasComments(imageId));

                Assert.IsTrue(commentService.GetCommentsByImage(imageId).Count == 1);
                Assert.IsTrue(commentService.GetCommentsByAuthor(userId).Count == 1);

                Comment actual1 = commentService.GetCommentsByImage(imageId)[0];
                Comment actual2 = commentService.GetCommentsByAuthor(userId)[0];
                Assert.AreEqual(actual1, actual2);

                // Check data
                Assert.AreEqual(commentId, actual1.commentId);
                Assert.AreEqual(userId, actual1.author);
                Assert.AreEqual(imageId, actual1.imageId);
                Assert.AreEqual(commentText, actual1.commentText);
                Assert.IsTrue(Math.Abs((expectedDate - actual1.postedDate).TotalSeconds) < 1);

                commentService.UpdateComment(commentId, newCommentText);

                // Check data
                Assert.AreEqual(commentId, actual1.commentId);
                Assert.AreEqual(userId, actual1.author);
                Assert.AreEqual(imageId, actual1.imageId);
                Assert.AreEqual(newCommentText, actual1.commentText);
                Assert.IsTrue(Math.Abs((expectedDate - actual1.postedDate).TotalSeconds) < 1);

            }
        }

        [TestMethod]
        public void DeleteComment()
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

                Assert.IsFalse(commentService.HasComments(imageId));

                var expectedDate = DateTime.Now;

                var commentId = commentService.CommentImage(userId, imageId, commentText);

                Assert.IsTrue(commentService.HasComments(imageId));

                Assert.IsTrue(commentService.GetCommentsByImage(imageId).Count == 1);
                Assert.IsTrue(commentService.GetCommentsByAuthor(userId).Count == 1);

                Comment actual1 = commentService.GetCommentsByImage(imageId)[0];
                Comment actual2 = commentService.GetCommentsByAuthor(userId)[0];
                Assert.AreEqual(actual1, actual2);

                // Check data
                Assert.AreEqual(commentId, actual1.commentId);
                Assert.AreEqual(userId, actual1.author);
                Assert.AreEqual(imageId, actual1.imageId);
                Assert.AreEqual(commentText, actual1.commentText);
                Assert.IsTrue(Math.Abs((expectedDate - actual1.postedDate).TotalSeconds) < 1);

                commentService.DeleteComment(commentId);

                Assert.IsFalse(commentService.HasComments(imageId));

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
            commentDao = kernel.Get<ICommentDao>();
            userService = kernel.Get<IUserService>();
            imageService = kernel.Get<IImageService>();
            commentService = kernel.Get<ICommentService>();
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