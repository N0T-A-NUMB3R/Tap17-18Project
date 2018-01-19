using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace NanPlannerTests
{
    [TestFixture]
    public class NANPlannerFactoryTestSuite : NANTestInitializer
    {
        [Test]
        public void CreateNewPlannerReturnsOk()
        {
            var anObject = plannerFactory.CreateNew();
            Assert.IsEmpty(anObject.KnownTravelCompanies());

        }


    }
}
