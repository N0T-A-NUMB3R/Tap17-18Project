using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAP2017_2018_TravelCompanyInterface;

namespace TAP2017_2018_Implementation
{
    public class ReadOnlyTravelCompanyModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IReadOnlyTravelCompanyFactory>().To<ReadOnlyTravelCompanyFactory>();
        }
    }

    public class TravelCompanyBrokerModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ITravelCompanyBrokerFactory>().To<TravelCompanyBrokerFactory>();
        }
    }

    public class TravelCompanyModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ITravelCompanyFactory>().To<TravelCompanyFactory>();
        }
    }
}