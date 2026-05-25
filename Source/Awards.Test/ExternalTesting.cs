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
    /// Tests the funcionalities of <see cref="ExternalLogic"/> and its descendents.
    /// </summary>
    [TestFixture]
    public class ExternalTesting
    {
        /// <summary>
        /// Testing for the ExternalLogic.ListAllEntities method.
        /// </summary>
        [Test]
        public void TestGetAll()
        {
            Mock<IBrandRepository> mockedBrandRepo = new Mock<IBrandRepository>(MockBehavior.Loose);
            Mock<IProductRepository> mockedProductRepo = new Mock<IProductRepository>(MockBehavior.Loose);
            Mock<IExpertGroupRepository> mockedEGrepo = new Mock<IExpertGroupRepository>(MockBehavior.Loose);
            List<Brand> brands = MockRepoGenerator.GenerateBrandList();
            List<Product> products = MockRepoGenerator.GenerateProductList();
            List<ExpertGroup> expertGroups = MockRepoGenerator.GenerateExpertGroupList();
            mockedBrandRepo.Setup(repo => repo.GetAll()).Returns(brands.AsQueryable());
            mockedEGrepo.Setup(repo => repo.GetAll()).Returns(expertGroups.AsQueryable());
            mockedProductRepo.Setup(repo => repo.GetAll()).Returns(products.AsQueryable());
            ExternalAuditLogic externalAuditLogic = new ExternalAuditLogic(2, mockedBrandRepo.Object, mockedProductRepo.Object, mockedEGrepo.Object);
            List<ExpertGroup> sampleEGs = expertGroups;
            Product firstProduct = products[0];

            IEnumerable<Brand> allBrands = externalAuditLogic.ListAllBrands(out _);
            IEnumerable<ExpertGroup> allEGs = externalAuditLogic.ListAllExpertgroups(out _);
            IEnumerable<Product> allProducts = externalAuditLogic.ListAllProducts(out _);

            Assert.That(allBrands.Count(), Is.EqualTo(brands.Count));
            Assert.That(expertGroups, Is.EquivalentTo(sampleEGs));
            Assert.That(products, Does.Contain(firstProduct));
            mockedBrandRepo.Verify(repo => repo.GetAll(), Times.Once);
            mockedEGrepo.Verify(repo => repo.GetAll(), Times.Once);
            mockedProductRepo.Verify(repo => repo.GetAll(), Times.Once);
            mockedBrandRepo.Verify(repo => repo.GetOne(It.IsAny<int>()), Times.Never);
            mockedEGrepo.Verify(repo => repo.GetOne(It.IsAny<int>()), Times.Never);
            mockedProductRepo.Verify(repo => repo.GetOne(It.IsAny<int>()), Times.Never);
        }

        /// <summary>
        /// Testing for the ExternalLogic.GetOneEntity method.
        /// </summary>
        [Test]
        public void TestGetOne()
        {
            Mock<IBrandRepository> mockedBrandRepo = new Mock<IBrandRepository>(MockBehavior.Loose);
            Mock<IProductRepository> mockedProductRepo = new Mock<IProductRepository>(MockBehavior.Loose);
            Mock<IExpertGroupRepository> mockedEGrepo = new Mock<IExpertGroupRepository>(MockBehavior.Loose);
            List<Brand> brands = MockRepoGenerator.GenerateBrandList();
            List<Product> products = MockRepoGenerator.GenerateProductList();
            List<ExpertGroup> expertGroups = MockRepoGenerator.GenerateExpertGroupList();
            Brand returnedBrand = new Brand();
            ExpertGroup returnedExpertGroup = new ExpertGroup();
            Product returnedProduct = new Product();
            mockedBrandRepo.Setup(repo => repo.GetOne(It.IsAny<int>())).Returns(returnedBrand);
            mockedEGrepo.Setup(repo => repo.GetOne(It.IsAny<int>())).Returns(returnedExpertGroup);
            mockedProductRepo.Setup(repo => repo.GetOne(It.IsAny<int>())).Returns(returnedProduct);
            ExternalAuditLogic externalAuditLogic = new ExternalAuditLogic(2, mockedBrandRepo.Object, mockedProductRepo.Object, mockedEGrepo.Object);
            List<ExpertGroup> sampleEGs = expertGroups;
            Product firstProduct = products[0];

            Brand resultBrand = externalAuditLogic.GetOneBrand(1);
            Product resultProduct = externalAuditLogic.GetOneProduct(2);

            mockedBrandRepo.Verify(repo => repo.GetOne(1), Times.Once);
            mockedBrandRepo.Verify(repo => repo.GetAll(), Times.Never);
            mockedProductRepo.Verify(repo => repo.GetOne(1), Times.Never);
            mockedProductRepo.Verify(repo => repo.GetOne(It.IsAny<int>()), Times.Once);
        }

        /// <summary>
        /// (NON-CRUD) Testing for <see cref="ExternalAuditLogic.ListTopBrands"/>.
        /// </summary>
        [Test]
        public void TestListTopBrands()
        {
            Mock<IBrandRepository> mockedBrandRepo = new Mock<IBrandRepository>(MockBehavior.Loose);
            Mock<IProductRepository> mockedProductRepo = new Mock<IProductRepository>(MockBehavior.Loose);
            Mock<IExpertGroupRepository> mockedEGrepo = new Mock<IExpertGroupRepository>(MockBehavior.Loose);
            List<Brand> brands = MockRepoGenerator.GenerateBrandList();
            List<Product> products = MockRepoGenerator.GenerateProductList();
            List<ExpertGroup> expertGroups = MockRepoGenerator.GenerateExpertGroupList();
            mockedBrandRepo.Setup(repo => repo.GetAll()).Returns(brands.AsQueryable());
            mockedEGrepo.Setup(repo => repo.GetAll()).Returns(expertGroups.AsQueryable());
            mockedProductRepo.Setup(repo => repo.GetAll()).Returns(products.AsQueryable());
            ExternalAuditLogic externalAuditLogic = new ExternalAuditLogic(2, mockedBrandRepo.Object, mockedProductRepo.Object, mockedEGrepo.Object);

            var results = externalAuditLogic.ListTopBrands();
            var expectedResults = new List<BrandAndNumber>
            {
                new BrandAndNumber
                {
                    Brand = new Brand { BrandId = 12, Name = "LG", CountryID = 9, Address = "Seoul", Homepage = "lg.com" },
                    Number = 3,
                },
                new BrandAndNumber
                {
                    Brand = new Brand { BrandId = 4, Name = "DxO Labs", CountryID = 5, Address = "Paris", Homepage = "dxo.fr" },
                    Number = 2,
                },
            };

            Assert.That(results, Is.EquivalentTo(expectedResults));
            mockedProductRepo.Verify(repo => repo.GetAll(), Times.Exactly(2));
            mockedBrandRepo.Verify(repo => repo.GetAll(), Times.Once);
            mockedEGrepo.Verify(repo => repo.GetAll(), Times.Never);
        }

        /// <summary>
        /// (NON-CRUD) Testing for <see cref="ExternalAuditLogic.GetMaxPriceProdInEveryEG"/>.
        /// </summary>
        [Test]
        public void TestGetMaxPriceProdInEveryEG()
        {
            Mock<IBrandRepository> mockedBrandRepo = new Mock<IBrandRepository>(MockBehavior.Loose);
            Mock<IProductRepository> mockedProductRepo = new Mock<IProductRepository>(MockBehavior.Loose);
            Mock<IExpertGroupRepository> mockedEGrepo = new Mock<IExpertGroupRepository>(MockBehavior.Loose);
            List<Brand> brands = MockRepoGenerator.GenerateBrandList();
            List<Product> products = MockRepoGenerator.GenerateProductList();
            List<ExpertGroup> expertGroups = MockRepoGenerator.GenerateExpertGroupList();
            mockedBrandRepo.Setup(repo => repo.GetAll()).Returns(brands.AsQueryable());
            mockedEGrepo.Setup(repo => repo.GetAll()).Returns(expertGroups.AsQueryable());
            mockedProductRepo.Setup(repo => repo.GetAll()).Returns(products.AsQueryable());
            ExternalAuditLogic externalAuditLogic = new ExternalAuditLogic(1, mockedBrandRepo.Object, mockedProductRepo.Object, mockedEGrepo.Object);

            var result = externalAuditLogic.GetMaxPriceProdInEveryEG();
            var expectedResult = new List<ExpertgroupProduct>
            {
                new ExpertgroupProduct
                {
                    ExpertGroup = new ExpertGroup { ExpertGroupID = 1, Name = "Hi-Fi" },
                    Product = new Product { ProductID = 3, BrandId = 3, Name = "Epikon", Category = "BEST DALI FLOORSTANDER", ExpertGroupID = 1, Price = 1999 },
                },
                new ExpertgroupProduct
                {
                    ExpertGroup = new ExpertGroup { ExpertGroupID = 2, Name = "Home Theatre Audio" },
                    Product = new Product { ProductID = 1, BrandId = 2, Name = "Beosound Stage", Category = "BEST PREMIUM SOUNDBAR", ExpertGroupID = 2, Price = 2000 },
                },
                new ExpertgroupProduct
                {
                    ExpertGroup = new ExpertGroup { ExpertGroupID = 3, Name = "Home Theatre Display & Video" },
                    Product = new Product { ProductID = 8, BrandId = 12, Name = "OLED65GX", Category = "BEST PREMIUM OLED TV", ExpertGroupID = 3, Price = 29999 },
                },
                new ExpertgroupProduct
                {
                    ExpertGroup = new ExpertGroup { ExpertGroupID = 6, Name = "Photography" },
                    Product = new Product { ProductID = 2, BrandId = 9, Name = "EOS-1D X Mark III", Category = "BEST PROFESSIONAL CAMERA", ExpertGroupID = 6, Price = 9183 },
                },
            };

            Assert.That(result, Is.EquivalentTo(expectedResult));
            mockedProductRepo.Verify(repo => repo.GetAll(), Times.Exactly(2));
            mockedEGrepo.Verify(repo => repo.GetAll(), Times.Once);
            mockedBrandRepo.Verify(repo => repo.GetAll(), Times.Never);
        }
    }
}
