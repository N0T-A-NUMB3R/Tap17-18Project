using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using TAP2017_2018_PlannerInterface;
using TAP2017_2018_TravelCompanyInterface;
using TAP2017_2018_TravelCompanyInterface.Exceptions;
using Key = System.Tuple<string, string, string>;
using System.Linq;
using NanPlannerTests;

namespace NanPlannerTests
{
    [TestFixture]
    public class PrivatePlannerTestSuite : NANTestInitializer
    {

        IPlanner planner;

        protected string A, B, C, D, E, F, unreachable;
        protected List<string> allDestinations = new List<string>(new String[] { "A", "B", "C", "D", "E", "F", "unreachable" });
        protected IReadOnlyTravelCompany rotcA, rotcB, rotcC;
        protected Dictionary<Key, ILegDTO> createdLegs;
        string rotcAName = "A";
        string rotcBName = "B";
        string rotcCName = "C";
        protected Mock<IReadOnlyTravelCompany> mockRotcA, mockRotcB, mockRotcC;

        protected ILegDTO getMockedLegDTO(string from, string to, int distance, int cost, TransportType type)
        {
            var mock = new Mock<ILegDTO>();
            mock.Setup(l => l.Cost).Returns(cost);
            mock.Setup(l => l.Distance).Returns(distance);
            mock.Setup(l => l.From).Returns(from);
            mock.Setup(l => l.To).Returns(to);
            mock.Setup(l => l.Type).Returns(type);
            return mock.Object;
        }

        protected List<ILegDTO> getLegsFromChosenDestination(string rotcName, string from, Dictionary<Key, ILegDTO> dictionary)
        {
            var rotcLegs = new List<ILegDTO>();
            foreach (var k in createdLegs)
            {
                if (k.Key.Item1.Equals(rotcName) && k.Key.Item2.Equals(from)) rotcLegs.Add(createdLegs[k.Key]);
            }
            return rotcLegs;
        }

