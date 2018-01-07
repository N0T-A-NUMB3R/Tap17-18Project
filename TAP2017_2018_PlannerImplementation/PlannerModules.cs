using System;
using Ninject.Modules;
using TAP2017_2018_PlannerInterface;

namespace TAP2017_2018_PlannerImplementation
{
    public class PlannerModules : NinjectModule
    {
        public override void Load()
        {
            Bind<IPlannerFactory>().To<PlannerFactory>();
        }
    }
}
