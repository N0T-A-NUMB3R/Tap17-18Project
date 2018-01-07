﻿using System;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using TAP2017_2018_TravelCompanyInterface;
using TAP2017_2018_TravelCompanyInterface.Exceptions;

namespace TAP2017_2018_Implementation
{
    
     internal static class Checker
     {
        public static void CheckConnectionString(string cs)
        { 
            if (cs == null)
                throw new ArgumentNullException();
            if (cs == string.Empty)
                throw new ArgumentException();
            if (cs.Length < DomainConstraints.ConnectionStringMinLength || cs.Length > DomainConstraints.ConnectionStringMaxLength)
                throw new ArgumentException();
        }

        public static void CheckTwoConnString(string cs1, string cs2)
        {
            if (cs1 == cs2)
                throw new SameConnectionStringException();
        }
         public static void CheckTwoString(string s1, string s2)
         {
             if (s1 == s2)
                 throw new ArgumentException();
         }

        public static void CheckCatalog(string cs)
         {
             var csb = new SqlConnectionStringBuilder(cs);
             String initialCatalog = csb.InitialCatalog;
                Regex r = new Regex("^[a-zA-Z0-9]*$");

             if (!r.IsMatch(initialCatalog))
                 throw new ArgumentException();
        }

         public static bool CheckNegative(int argument)
         {
             return argument <= 0;
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
            CheckTwoString(from,to);
            CheckString(from);
            CheckString(to);
            if (CheckNegative(cost) || CheckNegative(distance))
                throw new ArgumentException();
            if (transportType == TransportType.None)
                throw new ArgumentException();
        }

         public static void CheckDepartures(string from, TransportType allowedTransportTypes)
         {
            CheckString(from);
            if (allowedTransportTypes == TransportType.None)
                throw new ArgumentException();
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