        [SetUp]
        public void PlannerInit()
        {
            planner = plannerFactory.CreateNew();
            A = allDestinations[0];
            B = allDestinations[1];
            C = allDestinations[2];
            D = allDestinations[3];
            E = allDestinations[4];
            F = allDestinations[5];
            unreachable = allDestinations[6];
            createdLegs = new Dictionary<Key, ILegDTO>
            {
                [new Key(rotcAName, A, B)] = getMockedLegDTO(A, B, 2, 1000, TransportType.Train),
                [new Key(rotcAName, B, A)] = getMockedLegDTO(B, A, 2, 10, TransportType.Train),
                [new Key(rotcAName, A, C)] = getMockedLegDTO(A, C, 1, 1, TransportType.Train),
                [new Key(rotcAName, C, A)] = getMockedLegDTO(C, A, 1, 1, TransportType.Train),
                [new Key(rotcAName, E, C)] = getMockedLegDTO(E, C, 20, 100, TransportType.Train),
                [new Key(rotcAName, C, E)] = getMockedLegDTO(C, E, 20, 100, TransportType.Train),
                [new Key(rotcAName, C, F)] = getMockedLegDTO(C, F, 3, 5, TransportType.Train),
                [new Key(rotcAName, F, D)] = getMockedLegDTO(F, D, 5, 4, TransportType.Train),
                [new Key(rotcAName, E, B)] = getMockedLegDTO(E, B, 1, 1, TransportType.Train),
                [new Key(rotcAName, D, B)] = getMockedLegDTO(D, B, 1, 1, TransportType.Train),
                [new Key(rotcAName, D, E)] = getMockedLegDTO(D, E, 3, 3, TransportType.Train),


                [new Key(rotcBName, A, E)] = getMockedLegDTO(A, E, 25, 100, TransportType.Plane),
                [new Key(rotcBName, A, F)] = getMockedLegDTO(A, F, 10, 20, TransportType.Plane),
                [new Key(rotcBName, F, E)] = getMockedLegDTO(F, E, 20, 45, TransportType.Plane),
                [new Key(rotcBName, F, B)] = getMockedLegDTO(F, B, 10, 3, TransportType.Plane),

                [new Key(rotcCName, A, B)] = getMockedLegDTO(A, B, 2, 1000, TransportType.Train),
                [new Key(rotcCName, B, A)] = getMockedLegDTO(B, A, 2, 10, TransportType.Train),
                [new Key(rotcCName, A, C)] = getMockedLegDTO(A, C, 1, 1, TransportType.Train),
                [new Key(rotcCName, C, A)] = getMockedLegDTO(C, A, 1, 1, TransportType.Train),
                [new Key(rotcCName, E, C)] = getMockedLegDTO(E, C, 20, 100, TransportType.Train),
                [new Key(rotcCName, C, E)] = getMockedLegDTO(C, E, 20, 100, TransportType.Train),
                [new Key(rotcCName, C, F)] = getMockedLegDTO(C, F, 3, 5, TransportType.Train),
                [new Key(rotcCName, F, D)] = getMockedLegDTO(F, D, 5, 4, TransportType.Train),
                [new Key(rotcCName, E, B)] = getMockedLegDTO(E, B, 1, 1, TransportType.Train),
                [new Key(rotcCName, D, B)] = getMockedLegDTO(D, B, 1, 1, TransportType.Train),
                [new Key(rotcCName, D, E)] = getMockedLegDTO(D, E, 3, 3, TransportType.Train),
                [new Key(rotcCName, A, E)] = getMockedLegDTO(A, E, 25, 100, TransportType.Plane),
                [new Key(rotcCName, A, F)] = getMockedLegDTO(A, F, 10, 20, TransportType.Plane),
                [new Key(rotcCName, F, E)] = getMockedLegDTO(F, E, 4, 45, TransportType.Plane),
                [new Key(rotcCName, F, B)] = getMockedLegDTO(F, B, 10, 3, TransportType.Plane)

            };

            var rotcAList = new List<ILegDTO>();
            var rotcBList = new List<ILegDTO>();
            var rotcCList = new List<ILegDTO>();

            foreach (var v in createdLegs)
            {
                if (v.Key.Item1.Equals(rotcAName)) rotcAList.Add((v.Value));
                else if (v.Key.Item1.Equals(rotcBName)) rotcBList.Add((v.Value));
                else if (v.Key.Item1.Equals(rotcCName)) rotcCList.Add(v.Value);
            }

            var queryableA = rotcAList.AsQueryable();
            var queryableB = rotcBList.AsQueryable();
            var queryableC = rotcCList.AsQueryable();

            mockRotcA = new Mock<IReadOnlyTravelCompany>(MockBehavior.Strict);

            foreach (var destination in allDestinations)
            {
                mockRotcA
                    .Setup(l => l.FindDepartures(destination, TransportType.Train))
                    .Returns(new ReadOnlyCollection<ILegDTO>(getLegsFromChosenDestination(rotcAName, destination, createdLegs)));
            }
            mockRotcA
                .Setup(lst => lst.FindLegs(It.IsAny<Expression<Func<ILegDTO, bool>>>()))
                .Returns(
                         (Expression<Func<ILegDTO, bool>> p) => { return new ReadOnlyCollection<ILegDTO>(queryableA.Where(p.Compile()).ToList()); });

            mockRotcB = new Mock<IReadOnlyTravelCompany>(MockBehavior.Strict);
            mockRotcB
                .Setup(l => l.FindDepartures(A, TransportType.Plane))
                .Returns(new ReadOnlyCollection<ILegDTO>(getLegsFromChosenDestination(rotcBName, A, createdLegs)));
            mockRotcB
                .Setup(l => l.FindDepartures(F, TransportType.Plane))
                .Returns(new ReadOnlyCollection<ILegDTO>(getLegsFromChosenDestination(rotcBName, F, createdLegs)));

            mockRotcB
                .Setup(l => l.FindDepartures(B, TransportType.Plane))
                .Returns(new ReadOnlyCollection<ILegDTO>(getLegsFromChosenDestination(rotcBName, B, createdLegs)));
            mockRotcB
                .Setup(l => l.FindDepartures(E, TransportType.Plane))
                .Returns(new ReadOnlyCollection<ILegDTO>(getLegsFromChosenDestination(rotcBName, E, createdLegs)));

            mockRotcB
                .Setup(lst => lst.FindLegs(It.IsAny<Expression<Func<ILegDTO, bool>>>()))
                .Returns(
                    (Expression<Func<ILegDTO, bool>> p) => { return new ReadOnlyCollection<ILegDTO>(queryableB.Where(p.Compile()).ToList()); });


            mockRotcC = new Mock<IReadOnlyTravelCompany>(MockBehavior.Strict);

            foreach (var destination in allDestinations)
            {
                mockRotcC
                    .Setup(l => l.FindDepartures(destination, TransportType.Train | TransportType.Plane))
                    .Returns(new ReadOnlyCollection<ILegDTO>(getLegsFromChosenDestination(rotcCName, destination, createdLegs)));
            }
            mockRotcC
                .Setup(lst => lst.FindLegs(It.IsAny<Expression<Func<ILegDTO, bool>>>()))
                .Returns(
                    (Expression<Func<ILegDTO, bool>> p) => { return new ReadOnlyCollection<ILegDTO>(queryableC.Where(p.Compile()).ToList()); });

            rotcA = mockRotcA.Object;
            rotcB = mockRotcB.Object;
            rotcC = mockRotcC.Object;
        }


