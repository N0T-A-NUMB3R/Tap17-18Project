using NUnit.Framework;
using System;
using TAP2017_2018_TravelCompanyInterface;

namespace TAP2017_2018_TravelCompanyTests
{
    [TestFixture]
    public class ReadOnlyTravelCompanyTestSuite : TravelCompanyTestInitializer
    {
        protected string A, B, C, D, E, F, unreachable;
        protected ILegDTO ae, ab, bf;
        protected int aeIndex, abIndex, bfIndex;

        [SetUp]
        public void InitializePaths()
        {

            A = "A";
            B = "B";
            C = "C";
            D = "D";
            E = "E";
            F = "G";
            unreachable = "UNREACHABLE";
            travelCompany = travelCompanyFactory.Get(ExampleName);
            aeIndex = travelCompany.CreateLeg(A, E, 10, 45, TransportType.Train);
            ae = travelCompany.GetLegDTOFromId(aeIndex);
            abIndex = travelCompany.CreateLeg(A, B, 2, 4, TransportType.Train);
            ab = travelCompany.GetLegDTOFromId(abIndex);
            bfIndex = travelCompany.CreateLeg(B, F, 5, 20, TransportType.Train);
            bf = travelCompany.GetLegDTOFromId(bfIndex);
            travelCompany.CreateLeg(C, D, 1, 4, TransportType.Train);
            travelCompany.CreateLeg(D, F, 2, 8, TransportType.Train);
        }


        [Test]
        public void FindLegsReadOnlyTravelCompanyReturnsOk()
        {
            var result =readOnlyTravelCompany.FindLegs(l => l.From.Equals(A) && l.Type.Equals(TransportType.Train));
            Assert.Contains(ae, result);
            Assert.Contains(ab, result);
            Assert.That(!result.Contains(bf));
            Assert.AreEqual(2,result.Count);
        }

        [Test]
        public void FindDeparturesReadOnlyTravelCompanyReturnsOk()
        {
            var result = readOnlyTravelCompany.FindDepartures(A,TransportType.Train);
            Assert.Contains(ae, result);
            Assert.Contains(ab, result);
            Assert.That(!result.Contains(bf));
            Assert.AreEqual(2, result.Count);
        }

        [Test]
        public void FindDeparturesEmptyResultReturnsEmptyListAndIsOk()
        {
            var result = readOnlyTravelCompany.FindDepartures(unreachable, TransportType.Train);
            Assert.IsEmpty(result);
        }

        [Test]
        public void FindDeparturesBadStringThrowsException()
        {
            Assert.Throws<ArgumentException>(() => readOnlyTravelCompany.FindDepartures("nicdn*", TransportType.Train));
        }

        [Test]
        public void FindDeparturesEmptyStringThrowsException()
        {
            Assert.Throws<ArgumentException>(() => readOnlyTravelCompany.FindDepartures("", TransportType.Train));
        }

        [Test]
        public void FindDeparturesTooLongStringThrowsException()
        {
            Assert.Throws<ArgumentException>(() => readOnlyTravelCompany.FindDepartures(new string('S',DomainConstraints.NameMaxLength+1), TransportType.Train));
        }

        [Test]
        public void FindDeparturesNullStringThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => readOnlyTravelCompany.FindDepartures(null, TransportType.Train));
        }

        [Test]
        public void FindDeparturesNoneTypeThrowsException()
        {
            Assert.Throws<ArgumentException>(() => readOnlyTravelCompany.FindDepartures("ciaone", TransportType.None));
        }
    }
}
