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
/*
G1 = grafo semplice
G2 = grafo con camminimi paralleli
G3 = Grafo semplice con cicli
G4 = Grafo con camminimi paralleli e cicli
 
*/
namespace NanPlannerTests
{
    [TestFixture]
    public class NanPlannerTestSuite : NANTestInitializer
    {

        IPlanner planner;

        protected string s,u,v,x,y;

        protected List<string> allDestinations =
            new List<string>(new String[] {"s","u","v","x","y"});

        protected IReadOnlyTravelCompany rotcG1;

        protected Dictionary<Key, ILegDTO> createdLegs;
        string G1Name = "Multy Graph with loops and parallel edges";
        

        protected Mock<IReadOnlyTravelCompany> mockRotcA;

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


       

        protected List<ILegDTO> getLegsFromChosenDestination(string rotcName, string from,
            Dictionary<Key, ILegDTO> dictionary,TransportType allowedTransportTypes)
        {
            var rotcLegs = new List<ILegDTO>();
            foreach (var k in createdLegs)
            {
                if (k.Key.Item1.Equals(rotcName) && k.Key.Item2.Equals(from) &&
                    (k.Value.Type & allowedTransportTypes) == k.Value.Type)
                    rotcLegs.Add(createdLegs[k.Key]);

            }
            return rotcLegs;
        }

