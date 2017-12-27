using Ninject;
using NUnit.Framework;
using System;

using TAP2017_2018_TravelCompanyInterface;
using TAP2017_2018_TravelCompanyInterface.Exceptions;

namespace TAP2017_2018_TravelCompanyTests
{
    [TestFixture]
    public class BrokerFactoryTestSuite : BasicTestInitializer
    {


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

    }
}
