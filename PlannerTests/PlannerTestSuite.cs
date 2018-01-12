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

namespace TAP2017_2018_PlannerTests
{
    [TestFixture]
    public class PlannerTestSuite : PlannerTestInitializer
    {

        IPlanner planner;

        protected string A, B, C, D, E, F, unreachable;
        protected List<string> allDestinations = new List<string>(new String[]{"A","B","C","D","E","F","unreachable"});
        protected IReadOnlyTravelCompany rotcA, rotcB, rotcC;
        protected Dictionary<Key, ILegDTO> createdLegs;
        string rotcAName = "A";
        string rotcBName = "B";
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

        protected List<ILegDTO> getLegsFromChosenDestination(string rotcName, string from, Dictionary<Key, ILegDTO> dictionary) {
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
                [new Key(rotcAName, A, B)] = getMockedLegDTO(A, B, 2, 10, TransportType.Train),
                [new Key(rotcAName, B, A)] = getMockedLegDTO(B, A, 2, 10, TransportType.Train),
                [new Key(rotcAName, A, C)] = getMockedLegDTO(A, C, 1, 1, TransportType.Train),
                [new Key(rotcAName, C, A)] = getMockedLegDTO(C, A, 1, 1, TransportType.Train),
                [new Key(rotcAName, E, C)] = getMockedLegDTO(E, C, 20, 100, TransportType.Train),
                [new Key(rotcAName, C, E)] = getMockedLegDTO(C, E, 20, 100, TransportType.Train),
                [new Key(rotcAName, C, F)] = getMockedLegDTO(C, F, 3, 5, TransportType.Train),
                [new Key(rotcAName, F, D)] = getMockedLegDTO(C, F, 5, 4, TransportType.Train),
                [new Key(rotcAName, D, B)] = getMockedLegDTO(D, B, 1, 1, TransportType.Train),

                [new Key(rotcBName, A, E)] = getMockedLegDTO(A, E, 25, 100, TransportType.Plane),
                [new Key(rotcBName, A, F)] = getMockedLegDTO(A, F, 10, 100, TransportType.Plane),
                [new Key(rotcBName, F, E)] = getMockedLegDTO(F, E, 20, 45, TransportType.Plane),
                [new Key(rotcBName, F, B)] = getMockedLegDTO(F, B, 10, 3, TransportType.Plane)

            };

            var rotcAList = new List<ILegDTO>();
            var rotcBList = new List<ILegDTO>();
            foreach(var v in createdLegs){
                if (v.Key.Item1.Equals(rotcAName)) rotcAList.Add((v.Value));
                else if (v.Key.Item1.Equals(rotcBName)) rotcBList.Add((v.Value));
            }

            var queryableA = rotcAList.AsQueryable();
            var queryableB = rotcBList.AsQueryable();

            mockRotcA = new Mock<IReadOnlyTravelCompany>(MockBehavior.Strict);

            foreach(var destination in allDestinations){
                mockRotcA
                    .Setup(l => l.FindDepartures(destination, TransportType.Train))
                    .Returns(new ReadOnlyCollection<ILegDTO>(getLegsFromChosenDestination(rotcAName, destination, createdLegs)));
            }
            mockRotcA
                .Setup(lst => lst.FindLegs(It.IsAny<Expression<Func<ILegDTO, bool>>>()))
                .Returns( 
                         (Expression<Func<ILegDTO,bool>> p) => { return new ReadOnlyCollection<ILegDTO>(queryableA.Where(p.Compile()).ToList()); });

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

            mockRotcC
              .Setup(l => l.FindDepartures(It.IsAny<string>(), It.IsAny<TransportType>()))
              .Returns(new ReadOnlyCollection<ILegDTO>(new List<ILegDTO>()));
            mockRotcC
                .Setup(lst => lst.FindLegs(It.IsAny<Expression<Func<ILegDTO, bool>>>()))
                .Returns(new ReadOnlyCollection<ILegDTO>(new List<ILegDTO>()));

            rotcA = mockRotcA.Object;
            rotcB = mockRotcB.Object;
            rotcC = mockRotcC.Object;

           
        }

