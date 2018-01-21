using NUnit.Framework;
using TAP2017_2018_TravelCompanyInterface;

namespace NANTravelCompanyTests
{
    [TestFixture]
    public class BrokerTestInitializer : BasicTestInitializer
    {

        protected ITravelCompanyBroker broker;

        [SetUp]
        public void SetupBasicTest()
        {

            broker = brokerFactory.CreateNewBroker(AllTravelCompaniesConnectionString);
            travelCompanyFactory = broker.GetTravelCompanyFactory();
            travelCompanyFactory.CreateNew(ExampleConnectionString, ExampleName);

            readOnlyTravelCompanyFactory = broker.GetReadOnlyTravelCompanyFactory();



        }
    }
}