using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using TAP2017_2018_TravelCompanyInterface.Exceptions;


namespace NANTravelCompanyTests
{
    [TestFixture]
    public class BrokerTCTestsuiteNAN : BrokerTestInitializer
    {
        [SetUp]
        public void Setup()
        {
            broker = brokerFactory.CreateNewBroker(AllTravelCompaniesConnectionString);
        }

        [Test]
        public void GetTravelCompanyfactoryReturnsOk()
        {
            broker.GetTravelCompanyFactory();
            Assert.Pass();
        }

        [Test]
        public void GetReadOnlyTravelCompanyfactoryReturnsOk()
        {
            broker.GetReadOnlyTravelCompanyFactory();
            Assert.Pass();
        }

        [Test]
        public void KnownTravelCOmpaniesNoAddReturnsEmpty()
        {
            Assert.IsEmpty(broker.KnownTravelCompanies());
        }

        [Test]
        public void KnownTravelCompaniesAddSomeCompaniesReturnsOk()
        {
            var tcf = broker.GetTravelCompanyFactory();
            const string anotherTravelCompanyName = "another";
            var tc = tcf.CreateNew(CreateConnectionString("anotherone"), anotherTravelCompanyName);

            Assert.Contains(anotherTravelCompanyName, broker.KnownTravelCompanies());
        }

        
    }
}
