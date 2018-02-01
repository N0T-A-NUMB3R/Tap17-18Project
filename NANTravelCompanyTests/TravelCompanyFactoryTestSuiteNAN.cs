using System;
using NANTravelCompanyTests.Inizialiazer;
using NUnit.Framework;
using TAP2017_2018_TravelCompanyInterface;
using TAP2017_2018_TravelCompanyInterface.Exceptions;

namespace NANTravelCompanyTests
{
    [TestFixture]
    public class TravelCompanyFactoryTestSuiteNan : TravelCompanyTestInitializer
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
        public void CreateNew_CompanyWithTheBrokerCS_Throw_SameConnectionStringException()
        {
            CompanyName = "Alitalia";
            var brokerConnectionString = AllTravelCompaniesConnectionString;
            Assert.That(() => travelCompanyFactory.CreateNew(brokerConnectionString, CompanyName),
                Throws.TypeOf<SameConnectionStringException>());
        }

        [Test]
        public void CreateNew_CompaniesWithTheSameConnectionString_Throw_SameConnectionStringException()
        {
            CompanyName1 = "Alitalia";
            CompanyName2 = "TreNord";
            var company1Cs = CreateConnectionString(CompanyName1);
            travelCompanyFactory.CreateNew(company1Cs, CompanyName1);
            Assert.That(() => travelCompanyFactory.CreateNew(company1Cs, CompanyName2),
                Throws.TypeOf<SameConnectionStringException>());

        }

        [Test]
        public void CreateNew_CompaniesWithTheSameName_Throw_TapDuplicateObjectException()
        {
            TheSameCompanyName = "Alitalia";
            var cs = CreateConnectionString(TheSameCompanyName);
            var cs2 = CreateConnectionString("Costa");
            travelCompanyFactory.CreateNew(cs, TheSameCompanyName);

            Assert.That(() => travelCompanyFactory.CreateNew((cs2), TheSameCompanyName),
                Throws.TypeOf<TapDuplicatedObjectException>());

        }


    }
}