        [SetUp]
        public void PlannerInit()
        {
            planner = plannerFactory.CreateNew();
            s = allDestinations[0];
            u = allDestinations[1];
            v = allDestinations[2];
            x = allDestinations[3];
            y = allDestinations[4];
            
            //my new  simple Graph
            createdLegs = new Dictionary<Key, ILegDTO>
            {
                //from s to (u and x)
                [new Key(G1Name,s,u)] = getMockedLegDTO(s,u,10,100,TransportType.Ship),
                [new Key(G1Name,u,s)] = getMockedLegDTO(s,u,100,1000,TransportType.Ship),
                [new Key(G1Name,s,x)] = getMockedLegDTO(s,x,5,50,TransportType.Bus),

                //from u to (v and x)
                [new Key(G1Name,u,v)] = getMockedLegDTO(u,v,1,10,TransportType.Plane),
                [new Key(G1Name,v, u)] = getMockedLegDTO(u, v, 2, 20, TransportType.Plane),
                [new Key(G1Name,u,x)] = getMockedLegDTO(u,x,2,20,TransportType.Bus),
                
                //from x to (u and y and v)
                [new Key(G1Name,x,u)] = getMockedLegDTO(x,u,3,30,TransportType.Bus),
                [new Key(G1Name,x,y)] = getMockedLegDTO(x,y,2,20,TransportType.Bus),
                [new Key(G1Name,x,v)] = getMockedLegDTO(x,v,9,90,TransportType.Bus),

                //from v to y
                [new Key(G1Name,v,y)] = getMockedLegDTO(v,y,4,40,TransportType.Train),

                //from y to (v and s)
                [new Key(G1Name,y,v)] = getMockedLegDTO(y,v,6,60,TransportType.Train),
                [new Key(G1Name,y,s)] = getMockedLegDTO(y,s,7,70,TransportType.Bus),
                [new Key(G1Name,s,y)] = getMockedLegDTO(y,s,100,1000,TransportType.Bus)

                
               
      
            };

           


            var rotcAList = new List<ILegDTO>();
            foreach (var v in createdLegs)
            {
                if (v.Key.Item1.Equals(G1Name)) rotcAList.Add((v.Value));
            }

            var queryableA = rotcAList.AsQueryable();

            mockRotcA = new Mock<IReadOnlyTravelCompany>(MockBehavior.Strict);


            foreach (var destination in allDestinations)
            {
                mockRotcA.Setup(l => l.FindDepartures(destination, TransportType.Bus))
                    .Returns(new ReadOnlyCollection<ILegDTO>(
                        getLegsFromChosenDestination(G1Name, destination, createdLegs,
                            TransportType.Bus)));

                mockRotcA.Setup(l => l.FindDepartures(destination, TransportType.Train))
                    .Returns(new ReadOnlyCollection<ILegDTO>(
                        getLegsFromChosenDestination(G1Name, destination, createdLegs,
                            TransportType.Train)));

                mockRotcA.Setup(l => l.FindDepartures(destination, TransportType.Ship))
                    .Returns(new ReadOnlyCollection<ILegDTO>(
                        getLegsFromChosenDestination(G1Name, destination, createdLegs,
                            TransportType.Ship)));

                mockRotcA.Setup(l => l.FindDepartures(destination, TransportType.Plane))
                    .Returns(new ReadOnlyCollection<ILegDTO>(
                        getLegsFromChosenDestination(G1Name, destination, createdLegs,
                            TransportType.Plane)));

                mockRotcA.Setup(l => l.FindDepartures(destination, TransportType.Bus | TransportType.Train))
                    .Returns(new ReadOnlyCollection<ILegDTO>(
                        getLegsFromChosenDestination(G1Name, destination, createdLegs,
                            TransportType.Bus | TransportType.Train)));

                mockRotcA.Setup(l => l.FindDepartures(destination, TransportType.Bus | TransportType.Plane))
                    .Returns(new ReadOnlyCollection<ILegDTO>(
                        getLegsFromChosenDestination(G1Name, destination, createdLegs,
                            TransportType.Bus | TransportType.Plane)));
                mockRotcA.Setup(l => l.FindDepartures(destination, TransportType.Bus | TransportType.Ship))
                    .Returns(new ReadOnlyCollection<ILegDTO>(
                        getLegsFromChosenDestination(G1Name, destination, createdLegs,
                            TransportType.Bus | TransportType.Ship)));

                mockRotcA.Setup(l => l.FindDepartures(destination, TransportType.Train | TransportType.Plane))
                    .Returns(new ReadOnlyCollection<ILegDTO>(
                        getLegsFromChosenDestination(G1Name, destination, createdLegs,
                            TransportType.Train | TransportType.Plane)));

                mockRotcA.Setup(l => l.FindDepartures(destination, TransportType.Train | TransportType.Ship))
                    .Returns(new ReadOnlyCollection<ILegDTO>(
                        getLegsFromChosenDestination(G1Name, destination, createdLegs,
                            TransportType.Train | TransportType.Ship)));

                mockRotcA.Setup(l => l.FindDepartures(destination, TransportType.Plane | TransportType.Ship))
                    .Returns(new ReadOnlyCollection<ILegDTO>(
                        getLegsFromChosenDestination(G1Name, destination, createdLegs,
                            TransportType.Plane | TransportType.Ship)));

                mockRotcA.Setup(l => l.FindDepartures(destination, TransportType.Bus | TransportType.Train | TransportType.Ship))
                    .Returns(new ReadOnlyCollection<ILegDTO>(
                        getLegsFromChosenDestination(G1Name, destination, createdLegs,
                            TransportType.Bus | TransportType.Train | TransportType.Ship)));

                mockRotcA.Setup(l => l.FindDepartures(destination, TransportType.Bus | TransportType.Train | TransportType.Plane))
                    .Returns(new ReadOnlyCollection<ILegDTO>(
                        getLegsFromChosenDestination(G1Name, destination, createdLegs,
                            TransportType.Bus | TransportType.Train | TransportType.Plane)));

                mockRotcA.Setup(l => l.FindDepartures(destination, TransportType.Bus | TransportType.Ship | TransportType.Plane))
                    .Returns(new ReadOnlyCollection<ILegDTO>(
                        getLegsFromChosenDestination(G1Name, destination, createdLegs,
                            TransportType.Bus | TransportType.Ship | TransportType.Plane)));

                mockRotcA.Setup(l => l.FindDepartures(destination, TransportType.Plane | TransportType.Train | TransportType.Ship))
                    .Returns(new ReadOnlyCollection<ILegDTO>(
                        getLegsFromChosenDestination(G1Name, destination, createdLegs,
                            TransportType.Plane | TransportType.Train | TransportType.Ship)));


            }



            

            mockRotcA
                .Setup(lst => lst.FindLegs(It.IsAny<Expression<Func<ILegDTO, bool>>>()))
                .Returns(
                    (Expression<Func<ILegDTO, bool>> p) =>
                    {
                        return new ReadOnlyCollection<ILegDTO>(queryableA.Where(p.Compile()).ToList());
                    });

         

            rotcG1 = mockRotcA.Object;


        }


