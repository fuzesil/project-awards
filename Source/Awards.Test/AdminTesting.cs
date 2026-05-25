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
    /// Tests the funcionalities of <see cref="AdminLogic"/> and its descendents.
    /// </summary>
    [TestFixture]
    public class AdminTesting
    {
        /// <summary>
        /// Testing for the <see cref="AdminLogic.InsertEG(string)"/>.
        /// </summary>
        [Test]
        public void TestInsertExpertGroup()
        {
            Mock<IExpertGroupRepository> mockedEGrepo = new Mock<IExpertGroupRepository>(MockBehavior.Loose);
            List<ExpertGroup> expertGroups = MockRepoGenerator.GenerateExpertGroupList();
            AdminLogic testAL = new AdminLogic(brands: null, countries: null, mockedEGrepo.Object);
            ExpertGroup eg = new ExpertGroup();
            mockedEGrepo.Setup(repo => repo.Insert(It.IsAny<ExpertGroup>())).Callback<ExpertGroup>(item => eg = item);

            testAL.InsertEG("Developers");

            mockedEGrepo.Verify(repo => repo.Insert(It.IsAny<ExpertGroup>()), Times.Once);
            mockedEGrepo.Verify(repo => repo.Remove(It.IsAny<ExpertGroup>()), Times.Never);
        }

        /// <summary>
        /// Testing for the <see cref="AdminLogic.RemoveProduct(int)"/>.
        /// </summary>
        [Test]
        public void TestRemoveProduct()
        {
            Mock<IProductRepository> mProductRepo = new Mock<IProductRepository>(MockBehavior.Loose);
            List<Product> productList = MockRepoGenerator.GenerateProductList();
            AdminLogic testAL = new AdminLogic(brands: null, countries: null, expertgroups: null, members: null, mProductRepo.Object);
            mProductRepo.Setup(repo => repo.GetAll()).Returns(productList.AsQueryable);
            mProductRepo.Setup(repo => repo.Remove(It.IsAny<int>()));

            Product prodToRemove = productList[0];
            testAL.RemoveProduct(1);

            mProductRepo.Verify(repo => repo.Remove(prodToRemove.ProductID), Times.Once);
            mProductRepo.Verify(repo => repo.Remove(It.IsAny<int>()), Times.Once);
            mProductRepo.Verify(repo => repo.Insert(It.IsAny<Product>()), Times.Never);
        }

        /// <summary>
        /// Testing for the <see cref="AdminLogic.ChangeEGName(int, string)"/>.
        /// </summary>
        [Test]
        public void TestChangeEGName()
        {
            Mock<IExpertGroupRepository> mockedEGrepo = new Mock<IExpertGroupRepository>(MockBehavior.Loose);
            List<ExpertGroup> expertGroups = MockRepoGenerator.GenerateExpertGroupList();
            AdminLogic testAL = new AdminLogic(brands: null, countries: null, mockedEGrepo.Object);
            ExpertGroup eg = new ExpertGroup();
            mockedEGrepo.Setup(repo => repo.ChangeName(It.IsAny<int>(), It.IsAny<string>())).Callback<int, string>(
                (id, name) =>
                {
                    eg.ExpertGroupID = id;
                    eg.Name = name;
                });

            int idChange = 1;
            string newname = "Something else";
            testAL.ChangeEGName(idChange, newname);

            mockedEGrepo.Verify(repo => repo.ChangeName(It.IsAny<int>(), It.IsAny<string>()), Times.Once);
        }

        /*
        /// <summary>
        /// [NOT WORKING] (NON-CRUD) Testing for <see cref="AdminLogic.ListBrandsAndMembersAtSameAdress"/>.
        /// </summary>
        [Test]
        public void TestListBrandsAndMembersAtSameAdress()
        {
            Mock<IBrandRepository> mckBrandRepo = new Mock<IBrandRepository>(MockBehavior.Loose);
            Mock<ICountryRepository> mckCountryRepo = new Mock<ICountryRepository>(MockBehavior.Loose);
            Mock<IMemberRepository> mckMemberRepo = new Mock<IMemberRepository>(MockBehavior.Loose);
            List<Brand> brandList = MockRepoGenerator.GenerateBrandList();
            List<Country> countryList = MockRepoGenerator.GenerateCountryList();
            List<Member> memberList = MockRepoGenerator.GenerateMemberList();
            mckBrandRepo.Setup(repo => repo.GetAll()).Returns(brandList.AsQueryable);
            mckCountryRepo.Setup(repo => repo.GetAll()).Returns(countryList.AsQueryable);
            mckMemberRepo.Setup(repo => repo.GetAll()).Returns(memberList.AsQueryable);
            AdminLogic adminLogic = new AdminLogic(mckBrandRepo.Object, mckCountryRepo.Object, null, mckMemberRepo.Object, null);

            var result = adminLogic.ListBrandsAndMembersAtSameAdress();
            var expectedResult = ...

            Assert.That(result, Is.EquivalentTo(expectedResult));

            mckBrandRepo.Verify(repo => repo.GetAll(), Times.Once);
            mckCountryRepo.Verify(repo => repo.GetAll(), Times.Once);
            mckMemberRepo.Verify(repo => repo.GetAll(), Times.Once);
        }
        */
    }
}
