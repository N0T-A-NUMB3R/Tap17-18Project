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
            CheckConnectionString(connectionString);
            this.connectionString = connectionString;
        }

       

        public int CreateLeg(string from, string to, int cost, int distance, TransportType transportType)
        {
            CheckDescription(planDescription);
            CheckName(planName);
            using (var context = new Context(_connectionString))
            {
                context.Settings.FindElem(scheduleSettingId);
                var plan = new Plan
                {
                    Name = planName,
                    Description = planDescription,
                    SettingId = scheduleSettingId
                };
                context.Plans.Add(plan);
                context.SaveChanges();
                return plan.PlanId;
            }
        }
       
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