        [TestCase(TransportType.Bus)]
        [TestCase(TransportType.Ship)]
        [TestCase(TransportType.Train)]
        [TestCase(TransportType.Plane)]

        [TestCase(TransportType.Bus | TransportType.Plane)]
        [TestCase(TransportType.Bus | TransportType.Ship)]
        [TestCase(TransportType.Bus | TransportType.Train)]
        [TestCase(TransportType.Train | TransportType.Ship)]
        [TestCase(TransportType.Train | TransportType.Plane)]
        [TestCase(TransportType.Ship | TransportType.Plane)]

        [TestCase(TransportType.Bus | TransportType.Train | TransportType.Ship)]
        [TestCase(TransportType.Bus | TransportType.Train | TransportType.Plane)]
        [TestCase(TransportType.Bus | TransportType.Ship | TransportType.Plane)]
        [TestCase(TransportType.Train | TransportType.Ship | TransportType.Plane)]

        public void Should_FindTrip_With_MinCost_And_OnlyOneVehicle_In_SimpleGraphMoreHopReturnOk(TransportType vehicle)
        {

            planner.AddTravelCompany(rotcG1);
            ITrip trip;
            ILegDTO sx, su, ux, uv, vy, xu, xv, xy, yv;
            sx = createdLegs[new Key(G1Name, s, x)];
            xu = createdLegs[new Key(G1Name, x, u)];
            uv = createdLegs[new Key(G1Name, u, v)];
            xv = createdLegs[new Key(G1Name, x, v)];
            su = createdLegs[new Key(G1Name, s, u)];
            yv = createdLegs[new Key(G1Name, y, v)];
            ux = createdLegs[new Key(G1Name, u, x)];
            xy = createdLegs[new Key(G1Name, x, y)];
            vy = createdLegs[new Key(G1Name, v, y)];

            switch (vehicle)
            {
                case TransportType.Bus:
                    trip = planner.FindTrip(s, v, FindOptions.MinimumCost, vehicle);
                    sx = createdLegs[new Key(G1Name, s, x)];
                    xv = createdLegs[new Key(G1Name, x, v)];
                    Assert.AreEqual(sx.Cost + xv.Cost, trip.TotalCost);
                    Assert.AreEqual(sx.Distance + xv.Distance, trip.TotalDistance);
                    Assert.AreEqual(s, trip.From);
                    Assert.AreEqual(v, trip.To);
                    Assert.That(trip.Path, Is.EquivalentTo(new[] { sx, xv }));
                    break;

                case TransportType.Ship:
                     trip = planner.FindTrip(s, u, FindOptions.MinimumCost, vehicle);
                    su = createdLegs[new Key(G1Name, s, u)];
                    Assert.AreEqual(su.Cost, trip.TotalCost);
                    Assert.AreEqual(su.Distance, trip.TotalDistance);
                    Assert.AreEqual(s, trip.From);
                    Assert.AreEqual(u, trip.To);
                    Assert.That(trip.Path, Is.EquivalentTo(new[] { su }));
                    break;

                case TransportType.Plane:
                    trip = planner.FindTrip(u, v, FindOptions.MinimumCost, vehicle);
                    uv = createdLegs[new Key(G1Name, u, v)];
                    Assert.AreEqual(uv.Cost, trip.TotalCost);
                    Assert.AreEqual(uv.Distance, trip.TotalDistance);
                    Assert.AreEqual(u, trip.From);
                    Assert.AreEqual(v, trip.To);
                    Assert.That(trip.Path, Is.EquivalentTo(new[] { uv }));
                    break;

                case TransportType.Train:
                    trip = planner.FindTrip(y, v, FindOptions.MinimumCost, vehicle);
                    yv = createdLegs[new Key(G1Name, y, v)];
                    Assert.AreEqual(yv.Cost, trip.TotalCost);
                    Assert.AreEqual(yv.Distance, trip.TotalDistance);
                    Assert.AreEqual(y, trip.From);
                    Assert.AreEqual(v, trip.To);
                    Assert.That(trip.Path, Is.EquivalentTo(new[] { yv }));

                    break;

                case TransportType.Bus | TransportType.Plane:
                    trip = planner.FindTrip(s, v, FindOptions.MinimumCost, vehicle);
                    sx = createdLegs[new Key(G1Name, s, x)];
                    xu = createdLegs[new Key(G1Name, x, u)];
                    uv = createdLegs[new Key(G1Name, u, v)];
                    Assert.That(sx.Cost + xu.Cost + uv.Cost , Is.EqualTo(trip.TotalCost));
                    Assert.That(sx.Distance + xu.Distance + uv.Distance, Is.EqualTo(trip.TotalDistance));
                    Assert.That(s, Is.EqualTo(trip.From));
                    Assert.That(v,Is.EqualTo(trip.To));
                    Assert.That(trip.Path,Is.EquivalentTo(new[]{sx,xu,uv}));

                    break;

                case TransportType.Bus | TransportType.Ship:
                    trip = planner.FindTrip(s, v, FindOptions.MinimumCost, vehicle);
                    Assert.That(sx.Cost + xv.Cost, Is.EqualTo(trip.TotalCost));
                    Assert.That(sx.Distance + xv.Distance, Is.EqualTo(trip.TotalDistance));
                    Assert.That(s, Is.EqualTo(trip.From));
                    Assert.That(v, Is.EqualTo(trip.To));
                    Assert.That(trip.Path, Is.EquivalentTo(new[] { sx, xv }));

                    break;


                case TransportType.Bus | TransportType.Train:
                    trip = planner.FindTrip(u, v, FindOptions.MinimumCost, vehicle);
                    Assert.That(ux.Cost + xy.Cost + yv.Cost, Is.EqualTo(trip.TotalCost));
                    Assert.That(ux.Distance + xy.Distance + yv.Distance, Is.EqualTo(trip.TotalDistance));
                    Assert.That(u, Is.EqualTo(trip.From));
                    Assert.That(v, Is.EqualTo(trip.To));
                    Assert.That(trip.Path, Is.EquivalentTo(new[] { ux, xy, yv }));

                    break;

                case TransportType.Train | TransportType.Ship:
                    trip = planner.FindTrip(s, y, FindOptions.MinimumCost, vehicle);
                    Assert.That(trip, Is.Null);
                    break;

                case TransportType.Train | TransportType.Plane:
                    trip = planner.FindTrip(u, y, FindOptions.MinimumCost, vehicle);
                    Assert.That(uv.Cost + vy.Cost, Is.EqualTo(trip.TotalCost));
                    Assert.That(uv.Distance + vy.Distance, Is.EqualTo(trip.TotalDistance));
                    Assert.That(u, Is.EqualTo(trip.From));
                    Assert.That(y, Is.EqualTo(trip.To));
                    Assert.That(trip.Path, Is.EquivalentTo(new[] { uv, vy }));
                    break;

                case TransportType.Ship | TransportType.Plane:
                    trip = planner.FindTrip(s, v, FindOptions.MinimumCost, vehicle);
                    Assert.That(su.Cost + uv.Cost, Is.EqualTo(trip.TotalCost));
                    Assert.That(su.Distance + uv.Distance, Is.EqualTo(trip.TotalDistance));
                    Assert.That(s, Is.EqualTo(trip.From));
                    Assert.That(v, Is.EqualTo(trip.To));
                    Assert.That(trip.Path, Is.EquivalentTo(new[] { su, uv }));
                    break;

                case TransportType.Bus | TransportType.Train | TransportType.Ship:
                    trip = planner.FindTrip(s, v, FindOptions.MinimumCost, vehicle);
                    Assert.That(s, Is.EqualTo(trip.From));
                    Assert.That(v, Is.EqualTo(trip.To));
                    Assert.That(sx.Cost + xy.Cost + yv.Cost, Is.EqualTo(trip.TotalCost));
                    Assert.That(sx.Distance + xy.Distance + yv.Distance, Is.EqualTo(trip.TotalDistance));
                    Assert.That(trip.Path, Is.EquivalentTo(new[] { sx, xy, yv }));
                    break;

                case TransportType.Bus | TransportType.Train | TransportType.Plane:
                    trip = planner.FindTrip(s, v, FindOptions.MinimumCost, vehicle);
                    Assert.That(s, Is.EqualTo(trip.From));
                    Assert.That(v, Is.EqualTo(trip.To));
                    Assert.That(sx.Cost + xu.Cost + uv.Cost, Is.EqualTo(trip.TotalCost));
                    Assert.That(sx.Distance + xu.Distance + uv.Distance, Is.EqualTo(trip.TotalDistance));
                    Assert.That(trip.Path, Is.EquivalentTo(new[] { sx, xu, uv }));
                    break;
                case TransportType.Bus | TransportType.Ship | TransportType.Plane:
                    trip = planner.FindTrip(s, v, FindOptions.MinimumCost, vehicle);
                    Assert.That(s, Is.EqualTo(trip.From));
                    Assert.That(v, Is.EqualTo(trip.To));
                    Assert.That(sx.Cost + xu.Cost + uv.Cost, Is.EqualTo(trip.TotalCost));
                    Assert.That(sx.Distance + xu.Distance + uv.Distance, Is.EqualTo(trip.TotalDistance));
                    Assert.That(trip.Path, Is.EquivalentTo(new[] { sx, xu, uv }));
                    break;
                case TransportType.Train | TransportType.Ship | TransportType.Plane:
                    trip = planner.FindTrip(v, u, FindOptions.MinimumCost, vehicle);
                    Assert.That(trip, Is.Null);
                    break;
            }

        }


