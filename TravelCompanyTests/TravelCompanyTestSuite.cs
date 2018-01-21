using System;
using NUnit.Framework;
using TAP2017_2018_TravelCompanyInterface;
using TAP2017_2018_TravelCompanyInterface.Exceptions;

namespace TAP2017_2018_TravelCompanyTests
{
    [TestFixture]
    public class TravelCompanyTestSuite : TravelCompanyTestInitializer
    {

        [Test]
        public void CreateCorrectLegReturnsOk()
        {
            const int cost = 100;
            const int distance = 715;
            const TransportType plane = TransportType.Plane;
            int legId = travelCompany.CreateLeg(pointA, pointB, cost, distance, plane);
            var leg = travelCompany.GetLegDTOFromId(legId);
            Assert.AreEqual(leg.From, pointA);
            Assert.AreEqual(leg.To, pointB);
            Assert.AreEqual(leg.Distance, distance);
            Assert.AreEqual(leg.Cost, cost);
            Assert.AreEqual(leg.Type,plane);
        }

        [Test]
        public void DeleteLegCorrectIdIsOk()
        {
            const int cost = 100;
            const int distance = 715;
            const TransportType plane = TransportType.Plane;
            int legId = travelCompany.CreateLeg(pointA, pointB, cost, distance, plane);
            travelCompany.DeleteLeg(legId);
            Assert.Throws<NonexistentObjectException>(() => travelCompany.GetLegDTOFromId(legId));
        }

        [Test]
        public void DeleteLegWrongIdThrowsException()
        {
            Assert.Throws<NonexistentObjectException>(() => travelCompany.DeleteLeg(9999));
        }

        [Test]
        public void CreateLegLongNameFromThrowsException()
        {
            const int cost = 100;
            const int distance = 715;
            const TransportType plane = TransportType.Plane;
            Assert.Throws<ArgumentException>(()=>travelCompany.CreateLeg(new string('A', DomainConstraints.NameMaxLength + 1), pointB, cost, distance, plane));
        }

        [Test]
        public void CreateLegLongNameToThrowsException()
        {
            const int cost = 100;
            const int distance = 715;
            const TransportType plane = TransportType.Plane;
            Assert.Throws<ArgumentException>(() => travelCompany.CreateLeg(pointA,new string('A', DomainConstraints.NameMaxLength + 1), cost, distance, plane));
        }

        [Test]
        public void CreateLegBadNameFromThrowsException()
        {
            const int cost = 100;
            const int distance = 715;
            const TransportType plane = TransportType.Plane;
            Assert.Throws<ArgumentException>(() => travelCompany.CreateLeg("sasas*", pointB, cost, distance, plane));
        }

        [Test]
        public void CreateLegBadNameToThrowsException()
        {
            const int cost = 100;
            const int distance = 715;
            const TransportType plane = TransportType.Plane;
            Assert.Throws<ArgumentException>(() => travelCompany.CreateLeg(pointA, "ncbidunw*?", cost, distance, plane));
        }

        [Test]
        public void CreateLegEmptyFromThrowsException()
        {
            const int cost = 100;
            const int distance = 715;
            const TransportType plane = TransportType.Plane;
            Assert.Throws<ArgumentException>(() => travelCompany.CreateLeg("", pointB, cost, distance, plane));
        }

        [Test]
        public void CreateLegEmptyToThrowsException()
        {
            const int cost = 100;
            const int distance = 715;
            const TransportType plane = TransportType.Plane;
            Assert.Throws<ArgumentException>(() => travelCompany.CreateLeg(pointA, "", cost, distance, plane));
        }

        [Test]
        public void CreateLegNullFromThrowsException()
        {
            const int cost = 100;
            const int distance = 715;
            const TransportType plane = TransportType.Plane;
            Assert.Throws<ArgumentNullException>(() => travelCompany.CreateLeg(null, pointB, cost, distance, plane));
        }

        [Test]
        public void CreateLegNullToThrowsException()
        {
            const int cost = 100;
            const int distance = 715;
            const TransportType plane = TransportType.Plane;
            Assert.Throws<ArgumentNullException>(() => travelCompany.CreateLeg(pointA, null, cost, distance, plane));
        }

        [Test]
        public void CreateLegNoneTransportTypeThrowsException()
        {
            const int cost = 100;
            const int distance = 715;
            Assert.Throws<ArgumentException>(() => travelCompany.CreateLeg(pointA, pointB, cost, distance, TransportType.None));
        }

        [Test]
        public void CreateLegWithSameDestinationAndArrivalThrowsException()
        {
            Assert.Throws<ArgumentException>(()=>travelCompany.CreateLeg(pointA, pointA, 1, 1, TransportType.Bus));
        }

        [Test]
        public void CreateLegWithZeroDistanceThrowsException()
        {
            Assert.Throws<ArgumentException>(() => travelCompany.CreateLeg(pointA, pointB, 1, 0, TransportType.Bus));
        }

        [Test]
        public void CreateLegWithNegativeDistanceThrowsException()
        {
            Assert.Throws<ArgumentException>(() => travelCompany.CreateLeg(pointA, pointB, 1, -200, TransportType.Bus));
        }

        [Test]
        public void CreateLegWithZeroCostThrowsException()
        {
            Assert.Throws<ArgumentException>(() => travelCompany.CreateLeg(pointA, pointB, 0, 200, TransportType.Bus));
        }

        [Test]
        public void CreateLegWithNegativeCostThrowsException()
        {
            Assert.Throws<ArgumentException>(() => travelCompany.CreateLeg(pointA, pointB, -2000, 200, TransportType.Bus));
        }

        [Test]
        public void GetLegDTOIncorrectIdThrowsException()
        {
            Assert.Throws<NonexistentObjectException>(() => travelCompany.GetLegDTOFromId(99999));
        }
    }
}
