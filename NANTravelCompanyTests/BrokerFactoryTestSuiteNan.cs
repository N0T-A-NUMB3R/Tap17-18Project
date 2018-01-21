using System;
using NUnit.Framework;

namespace NANTravelCompanyTests
{

    class BrokerFactoryTestSuiteNAN : BasicTestInitializer
    {
        /*
        [Test]
        public void CreateNewBrokerReturnsOk()
        {
            var br = brokerFactory.CreateNewBroker(ExampleConnectionString);
            Assert.Pass();
        }

        [Test]
        public void CreateBrokerEmptyConnectionStringThrowsException()
        {
            Assert.Throws<ArgumentException>(() => brokerFactory.CreateNewBroker(""));
        }

        [Test]
        public void CreateBrokerNullConnectionStringThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => brokerFactory.CreateNewBroker(null));
        }

        [Test]
        public void CreateBrokerTooLongConnectionStringThrowsException()
        {
            Assert.Throws<ArgumentException>(() => brokerFactory.CreateNewBroker(new string('A', DomainConstraints.ConnectionStringMaxLength + 1)));
        }

        [Test]
        public void CreateBrokerTooShortConnectionStringThrowsException()
        {
            Assert.Throws<ArgumentException>(() => brokerFactory.CreateNewBroker(new string('A', DomainConstraints.ConnectionStringMinLength - 1)));
        }

        [Test]
        public void GetBrokerReturnsOk()
        {
            var br = brokerFactory.CreateNewBroker(ExampleConnectionString);
            Assert.AreEqual(br, brokerFactory.GetBroker(ExampleConnectionString));
        }

        [Test]
        public void GetNotExistingBrokerThrowsException()
        {
            Assert.Throws<NonexistentObjectException>(() => brokerFactory.GetBroker("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAA"));
        }

        [Test]
        public void GetTooShortConnectionStringThrowsException()
        {
            Assert.Throws<ArgumentException>(() => brokerFactory.GetBroker(new string('A', DomainConstraints.ConnectionStringMinLength - 1)));
        }

        [Test]
        public void GetTooLongConnectionStringThrowsException()
        {
            Assert.Throws<ArgumentException>(() => brokerFactory.GetBroker(new string('A', DomainConstraints.ConnectionStringMaxLength + 1)));
        }

        [Test]
        public void GetEmptyConnectionStringThrowsException()
        {
            Assert.Throws<ArgumentException>(() => brokerFactory.GetBroker(""));
        }

        [Test]
        public void GetNullConnectionStringThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => brokerFactory.GetBroker(null));
        }
        */

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

        



    }
}
