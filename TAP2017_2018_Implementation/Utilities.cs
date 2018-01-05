using System;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using TAP2017_2018_TravelCompanyInterface;
using TAP2017_2018_TravelCompanyInterface.Exceptions;

namespace TAP2017_2018_Implementation
{
    
     public static class Utilities
     {
       

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
                throw new ArgumentNullException();
            if (cs == string.Empty)
                throw new ArgumentException();
            if (cs.Length < DomainConstraints.ConnectionStringMinLength || cs.Length > DomainConstraints.ConnectionStringMaxLength)
                throw new ArgumentException();

        }

         public static void CheckTwoString(string cs1, string cs2)
         {
             if (cs1 == cs2)
                throw new SameConnectionStringException();
         }

         public static void CheckCatalog(string cs)
         {
             var csb = new SqlConnectionStringBuilder(cs);
             String initialCatalog = csb.InitialCatalog;
                Regex r = new Regex("^[a-zA-Z0-9]*$");

             if (!r.IsMatch(initialCatalog))
                 throw new ArgumentException();
        }

         public static void CheckNegative(int argument)
         {
             if (argument <= 0)
                 throw new ArgumentException();
         }

        public static Boolean IsAlphaNumeric(string strToCheck)
        {
            Regex rg = new Regex(@"^[a-zA-Z0-9]*$");
            return rg.IsMatch(strToCheck);
        }


         public static void CheckString(string s)
         {
             if (s == null)
                 throw new ArgumentNullException();
             if (s == string.Empty)
                 throw new ArgumentException();
             if (!IsAlphaNumeric(s))
                 throw new ArgumentException();
             if (s.Length < DomainConstraints.NameMinLength)
                 throw new ArgumentException();
             if (s.Length > DomainConstraints.NameMaxLength)
                 throw new ArgumentException();
        }


         public static void CheckLeg(string from, string to, int cost, int distance, TransportType transportType)
         {
             CheckTwoString(from, to);

             if ((cost <= 0) || (distance <= 0) )
                 throw new ArgumentException();
             if (transportType == TransportType.None) 
                 throw new ArgumentException();

             CheckString(from);
             CheckString(to);
                
            
        }

         public static void CheckDepartures(string from, TransportType allowedTransportTypes)
         {
             if (!IsAlphaNumeric(from))
                 throw new ArgumentException();
             CheckString(from);
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
