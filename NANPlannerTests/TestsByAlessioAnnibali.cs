using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NUnit.Framework;
using TAP2017_2018_PlannerInterface;
using TAP2017_2018_TravelCompanyInterface;
using Moq;
using Key = System.Tuple<string, string, string>;
using static TAP2017_2018_TravelCompanyInterface.TransportType;
using static TAP2017_2018_PlannerInterface.FindOptions;

namespace NanPlannerTests
{

 [TestFixture]

 public class TestByAlessioAnnibali : NANTestInitializer
 {
            protected IPlanner Planner;

            protected TransportType AllTransports = Train | Plane | Ship;
            protected TransportType NoPlane = Train | Ship;

            protected IReadOnlyTravelCompany Trenord, Alitalia, Costa;
            protected Mock<IReadOnlyTravelCompany> MockTrenord, MockAlitalia, MockCosta;

            protected string trenord = nameof(trenord);
            protected string alitalia = nameof(alitalia);
            protected string costa = nameof(costa);

            protected string A, B, C, D, E, F, U;
            protected List<string> Cities = new List<string>(new[] { "A", "B", "C", "D", "E", "F", "U" });

            protected Dictionary<Key, ILegDTO> CreatedLegs;

            protected ILegDTO GetMockLegDTO(string from, string to, int cost, int distance, TransportType type)
            {
                var mock = new Mock<ILegDTO>();
                mock.Setup(m => m.From).Returns(from);
                mock.Setup(m => m.To).Returns(to);
                mock.Setup(m => m.Cost).Returns(cost);
                mock.Setup(m => m.Distance).Returns(distance);
                mock.Setup(m => m.Type).Returns(type);
                return mock.Object;
            }

            protected List<ILegDTO> GetDepartures(string company, string city,TransportType allowedTransportTypes)
            {
                var departures = new List<ILegDTO>();

                foreach (var leg in CreatedLegs)
                {
                    if (leg.Key.Item1 == company && leg.Key.Item2 == city &&
                            (leg.Value.Type & allowedTransportTypes) == leg.Value.Type)
                        departures.Add(CreatedLegs[leg.Key]);
                }

                return departures;
            }