        [Test]
        public void FindTripMinHopSimpleGraphOneHopReturnOk_Plane()
        {
            planner.AddTravelCompany(rotcB);
            var trip = planner.FindTrip(A, E, FindOptions.MinimumHops, TransportType.Plane);
            ILegDTO legDTO = createdLegs[new Key(rotcBName, A, E)];
            Assert.AreEqual(legDTO.Cost, trip.TotalCost);
            Assert.AreEqual(legDTO.Distance, trip.TotalDistance);
            Assert.AreEqual(A, trip.From);
            Assert.AreEqual(E, trip.To);
            Assert.That(trip.Path, Is.EquivalentTo(new[] { legDTO }));
        }

        [Test]
        public void FindTripMinHopSimpleGraphTwoHopReturnOk_Train()
        {
            planner.AddTravelCompany(rotcA);
            var trip = planner.FindTrip(A, E, FindOptions.MinimumHops, TransportType.Train);
            ILegDTO legDTOPrimo = createdLegs[new Key(rotcAName, A, C)];
            ILegDTO legDTOSecondo = createdLegs[new Key(rotcAName, C, E)];
            Assert.AreEqual(legDTOPrimo.Cost + legDTOSecondo.Cost, trip.TotalCost);
            Assert.AreEqual(legDTOPrimo.Distance + legDTOSecondo.Distance, trip.TotalDistance);
            Assert.AreEqual(A, trip.From);
            Assert.AreEqual(E, trip.To);
            Assert.That(trip.Path, Is.EquivalentTo(new[] { legDTOPrimo, legDTOSecondo }));
        }

        [Test]
        public void FindTripMinDistanceComplexGraphReturnOk_Train()
        {
            planner.AddTravelCompany(rotcA);
            var trip = planner.FindTrip(A, E, FindOptions.MinimumDistance, TransportType.Train);
            ILegDTO legDTOPrimo = createdLegs[new Key(rotcAName, A, C)];
            ILegDTO legDTOSecondo = createdLegs[new Key(rotcAName, C, F)];
            ILegDTO legDTOTerzo = createdLegs[new Key(rotcAName, F, D)];
            ILegDTO legDTOQuarto = createdLegs[new Key(rotcAName, D, E)];
            Assert.AreEqual(legDTOPrimo.Cost + legDTOSecondo.Cost + legDTOTerzo.Cost + legDTOQuarto.Cost, trip.TotalCost);
            Assert.AreEqual(legDTOPrimo.Distance + legDTOSecondo.Distance + legDTOTerzo.Distance + legDTOQuarto.Distance, trip.TotalDistance);
            Assert.AreEqual(A, trip.From);
            Assert.AreEqual(E, trip.To);
            Assert.That(trip.Path, Is.EquivalentTo(new[] { legDTOPrimo, legDTOSecondo, legDTOTerzo, legDTOQuarto }));
        }

