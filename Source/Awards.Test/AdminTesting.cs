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
    /// Tests the functionalities of <see cref="AdminLogic"/> and its descendants.
    /// </summary>
    [TestFixture]
    public class AdminTesting
    {
        private AdminLogic adminLogic;

        private Mock<IBrandRepository> brandRepo;
        private Mock<ICountryRepository> countryRepo;
        private Mock<IExpertGroupRepository> egRepo;
        private Mock<IMemberRepository> memberRepo;
        private Mock<IProductRepository> productRepo;

        private IList<Brand> brandList;
        private IList<Product> productList;
        private IList<ExpertGroup> egList;
        private IList<Country> countryList;
        private IList<Member> memberList;

        /// <summary>
        /// The method to be called immediately before each <see cref="AdminTesting"/> test is run.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.brandRepo = new Mock<IBrandRepository>(MockBehavior.Loose);
            this.countryRepo = new Mock<ICountryRepository>(MockBehavior.Loose);
            this.egRepo = new Mock<IExpertGroupRepository>(MockBehavior.Loose);
            this.memberRepo = new Mock<IMemberRepository>(MockBehavior.Loose);
            this.productRepo = new Mock<IProductRepository>(MockBehavior.Loose);
            this.adminLogic = new AdminLogic(this.brandRepo.Object, this.countryRepo.Object, this.egRepo.Object, this.memberRepo.Object, this.productRepo.Object);
            this.brandList = MockRepoGenerator.GenerateBrandList();
            this.countryList = MockRepoGenerator.GenerateCountryList();
            this.egList = MockRepoGenerator.GenerateExpertGroupList();
            this.memberList = MockRepoGenerator.GenerateMemberList();
            this.productList = MockRepoGenerator.GenerateProductList();
        }

        /// <summary>
        /// Testing for the <see cref="AdminLogic.InsertEG(string)"/>.
        /// </summary>
        /// <param name="name">The name of the expert group to insert.</param>
        [TestCase("Developers")]
        public void TestInsertExpertGroup(string name)
        {
            ExpertGroup insertedEg = null;
            int oldLastEgId = this.egList[^1].ExpertGroupID;
            this.egRepo.Setup(repo => repo.Insert(It.IsAny<ExpertGroup>()))
                .Callback<ExpertGroup>(item =>
                {
                    item.ExpertGroupID = oldLastEgId + 1;
                    this.egList.Add(item);
                    insertedEg = item;
                });

            this.adminLogic.InsertEG(name);

            Assert.That(insertedEg, Is.EqualTo(this.egList[^1]));
            this.egRepo.Verify(repo => repo.Insert(It.IsAny<ExpertGroup>()), Times.Once);
            this.egRepo.Verify(repo => repo.Remove(It.IsAny<ExpertGroup>()), Times.Never);
            this.egRepo.Verify(repo => repo.Update(It.IsAny<ExpertGroup>()), Times.Never);
        }

        /// <summary>
        /// Testing for the <see cref="AdminLogic.RemoveProduct(int)"/>.
        /// </summary>
        [Test]
        public void TestRemoveProduct()
        {
            this.productRepo.Setup(repo => repo.GetAll()).Returns(this.productList.AsQueryable);
            IEnumerable<Product> remaining = Enumerable.Empty<Product>();
            this.productRepo.Setup(repo => repo.Remove(It.IsAny<int>())).Callback<int>(ogArg => remaining = this.productList.Where(prod => prod.ProductID != ogArg));
            Product prodToRemove = this.productList[1];

            this.adminLogic.RemoveProduct(prodToRemove.ProductID);

            Assert.That(remaining, Does.Not.Contain(prodToRemove));
            this.productRepo.Verify(repo => repo.Remove(prodToRemove.ProductID), Times.Once);
            this.productRepo.Verify(repo => repo.Remove(It.IsAny<int>()), Times.Once);
            this.productRepo.Verify(repo => repo.GetAll(), Times.Never);
        }

        /// <summary>
        /// Testing for the <see cref="AdminLogic.ChangeEGName(int, string)"/>.
        /// </summary>
        /// <param name="id">The ID of the expert group.</param>
        /// <param name="name">The new name for the expert group.</param>
        [TestCase(1, "THE VERY FIRST EXPERT GROUP")]
        public void TestChangeEGName(int id, string name)
        {
            ExpertGroup oldEG = this.egList[id - 1];
            ExpertGroup newEG = new();
            this.egRepo.Setup(repo => repo.ChangeName(It.IsAny<int>(), It.IsAny<string>())).Callback<int, string>((id, name) => newEG = new ExpertGroup { ExpertGroupID = id, Name = name });

            this.adminLogic.ChangeEGName(oldEG.ExpertGroupID, name);

            Assert.That(newEG.ExpertGroupID, Is.EqualTo(oldEG.ExpertGroupID));
            Assert.That(newEG.Name, Is.EqualTo(name));
            this.egRepo.Verify(repo => repo.ChangeName(oldEG.ExpertGroupID, name), Times.Once);
            this.egRepo.Verify(repo => repo.ChangeName(It.IsAny<int>(), It.IsAny<string>()), Times.Once);
        }

        /// <summary>
        /// (NON-CRUD) Testing for <see cref="AdminLogic.ListBrandsAndMembersAtSameAdress"/>.
        /// </summary>
        [Test]
        public void TestListBrandsAndMembersAtSameAddress()
        {
            this.brandRepo.Setup(repo => repo.GetAll()).Returns(this.brandList.AsQueryable);
            this.countryRepo.Setup(repo => repo.GetAll()).Returns(this.countryList.AsQueryable);
            this.memberRepo.Setup(repo => repo.GetAll()).Returns(this.memberList.AsQueryable);

            IEnumerable<MemberBrand> actualResult = this.adminLogic.ListBrandsAndMembersAtSameAdress();
            MemberBrand[] expectedResult = new MemberBrand[]
            {
                new() { Brand = this.brandList[3], Member = this.memberList[4] },
                new() { Brand = this.brandList[4], Member = this.memberList[4] },
                new() { Brand = this.brandList[8], Member = this.memberList[13] },
                new() { Brand = this.brandList[9], Member = this.memberList[13] },
                new() { Brand = this.brandList[10], Member = this.memberList[13] },
            };

            Assert.That(actualResult, Is.EquivalentTo(expectedResult));
            this.brandRepo.Verify(repo => repo.GetAll(), Times.Once);
            this.countryRepo.Verify(repo => repo.GetAll(), Times.Once);
            this.memberRepo.Verify(repo => repo.GetAll(), Times.Once);
        }
    }
}