            [SetUp]
            public void SetUp()
            {
                Planner = plannerFactory.CreateNew();

                A = Cities[0];
                B = Cities[1];
                C = Cities[2];
                D = Cities[3];
                E = Cities[4];
                F = Cities[5];
                U = Cities[6];

                CreatedLegs = new Dictionary<Key, ILegDTO>
                {
                    [new Key(trenord, A, B)] = GetMockLegDTO(A, B, 5, 2, Train),
                    [new Key(alitalia, A, B)] = GetMockLegDTO(A, B, 10, 1, Plane),

                    [new Key(costa, A, F)] = GetMockLegDTO(A, F, 20, 10, Ship),

                    [new Key(alitalia, B, D)] = GetMockLegDTO(B, D, 5, 1, Plane),
                    [new Key(trenord, B, D)] = GetMockLegDTO(B, D, 4, 2, Train),

                    [new Key(alitalia, B, E)] = GetMockLegDTO(B, E, 50, 5, Plane),

                    [new Key(costa, D, F)] = GetMockLegDTO(D, F, 5, 4, Ship),

                    [new Key(trenord, A, C)] = GetMockLegDTO(A, C, 6, 3, Train),

                    [new Key(trenord, C, A)] = GetMockLegDTO(C, A, 6, 3, Train),
                    [new Key(trenord, C, E)] = GetMockLegDTO(C, E, 7, 4, Train),

                    [new Key(trenord, E, F)] = GetMockLegDTO(E, F, 8, 20, Train),
                    [new Key(alitalia, E, D)] = GetMockLegDTO(E, D, 50, 10, Plane),

                    [new Key(trenord, F, E)] = GetMockLegDTO(F, E, 4, 20, Train),
                    [new Key(costa, F, D)] = GetMockLegDTO(F, D, 5, 4, Ship)
                };


                var trenordOffers = new List<ILegDTO>();
                var alitaliaOffers = new List<ILegDTO>();
                var costaOffers = new List<ILegDTO>();

                foreach (var leg in CreatedLegs)
                {
                    if (leg.Key.Item1 == trenord)
                        trenordOffers.Add(leg.Value);
                    if (leg.Key.Item1 == alitalia)
                        alitaliaOffers.Add(leg.Value);
                    if (leg.Key.Item1 == costa)
                        costaOffers.Add(leg.Value);
                }

                var trenordLegs = trenordOffers.AsQueryable();
                var alitaliaLegs = alitaliaOffers.AsQueryable();
                var costaLegs = costaOffers.AsQueryable();


                /********************************* TRENORD ************************************/

                MockTrenord = new Mock<IReadOnlyTravelCompany>(MockBehavior.Strict);

                foreach (var city in new[] { "A", "B", "C","D", "E", "F" })
                {
                    MockTrenord
                        .Setup(m => m.FindDepartures(city, Bus))
                        .Returns(GetDepartures(trenord, city,Bus).AsReadOnly);
                    MockTrenord
                        .Setup(m => m.FindDepartures(city, Train))
                        .Returns(GetDepartures(trenord, city, Train).AsReadOnly);
                    MockTrenord
                        .Setup(m => m.FindDepartures(city, Ship))
                        .Returns(GetDepartures(trenord, city, Ship).AsReadOnly);
                    MockTrenord
                        .Setup(m => m.FindDepartures(city, Plane))
                        .Returns(GetDepartures(trenord, city, Plane).AsReadOnly);

                    MockTrenord
                        .Setup(m => m.FindDepartures(city, TransportType.Bus|Train))
                        .Returns(GetDepartures(trenord, city, TransportType.Bus|Train).AsReadOnly);

                    MockTrenord
                        .Setup(m => m.FindDepartures(city, TransportType.Bus | Plane))
                        .Returns(GetDepartures(trenord, city, TransportType.Bus | Plane).AsReadOnly);

                    MockTrenord
                        .Setup(m => m.FindDepartures(city, TransportType.Bus | Ship))
                        .Returns(GetDepartures(trenord, city, TransportType.Bus | Ship).AsReadOnly);

                    MockTrenord
                        .Setup(m => m.FindDepartures(city, TransportType.Train | Plane))
                        .Returns(GetDepartures(trenord, city, TransportType.Train | Plane).AsReadOnly);

                    MockTrenord
                        .Setup(m => m.FindDepartures(city, TransportType.Train | Ship))
                        .Returns(GetDepartures(trenord, city, TransportType.Train | Ship).AsReadOnly);

                    MockTrenord
                        .Setup(m => m.FindDepartures(city, TransportType.Plane | Ship))
                        .Returns(GetDepartures(trenord, city, TransportType.Plane | Ship).AsReadOnly);


                    MockTrenord
                        .Setup(m => m.FindDepartures(city, TransportType.Plane | Ship | Train))
                        .Returns(GetDepartures(trenord, city, TransportType.Plane | Ship | Train).AsReadOnly);
                    MockTrenord
                        .Setup(m => m.FindDepartures(city, TransportType.Plane | Ship | Bus))
                        .Returns(GetDepartures(trenord, city, TransportType.Plane | Ship | Bus).AsReadOnly);


                    MockTrenord
                        .Setup(m => m.FindDepartures(city, TransportType.Plane | Bus|Train))
                        .Returns(GetDepartures(trenord, city, TransportType.Plane | Bus | Train).AsReadOnly);

                    MockTrenord
                        .Setup(m => m.FindDepartures(city, TransportType.Bus | Ship | Train))
                        .Returns(GetDepartures(trenord, city, TransportType.Bus | Ship |Train).AsReadOnly);

                }

                MockTrenord
                    .Setup(m => m.FindLegs(It.IsAny<Expression<Func<ILegDTO, bool>>>()))
                    .Returns((Expression<Func<ILegDTO, bool>> p) =>
                        trenordLegs.AsEnumerable().Where(p.Compile()).ToList().AsReadOnly());


                /********************************* ALITALIA ************************************/

                MockAlitalia = new Mock<IReadOnlyTravelCompany>(MockBehavior.Strict);

                foreach (var city in new[] { "A", "B","C","D", "E", "F" })
                {
                    MockAlitalia
                        .Setup(m => m.FindDepartures(city, Bus))
                        .Returns(GetDepartures(alitalia, city,Bus).AsReadOnly);
                    MockAlitalia
                        .Setup(m => m.FindDepartures(city, Train))
                        .Returns(GetDepartures(alitalia, city, Train).AsReadOnly);
                    MockAlitalia
                        .Setup(m => m.FindDepartures(city, Plane))
                        .Returns(GetDepartures(alitalia, city, Plane).AsReadOnly);

                    MockAlitalia
                        .Setup(m => m.FindDepartures(city, Ship))
                        .Returns(GetDepartures(alitalia, city, Ship).AsReadOnly);


                    MockAlitalia
                        .Setup(m => m.FindDepartures(city, Bus | Train))
                        .Returns(GetDepartures(alitalia, city, Bus|Train).AsReadOnly);

                    MockAlitalia
                        .Setup(m => m.FindDepartures(city, Bus | Plane))
                        .Returns(GetDepartures(alitalia, city, Bus | Plane).AsReadOnly);

                    MockAlitalia
                        .Setup(m => m.FindDepartures(city, Bus | Ship))
                        .Returns(GetDepartures(alitalia, city, Bus | Ship).AsReadOnly);

                    MockAlitalia
                        .Setup(m => m.FindDepartures(city, Train | Plane))
                        .Returns(GetDepartures(alitalia, city, Train | Plane).AsReadOnly);

                    MockAlitalia
                        .Setup(m => m.FindDepartures(city, Train | Ship))
                        .Returns(GetDepartures(alitalia, city, Train | Ship).AsReadOnly);

                    MockAlitalia
                        .Setup(m => m.FindDepartures(city, Plane | Ship))
                        .Returns(GetDepartures(alitalia, city, Plane | Ship).AsReadOnly);

                    MockAlitalia
                        .Setup(m => m.FindDepartures(city, Plane | Ship | Train))
                        .Returns(GetDepartures(alitalia, city, Plane | Ship | Train).AsReadOnly);
                    MockAlitalia
                        .Setup(m => m.FindDepartures(city, Plane | Ship | Bus))
                        .Returns(GetDepartures(alitalia, city, Plane | Ship | Bus).AsReadOnly);
                    MockAlitalia
                        .Setup(m => m.FindDepartures(city, Train | Ship | Bus))
                        .Returns(GetDepartures(alitalia, city, Train | Ship | Bus).AsReadOnly);

                    MockAlitalia
                        .Setup(m => m.FindDepartures(city, Train | Plane | Bus))
                        .Returns(GetDepartures(alitalia, city, Train | Plane | Bus).AsReadOnly);



                }

                MockAlitalia
                    .Setup(m => m.FindLegs(It.IsAny<Expression<Func<ILegDTO, bool>>>()))
                    .Returns((Expression<Func<ILegDTO, bool>> p) =>
                        alitaliaLegs.AsEnumerable().Where(p.Compile()).ToList().AsReadOnly());


                /********************************* COSTA ************************************/

                MockCosta = new Mock<IReadOnlyTravelCompany>(MockBehavior.Strict);

                foreach (var city in new[] { "A", "B", "C", "D", "E", "F" })
                {
                    MockCosta
                        .Setup(m => m.FindDepartures(city, Bus))
                        .Returns(GetDepartures(costa, city, Bus).AsReadOnly);
                    MockCosta
                        .Setup(m => m.FindDepartures(city, Train))
                        .Returns(GetDepartures(costa, city, Train).AsReadOnly);
                    MockCosta
                        .Setup(m => m.FindDepartures(city, Plane))
                        .Returns(GetDepartures(costa, city, Plane).AsReadOnly);
                    MockCosta
                        .Setup(m => m.FindDepartures(city, Ship))
                        .Returns(GetDepartures(costa, city, Ship).AsReadOnly);


                    MockCosta
                        .Setup(m => m.FindDepartures(city, Bus | Train))
                        .Returns(GetDepartures(costa, city, Bus | Train).AsReadOnly);

                    MockCosta
                        .Setup(m => m.FindDepartures(city, Bus | Plane))
                        .Returns(GetDepartures(costa, city, Bus | Plane).AsReadOnly);

                    MockCosta
                        .Setup(m => m.FindDepartures(city, Bus | Ship))
                        .Returns(GetDepartures(costa, city, Bus | Ship).AsReadOnly);

                    MockCosta
                        .Setup(m => m.FindDepartures(city, Train | Plane))
                        .Returns(GetDepartures(costa, city, Train | Plane).AsReadOnly);

                    MockCosta
                        .Setup(m => m.FindDepartures(city, Train | Ship))
                        .Returns(GetDepartures(costa, city, Train|Ship).AsReadOnly);

                    MockCosta
                        .Setup(m => m.FindDepartures(city, Plane | Ship))
                        .Returns(GetDepartures(costa, city, Plane | Ship).AsReadOnly);

                    MockCosta
                        .Setup(m => m.FindDepartures(city, Plane | Ship | Train))
                        .Returns(GetDepartures(costa, city, Plane | Ship | Train).AsReadOnly);
                    MockCosta
                        .Setup(m => m.FindDepartures(city, Plane | Ship | Bus))
                        .Returns(GetDepartures(costa, city, Plane | Ship | Bus).AsReadOnly);

                    MockCosta
                        .Setup(m => m.FindDepartures(city, Plane | Bus | Train))
                        .Returns(GetDepartures(costa, city, Plane | Bus | Train).AsReadOnly);

                    MockCosta
                        .Setup(m => m.FindDepartures(city, Bus | Ship | Train))
                        .Returns(GetDepartures(costa, city, Bus | Ship | Train).AsReadOnly);
                }

                MockCosta
                    .Setup(m => m.FindLegs(It.IsAny<Expression<Func<ILegDTO, bool>>>()))
                    .Returns((Expression<Func<ILegDTO, bool>> p) =>
                        costaLegs.AsEnumerable().Where(p.Compile()).ToList().AsReadOnly());


                Trenord = MockTrenord.Object;
                Alitalia = MockAlitalia.Object;
                Costa = MockCosta.Object;
            }


