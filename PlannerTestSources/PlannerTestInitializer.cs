using NUnit.Framework;
using System;
using Ninject;
using TAP2017_2018_PlannerInterface;

namespace TAP2017_2018_PlannerTests
{
    [TestFixture()]
    public class PlannerTestInitializer
    {
        protected IPlannerFactory plannerFactory;

        internal const string ImplementationAssemblyPlanner = @"..\..\..\TAP2017_2018_PlannerImplementation\bin\Debug\TAP2017_2018_PlannerImplementation.dll";

        protected string pointA;
        protected string pointB;

        public PlannerTestInitializer(){

            var kernelPlanner = new StandardKernel();
            try { 
                kernelPlanner.Load(ImplementationAssemblyPlanner);
            }
            catch (Exception e) { 
                Console.WriteLine(e); 
            }
            
            plannerFactory = kernelPlanner.Get<IPlannerFactory>();
            
        }


    }
}
