using System;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using TAP2017_2018_Implementation.Persistent_Layer;
using TAP2017_2018_TravelCompanyInterface;
using TAP2017_2018_TravelCompanyInterface.Exceptions;

namespace TAP2017_2018_Implementation.Utilities
{

    internal static class Checker
    {
        /// <summary>
        /// Auxiliary method that controls  if the connection string is null, empty or does not respect the constraints
        /// </summary>
        /// <param name="cs"></param>
        public static void CheckConnectionString(string cs)
        {
            if (cs == null)
                throw new ArgumentNullException();
            if (cs == String.Empty)
                throw new ArgumentException();
            if (cs.Length < DomainConstraints.ConnectionStringMinLength || cs.Length > DomainConstraints.ConnectionStringMaxLength)
                throw new ArgumentException();
        }
        /// <summary>
        /// Auxiliary method that controls  if two connection string are equal 
        /// </summary>
        /// <param name="cs1"></param>
        /// <param name="cs2"></param>
        public static void CheckTwoConnString(string cs1, string cs2)
        {
            if (cs1 == cs2)
                throw new SameConnectionStringException();
        }
        /// <summary>
        /// Auxiliary method that controls  if two  string(from, to, Tc names) are equal 
        /// </summary>
        /// <param name="s1"></param>
        /// <param name="s2"></param>
        public static void CheckTwoStringEquals(string s1, string s2)
        {
            if (s1 == s2)
                throw new ArgumentException();
        }
        /// <summary>
        /// Auxiliary method that controls  if the catalog of connection string is alphanumeric
        /// </summary>
        /// <param name="cs"></param>
        public static void CheckCatalog(string cs)
        {
            var csb = new SqlConnectionStringBuilder(cs);
            String initialCatalog = csb.InitialCatalog;
            Regex r = new Regex("^[a-zA-Z0-9]*$");

            if (!r.IsMatch(initialCatalog))
                throw new ArgumentException();
        }
        /// <summary>
        /// Auxiliary method that controls  if the input integer is negative
        /// </summary>
        /// <param name="argument"></param>
        /// <returns></returns>
        public static bool CheckNegative(int argument)
        {
            return argument <= 0;
        }
        /// <summary>
        /// Auxiliary method that controls  if the  string is alphanumeric
        /// </summary>
        /// <param name="strToCheck"></param>
        /// <returns></returns>
        public static Boolean IsAlphaNumeric(string strToCheck)
        {
            Regex rg = new Regex(@"^[a-zA-Z0-9]*$");
            return rg.IsMatch(strToCheck);
        }
        /// <summary>
        /// Auxiliary method that controls  if the string is null, empty, alphanumerics or does not respect the constraints
        /// </summary>
        /// <param name="s"></param>
        public static void CheckName(string s)
        {
            if (s == null)
                throw new ArgumentNullException();
            if (s == String.Empty)
                throw new ArgumentException();
            if (!IsAlphaNumeric(s))
                throw new ArgumentException();
            if (s.Length < DomainConstraints.NameMinLength)
                throw new ArgumentException();
            if (s.Length > DomainConstraints.NameMaxLength)
                throw new ArgumentException();
        }

        /// <summary>
        /// Auxiliary method that controls  if the strings are null, empty, alphanumerics or does not respect the constraints, the int is positive and trasport
        /// isnt none
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="cost"></param>
        /// <param name="distance"></param>
        /// <param name="transportType"></param>
        public static void CheckLeg(string from, string to, int cost, int distance, TransportType transportType)
        {
            CheckTwoString(from, to);
            if (CheckNegative(cost) || CheckNegative(distance))
                throw new ArgumentException();
            if (transportType == TransportType.None)
                throw new ArgumentException();
        }
        /// <summary>
        /// Auxiliary method that controls  if the LegEntity is null
        /// </summary>
        /// <param name="leg"></param>
        public static void CheckLegEntity(LegEntity leg)
        {
            if (leg == null)
                throw new NonexistentObjectException();
        }

        /// <summary>
        /// Auxiliary method that controls  if the strings are null, empty, alphanumerics or does not respect the constraints
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        private static void CheckTwoString(string from, string to)
        {
            CheckTwoStringEquals(from, to);
            CheckName(from);
            CheckName(to);
        }
        /// <summary>
        /// Auxiliary method that controls  if the strings are null, empty, alphanumerics or does not respect the constraints and and trasport
        /// isnt none
        /// </summary>
        /// <param name="from"></param>
        /// <param name="allowedTransportTypes"></param>
        public static void CheckDepartures(string from, TransportType allowedTransportTypes)
        {
            CheckName(from);
            if (allowedTransportTypes == TransportType.None)
                throw new ArgumentException();
        }
        /// <summary>
        ///  Auxiliary method that controls  if the Exp are null
        /// </summary>
        /// <param name="predicate"></param>
        public static void CheckNullExp(Expression<Func<ILegDTO, bool>> predicate)
        {
            if(predicate == null)
                throw new ArgumentException();
        }
        /// <summary>
        ///  Auxiliary method that controls  if the id isn't negative
        /// </summary>
        /// <param name="legId"></param>
        public static void CheckNegativeId(int legId)
        {
            if (legId < 0)
                throw new NonexistentObjectException();
        }
        
        /// <summary>
        /// Auxiliary method that controls  if the objects are null
        /// </summary>
        /// <param name="objects"></param>
        public static void CheckNull(params object[] objects)
        {
            if (objects.Any(o => o == null))
            {
                throw new NonexistentObjectException();
            }
        }
    }
}