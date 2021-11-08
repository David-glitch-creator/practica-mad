using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;
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
    public class ImageEntityDaoTest
    {
        private static IKernel kernel;
        private static IUserProfileDao userProfileDao;
        private static ICategoryDao categoryDao;
        private static IImageEntityDao imageEntityDao;
        private static UserProfile userProfile;
        private static Category category;
        private static ImageEntity imageEntity;

        // Variables used in several tests are initialized here
        private const String loginName = "loginNameTest";
        private const String clearPassword = "password";
        private const String firstName = "name";
        private const String lastName = "lastName";
        private const String email = "user@udc.es";
        private const String language = "es";
        private const String country = "ES";
        private const long NON_EXISTENT_USER_ID = -1;

        private const String categoryName = "Categoria1";

        private const String title = "foto";
        private const String imageDescription = "foto que saqué en Pontevedra";
        private DateTime uploadDate = DateTime.Now;
        private const String aperture = "2.00";
        private const String exposureTime = "1/659 s";
        private const String iso = "ISO 100/21";
        private const String whiteBalance = "4";
        private Byte[] imageFile = File.ReadAllBytes(@"..\\..\\..\\foto.jpg");

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
    }

        // Use TestCleanup para ejecutar el código una vez ejecutadas todas las pruebas
        [TestCleanup()]
        public void MyTestCleanup()
        {
            transaction.Dispose();
        }

        #endregion

        [TestMethod()]
        public void DAO_FindByAuthorTest()
        {
            ImageEntity actual = imageEntityDao.FindByAuthor(userProfile.userId)[0];

            Assert.AreEqual(imageEntity, actual, "Expected image not equal to actual image");
        }

        [TestMethod()]
        public void DAO_FindByCategoryTest()
        {
            ImageEntity actual = imageEntityDao.FindByCategory(category.categoryId)[0];

            Assert.AreEqual(imageEntity, actual, "Expected image not equal to actual image");
        }

        [TestMethod()]
        public void DAO_FindAllTest()
        {
            ImageEntity actual = imageEntityDao.FindAll()[0];

            Assert.AreEqual(imageEntity, actual, "Expected image not equal to actual image");
        }

        [TestMethod()]
        public void DAO_FindByKeywordsTest()
        {
            ImageEntity actual = imageEntityDao.FindByKeywords(imageEntity.title)[0];

            Assert.AreEqual(imageEntity, actual, "Expected image not equal to actual image");
        }

        [TestMethod()]
        public void DAO_FindByCategoryKeywordsTest()
        {
            ImageEntity actual = imageEntityDao.FindByCategoryKeywords(category.categoryId, imageEntity.title)[0];

            Assert.AreEqual(imageEntity, actual, "Expected image not equal to actual image");
        }
    }
}
