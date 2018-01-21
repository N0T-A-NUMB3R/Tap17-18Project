using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace NANTravelCompanyTests.Inizialiazer
{
   public  class TravelCompanyTestInizializerNan
    {
        private string TheSameCompanyName { get; set; }
        private string CompanyName { get; set; }
        private string CompanyName1 { get; set; }
        private string CompanyName2 { get; set; }


        [SetUp]
        public void SetUp()
        {
            TheSameCompanyName = "";
            CompanyName = "";
            CompanyName1 = "";
            CompanyName2 = "";
        }


    }
}