        [TestCase(TransportType.Bus)]
        [TestCase(TransportType.Ship)]
        [TestCase(TransportType.Train)]
        [TestCase(TransportType.Plane)]
        [TestCase(TransportType.Bus | TransportType.Plane)]
        [TestCase(TransportType.Bus | TransportType.Ship)]
        [TestCase(TransportType.Bus | TransportType.Train)]
        [TestCase(TransportType.Train | TransportType.Ship)]
        [TestCase(TransportType.Train | TransportType.Plane)]
        [TestCase(TransportType.Ship | TransportType.Plane)]

        [TestCase(TransportType.Bus | TransportType.Train | TransportType.Ship)]
        [TestCase(TransportType.Bus | TransportType.Train | TransportType.Plane)]
        [TestCase(TransportType.Bus | TransportType.Ship | TransportType.Plane)]
        [TestCase(TransportType.Train | TransportType.Ship | TransportType.Plane)]

        public void Should_FindTrip_With_MinDistance_And_OnlyOneVehicle_In_SimpleGraphMoreHopReturnOk(TransportType vehicle)
        {

            planner.AddTravelCompany(rotcG1);
            ITrip trip;
            ILegDTO sx, su,ux, uv, vy ,xu, xv,xy, yv;
            sx = createdLegs[new Key(G1Name, s, x)];
            xu = createdLegs[new Key(G1Name, x, u)];
            uv = createdLegs[new Key(G1Name, u, v)];
            xv = createdLegs[new Key(G1Name, x, v)];
            su = createdLegs[new Key(G1Name, s, u)];
            yv = createdLegs[new Key(G1Name, y, v)];
            ux = createdLegs[new Key(G1Name, u, x)];
            xy = createdLegs[new Key(G1Name, x, y)];
            vy = createdLegs[new Key(G1Name, v, y)];


            switch (vehicle)
            {
                case TransportType.Bus:
                    trip = planner.FindTrip(s, v, FindOptions.MinimumDistance, vehicle);
                    
                    Assert.AreEqual(sx.Cost + xv.Cost, trip.TotalCost);
                    Assert.AreEqual(sx.Distance + xv.Distance, trip.TotalDistance);
                    Assert.AreEqual(s, trip.From);
                    Assert.AreEqual(v, trip.To);
                    Assert.That(trip.Path, Is.EquivalentTo(new[] { sx, xv }));
                    break;
                case TransportType.Ship:
                    trip = planner.FindTrip(s, u, FindOptions.MinimumDistance, vehicle);
                   
                    Assert.AreEqual(su.Cost, trip.TotalCost);
                    Assert.AreEqual(su.Distance, trip.TotalDistance);
                    Assert.AreEqual(s, trip.From);
                    Assert.AreEqual(u, trip.To);
                    Assert.That(trip.Path, Is.EquivalentTo(new[] { su }));
                    break;
                case TransportType.Plane:
                    trip = planner.FindTrip(u, v, FindOptions.MinimumDistance, vehicle);
                    Assert.AreEqual(uv.Cost, trip.TotalCost);
                    Assert.AreEqual(uv.Distance, trip.TotalDistance);
                    Assert.AreEqual(u, trip.From);
                    Assert.AreEqual(v, trip.To);
                    Assert.That(trip.Path, Is.EquivalentTo(new[] { uv }));
                    break;
                case TransportType.Train:
                    trip = planner.FindTrip(y, v, FindOptions.MinimumDistance, vehicle);
                    Assert.AreEqual(yv.Cost, trip.TotalCost);
                    Assert.AreEqual(yv.Distance, trip.TotalDistance);
                    Assert.AreEqual(y, trip.From);
                    Assert.AreEqual(v, trip.To);
                    Assert.That(trip.Path, Is.EquivalentTo(new[] { yv }));
                    break;
                case TransportType.Bus | TransportType.Plane:
                    trip = planner.FindTrip(s, v, FindOptions.MinimumDistance, vehicle);
                 
                    Assert.That(sx.Cost + xu.Cost + uv.Cost, Is.EqualTo(trip.TotalCost));
                    Assert.That(sx.Distance + xu.Distance + uv.Distance, Is.EqualTo(trip.TotalDistance));
                    Assert.That(s, Is.EqualTo(trip.From));
                    Assert.That(v, Is.EqualTo(trip.To));
                    Assert.That(trip.Path, Is.EquivalentTo(new[] { sx, xu, uv }));
                    break;

                case TransportType.Bus | TransportType.Ship:
                    trip = planner.FindTrip(s, v, FindOptions.MinimumDistance, vehicle);
                    Assert.That(sx.Cost + xv.Cost, Is.EqualTo(trip.TotalCost));
                    Assert.That(sx.Distance + xv.Distance , Is.EqualTo(trip.TotalDistance));
                    Assert.That(s, Is.EqualTo(trip.From));
                    Assert.That(v, Is.EqualTo(trip.To));
                    Assert.That(trip.Path, Is.EquivalentTo(new[] { sx, xv }));

                    break;


                case TransportType.Bus | TransportType.Train:
                    trip = planner.FindTrip(u, v, FindOptions.MinimumDistance, vehicle);
                    Assert.That(ux.Cost + xy.Cost + yv.Cost, Is.EqualTo(trip.TotalCost));
                    Assert.That(ux.Distance + xy.Distance +yv.Distance, Is.EqualTo(trip.TotalDistance));
                    Assert.That(u, Is.EqualTo(trip.From));
                    Assert.That(v, Is.EqualTo(trip.To));
                    Assert.That(trip.Path, Is.EquivalentTo(new[] {ux,xy,yv }));

                    break;

                case TransportType.Train | TransportType.Ship:
                    trip = planner.FindTrip(s, y, FindOptions.MinimumDistance, vehicle);
                    Assert.That(trip, Is.Null);
                    break;

                case TransportType.Train | TransportType.Plane:
                    trip = planner.FindTrip(u, y, FindOptions.MinimumDistance, vehicle);
                    Assert.That(uv.Cost + vy.Cost, Is.EqualTo(trip.TotalCost));
                    Assert.That(uv.Distance + vy.Distance , Is.EqualTo(trip.TotalDistance));
                    Assert.That(u, Is.EqualTo(trip.From));
                    Assert.That(y, Is.EqualTo(trip.To));
                    Assert.That(trip.Path, Is.EquivalentTo(new[] { uv,vy }));
                    break;

                case TransportType.Ship | TransportType.Plane:
                    trip = planner.FindTrip(s, v, FindOptions.MinimumDistance, vehicle);
                    Assert.That(su.Cost + uv.Cost, Is.EqualTo(trip.TotalCost));
                    Assert.That(su.Distance + uv.Distance, Is.EqualTo(trip.TotalDistance));
                    Assert.That(s, Is.EqualTo(trip.From));
                    Assert.That(v, Is.EqualTo(trip.To));
                    Assert.That(trip.Path, Is.EquivalentTo(new[] {su,uv }));
                    break;


                case TransportType.Bus | TransportType.Train | TransportType.Ship:
                    trip = planner.FindTrip(s, v, FindOptions.MinimumDistance, vehicle);
                    Assert.That(s, Is.EqualTo(trip.From));
                    Assert.That(v, Is.EqualTo(trip.To));
                    Assert.That(sx.Cost + xy.Cost + yv.Cost, Is.EqualTo(trip.TotalCost));
                    Assert.That(sx.Distance + xy.Distance + yv.Distance, Is.EqualTo(trip.TotalDistance));
                    Assert.That(trip.Path, Is.EquivalentTo(new[] { sx, xy,yv }));
                    break;

                case TransportType.Bus | TransportType.Train | TransportType.Plane:
                    trip = planner.FindTrip(s, v, FindOptions.MinimumDistance, vehicle);
                    Assert.That(s, Is.EqualTo(trip.From));
                    Assert.That(v, Is.EqualTo(trip.To));
                    Assert.That(sx.Cost + xu.Cost + uv.Cost, Is.EqualTo(trip.TotalCost));
                    Assert.That(sx.Distance + xu.Distance + uv.Distance, Is.EqualTo(trip.TotalDistance));
                    Assert.That(trip.Path, Is.EquivalentTo(new[] { sx, xu,uv }));
                    break;
                case TransportType.Bus | TransportType.Ship | TransportType.Plane:
                    trip = planner.FindTrip(s, v, FindOptions.MinimumDistance, vehicle);
                    Assert.That(s, Is.EqualTo(trip.From));
                    Assert.That(v, Is.EqualTo(trip.To));
                    Assert.That(sx.Cost + xu.Cost + uv.Cost, Is.EqualTo(trip.TotalCost));
                    Assert.That(sx.Distance + xu.Distance+ uv.Distance, Is.EqualTo(trip.TotalDistance));
                    Assert.That(trip.Path, Is.EquivalentTo(new[] { sx,xu,uv }));
                    break;
                case TransportType.Train | TransportType.Ship | TransportType.Plane:
                    trip = planner.FindTrip(v, u, FindOptions.MinimumDistance, vehicle);
                    Assert.That(trip, Is.Null);
                    break;

            }

        }


