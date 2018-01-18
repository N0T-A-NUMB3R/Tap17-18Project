﻿using System;
using System.Collections.Generic;
using System.Text;
using Ninject;
using NUnit.Framework;
using TAP2017_2018_TravelCompanyInterface;

namespace TravelCompanyTestsNAN
{

        [TestFixture()]
        public class BasicTestInitializer
        {

            protected ITravelCompanyFactory travelCompanyFactory;
            protected IReadOnlyTravelCompanyFactory readOnlyTravelCompanyFactory;
            protected ITravelCompanyBrokerFactory brokerFactory;

            protected string AllTravelCompaniesConnectionString = BasicTestInitializer.CreateConnectionString("ALLTC");
            protected string ExampleConnectionString = BasicTestInitializer.CreateConnectionString("TCITravel");
            protected string ExampleConnectionString2 = BasicTestInitializer.CreateConnectionString("differentTC2");
            protected string ExampleName = "TestTravelCompany";
            internal const string ImplementationAssembly = @"..\..\..\TAP2017_2018_Implementation\bin\Debug\TAP2017_2018_Implementation.dll";

            public BasicTestInitializer()
            {
                var kernel = new StandardKernel();
                try { kernel.Load(ImplementationAssembly); }
                catch (Exception e) { Console.WriteLine(e); }
                brokerFactory = kernel.Get<ITravelCompanyBrokerFactory>();
            }

            protected static string CreateConnectionString(string catalogName)
            {
                return String.Format(@"Server=.\SQLEXPRESS;Initial Catalog={0};Integrated Security=SSPI; MultipleActiveResultSets=True", catalogName);
            }

        }
    }
