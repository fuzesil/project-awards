namespace Awards.Test
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Metrics;
    using System.Linq;
    using Awards.Data;
    using Awards.Logic;
    using Awards.Repository;
    using Moq;
    using NUnit.Framework;

    /// <summary>
    /// Tests the funcionalities of <see cref="InternalLogic"/> and its descendents.
    /// </summary>
    [TestFixture]
    public class InternalTesting
    {
        private InternalAuditLogic internalLogic;

        private Mock<ICountryRepository> countryRepo;
        private Mock<IMemberRepository> memberRepo;
        private Mock<IExpertGroupRepository> egRepo;

        private IList<ExpertGroup> egList;
        private IList<Country> countryList;
        private IList<Member> memberList;

        /// <summary>
        /// The method to be called immediately before each <see cref="InternalTesting"/> test is run.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.countryRepo = new Mock<ICountryRepository>(MockBehavior.Loose);
            this.egRepo = new Mock<IExpertGroupRepository>(MockBehavior.Loose);
            this.memberRepo = new Mock<IMemberRepository>(MockBehavior.Loose);
            this.internalLogic = new InternalAuditLogic(2, this.memberRepo.Object, this.countryRepo.Object, this.egRepo.Object);
            this.countryList = MockRepoGenerator.GenerateCountryList();
            this.egList = MockRepoGenerator.GenerateExpertGroupList();
            this.memberList = MockRepoGenerator.GenerateMemberList();
        }

        /// <summary>
        /// Testing the return value of <see cref="InternalLogic.GetOneCountry(int, string)"/>.
        /// </summary>
        /// <param name="id">Any integer.</param>
        /// <param name="name">Any of the names in the data set.</param>
        [TestCase(1, "Australia")]
        [TestCase(7, "Hungary")]
        [TestCase(int.MaxValue / 2, "qwertzuiop")]
        [TestCase(int.MinValue, "Invalid")]
        [TestCase(-1, null)]
        [TestCase(0, "")]
        public void TestGetOneCountryReturnsValidOrNull(int id, string name)
        {
            this.countryRepo.Setup(repo => repo.GetOne(id))
                .Returns<int>(idArg => this.countryList.SingleOrDefault(c => c.CountryID == idArg));
            this.countryRepo.Setup(repo => repo.GetOne(name))
                .Returns<string>(nameArg => this.countryList.FirstOrDefault(c => c.Name == nameArg));

            Country expectedCountryById = this.internalLogic.GetOneCountry(id);
            Country expectedCountryByName = this.internalLogic.GetOneCountry(It.Is<int>(i => i < 1), name);

            Assert.AreEqual(expectedCountryById, this.countryList.SingleOrDefault(c => c.CountryID == id));
            Assert.AreEqual(expectedCountryByName, this.countryList.FirstOrDefault(c => c.Name == name));
            Assert.AreEqual(expectedCountryById, expectedCountryByName);
            this.countryRepo.Verify(repo => repo.GetOne(id), Times.AtMostOnce);
            this.countryRepo.Verify(repo => repo.GetOne(It.IsAny<int>()), Times.AtMostOnce);
            this.countryRepo.Verify(repo => repo.GetOne(name), Times.AtMostOnce);
            this.countryRepo.Verify(repo => repo.GetOne(It.IsAny<string>()), Times.AtMostOnce);
            this.countryRepo.Verify(repo => repo.GetAll(), Times.Never);
        }

        /// <summary>
        /// Testing the return value of <see cref="InternalLogic.GetOneExpertGroup(int, string)"/>.
        /// </summary>
        /// <param name="id">Any integer.</param>
        /// <param name="name">Any of the names in the data set.</param>
        [TestCase(1, "Hi-Fi")]
        [TestCase(3, "Home Theatre Display & Video")]
        [TestCase(int.MaxValue / 2, "qwertzuiop")]
        [TestCase(int.MinValue, "Invalid")]
        [TestCase(-1, null)]
        [TestCase(0, "")]
        public void TestGetOneExpertgroupReturnsValidOrNull(int id, string name)
        {
            this.egRepo.Setup(repo => repo.GetOne(id))
                .Returns<int>(idArg => this.egList.SingleOrDefault(eg => eg.ExpertGroupID == idArg));
            this.egRepo.Setup(repo => repo.GetOne(name))
                .Returns<string>(nameArg => this.egList.FirstOrDefault(eg => eg.Name == nameArg));

            ExpertGroup resultEGById = this.internalLogic.GetOneExpertGroup(id);
            ExpertGroup resultEGByName = this.internalLogic.GetOneExpertGroup(It.Is<int>(i => i < 1), name);

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
        /// Testing the return value of <see cref="InternalLogic.GetOneMember(int, string)"/> method.
        /// </summary>
        /// <param name="id">Any integer.</param>
        /// <param name="name">Any of the names in the data set.</param>
        [TestCase(5, "Les Années Laser")]
        [TestCase(9, "Sztereó Sound&Vision")]
        [TestCase(int.MaxValue / 2, "qwertzuiop")]
        [TestCase(int.MinValue, "Invalid")]
        [TestCase(-1, null)]
        [TestCase(0, "")]
        public void TestGetOneMemberReturnsValidOrNull(int id, string name)
        {
            this.memberRepo.Setup(repo => repo.GetOne(id))
                .Returns<int>(idArg => this.memberList.SingleOrDefault(m => m.MemberID == idArg));
            this.memberRepo.Setup(repo => repo.GetOne(name))
                .Returns<string>(nameArg => this.memberList.FirstOrDefault(m => m.Name == nameArg));

            Member expectedMemberById = this.internalLogic.GetOneMember(id);
            Member expectedMemberByName = this.internalLogic.GetOneMember(It.Is<int>(i => i < 1), name);

            Assert.AreEqual(expectedMemberById, this.memberList.SingleOrDefault(m => m.MemberID == id));
            Assert.AreEqual(expectedMemberByName, this.memberList.FirstOrDefault(m => m.Name == name));
            Assert.AreEqual(expectedMemberById, expectedMemberByName);
            this.memberRepo.Verify(repo => repo.GetOne(id), Times.AtMostOnce);
            this.memberRepo.Verify(repo => repo.GetOne(It.IsAny<int>()), Times.AtMostOnce);
            this.memberRepo.Verify(repo => repo.GetOne(name), Times.AtMostOnce);
            this.memberRepo.Verify(repo => repo.GetOne(It.IsAny<string>()), Times.AtMostOnce);
            this.memberRepo.Verify(repo => repo.GetAll(), Times.Never);
        }

        /// <summary>
        /// Testing for the <see cref="InternalLogic"/> ListAll() methods.
        /// </summary>
        [Test]
        public void TestGetAll()
        {
            this.countryRepo.Setup(repo => repo.GetAll()).Returns(this.countryList.AsQueryable);
            this.memberRepo.Setup(repo => repo.GetAll()).Returns(this.memberList.AsQueryable);
            this.egRepo.Setup(repo => repo.GetAll()).Returns(this.egList.AsQueryable);
            Member firstMember = this.memberList[0];
            Country lastCountry = this.countryList[^1];

            IEnumerable<Country> allCountries = this.internalLogic.ListAllCountries(out int countAllCountries);
            IEnumerable<ExpertGroup> allEGs = this.internalLogic.ListAllExpertgroups(out _);
            IEnumerable<Member> allMembers = this.internalLogic.ListAllMembers(out _);

            Assert.That(countAllCountries, Is.EqualTo(this.countryList.Count));
            Assert.That(allCountries.ElementAt(countAllCountries - 1), Is.EqualTo(lastCountry));
            Assert.That(allEGs, Is.EquivalentTo(this.egList));
            Assert.That(allMembers.ElementAt(0), Is.EqualTo(firstMember));
            this.countryRepo.Verify(repo => repo.GetAll(), Times.Once);
            this.egRepo.Verify(repo => repo.GetAll(), Times.Once);
            this.memberRepo.Verify(repo => repo.GetAll(), Times.Once);
            this.countryRepo.Verify(repo => repo.GetOne(It.IsAny<int>()), Times.Never);
            this.egRepo.Verify(repo => repo.GetOne(It.IsAny<int>()), Times.Never);
            this.memberRepo.Verify(repo => repo.GetOne(It.IsAny<int>()), Times.Never);
        }

        /// <summary>
        /// (NON-CRUD) Testing for <see cref="InternalAuditLogic.ListMembersInCapitalCity(bool)"/>.
        /// </summary>
        [Test]
        public void TestListMembersInCapitalCity()
        {
            this.countryRepo.Setup(repo => repo.GetAll()).Returns(this.countryList.AsQueryable);
            this.memberRepo.Setup(repo => repo.GetAll()).Returns(this.memberList.AsQueryable);

            IEnumerable<MemberCountry> membersInCaps = this.internalLogic.ListMembersInCapitalCity(true);
            MemberCountry[] expectedIn = new MemberCountry[]
            {
                new() { Member = this.memberList[2], Country = this.countryList[2] },
                new() { Member = this.memberList[3], Country = this.countryList[2] },
                new() { Member = this.memberList[4], Country = this.countryList[4] },
                new() { Member = this.memberList[8], Country = this.countryList[6] },
                new() { Member = this.memberList[9], Country = this.countryList[6] },
                new() { Member = this.memberList[10], Country = this.countryList[6] },
                new() { Member = this.memberList[11], Country = this.countryList[7] },
                new() { Member = this.memberList[12], Country = this.countryList[7] },
                new() { Member = this.memberList[13], Country = this.countryList[8] },
                new() { Member = this.memberList[14], Country = this.countryList[10] },
            };
            IEnumerable<MemberCountry> membersOutOfCaps = this.internalLogic.ListMembersInCapitalCity(false);
            MemberCountry[] expectedOut = new MemberCountry[]
            {
                new() { Member = this.memberList[0], Country = this.countryList[0] },
                new() { Member = this.memberList[1], Country = this.countryList[1] },
                new() { Member = this.memberList[5], Country = this.countryList[5] },
                new() { Member = this.memberList[6], Country = this.countryList[5] },
                new() { Member = this.memberList[7], Country = this.countryList[5] },
                new() { Member = this.memberList[15], Country = this.countryList[11] },
                new() { Member = this.memberList[16], Country = this.countryList[8] },
            };

            Assert.That(membersInCaps, Is.EquivalentTo(expectedIn));
            Assert.That(membersOutOfCaps, Is.EquivalentTo(expectedOut));
            this.countryRepo.Verify(repo => repo.GetAll(), Times.Exactly(2));
            this.memberRepo.Verify(repo => repo.GetAll(), Times.Exactly(2));
        }

        /// <summary>
        /// (NON-CRUD) Testing for <see cref="InternalAuditLogic.GetRichestMemberInExpertGroup"/>.
        /// </summary>
        [Test]
        public void TestGetRichestMemberInExpertGroup()
        {
            this.countryRepo.Setup(repo => repo.GetAll()).Returns(this.countryList.AsQueryable);
            this.memberRepo.Setup(repo => repo.GetAll()).Returns(this.memberList.AsQueryable);
            this.egRepo.Setup(repo => repo.GetAll()).Returns(this.egList.AsQueryable);

            IEnumerable<ExpertgroupMemberCountry> results = this.internalLogic.GetRichestMemberInExpertGroup();
            ExpertgroupMemberCountry[] expectedResult = new ExpertgroupMemberCountry[]
            {
                new() { Expertgroup = this.egList[0], Member = this.memberList[7], Country = this.countryList[5] },
                new() { Expertgroup = this.egList[1], Member = this.memberList[15], Country = this.countryList[11] },
                new() { Expertgroup = this.egList[2], Member = this.memberList[6], Country = this.countryList[5] },
                /* no members in Photography group */
            };

            Assert.That(results, Is.EquivalentTo(expectedResult));
            this.countryRepo.Verify(repo => repo.GetAll(), Times.Exactly(2));
            this.memberRepo.Verify(repo => repo.GetAll(), Times.Exactly(2));
            this.egRepo.Verify(repo => repo.GetAll(), Times.Once);
        }
    }
}
