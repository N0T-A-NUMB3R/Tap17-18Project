using NUnit.Framework;

namespace TAP2017_2018_PlannerTests
{
    [TestFixture]
    public class PlannerFactoryTestSuite : PlannerTestInitializer
    {
        [Test]
        public void CreateNewPlannerReturnsOk()
        {
            var anObject = plannerFactory.CreateNew();
            Assert.IsEmpty(anObject.KnownTravelCompanies());
            
        }


    }
}