        [Test]
        public void AddTravelCompanyToPlannerReturnsOk()
        {
            planner.AddTravelCompany(rotcA);
            var known = new List<IReadOnlyTravelCompany>(planner.KnownTravelCompanies());
            Assert.That(known,Is.EquivalentTo(new[]{rotcA}));
        }

        [Test]
        public void AddMoreTravelCompanyToPlannerReturnsOk()
        {
            planner.AddTravelCompany(rotcA);
            planner.AddTravelCompany(rotcB);
            var known = new List<IReadOnlyTravelCompany>(planner.KnownTravelCompanies());
            Assert.That(known, Is.EquivalentTo(new[] { rotcA,rotcB }));
        }

        [Test]
        public void AddSameTravelCompanyThrowsException()
        {
            planner.AddTravelCompany(rotcA);
            Assert.Throws<TapDuplicatedObjectException>(() => planner.AddTravelCompany(rotcA));
        }

        [Test]
        public void AddNullTravelCompanyThrowsException()
        {
            Assert.Throws<ArgumentNullException>(()=>planner.AddTravelCompany(null));
        }

        [Test]
        public void KnownTravelCompanyReturnsOk()
        {
            planner.AddTravelCompany(rotcA);
            planner.AddTravelCompany(rotcB);
            planner.AddTravelCompany(rotcC);
            var known = new List<IReadOnlyTravelCompany>(planner.KnownTravelCompanies());
            Assert.That(known, Is.EquivalentTo(new[] { rotcA,rotcB,rotcC }));
        }

        public void KnownTravelCompaniesWithEmptyReturns0AndIsOk()
        {
            Assert.IsEmpty(planner.KnownTravelCompanies());
        }

        [Test]
        public void KnownTravelCompaniesExludesNotAddedTravelCompaniesReturnsOk()
        {
            planner.AddTravelCompany(rotcA);
            planner.AddTravelCompany(rotcB);
            
            var known = new List<IReadOnlyTravelCompany>(planner.KnownTravelCompanies());

            Assert.Contains(rotcA, known);
            Assert.Contains(rotcB, known);
            Assert.That(!known.Contains(rotcC));
            Assert.That(known.Count.Equals(2));
        }

        [Test]
        public void RemoveCorrectTravelCompanyReturnsOk()
        {
            planner.AddTravelCompany(rotcA);
            planner.RemoveTravelCompany(rotcA);
            var known = new List<IReadOnlyTravelCompany>(planner.KnownTravelCompanies());
            Assert.That(known.Count.Equals(0));
        }
       
        [Test]
        public void RemoveUnknownTravelCompanyThrowsException()
        {
            Assert.Throws<NonexistentTravelCompanyException>(()=>planner.RemoveTravelCompany(rotcA));
        }

        [Test]
        public void ContainsTravelCompanyReturnsOk()
        {
            planner.AddTravelCompany(rotcA);
            Assert.True(planner.ContainsTravelCompany(rotcA));
        }

        [Test]
        public void ContainsNotAddedTravelCompanyReturnsOk()
        {
            Assert.False(planner.ContainsTravelCompany(rotcA));
        }

        [Test]
        public void ContainsNullTravelCompanyThrowsException()
        {
            Assert.Throws<ArgumentNullException>(()=>planner.ContainsTravelCompany(null));
        }

