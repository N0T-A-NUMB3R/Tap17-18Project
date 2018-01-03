using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
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

            if (cs == null)
                throw new ArgumentNullException("Null connectionString");
            if (cs == string.Empty)
                throw new ArgumentException("Empty ConnectionString");
            if (cs.Length < DomainConstraints.ConnectionStringMinLength || cs.Length > DomainConstraints.ConnectionStringMaxLength)
                throw new ArgumentException("Connection String is too short or too long!");

        }

         public static void CheckTwoConnectionString(string cs1, string cs2)
         {
             if (cs1 == cs2)
                throw new SameConnectionStringException("the Cs of Travel Agency is equal to the Cs of the broker");
         }

         public static void CheckCatalog(string cs)
         {
             var csb = new SqlConnectionStringBuilder(cs);
             string dataSource = csb.DataSource;
             String initialCatalog = csb.InitialCatalog;
                Regex r = new Regex("^[a-zA-Z0-9]*$");

             if (!r.IsMatch(initialCatalog))
                 throw new ArgumentException("InitialCatalog with no Alphanumerics chars");
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
