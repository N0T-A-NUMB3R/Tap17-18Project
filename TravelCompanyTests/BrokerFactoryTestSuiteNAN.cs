using System;
using NUnit.Framework;
using TAP2017_2018_TravelCompanyTests;

namespace TravelCompanyTests
{
    class BrokerFactoryTestSuiteNAN : BasicTestInitializer
    {
            /*
            
            NON PUO PASSARE PERCHE SECONDO SPECIFICHE VA ELIMINATO IL DB
            [Test]
            public void CreateNewBrokerWithExistentCS()
            {
                var cs = BasicTestInitializer.CreateConnectionString("TreniNan");
                var br = brokerFactory.CreateNewBroker(cs);
                Assert.Throws<SameConnectionStringException>(() => brokerFactory.CreateNewBroker(cs));
             }  
             */  
            [Test]
            public void GetBrokerReturnsOk()
            {
                var br = brokerFactory.CreateNewBroker(ExampleConnectionString);
                Assert.AreEqual(br, brokerFactory.GetBroker(ExampleConnectionString));
             }

            [TestCase("     ")]
            public void CreateBrokerEmptyCSMidThrowsException(string fiveSpace)
            {
                var cs = CreateConnectionString(fiveSpace);
                Assert.Throws<ArgumentException>(() => brokerFactory.CreateNewBroker(fiveSpace));
            }
            [TestCase(" ")]
            public void CreateBrokerEmptyCSMinThrowsException(string oneSpace)
            {
                var cs = CreateConnectionString(oneSpace);
                Assert.Throws<ArgumentException>(() => brokerFactory.CreateNewBroker(oneSpace));

            }
             [TestCase("                                ")]
             public void CreateBrokerEmptyCSMaxThrowsException(string thirtyThree)
             {
                 var cs = CreateConnectionString(thirtyThree);
                Assert.Throws<ArgumentException>(() => brokerFactory.CreateNewBroker(thirtyThree));
             }

          
            [TestCase("$")]
            public void CreateBrokerNoAlphanumericsCSMinThrowsException(string input)
            {
                var cs = CreateConnectionString(input);
                Assert.Throws<ArgumentException>(() => brokerFactory.CreateNewBroker(input));
            }
            
            [TestCase("     ")]
            public void GetBrokerEmptyCSMidThrowsException(string fiveSpace)
            {
                 var cs = CreateConnectionString(fiveSpace);
                 Assert.Throws<ArgumentException>(() => brokerFactory.GetBroker(fiveSpace));
            }
            [TestCase(" ")]
            public void GetBrokerEmptyCSMinThrowsException(string oneSpace)
            {
                var cs = CreateConnectionString(oneSpace);
                Assert.Throws<ArgumentException>(() => brokerFactory.GetBroker(oneSpace));

            }
            [TestCase("                                ")]
            public void GetBrokerEmptyCSMaxThrowsException(string thirtyThree)
            {
                var cs = CreateConnectionString(thirtyThree);
                 Assert.Throws<ArgumentException>(() => brokerFactory.GetBroker(thirtyThree));
            }

            
            [TestCase("$")]
            public void GetBrokerNoAlphanumericsCSMinThrowsException(string input)
            {
                var cs = CreateConnectionString(input);
                 Assert.Throws<ArgumentException>(() => brokerFactory.GetBroker(input));
            }

        


    }
}
