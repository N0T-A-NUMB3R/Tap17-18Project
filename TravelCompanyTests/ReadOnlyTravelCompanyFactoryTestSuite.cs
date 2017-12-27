using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAP2017_2018_TravelCompanyInterface;
using TAP2017_2018_TravelCompanyTests;

namespace TAP2017_2018_Tests
{
    [TestFixture]
    public class ReadOnlyTravelCompanyFactoryTestSuite : BrokerTestInitializer
    {
        [Test]
        public void GetReturnsOk()
        {
            var rotc = readOnlyTravelCompanyFactory.Get(ExampleName);
            Assert.Pass();
        }

        [Test]
        public void GetEmptyNameThrowsException()
        {
            Assert.Throws<ArgumentException>(() => readOnlyTravelCompanyFactory.Get(""));
        }

        [Test]
        public void GetNullNameThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => readOnlyTravelCompanyFactory.Get(null));
        }

        [Test]
        public void GetNotAlphanumCharNameThrowsException()
        {
            Assert.Throws<ArgumentException>(() => readOnlyTravelCompanyFactory.Get("+è+è+è+è+è+è+"));
        }

        [Test]
        public void GetTooLongThrowsException()
        {
            Assert.Throws<ArgumentException>(() => readOnlyTravelCompanyFactory.Get(new string('A',DomainConstraints.NameMaxLength+1)));
        }
        [Test]
        public void GetTooShortThrowsException()
        {
            Assert.Throws<ArgumentException>(() => readOnlyTravelCompanyFactory.Get(new string('A', DomainConstraints.NameMinLength-1)));
        }
    }
}
