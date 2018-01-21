using Ninject.Modules;
using TAP2017_2018_Implementation.Admin;
using TAP2017_2018_Implementation.Broker;
using TAP2017_2018_Implementation.User;
using TAP2017_2018_TravelCompanyInterface;

namespace TAP2017_2018_Implementation.Utilities
{
    /// <summary>
    /// This binding means that whenever Ninject encounters a dependency on IReadOnlyTravelCompanyFactory, it will resolve an instance of 
    /// ReadOnlyTravelCompanyFactory and inject it.
    /// </summary>
    public class ReadOnlyTravelCompanyModule : NinjectModule
    {
        public override void Load() => Bind<IReadOnlyTravelCompanyFactory>().To<ReadOnlyTravelCompanyFactory>();
    }
    /// <summary>
    /// his binding means that whenever Ninject encounters a dependency on ITravelCompanybrokerFactory,
    ///  it will resolve an instance of TravelCompanyBrokerFactory and inject it.
    /// </summary>
    public class TravelCompanyBrokerModule : NinjectModule
    {
        public override void Load() => Bind<ITravelCompanyBrokerFactory>().To<TravelCompanyBrokerFactory>();
    }
    /// <summary>
    /// his binding means that whenever Ninject encounters a dependency on ITravelCompanyFactory,
    ///  it will resolve an instance of TravelCompanyFactory and inject it.
    /// </summary>
    public class TravelCompanyModule : NinjectModule
    {
        public override void Load() => Bind<ITravelCompanyFactory>().To<TravelCompanyFactory>();
    }
}