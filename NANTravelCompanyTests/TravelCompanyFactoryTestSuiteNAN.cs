using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NANTravelCompanyTests.Inizialiazer;
using NUnit.Framework;
using TAP2017_2018_TravelCompanyInterface;
using TAP2017_2018_TravelCompanyInterface.Exceptions;

namespace NANTravelCompanyTests
{
    [TestFixture]
    public class TravelCompanyFactoryTestSuiteNAN : TravelCompanyTestInizializerNan
    {
        private string TheSameCompanyName { get; set; }
        private string CompanyName { get; set; }
        private string CompanyName1 { get; set; }
        private string CompanyName2 { get; set; }


        [SetUp]
        public void SetUp()
        {
            TheSameCompanyName = "";
            CompanyName = "";
            CompanyName1 = "";
            CompanyName2 = "";
        }

        [Test]
        public void Should_DontCreateACompanyWithTheBrokerConnectionString_Throw_SameConnectionStringException()
        {
            CompanyName = "Alitalia";
            var brokerConnectionString = AllTravelCompaniesConnectionString;
            Assert.That(() => travelCompanyFactory.CreateNew(brokerConnectionString, CompanyName), Throws.TypeOf<SameConnectionStringException>());
        }
        [Test]
        public void Should_DontCreateTwoCompaniesWithTheSameConnectionString_Throw_SameConnectionStringException()
        {
            CompanyName1 = "Alitalia";
            CompanyName2 = "Trenitalia";
            var theSameConnectionString = AlitaliaConnectionString;

            travelCompanyFactory.CreateNew(theSameConnectionString, CompanyName1);

            Assert.That(() => travelCompanyFactory.CreateNew(theSameConnectionString, CompanyName2),
                Throws.TypeOf<SameConnectionStringException>());

        }
        [Test]
        public void Should_DontCreateTwoCompaniesWithTheSameName_Throw_TapDuplicateObjectException()
        {
            TheSameCompanyName = "Alitalia";

            travelCompanyFactory.CreateNew(AlitaliaConnectionString, TheSameCompanyName);

            Assert.That(() => travelCompanyFactory.CreateNew(TreniItaliaConnectionString, TheSameCompanyName),
                Throws.TypeOf<TapDuplicatedObjectException>());

        }

        [Test]
        public void CreateTravelCompanyReturnsOk()
        {
            const string aNewTravelCompanyName = "newOne";
            var tc = travelCompanyFactory.CreateNew(ExampleConnectionString2, aNewTravelCompanyName);
            var allKnown = broker.KnownTravelCompanies();
            Assert.Contains(aNewTravelCompanyName, allKnown);
        }

        [Test]
        public void CreateWithAlreadyUsedNameThrowsException()
        {
            Assert.Throws<TapDuplicatedObjectException>(() => travelCompanyFactory.CreateNew(ExampleConnectionString2, travelCompany.Name));
        }

        [Test]
        public void CreateWithEmptyNameThrowsException()
        {
            Assert.Throws<ArgumentException>(() => travelCompanyFactory.CreateNew(ExampleConnectionString2, ""));
        }

        [Test]
        public void CreateWithNullNameThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => travelCompanyFactory.CreateNew(ExampleConnectionString2, null));
        }
        [Test]
        public void CreateWithNonAlphaNumNameThrowsException()
        {
            Assert.Throws<ArgumentException>(() => travelCompanyFactory.CreateNew(ExampleConnectionString2, "The best TC"));
        }

        [Test]
        public void CreateWithTooLongNameThrowsException()
        {
            Assert.Throws<ArgumentException>(() => travelCompanyFactory.CreateNew(ExampleConnectionString2, "Ab0Ab1Ab2Ab3Ab4Ab5Ab6Ab7Ab8Ab9xyz"));
        }


        [Test]
        public void CreateWithEmptyConnectionStringThrowsException()
        {
            Assert.Throws<ArgumentException>(() => travelCompanyFactory.CreateNew("", "NAME"));
        }

        [Test]
        public void CreateWithNullConnectionStringThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => travelCompanyFactory.CreateNew(null, "NAME"));
        }

        [Test]
        public void CreateTooLongCOnnectionStringThrowsException()
        {
            Assert.Throws<ArgumentException>(() => travelCompanyFactory.CreateNew(new string('A', DomainConstraints.ConnectionStringMaxLength + 1), "NAME"));
        }

        [Test]
        public void CreateTooShortConnectionStringThrowsException()
        {
            Assert.Throws<ArgumentException>(() => travelCompanyFactory.CreateNew(new string('A', DomainConstraints.ConnectionStringMinLength - 1), "NAME"));
        }

        [Test]
        public void GetAlreadyCreatedTravelCompanyReturnsOk()
        {

            var loadedTravelCompany = travelCompanyFactory.Get(travelCompany.Name);
            Assert.AreEqual(loadedTravelCompany, travelCompany);
        }

        [Test]
        public void GetNotExistingTravelCompanyThrowsException()
        {
            string name = "RandomTrains";
            Assert.Throws<NonexistentTravelCompanyException>(() => travelCompanyFactory.Get(name));

        }

        [Test]
        public void GetTravelCompanyEmptyNameThrowsException()
        {
            Assert.Throws<ArgumentException>(() => travelCompanyFactory.Get(""));

        }

        [Test]
        public void GetTravelCompanyNullNameThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => travelCompanyFactory.Get(null));

        }


        [Test]
        public void GetWithNonAlphaNumNameThrowsException()
        {
            Assert.Throws<ArgumentException>(() => travelCompanyFactory.Get("$TC"));
        }

        [Test]
        public void GetWithTooLongNameThrowsException()
        {
            Assert.Throws<ArgumentException>(() => travelCompanyFactory.Get(new string('a', DomainConstraints.NameMaxLength + 1)));
        }


    }
}