using System;
using Ninject;
using NUnit.Framework;
using TAP2017_2018_PlannerInterface;

namespace NanPlannerTests
{

    [TestFixture]
    public class NANTestInitializer
    {
        protected IPlannerFactory plannerFactory;

        internal const string ImplementationAssemblyPlanner = @"..\..\..\TAP2017_2018_PlannerImplementation\bin\Debug\TAP2017_2018_PlannerImplementation.dll";

        protected string pointA;
        protected string pointB;

        public NANTestInitializer()
        {

            var kernelPlanner = new StandardKernel();
            try
            {
                kernelPlanner.Load(ImplementationAssemblyPlanner);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            plannerFactory = kernelPlanner.Get<IPlannerFactory>();

        }


    }
}
