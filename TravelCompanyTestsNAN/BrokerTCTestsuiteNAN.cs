using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using TravelCompanyTestsNAN;

namespace TCTestsNAN
{
    [TestFixture]
    public class BrokerTCTestsuiteNAN : BrokerTestInitializer
    {


        [SetUp]
        public void Setup()
        {
            broker = brokerFactory.CreateNewBroker(AllTravelCompaniesConnectionString);
        }




    }
}

