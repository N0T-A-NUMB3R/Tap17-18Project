using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAP2017_2018_TravelCompanyInterface;

namespace TAP2017_2018_Implementation
{
    public class TravelCompanyFactory : ITravelCompanyFactory
    {
        public ITravelCompany CreateNew(string travelCompanyConnectionString, string name)
        {
            throw new NotImplementedException();
        }

        public ITravelCompany Get(string name)
        {
            throw new NotImplementedException();
        }
    }
}
    
