using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAP2017_2018_TravelCompanyInterface;
using static TAP2017_2018_Implementation.Utilities;

namespace TAP2017_2018_Implementation
{
    class TravelCompany : ITravelCompany
    {
        public string Name { get; set; }
        private readonly string connectionString;

        public TravelCompany(string connectionString)
        {
           Utilities.CheckNull(connectionString);
            this.connectionString = connectionString;
        }

       

        public int CreateLeg(string from, string to, int cost, int distance, TransportType transportType)
        {
            throw new NotImplementedException();
            /*
            Thrown if any of the following conditions holds:
                • cost or distance are not strictly positive
                • transportType is None
                • from or to are not alphanumeric
                • from and to are equal
                AlreadyExistsException If a leg having the same values for all the parameters exists
            */
        }

        public void DeleteLeg(int legToBeRemovedId)
        {
            throw new NotImplementedException();
        }

        public ILegDTO GetLegDTOFromId(int legId)
        {
            throw new NotImplementedException();
        }
    }
}
