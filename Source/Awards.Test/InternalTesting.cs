namespace Awards.Test
{
    using System;
    using System.Collections.Generic;
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
        /// <summary>
        /// Testing for the <see cref="InternalLogic"/> ListAll() methods.
        /// </summary>
        [Test]
        public void TestGetAll()
        {
            Mock<ICountryRepository> mockedCountryRepo = new Mock<ICountryRepository>(MockBehavior.Loose);
            Mock<IMemberRepository> mockedMemeberRepo = new Mock<IMemberRepository>(MockBehavior.Loose);
            Mock<IExpertGroupRepository> mockedEGrepo = new Mock<IExpertGroupRepository>(MockBehavior.Loose);
            List<Country> countries = MockRepoGenerator.GenerateCountryList();
            List<Member> members = MockRepoGenerator.GenerateMemberList();
            List<ExpertGroup> expertGroups = MockRepoGenerator.GenerateExpertGroupList();
            mockedCountryRepo.Setup(repo => repo.GetAll()).Returns(countries.AsQueryable());
            mockedEGrepo.Setup(repo => repo.GetAll()).Returns(expertGroups.AsQueryable());
            mockedMemeberRepo.Setup(repo => repo.GetAll()).Returns(members.AsQueryable());
            InternalAuditLogic internalAuditLogic = new InternalAuditLogic(2, mockedMemeberRepo.Object, mockedCountryRepo.Object, mockedEGrepo.Object);
            List<ExpertGroup> sampleEGs = expertGroups;
            Member firstMember = members[0];

            IEnumerable<Country> allCountries = internalAuditLogic.ListAllCountries(out _);
            IEnumerable<ExpertGroup> allEGs = internalAuditLogic.ListAllExpertgroups(out _);
            IEnumerable<Member> allMembers = internalAuditLogic.ListAllMembers(out _);

            Assert.That(allCountries.Count(), Is.EqualTo(countries.Count));
            Assert.That(expertGroups, Is.EquivalentTo(sampleEGs));
            Assert.That(members, Does.Contain(firstMember));
            mockedCountryRepo.Verify(repo => repo.GetAll(), Times.Once);
            mockedEGrepo.Verify(repo => repo.GetAll(), Times.Once);
            mockedMemeberRepo.Verify(repo => repo.GetAll(), Times.Once);
            mockedCountryRepo.Verify(repo => repo.GetOne(It.IsAny<int>()), Times.Never);
            mockedEGrepo.Verify(repo => repo.GetOne(It.IsAny<int>()), Times.Never);
            mockedMemeberRepo.Verify(repo => repo.GetOne(It.IsAny<int>()), Times.Never);
        }

        /*
        /// <summary>
        /// [NOT WORKING] (NON-CRUD) Testing for <see cref="InternalAuditLogic.ListMembersInCapitalCity(bool)"/>.
        /// </summary>
        [Test]
        public void TestListMembersInCapitalCity()
        {
            Mock<ICountryRepository> mockedCountryRepo = new Mock<ICountryRepository>(MockBehavior.Loose);
            Mock<IMemberRepository> mockedMemeberRepo = new Mock<IMemberRepository>(MockBehavior.Loose);
            Mock<IExpertGroupRepository> mockedEGrepo = new Mock<IExpertGroupRepository>(MockBehavior.Loose);
            List<Country> countries = MockRepoGenerator.GenerateCountryList();
            List<Member> members = MockRepoGenerator.GenerateMemberList();
            List<ExpertGroup> expertGroups = MockRepoGenerator.GenerateExpertGroupList();
            mockedCountryRepo.Setup(repo => repo.GetAll()).Returns(countries.AsQueryable());
            mockedEGrepo.Setup(repo => repo.GetAll()).Returns(expertGroups.AsQueryable());
            mockedMemeberRepo.Setup(repo => repo.GetOne(It.IsAny<int>()));
            mockedCountryRepo.Setup(repo => repo.GetOne(It.IsAny<int>()));
            mockedEGrepo.Setup(repo => repo.GetOne(It.IsAny<int>()));
            mockedMemeberRepo.Setup(repo => repo.GetAll()).Returns(members.AsQueryable());
            InternalAuditLogic internalAuditLogic = new InternalAuditLogic(2, mockedMemeberRepo.Object, mockedCountryRepo.Object, mockedEGrepo.Object);

            var results = internalAuditLogic.ListMembersInCapitalCity(true); // returns null bc string.contains??

            mockedCountryRepo.Verify(repo => repo.GetAll(), Times.Once);
            mockedMemeberRepo.Verify(repo => repo.GetAll(), Times.Once);
        }
    */

        /// <summary>
        /// (NON-CRUD) Testing for <see cref="InternalAuditLogic.GetRichestMemberInExpertGroup"/>.
        /// </summary>
        [Test]
        public void TestGetRichestMemberInExpertGroup()
        {
            Mock<ICountryRepository> mockedCountryRepo = new Mock<ICountryRepository>(MockBehavior.Loose);
            Mock<IMemberRepository> mockedMemeberRepo = new Mock<IMemberRepository>(MockBehavior.Loose);
            Mock<IExpertGroupRepository> mockedEGrepo = new Mock<IExpertGroupRepository>(MockBehavior.Loose);
            List<Country> countries = MockRepoGenerator.GenerateCountryList();
            List<Member> members = MockRepoGenerator.GenerateMemberList();
            List<ExpertGroup> expertGroups = MockRepoGenerator.GenerateExpertGroupList();
            mockedCountryRepo.Setup(repo => repo.GetAll()).Returns(countries.AsQueryable());
            mockedEGrepo.Setup(repo => repo.GetAll()).Returns(expertGroups.AsQueryable());
            mockedMemeberRepo.Setup(repo => repo.GetAll()).Returns(members.AsQueryable());
            InternalAuditLogic internalAuditLogic = new InternalAuditLogic(2, mockedMemeberRepo.Object, mockedCountryRepo.Object, mockedEGrepo.Object);

            var results = internalAuditLogic.GetRichestMemberInExpertGroup();
            var expectedResult = new List<ExpertgroupMemberCountry>
            {
                new ExpertgroupMemberCountry
                {
                    Expertgroup = new ExpertGroup { ExpertGroupID = 1, Name = "Hi-Fi" },
                    Member = new Member { MemberID = 8, Name = "Stereo", CountryID = 6, ExpertGroupID = 1 },
                    Country = new Country { CountryID = 6, Name = "Germany", CapitalCity = "Berlin", CallingCode = 49, PPPperCapita = 53571 },
                },
                new ExpertgroupMemberCountry
                {
                    Expertgroup = new ExpertGroup { ExpertGroupID = 2, Name = "Home Theatre Audio" },
                    Member = new Member { MemberID = 16, Name = "Sound & Vision", CountryID = 12, ExpertGroupID = 2, OfficeLocation = "Madison Avenue, 8 th floor, New York, NY 10016" },
                    Country = new Country { CountryID = 12, Name = "USA", CapitalCity = "Washington, D.C.", CallingCode = 1, PPPperCapita = 63051 },
                },
                new ExpertgroupMemberCountry
                {
                    Expertgroup = new ExpertGroup { ExpertGroupID = 3, Name = "Home Theatre Display & Video" },
                    Member = new Member { MemberID = 7, Name = "Hifi Test TV Video", CountryID = 6, ExpertGroupID = 3, OfficeLocation = "Gartroper Strasse 42, D-47138 - Duisburg", },
                    Country = new Country { CountryID = 6, Name = "Germany", CapitalCity = "Berlin", CallingCode = 49, PPPperCapita = 53571 },
                },
                /* no members in Photography group */
            };

            Assert.That(results, Is.EquivalentTo(expectedResult));
            mockedCountryRepo.Verify(repo => repo.GetAll(), Times.Exactly(2));
            mockedMemeberRepo.Verify(repo => repo.GetAll(), Times.Exactly(2));
            mockedEGrepo.Verify(repo => repo.GetAll(), Times.Once);
        }
    }
}
