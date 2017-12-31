using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TAP2017_2018_TravelCompanyInterface;
using TAP2017_2018_TravelCompanyInterface.Exceptions;

namespace TAP2017_2018_Implementation
{
    
     public static class Utilities
     {
        public class LegDTO : ILegDTO
        {
            public string From { get; set; }

            public string To { get; set; }

            public int Distance { get; set; }

            public int Cost { get; set; }

            public TransportType Type { get; set;}
        }

        public static readonly Expression<Func<LegEntity, ILegDTO>> SettingToDescribedEntityDto = x => new LegDTO
        {   
            From = x.From,
            To = x.To,
            Distance = x.Distance,
            Cost = x.Cost,
            Type = x.Type
         };

        public static T FindElem<T>(this DbSet<T> table, params object[] keys) where T : class
        {
            T found = table.Find(keys);
            if (found == null)
                throw new NonexistentObjectException();
            return found;
        }

        public static void CheckConnectionString(String cs)
        {
            Regex r = new Regex("^[a-zA-Z0-9]*$");
            if (!r.IsMatch(cs))
                throw new ArgumentException("String with no Alphanumerics chars");
            if (cs == null)
                throw new ArgumentNullException("Null connectionString");
            if (cs == string.Empty)
                throw new ArgumentException("Empty ConnectionString");
            if (cs.Length < DomainConstraints.ConnectionStringMinLength || cs.Length > DomainConstraints.ConnectionStringMaxLength)
                throw new ArgumentException("Connection String is too short or too long!");

        }

        public static void CheckNull(params object[] objects)
            {
                if (objects.Any(o => o == null))
                {
                    throw new ArgumentNullException();
                }
            }
        }
    }
