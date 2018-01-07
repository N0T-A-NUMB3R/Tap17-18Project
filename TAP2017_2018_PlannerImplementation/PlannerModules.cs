using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
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