        [Test]
        public void FindTripMinCostComplexGraphReturnOk_Train_Plane()
        {
            planner.AddTravelCompany(rotcC);
            var trip = planner.FindTrip(A, B, FindOptions.MinimumCost, TransportType.Train | TransportType.Plane);
            ILegDTO legDTOPrimo = createdLegs[new Key(rotcCName, A, C)];
            ILegDTO legDTOSecondo = createdLegs[new Key(rotcCName, C, F)];
            ILegDTO legDTOTerzo = createdLegs[new Key(rotcCName, F, B)];
            Assert.AreEqual(legDTOPrimo.Cost + legDTOSecondo.Cost + legDTOTerzo.Cost, trip.TotalCost);
            Assert.AreEqual(legDTOPrimo.Distance + legDTOSecondo.Distance + legDTOTerzo.Distance, trip.TotalDistance);
            Assert.AreEqual(A, trip.From);
            Assert.AreEqual(B, trip.To);
            Assert.That(trip.Path, Is.EquivalentTo(new[] { legDTOPrimo, legDTOSecondo, legDTOTerzo }));
        }

        [Test]
        public void FindTripMinDistanceComplexGraphReturnOk_Train_Plane()
        {
            planner.AddTravelCompany(rotcC);
            var trip = planner.FindTrip(B, E, FindOptions.MinimumDistance, TransportType.Train | TransportType.Plane);
            ILegDTO legDTOPrimo = createdLegs[new Key(rotcCName, B, A)];
            ILegDTO legDTOSecondo = createdLegs[new Key(rotcCName, A, C)];
            ILegDTO legDTOTerzo = createdLegs[new Key(rotcCName, C, F)];
            ILegDTO legDTOQuarto = createdLegs[new Key(rotcCName, F, E)];
            Assert.AreEqual(legDTOPrimo.Cost + legDTOSecondo.Cost + legDTOTerzo.Cost + legDTOQuarto.Cost, trip.TotalCost);
            Assert.AreEqual(legDTOPrimo.Distance + legDTOSecondo.Distance + legDTOTerzo.Distance + legDTOQuarto.Distance, trip.TotalDistance);
            Assert.AreEqual(B, trip.From);
            Assert.AreEqual(E, trip.To);
            Assert.That(trip.Path, Is.EquivalentTo(new[] { legDTOPrimo, legDTOSecondo, legDTOTerzo, legDTOQuarto }));
        }

        [Test]
        public void FindTripMinHopSimpleGraphTwoHopReturnOk_Train_Plane()
        {
            planner.AddTravelCompany(rotcC);
            var trip = planner.FindTrip(B, E, FindOptions.MinimumHops, TransportType.Train | TransportType.Plane);
            ILegDTO legDTOPrimo = createdLegs[new Key(rotcCName, B, A)];
            ILegDTO legDTOSecondo = createdLegs[new Key(rotcCName, A, E)];
            Assert.AreEqual(legDTOPrimo.Cost + legDTOSecondo.Cost, trip.TotalCost);
            Assert.AreEqual(legDTOPrimo.Distance + legDTOSecondo.Distance, trip.TotalDistance);
            Assert.AreEqual(B, trip.From);
            Assert.AreEqual(E, trip.To);
            Assert.That(trip.Path, Is.EquivalentTo(new[] { legDTOPrimo, legDTOSecondo }));
        }

        [Test]
        public void FindTripMinDistanceComplexGraphReturnsOk_Train()
        {
            planner.AddTravelCompany(rotcA);
            ILegDTO legDTO = createdLegs[new Key(rotcAName, A, B)];
            var trip = planner.FindTrip(A, B, FindOptions.MinimumDistance, TransportType.Train);
            Assert.AreEqual(legDTO.Cost, trip.TotalCost);
            Assert.AreEqual(legDTO.Distance, trip.TotalDistance);
            Assert.That(trip.Path, Is.EquivalentTo(new[] { legDTO }));
        }




    }
}
