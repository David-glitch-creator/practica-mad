using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace Es.Udc.DotNet.PracticaMaD.Test
{
    /// <summary>
    /// Descripción resumida de UserProfileDaoTest
    /// </summary>
    [TestClass]
    public class UserProfileDaoTest
    {
        private static IKernel kernel;
        private static IUserProfileDao userProfileDao;
        private static UserProfile userProfile;

        // Variables used in several tests are initialized here
        private const String loginName = "loginNameTest";

        private const String clearPassword = "password";
        private const String firstName = "name";
        private const String lastName = "lastName";
        private const String email = "user@udc.es";
        private const String language = "es";
        private const String country = "ES";
        private const long NON_EXISTENT_USER_ID = -1;

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
        }

        // Use TestCleanup para ejecutar el código una vez ejecutadas todas las pruebas
        [TestCleanup()]
        public void MyTestCleanup()
        {
            transaction.Dispose();
        }

        #endregion

        /// <summary>
        ///A test for FindByLoginName
        ///</summary>
        [TestMethod()]
        public void DAO_FindByLoginNameTest()
        {
            try
            {
                UserProfile actual = userProfileDao.FindByLoginName(userProfile.loginName);

                Assert.AreEqual(userProfile, actual, "User found does not correspond with the original one.");
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        /// <summary>
        ///A test for FindByEmail
        ///</summary>
        [TestMethod()]
        public void DAO_FindByEmailTest()
        {
            try
            {
                UserProfile actual = userProfileDao.FindByEmail(userProfile.email);

                Assert.AreEqual(userProfile, actual, "User found does not correspond with the original one.");
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
    }
}