            [Test]
            public void A_F_Train_OK()
            {
                Planner.AddTravelCompany(Trenord);

                var trip = Planner.FindTrip(A, F, MinimumHops, Train);
                var legs = new List<ILegDTO>
            {
                CreatedLegs[new Key(trenord, A, C)],
                CreatedLegs[new Key(trenord, C, E)],
                CreatedLegs[new Key(trenord, E, F)]
            };

                MakeAssertions(A, F, trip, legs);
            }


            [Test]
            public void A_F_Plane_Unreachable()
            {
                Planner.AddTravelCompany(Alitalia);
                var trip = Planner.FindTrip(A, F, MinimumHops, Plane);
                Assert.IsNull(trip);
            }


            [Test]
            public void A_F_MinDistance_PlanePlaneShip()
            {
                Planner.AddCompanies(Trenord, Alitalia, Costa);

                var trip = Planner.FindTrip(A, F, MinimumDistance, AllTransports);
                var legs = new List<ILegDTO>
            {
                CreatedLegs[new Key(alitalia, A, B)],
                CreatedLegs[new Key(alitalia, B, D)],
                CreatedLegs[new Key(costa, D, F)]
            };

                MakeAssertions(A, F, trip, legs);
            }


            [Test]
            public void B_E_MinCost_TrainShipTrain()
            {
                Planner.AddCompanies(Trenord, Alitalia, Costa);

                var trip = Planner.FindTrip(B, E, MinimumCost, AllTransports);
                var legs = new List<ILegDTO>
            {
                CreatedLegs[new Key(trenord, B, D)],
                CreatedLegs[new Key(costa, D, F)],
                CreatedLegs[new Key(trenord, F, E)]
            };

                MakeAssertions(B, E, trip, legs);
            }


