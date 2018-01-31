using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TAP2017_2018_TravelCompanyInterface; 


namespace TAP2017_2018_PlannerImplementation.Utilities
{
    internal class Checker 
    {
        /// <summary>
        /// Auxiliary method that controls  if the companies is null
        /// </summary>
        /// <param name="companies"></param>
        public static void CheckList(List<IReadOnlyTravelCompany> companies)
        {
            if (companies == null)
                throw new ArgumentNullException();
        }
        /// <summary>
        /// Auxiliary method that controls  if the dictionary is null
        /// </summary>
        /// <param name="previousDictionary"></param>
        public static void CheckDictionary(Dictionary<string, ILegDTO> previousDictionary)
        {
            if (previousDictionary == null)
                throw new ArgumentException();
        }
        /// <summary>
        /// Auxiliary method that controls  if the the list is null
        /// </summary>
        /// <param name="companies"></param>
        public static void CheckList(IEnumerable<IReadOnlyTravelCompany> companies)
        {
            if (companies == null)
                throw new ArgumentNullException();
            /*
            if ((companies.Select(c => c.GetType()).Distinct().Count() != 1))
                throw new ArgumentException();
            */
        }
        public static Boolean IsAlphaNumeric(string strToCheck)
        {
            Regex rg = new Regex(@"^[a-zA-Z0-9]*$");
            return rg.IsMatch(strToCheck);
        }
        
        /// <summary>
        /// Auxiliary method that controls  if the string is null, empty, alphanumerics or does not respect the constraints
        /// </summary>
        /// <param name="s"></param>
        public static void CheckString(string s)
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
        /// Auxiliary method that controls  if the company is null
        /// </summary>
        /// <param name="company"></param>
        public static void CheckTc(IReadOnlyTravelCompany company)
        {
            if (company == null)
                throw new ArgumentNullException();
        }

        /// <summary>
        /// Auxiliary method that controls  if the trasportype is none
        /// </summary>
        /// <param name="transportType"></param>
        public static void CheckTransportType(TransportType transportType)
        {
            if (transportType == TransportType.None)
                throw new ArgumentException();
        }

        /// <summary>
        /// Auxiliary method that controls  if the source, destination and  TrasportType and null, empty or none
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="allowedTransportType"></param>
        public static void CheckTrip(string source, string destination, 
            TransportType allowedTransportType)
        {
            CheckString(source);
            CheckString(destination);
            CheckTransportType(allowedTransportType);

        }

        
    }
}
