using Ninject;
using NUnit.Framework;
using TAP2017_2018_TravelCompanyInterface;

namespace TAP2017_2018_TravelCompanyTests
{
    [TestFixture]
    public class BrokerTravelCompanyTestSuite : BrokerTestInitializer
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
            var tc = tcf.CreateNew(CreateConnectionString("anotherone"),anotherTravelCompanyName);

            Assert.Contains(anotherTravelCompanyName, broker.KnownTravelCompanies());
        }

    }
}