            [Test]
            public void C_D_MinHops_TrainPlane()
            {
                Planner.AddCompanies(Trenord, Alitalia, Costa);

                var trip = Planner.FindTrip(C, D, MinimumHops, AllTransports);
                var legs = new List<ILegDTO>
            {
                CreatedLegs[new Key(trenord, C, E)],
                CreatedLegs[new Key(alitalia, E, D)]
            };

                MakeAssertions(C, D, trip, legs);
            }


            [Test]
            public void E_D_NoPlane_NoParty()
            {
                Planner.AddCompanies(Trenord, Costa);

                var trip = Planner.FindTrip(E, D, MinimumHops, AllTransports);
                var legs = new List<ILegDTO>
            {
                CreatedLegs[new Key(trenord, E, F)],
                CreatedLegs[new Key(costa, F, D)]
            };

                MakeAssertions(E, D, trip, legs);
            }


            [Test]
            public void B_E_MinDistance_NoPlane()
            {
                Planner.AddCompanies(Trenord, Alitalia, Costa);

                var trip = Planner.FindTrip(B, E, MinimumDistance, NoPlane);
                var legs = new List<ILegDTO>
            {
                CreatedLegs[new Key(trenord, B, D)],
                CreatedLegs[new Key(costa, D, F)],
                CreatedLegs[new Key(trenord, F, E)]
            };

                MakeAssertions(B, E, trip, legs);
            }


            [Test]
            public void A_Departures()
            {
                var departures = Trenord.FindDepartures(A, Train)
                    .Concat(Alitalia.FindDepartures(A, Plane))
                    .Concat(Costa.FindDepartures(A, Ship)).ToList();

                var legs = new List<ILegDTO>
            {
                CreatedLegs[new Key(trenord, A, B)],
                CreatedLegs[new Key(alitalia, A, B)],
                CreatedLegs[new Key(trenord, A, C)],
                CreatedLegs[new Key(costa, A, F)]
            };

                Assert.That(departures, Is.EquivalentTo(legs));
            }


            private static void MakeAssertions(string from, string to, ITrip trip, List<ILegDTO> legs)
            {
                Assert.AreEqual(legs.Sum(l => l.Cost), trip.TotalCost);
                Assert.AreEqual(legs.Sum(l => l.Distance), trip.TotalDistance);
                Assert.AreEqual(from, trip.From);
                Assert.AreEqual(to, trip.To);
                Assert.That(trip.Path, Is.EquivalentTo(legs.AsReadOnly()));
            }
        }


        public static class PlannerExtension
        {
            public static void AddCompanies(this IPlanner planner, params IReadOnlyTravelCompany[] companies)
            {
                foreach (var company in companies)
                    planner.AddTravelCompany(company);
            }
        }
    }

