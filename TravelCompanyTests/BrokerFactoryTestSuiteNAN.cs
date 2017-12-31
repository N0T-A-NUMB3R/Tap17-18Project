using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TAP2017_2018_TravelCompanyInterface;
using TAP2017_2018_TravelCompanyInterface.Exceptions;
using TAP2017_2018_TravelCompanyTests;

namespace TravelCompanyTests
{
    class BrokerFactoryTestSuiteNAN : BasicTestInitializer
    {
            [TestCase("     ")]
            public void CreateBrokerEmptyCSMidThrowsException(string fiveSpace)
            {
               Assert.Throws<ArgumentException>(() => brokerFactory.CreateNewBroker(fiveSpace));
            }
            [TestCase(" ")]
            public void CreateBrokerEmptyCSMinThrowsException(string oneSpace)
            {
                
                Assert.Throws<ArgumentException>(() => brokerFactory.CreateNewBroker(oneSpace));
            }
             [TestCase("                                ")]
             public void CreateBrokerEmptyCSMaxThrowsException(string thirtyThree)
             {
                Assert.Throws<ArgumentException>(() => brokerFactory.CreateNewBroker(thirtyThree));
             }

            [TestCase("!$€ffssfdsff£")]
            public void CreateBrokerNoAlphanumericsCSMidThrowsException(string input)
            {
                Assert.Throws<ArgumentException>(() => brokerFactory.CreateNewBroker(input));
             }

            [TestCase("$")]
            public void CreateBrokerNoAlphanumericsCSMinThrowsException(string input)
            {
                Assert.Throws<ArgumentException>(() => brokerFactory.CreateNewBroker(input));
            }
            [TestCase("!$€ffssfds$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$ff£")]
            public void CreateBrokerNoAlphanumericsCSMaxThrowsException(string input)
            {
                Assert.Throws<ArgumentException>(() => brokerFactory.CreateNewBroker(input));
            }
    }
}
