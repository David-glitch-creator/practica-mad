using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;
using Es.Udc.DotNet.PracticaMaD.Model.CommentDao;
using Es.Udc.DotNet.PracticaMaD.Model.ImageEntityDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Transactions;

namespace Es.Udc.DotNet.PracticaMaD.Test
{
    /// <summary>
    /// Descripción resumida de UserProfileDaoTest
    /// </summary>
    [TestClass]
    public class CommentDaoTest
    {
        private static IKernel kernel;
        private static IUserProfileDao userProfileDao;
        private static ICategoryDao categoryDao;
        private static IImageEntityDao imageEntityDao;
        private static ICommentDao commentDao;
        private static UserProfile userProfile;
        private static UserProfile userProfile2;
        private static Category category;
        private static ImageEntity imageEntity;
        private static Comment comment;
        private static Comment comment2;

        // Variables used in several tests are initialized here
        private const String loginName = "loginNameTest";
        private const String clearPassword = "password";
        private const String firstName = "name";
        private const String lastName = "lastName";
        private const String email = "user@udc.es";
        private const String language = "es";
        private const String country = "ES";
        private const long NON_EXISTENT_USER_ID = -1;

        private const String loginName2 = "loginNameTest2";
        private const String clearPassword2 = "password2";
        private const String firstName2 = "name2";
        private const String lastName2 = "lastName2";
        private const String email2 = "user2@udc.es";
        private const String language2 = "es";
        private const String country2 = "ES";

        private const String categoryName = "Categoria1";

        private const String title = "foto";
        private const String imageDescription = "foto que saqué en Pontevedra";
        private DateTime uploadDate = DateTime.Now;
        private const String aperture = "2.00";
        private const String exposureTime = "1/659 s";
        private const String iso = "ISO 100/21";
        private const String whiteBalance = "4";
        private Byte[] imageFile = File.ReadAllBytes(@"..\\..\\..\\foto.jpg");

        private const String commentText = "Buena foto, bro";
        private DateTime postedDate1 = DateTime.Now.AddMinutes(20);
        private const String commentText2 = "Gracias, bro";
        private DateTime postedDate2 = DateTime.Now.AddMinutes(30);

        private TransactionScope transaction;

        private TestContext testContextInstance;

        /// <summary>
        ///Obtiene o establece el contexto de las pruebas que proporciona
        ///información y funcionalidad para la serie de pruebas actual.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Atributos de prueba adicionales
        
        // Use ClassInitialize para ejecutar el código antes de ejecutar la primera prueba en la clase
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext) 
        {
            kernel = TestManager.ConfigureNInjectKernel();
            userProfileDao = kernel.Get<IUserProfileDao>();
            categoryDao = kernel.Get<ICategoryDao>();
            imageEntityDao = kernel.Get<IImageEntityDao>();
            commentDao = kernel.Get<ICommentDao>();
        }

        // Use ClassCleanup para ejecutar el código una vez ejecutadas todas las pruebas en una clase
        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            TestManager.ClearNInjectKernel(kernel);
        }

        // Usar TestInitialize para ejecutar el código antes de ejecutar cada prueba 
        [TestInitialize()]
        public void MyTestInitialize()
        {
            transaction = new TransactionScope();

            userProfile = new UserProfile();
            userProfile.loginName = loginName;
            userProfile.enPassword = clearPassword;
            userProfile.firstName = firstName;
            userProfile.lastName = lastName;
            userProfile.email = email;
            userProfile.lang = language;
            userProfile.country = country;

            userProfileDao.Create(userProfile);


            userProfile2 = new UserProfile();
            userProfile2.loginName = loginName2;
            userProfile2.enPassword = clearPassword2;
            userProfile2.firstName = firstName2;
            userProfile2.lastName = lastName2;
            userProfile2.email = email2;
            userProfile2.lang = language2;
            userProfile2.country = country2;

            userProfileDao.Create(userProfile2);


            category = new Category();
            category.categoryName = categoryName;

            categoryDao.Create(category);


            imageEntity = new ImageEntity();
            imageEntity.title = title;
            imageEntity.imageDescription = imageDescription;
            imageEntity.uploadDate = uploadDate;
            imageEntity.aperture = aperture;
            imageEntity.exposureTime = exposureTime;
            imageEntity.iso = iso;
            imageEntity.whiteBalance = whiteBalance;
            imageEntity.imageFile = imageFile;
            imageEntity.author = userProfile.userId;
            imageEntity.categoryId = category.categoryId;

            imageEntityDao.Create(imageEntity);


            comment = new Comment();
            comment.author = userProfile.userId;
            comment.imageId = imageEntity.imageId;
            comment.commentText = commentText;
            comment.postedDate = postedDate1;

            commentDao.Create(comment);


            comment2 = new Comment();
            comment2.author = userProfile2.userId;
            comment2.imageId = imageEntity.imageId;
            comment2.commentText = commentText2;
            comment2.postedDate = postedDate2;

            commentDao.Create(comment2);


        }

        // Use TestCleanup para ejecutar el código una vez ejecutadas todas las pruebas
        [TestCleanup()]
        public void MyTestCleanup()
        {
            transaction.Dispose();
        }

        #endregion

        [TestMethod()]
        public void DAO_FindByImageTest()
        {
            try
            {
                List<Comment> expected = new List<Comment>();
                expected.Add(comment2);
                expected.Add(comment);

                List<Comment> actual = commentDao.FindByImage(imageEntity.imageId);

                for (int i = 0; i < expected.Count; i++)
                {
                    Assert.AreEqual(expected[i], actual[i], "Comment found does not correspond with the original one.");
                }
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod()]
        public void DAO_FindByAuthorTest()
        {
            Comment actual = commentDao.FindByAuthor(userProfile2.userId)[0];

            Assert.AreEqual(comment2, actual, "Comment found does not correspond with the original one.");
        }
    }
}