        [TestCase(TransportType.Bus)]
        [TestCase(TransportType.Ship)]
        [TestCase(TransportType.Train)]
        [TestCase(TransportType.Plane)]

        [TestCase(TransportType.Bus | TransportType.Plane)]
        [TestCase(TransportType.Bus | TransportType.Ship)]
        [TestCase(TransportType.Bus | TransportType.Train)]
        [TestCase(TransportType.Train | TransportType.Ship)]
        [TestCase(TransportType.Train | TransportType.Plane)]
        [TestCase(TransportType.Ship|TransportType.Plane)]

        [TestCase(TransportType.Bus | TransportType.Train | TransportType.Ship)]
        [TestCase(TransportType.Bus | TransportType.Train | TransportType.Plane)]
        [TestCase(TransportType.Bus | TransportType.Ship | TransportType.Plane)]
        [TestCase(TransportType.Train | TransportType.Ship | TransportType.Plane)]

        public void Should_FindTrip_With_MinHops_And_OnlyOneVehicle_In_SimpleGraphMoreHopReturnOk(TransportType vehicle)
        {

            planner.AddTravelCompany(rotcG1);
            ITrip trip;
            ILegDTO sx, su, ux, uv, vy, xu, xv, xy, yv ,ys;
            sx = createdLegs[new Key(G1Name, s, x)];
            xu = createdLegs[new Key(G1Name, x, u)];
            uv = createdLegs[new Key(G1Name, u, v)];
            xv = createdLegs[new Key(G1Name, x, v)];
            su = createdLegs[new Key(G1Name, s, u)];
            yv = createdLegs[new Key(G1Name, y, v)];
            ux = createdLegs[new Key(G1Name, u, x)];
            xy = createdLegs[new Key(G1Name, x, y)];
            vy = createdLegs[new Key(G1Name, v, y)];
            ys = createdLegs[new Key(G1Name, y, s)];

            switch (vehicle)
            {
                case TransportType.Bus:
                    trip = planner.FindTrip(s, v, FindOptions.MinimumHops, vehicle);
                    Assert.AreEqual(s, trip.From);
                    Assert.AreEqual(v, trip.To);
                    Assert.That(trip.Path, Is.EquivalentTo(new[] { sx, xv }));
                    break;
                case TransportType.Ship:
                    trip = planner.FindTrip(s, u, FindOptions.MinimumHops, vehicle);
                    Assert.AreEqual(s, trip.From);
                    Assert.AreEqual(u, trip.To);
                    Assert.That(trip.Path, Is.EquivalentTo(new[] { su }));
                    break;
                case TransportType.Plane:
                    trip = planner.FindTrip(u, v, FindOptions.MinimumHops, vehicle);
                    Assert.AreEqual(u, trip.From);
                    Assert.AreEqual(v, trip.To);
                    Assert.That(trip.Path, Is.EquivalentTo(new[] { uv }));
                    break;
                case TransportType.Train:
                    trip = planner.FindTrip(y, v, FindOptions.MinimumHops, vehicle);
                   
                    Assert.AreEqual(y, trip.From);
                    Assert.AreEqual(v, trip.To);
                    Assert.That(trip.Path, Is.EquivalentTo(new[] { yv }));
                    break;
                    //
                case TransportType.Bus | TransportType.Plane:
                    trip = planner.FindTrip(s, v, FindOptions.MinimumHops, vehicle);
                    
                    Assert.That(s, Is.EqualTo(trip.From));
                    Assert.That(v, Is.EqualTo(trip.To));
                    Assert.That(trip.Path, Is.EquivalentTo(new[] { sx, xv }));
                    break;

                case TransportType.Bus | TransportType.Ship:
                    trip = planner.FindTrip(s, v, FindOptions.MinimumHops, vehicle);

                    Assert.That(s, Is.EqualTo(trip.From));
                    Assert.That(v, Is.EqualTo(trip.To));
                    Assert.That(trip.Path, Is.EquivalentTo(new[] { sx, xv }));
                    break;

                case TransportType.Bus | TransportType.Train:
                    trip = planner.FindTrip(s, v, FindOptions.MinimumHops, vehicle);

                    Assert.That(s, Is.EqualTo(trip.From));
                    Assert.That(v, Is.EqualTo(trip.To));
                    Assert.That(trip.Path, Is.EquivalentTo(new[] { sx, xv }));
                    break;

                case TransportType.Train | TransportType.Ship:
                    trip = planner.FindTrip(s, y, FindOptions.MinimumCost, vehicle);
                    Assert.That(trip, Is.Null);
                    break;

                case TransportType.Train | TransportType.Plane:
                    trip = planner.FindTrip(u, y, FindOptions.MinimumHops, vehicle);
                    Assert.That(u, Is.EqualTo(trip.From));
                    Assert.That(y, Is.EqualTo(trip.To));
                    Assert.That(trip.Path, Is.EquivalentTo(new[] { uv, vy }));
                    break;

                case TransportType.Ship | TransportType.Plane:
                    trip = planner.FindTrip(s, v, FindOptions.MinimumHops, vehicle);
                    Assert.That(s, Is.EqualTo(trip.From));
                    Assert.That(v, Is.EqualTo(trip.To));
                    Assert.That(trip.Path, Is.EquivalentTo(new[] { su,uv }));
                    break;

                case TransportType.Bus | TransportType.Train | TransportType.Ship:
                    trip = planner.FindTrip(s, v, FindOptions.MinimumHops, vehicle);
                    Assert.That(s, Is.EqualTo(trip.From));
                    Assert.That(v, Is.EqualTo(trip.To));
                    Assert.That(trip.Path, Is.EquivalentTo(new[] { sx,xv }));
                    break;

                case TransportType.Bus | TransportType.Train | TransportType.Plane:
                    trip = planner.FindTrip(s, v, FindOptions.MinimumHops, vehicle);
                    Assert.That(s, Is.EqualTo(trip.From));
                    Assert.That(v, Is.EqualTo(trip.To));
                    Assert.That(trip.Path, Is.EquivalentTo(new[] { sx,xv }));
                    break;
                case TransportType.Bus | TransportType.Ship | TransportType.Plane:
                    trip = planner.FindTrip(s, v, FindOptions.MinimumHops, vehicle);
                    Assert.That(s, Is.EqualTo(trip.From));
                    Assert.That(v, Is.EqualTo(trip.To));
                    Assert.That(trip.Path, Is.EquivalentTo(new[] { su, uv }));
                    break;
                case TransportType.Train | TransportType.Ship | TransportType.Plane:
                    trip = planner.FindTrip(v, u, FindOptions.MinimumHops, vehicle);
                    Assert.That(trip, Is.Null);
                    break;
            }

        }


        

   




    }
}
