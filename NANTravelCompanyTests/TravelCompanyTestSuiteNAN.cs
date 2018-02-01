 using System;
 using NUnit.Framework;
 using TAP2017_2018_TravelCompanyInterface;
 using TAP2017_2018_TravelCompanyInterface.Exceptions;

namespace NANTravelCompanyTests
{
    [TestFixture]
    public class TravelCompanyTestSuiteNAN : TravelCompanyTestInitializer
    {
        [Test]
        public void CreateLeg_TestUniqueLeg_Throw_TapDuplicateObjectException()
        {
            const int cost = 100;
            const int distance = 715;
            const TransportType plane = TransportType.Plane;
            int legId = travelCompany.CreateLeg(pointA, pointB, cost, distance, plane);
            Assert.That(() => travelCompany.CreateLeg(pointA, pointB, cost, distance, plane),
                Throws.TypeOf<TapDuplicatedObjectException>());

        }

    }
}
