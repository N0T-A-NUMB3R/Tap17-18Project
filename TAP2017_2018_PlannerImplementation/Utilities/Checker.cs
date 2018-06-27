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
                throw new ArgumentNullException($"The list of Companies is null");
        }
        /// <summary>
        /// Auxiliary method that controls  if the dictionary is null
        /// </summary>
        /// <param name="previousDictionary"></param>
        public static void CheckDictionary(Dictionary<string, ILegDTO> previousDictionary)
        {
            if (previousDictionary == null)
                throw new ArgumentException($"The Dictionary is null");
        }
        /// <summary>
        /// Auxiliary method that controls  if the the list is null
        /// </summary>
        /// <param name="companies"></param>
        public static void CheckList(IEnumerable<IReadOnlyTravelCompany> companies)
        {
            if (companies == null)
                throw new ArgumentNullException($"The list of Companies is null");
            
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
                throw new ArgumentNullException($"Argument {0} is null", nameof(s));
            if (s == string.Empty)
                throw new ArgumentException($"Argument {0} is empty", nameof(s));
            if (!IsAlphaNumeric(s))
                throw new ArgumentException($"Argument {0} isn't alphanumerics", nameof(s));
            if (s.Length < DomainConstraints.NameMinLength)
                throw new ArgumentException($"Argument {0} length less than the minimum length allowed", nameof(s));
            if (s.Length > DomainConstraints.NameMaxLength)
                throw new ArgumentException($"Argument {0} length over than the maximum length allowed", nameof(s));
           
        }
        /// <summary>
        /// Auxiliary method that controls  if the company is null
        /// </summary>
        /// <param name="company"></param>
        public static void CheckTc(IReadOnlyTravelCompany company)
        {
            if (company == null)
                throw new ArgumentNullException($"ROTC is null");
        }

        /// <summary>
        /// Auxiliary method that controls  if the trasportype is none
        /// </summary>
        /// <param name="transportType"></param>
        public static void CheckTransportType(TransportType transportType)
        {
            if (transportType == TransportType.None)
                throw new ArgumentException($"Trasporttype is .None");
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