        [Test]
        public void FindTripNullFromThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => planner.FindTrip(null, B, FindOptions.MinimumCost, TransportType.Bus));
        }

        [Test]
        public void FindTripNullToThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => planner.FindTrip(A, null, FindOptions.MinimumCost, TransportType.Bus));
        }

        [Test]
        public void FindTripNoneTransportTypeThrowsException()
        {
            Assert.Throws<ArgumentException>(() => planner.FindTrip(A, B, FindOptions.MinimumCost, TransportType.None));
        }

        [Test]
        public void FindTripFromTooLongThrowsException()
        {
            Assert.Throws<ArgumentException>(() => planner.FindTrip(new string('E', DomainConstraints.NameMaxLength+1), B, FindOptions.MinimumCost, TransportType.Plane));
        }

        [Test]
        public void FindTripFromTooShortThrowsException()
        {
            Assert.Throws<ArgumentException>(() => planner.FindTrip(new string('E', DomainConstraints.NameMinLength - 1), B, FindOptions.MinimumCost, TransportType.Plane));
        }

        [Test]
        public void FindTripFromNotAlphaCharThrowsException()
        {
            Assert.Throws<ArgumentException>(() => planner.FindTrip("è+èè+èèè+è", B, FindOptions.MinimumCost, TransportType.Plane));
        }

        [Test]
        public void FindTripToTooLongThrowsException()
        {
            Assert.Throws<ArgumentException>(() => planner.FindTrip(A,new string('E', DomainConstraints.NameMaxLength + 1), FindOptions.MinimumCost, TransportType.Plane));
        }

        [Test]
        public void FindTripToTooShortThrowsException()
        {
            Assert.Throws<ArgumentException>(() => planner.FindTrip(A,new string('E', DomainConstraints.NameMinLength - 1), FindOptions.MinimumCost, TransportType.Plane));
        }

        [Test]
        public void FindTripToNotAlphaCharThrowsException()
        {
            Assert.Throws<ArgumentException>(() => planner.FindTrip(A,"A+B", FindOptions.MinimumCost, TransportType.Plane));
        }

        [Test]
        public void FindTripFromEqualsToReturnsEmptyPath()
        {
            ITrip trip = planner.FindTrip(B, B, FindOptions.MinimumCost, TransportType.Bus);
            Assert.IsEmpty(trip.Path);
            Assert.AreEqual(0, trip.TotalCost);
            Assert.AreEqual(0, trip.TotalDistance);
            Assert.AreEqual(B, trip.From);
            Assert.AreEqual(B, trip.To);

        }

        [Test]
        public void FindTripMinHopSimpleGraphOneHopReturnOk()
        {
            planner.AddTravelCompany(rotcB);
            
            var trip = planner.FindTrip(A, E, FindOptions.MinimumHops, TransportType.Plane);
            ILegDTO legDTO = createdLegs[new Key(rotcBName, A, E)];
            Assert.AreEqual(legDTO.Cost, trip.TotalCost);
            Assert.AreEqual(legDTO.Distance, trip.TotalDistance);
            Assert.AreEqual(A, trip.From);
            Assert.AreEqual(E, trip.To);
            Assert.That(trip.Path,Is.EquivalentTo(new[]{legDTO}));
        }

        [Test]
        public void FindTripMinCostSimpleGraphMoreHopReturnOk()
        {

            planner.AddTravelCompany(rotcB);

            var trip = planner.FindTrip(A, B, FindOptions.MinimumCost, TransportType.Plane);
            ILegDTO legDTO1 = createdLegs[new Key(rotcBName, A, F)];
            ILegDTO legDTO2 = createdLegs[new Key(rotcBName, F, B)];
            Assert.AreEqual(legDTO1.Cost+legDTO2.Cost, trip.TotalCost);
            Assert.AreEqual(legDTO1.Distance+legDTO2.Distance, trip.TotalDistance);
            Assert.AreEqual(A, trip.From);
            Assert.AreEqual(B, trip.To);
            Assert.That(trip.Path, Is.EquivalentTo(new[] {legDTO1, legDTO2}));
        }

        [Test]
        public void FindTripMinDistanceComplexGraphReturnsOk()
        {
            planner.AddTravelCompany(rotcA);
            ILegDTO legDTO = createdLegs[new Key(rotcAName, A, B)];
            var trip = planner.FindTrip(A, B, FindOptions.MinimumDistance, TransportType.Train);
            Assert.AreEqual(legDTO.Cost, trip.TotalCost);
            Assert.AreEqual(legDTO.Distance, trip.TotalDistance);
            Assert.That(trip.Path, Is.EquivalentTo(new[] { legDTO }));
        }

        [Test]
        public void FindTripUnreachableOnComplexGraphReturnsNull()
        {
            planner.AddTravelCompany(rotcA);
            Assert.IsNull(planner.FindTrip(A, unreachable, FindOptions.MinimumCost, TransportType.Train));          
        }


    }
}
