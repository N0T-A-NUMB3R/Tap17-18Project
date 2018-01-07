using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TAP2017_2018_TravelCompanyTests;

namespace TravelCompanyTests
{

    [TestFixture]
    public class BrokerTCTestsuitNAN : BrokerTestInitializer
    {


        [SetUp]
        public void Setup()
        {
            broker = brokerFactory.CreateNewBroker(AllTravelCompaniesConnectionString);
        }
        

        [Test]
        public void KnownTravelCompaniesThrowException()
        {
            Assert.Throws<TAP2017_2018_TravelCompanyInterface.Exceptions.NonexistentObjectException>((() =>
                broker.KnownTravelCompanies()));
        }

    }
}
