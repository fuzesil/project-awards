namespace Awards.Test
{
    using System.Collections.Generic;
    using System.Linq;
    using Awards.Data;
    using Awards.Logic;
    using Awards.Repository;
    using Moq;
    using NUnit.Framework;

    /// <summary>
    /// Tests the functionalities of <see cref="ExternalLogic"/> and its descendants.
    /// </summary>
    [TestFixture]
    public class ExternalTesting
    {
        private ExternalAuditLogic externalLogic;

        private Mock<IBrandRepository> brandRepo;
        private Mock<IExpertGroupRepository> egRepo;
        private Mock<IProductRepository> productRepo;

        private IList<Brand> brandList;
        private IList<ExpertGroup> egList;
        private IList<Product> productList;

        /// <summary>
        /// The method to be called immediately before each <see cref="ExternalTesting"/> test is run.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.brandRepo = new Mock<IBrandRepository>(MockBehavior.Loose);
            this.productRepo = new Mock<IProductRepository>(MockBehavior.Loose);
            this.egRepo = new Mock<IExpertGroupRepository>(MockBehavior.Loose);
            this.externalLogic = new ExternalAuditLogic(2, this.brandRepo.Object, this.productRepo.Object, this.egRepo.Object);
            this.brandList = MockRepoGenerator.GenerateBrandList();
            this.productList = MockRepoGenerator.GenerateProductList();
            this.egList = MockRepoGenerator.GenerateExpertGroupList();
        }

        /// <summary>
        /// Testing for the ExternalLogic.ListAllEntities method.
        /// </summary>
        [Test]
        public void TestGetAll()
        {
            this.brandRepo.Setup(repo => repo.GetAll()).Returns(this.brandList.AsQueryable);
            this.egRepo.Setup(repo => repo.GetAll()).Returns(this.egList.AsQueryable);
            this.productRepo.Setup(repo => repo.GetAll()).Returns(this.productList.AsQueryable);
            Product firstProduct = this.productList[0];
            ExpertGroup lastEG = this.egList[^1];

            _ = this.externalLogic.ListAllBrands(out int countAllBrands);
            IEnumerable<ExpertGroup> allEGs = this.externalLogic.ListAllExpertgroups(out int countAllEGs);
            IEnumerable<Product> allProducts = this.externalLogic.ListAllProducts(out _);

            Assert.That(countAllBrands, Is.EqualTo(this.brandList.Count));
            Assert.That(this.egList, Is.EquivalentTo(allEGs));
            Assert.That(allProducts.ElementAt(0), Is.EqualTo(firstProduct));
            Assert.That(allEGs.ElementAt(countAllEGs - 1), Is.EqualTo(lastEG));
            this.brandRepo.Verify(repo => repo.GetAll(), Times.Once);
            this.egRepo.Verify(repo => repo.GetAll(), Times.Once);
            this.productRepo.Verify(repo => repo.GetAll(), Times.Once);
            this.brandRepo.Verify(repo => repo.GetOne(It.IsAny<int>()), Times.Never);
            this.egRepo.Verify(repo => repo.GetOne(It.IsAny<int>()), Times.Never);
            this.productRepo.Verify(repo => repo.GetOne(It.IsAny<int>()), Times.Never);
        }

        /// <summary>
        /// Testing the return value of <see cref="ExternalLogic.GetOneBrand(int, string)"/> method.
        /// </summary>
        /// <param name="id">Any positive integer.</param>
        /// <param name="name">Any of the names in the data set.</param>
        [TestCase(1, "NAD")]
        [TestCase(5, "Focal")]
        [TestCase(int.MaxValue / 2, "qwertzuiop")]
        [TestCase(int.MinValue, "Invalid")]
        [TestCase(-1, null)]
        [TestCase(0, "")]
        public void TestGetOneBrandReturnsValidOrNull(int id, string name)
        {
            this.brandRepo.Setup(repo => repo.GetOne(id))
                .Returns<int>(idArg => this.brandList.SingleOrDefault(b => b.BrandId == idArg));
            this.brandRepo.Setup(repo => repo.GetOne(name))
                .Returns<string>(nameArg => this.brandList.FirstOrDefault(b => b.Name == nameArg));

            Brand resultBrandById = this.externalLogic.GetOneBrand(id);
            Brand resultBrandByName = this.externalLogic.GetOneBrand(It.Is<int>(i => i < 1), name);

            Assert.AreEqual(resultBrandById, this.brandList.SingleOrDefault(b => b.BrandId == id));
            Assert.AreEqual(resultBrandByName, this.brandList.FirstOrDefault(b => b.Name == name));
            Assert.AreEqual(resultBrandById, resultBrandByName);
            this.brandRepo.Verify(repo => repo.GetOne(id), Times.AtMostOnce);
            this.brandRepo.Verify(repo => repo.GetOne(It.IsAny<int>()), Times.AtMostOnce);
            this.brandRepo.Verify(repo => repo.GetOne(name), Times.AtMostOnce);
            this.brandRepo.Verify(repo => repo.GetOne(It.IsAny<string>()), Times.AtMostOnce);
            this.brandRepo.Verify(repo => repo.GetAll(), Times.Never);
        }

        /// <summary>
        /// Testing the return value of <see cref="ExternalLogic.GetOneExpertGroup(int, string)"/> method.
        /// </summary>
        /// <param name="id">Any positive integer.</param>
        /// <param name="name">Any of the names in the data set.</param>
        [TestCase(1, "Hi-Fi")]
        [TestCase(3, "Home Theatre Display & Video")]
        [TestCase(int.MaxValue / 2, "qwertzuiop")]
        [TestCase(int.MinValue, "Invalid")]
        [TestCase(-1, null)]
        [TestCase(0, "")]
        public void TestGetOneExpertGroupReturnsValidOrNull(int id, string name)
        {
            this.egRepo.Setup(repo => repo.GetOne(id))
                .Returns<int>(idArg => this.egList.SingleOrDefault(eg => eg.ExpertGroupID == idArg));
            this.egRepo.Setup(repo => repo.GetOne(name))
                .Returns<string>(nameArg => this.egList.FirstOrDefault(eg => eg.Name == nameArg));

            ExpertGroup resultEGById = this.externalLogic.GetOneExpertGroup(id);
            ExpertGroup resultEGByName = this.externalLogic.GetOneExpertGroup(It.Is<int>(i => i < 1), name);

            Assert.AreEqual(resultEGById, this.egList.SingleOrDefault(eg => eg.ExpertGroupID == id));
            Assert.AreEqual(resultEGByName, this.egList.FirstOrDefault(eg => eg.Name == name));
            Assert.AreEqual(resultEGById, resultEGByName);
            this.egRepo.Verify(repo => repo.GetOne(id), Times.AtMostOnce);
            this.egRepo.Verify(repo => repo.GetOne(It.IsAny<int>()), Times.AtMostOnce);
            this.egRepo.Verify(repo => repo.GetOne(name), Times.AtMostOnce);
            this.egRepo.Verify(repo => repo.GetOne(It.IsAny<string>()), Times.AtMostOnce);
            this.egRepo.Verify(repo => repo.GetAll(), Times.Never);
        }

        /// <summary>
        /// Testing the return value of <see cref="ExternalLogic.GetOneProduct(int, string)"/> method.
        /// </summary>
        /// <param name="id">Any positive integer.</param>
        /// <param name="name">Any of the names in the data set.</param>
        [TestCase(2, "EOS-1D X Mark III")]
        [TestCase(6, "X100V")]
        [TestCase(int.MaxValue / 2, "qwertzuiop")]
        [TestCase(int.MinValue, "Invalid")]
        [TestCase(-1, null)]
        [TestCase(0, "")]
        public void TestGetOneProductReturnsValidOrNull(int id, string name)
        {
            this.productRepo.Setup(repo => repo.GetOne(id))
                .Returns<int>(idArg => this.productList.SingleOrDefault(p => p.ProductID == idArg));
            this.productRepo.Setup(repo => repo.GetOne(name))
                .Returns<string>(nameArg => this.productList.FirstOrDefault(p => p.Name == nameArg));

            Product resultProductById = this.externalLogic.GetOneProduct(id);
            Product resultProductByName = this.externalLogic.GetOneProduct(It.Is<int>(i => i < 1), name);

            Assert.AreEqual(resultProductById, this.productList.SingleOrDefault(p => p.ProductID == id));
            Assert.AreEqual(resultProductByName, this.productList.FirstOrDefault(p => p.Name == name));
            Assert.AreEqual(resultProductById, resultProductByName);
            this.productRepo.Verify(repo => repo.GetOne(id), Times.AtMostOnce);
            this.productRepo.Verify(repo => repo.GetOne(It.IsAny<int>()), Times.AtMostOnce);
            this.productRepo.Verify(repo => repo.GetOne(name), Times.AtMostOnce);
            this.productRepo.Verify(repo => repo.GetOne(It.IsAny<string>()), Times.AtMostOnce);
            this.productRepo.Verify(repo => repo.GetAll(), Times.Never);
        }

        /// <summary>
        /// (NON-CRUD) Testing for <see cref="ExternalAuditLogic.ListTopBrands"/>.
        /// </summary>
        [Test]
        public void TestListTopBrands()
        {
            this.brandRepo.Setup(repo => repo.GetAll()).Returns(this.brandList.AsQueryable);
            this.egRepo.Setup(repo => repo.GetAll()).Returns(this.egList.AsQueryable);
            this.productRepo.Setup(repo => repo.GetAll()).Returns(this.productList.AsQueryable);

            IEnumerable<BrandAndNumber> results = this.externalLogic.ListTopBrands();
            BrandAndNumber[] expectedResults = new BrandAndNumber[]
            {
                new() { Brand = this.brandList[11], Number = 3 },
                new() { Brand = this.brandList[3], Number = 2 },
            };

            Assert.That(results, Is.EquivalentTo(expectedResults));
            this.productRepo.Verify(repo => repo.GetAll(), Times.Exactly(2));
            this.brandRepo.Verify(repo => repo.GetAll(), Times.Once);
            this.egRepo.Verify(repo => repo.GetAll(), Times.Never);
        }

        /// <summary>
        /// (NON-CRUD) Testing for <see cref="ExternalAuditLogic.GetMaxPriceProdInEveryEG"/>.
        /// </summary>
        [Test]
        public void TestGetMaxPriceProdInEveryEG()
        {
            this.brandRepo.Setup(repo => repo.GetAll()).Returns(this.brandList.AsQueryable);
            this.egRepo.Setup(repo => repo.GetAll()).Returns(this.egList.AsQueryable);
            this.productRepo.Setup(repo => repo.GetAll()).Returns(this.productList.AsQueryable);

            IEnumerable<ExpertgroupProduct> result = this.externalLogic.GetMaxPriceProdInEveryEG();
            ExpertgroupProduct[] expectedResult = new ExpertgroupProduct[]
            {
                new() { ExpertGroup = this.egList[0], Product = this.productList[2] },
                new() { ExpertGroup = this.egList[1], Product = this.productList[0] },
                new() { ExpertGroup = this.egList[2], Product = this.productList[7] },
                new() { ExpertGroup = this.egList[3], Product = this.productList[1] },
            };

            Assert.That(result, Is.EquivalentTo(expectedResult));
            this.productRepo.Verify(repo => repo.GetAll(), Times.Exactly(2));
            this.egRepo.Verify(repo => repo.GetAll(), Times.Once);
            this.brandRepo.Verify(repo => repo.GetAll(), Times.Never);
        }
    }
}
