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

         public static void CheckNegative(int argument)
         {
             if (argument <= 0)
                 throw new ArgumentException("l' intero è negativo");
         }

        public static Boolean isAlphaNumeric(string strToCheck)
        {
            Regex rg = new Regex(@"^[a-zA-Z0-9]*$");
            return rg.IsMatch(strToCheck);
        }

         public static void CheckName(string name)
         {
             if (name == null)
                 throw new ArgumentNullException("Name is Null");
             if (name == string.Empty)
                 throw new ArgumentException("Name is Empty");
             if (!isAlphaNumeric(name))
                 throw new ArgumentException("Name isnt Alphanumerics");
             if (name.Length < DomainConstraints.NameMinLength)
                 throw new ArgumentException("Name is too Short");
             if (name.Length > DomainConstraints.NameMaxLength)
                 throw new ArgumentException("Name is too Long");
        }



         public static void CheckLeg(string from, string to, int cost, int distance, TransportType transportType)
         {
             if ((cost <= 0) || (distance <= 0) )
                 throw new ArgumentException("cost or distance is <= 0");
             if (transportType == TransportType.None) 
                 throw new ArgumentException("Trasport Type is null");
             if ((!isAlphaNumeric(to)) || (!isAlphaNumeric(from))) 
                 throw new ArgumentException("From or To isnt Alphanumerics");
             if (from == to)
                 throw new ArgumentException("From is equal To");
             if ((from == null) || (to == null))
                 throw new ArgumentNullException("To or From is null");
             if (( from == string.Empty)||(to == string.Empty))
                 throw new ArgumentException("Empty string");
             if ((from.Length > TAP2017_2018_TravelCompanyInterface.DomainConstraints.NameMaxLength) || (to.Length > TAP2017_2018_TravelCompanyInterface.DomainConstraints.NameMaxLength))
                 throw new ArgumentException("From or To is too long");
             if ((from.Length < TAP2017_2018_TravelCompanyInterface.DomainConstraints.NameMinLength) ||
                  (to.Length < TAP2017_2018_TravelCompanyInterface.DomainConstraints.NameMinLength))
                 throw new ArgumentException("From or To is too short");
        }

         public static void CheckDepartures(string from, TransportType allowedTransportTypes)
         {
             if (!isAlphaNumeric(from))
                 throw new ArgumentException("From isnt Alphanumerics");
             if (from == null)
                 throw new ArgumentNullException("From is Null");
             if (from == string.Empty)
                 throw new ArgumentException("From is Empty");
             if (from.Length < DomainConstraints.NameMinLength)
                 throw new ArgumentException("From is too Short");
             if (from.Length > DomainConstraints.NameMaxLength)
                 throw new ArgumentException("From is too Long");
             if (allowedTransportTypes == TransportType.None)
                 throw new ArgumentException("Trasport Type is null");
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
