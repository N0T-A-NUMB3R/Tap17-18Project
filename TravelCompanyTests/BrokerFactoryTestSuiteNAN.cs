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
         [Test]
            public void CreateNewBrokerWithExistentCS()
            {
                var cs = BasicTestInitializer.CreateConnectionString("TreniNan");
                var br = brokerFactory.CreateNewBroker(cs);
                Assert.Throws<SameConnectionStringException>(() => brokerFactory.CreateNewBroker(cs));
             }
            [Test]
            public void GetBrokerReturnsOk()
            {
                var br = brokerFactory.CreateNewBroker(ExampleConnectionString);
                Assert.AreEqual(br, brokerFactory.GetBroker(ExampleConnectionString));
             }

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

            [TestCase("     ")]
            public void GetBrokerEmptyCSMidThrowsException(string fiveSpace)
            {
                Assert.Throws<ArgumentException>(() => brokerFactory.GetBroker(fiveSpace));
            }
            [TestCase(" ")]
            public void GetBrokerEmptyCSMinThrowsException(string oneSpace)
            {
                Assert.Throws<ArgumentException>(() => brokerFactory.GetBroker(oneSpace));

            }
            [TestCase("                                ")]
            public void GetBrokerEmptyCSMaxThrowsException(string thirtyThree)
            {
                Assert.Throws<ArgumentException>(() => brokerFactory.GetBroker(thirtyThree));
            }

            [TestCase("!$€ffssfdsff£")]
            public void GetBrokerNoAlphanumericsCSMidThrowsException(string input)
            {
                Assert.Throws<ArgumentException>(() => brokerFactory.GetBroker(input));
            }   

            [TestCase("$")]
            public void GetBrokerNoAlphanumericsCSMinThrowsException(string input)
            {
                Assert.Throws<ArgumentException>(() => brokerFactory.GetBroker(input));
            }
            [TestCase("!$€ffssfds$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$ff£")]
            public void GetBrokerNoAlphanumericsCSMaxThrowsException(string input)
            {
                Assert.Throws<ArgumentException>(() => brokerFactory.GetBroker(input));
            }
    }
}
