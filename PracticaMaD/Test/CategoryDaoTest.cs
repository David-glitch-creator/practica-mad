using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.CategoryDao;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace Es.Udc.DotNet.PracticaMaD.Test
{
    /// <summary>
    /// Descripción resumida de CategoryDaoTest
    /// </summary>
    [TestClass]
    public class CategoryDaoTest
    {
        private static IKernel kernel;
        private static ICategoryDao categoryDao;
        private static Category category;
        private static Category category2;

        // Variables used in several tests are initialized here
        private const String categoryName = "categoryTest";

        private const String categoryName2 = "categoryTest2";

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
            categoryDao = kernel.Get<ICategoryDao>();
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

            category = new Category();
            category.categoryName = categoryName;

            categoryDao.Create(category);


            category2 = new Category();
            category2.categoryName = categoryName2;
            categoryDao.Create(category2);
        }

        // Use TestCleanup para ejecutar el código una vez ejecutadas todas las pruebas
        [TestCleanup()]
        public void MyTestCleanup()
        {
            transaction.Dispose();
        }

        #endregion

        [TestMethod]
        public void DAO_FindByNameTest()
        {
            try
            {
                Category actual = categoryDao.FindByName(category.categoryName);

                Assert.AreEqual(category, actual, "Category found does not correspond with the original one.");
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void DAO_FindAllTest()
        {
            try
            {
                List<Category> expected = new List<Category>();
                expected.Add(category);
                expected.Add(category2);

                List<Category> actual = categoryDao.FindAll();

                for(int i = 0; i<expected.Count; i++)
                {
                    Assert.AreEqual(expected[i], actual[i], "Category found does not correspond with the original one.");
                }
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
    }
}
