using Ninject.Modules;
using TAP2017_2018_PlannerInterface;

namespace TAP2017_2018_PlannerImplementation.Utilities
{
    public class PlannerModules : NinjectModule
    {
        /// <summary>
        ///  /// <summary>
        /// This binding means that whenever Ninject encounters a dependency on IPlannerFactory, it will resolve an instance of 
        /// PlannerFactory and inject it.
        /// </summary>
        /// </summary>
        public override void Load() => Bind<IPlannerFactory>().To<PlannerFactory>();
    }
}
