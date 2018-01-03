using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAP2017_2018_TravelCompanyInterface;

namespace TAP2017_2018_Implementation
{
    public class ReadOnlyTravelCompanyFactory : IReadOnlyTravelCompanyFactory
    {
        private string dBCONNECTION;
        
        public ReadOnlyTravelCompanyFactory(string dBCONNECTION)
        {
            this.dBCONNECTION = dBCONNECTION;
        }
        
        public IReadOnlyTravelCompany Get(string name)
        {
            Utilities.CheckName(name);

            using (var c = new BrokerContext(dBCONNECTION))
            {
                var existentTc = c.TravelCompanies.Single(tc => tc.Name == name);

                return new ReadOnlyTravelCompany(existentTc.ConnectionString);
            }
        }
    }
}

