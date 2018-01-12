using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using TAP2017_2018_PlannerInterface;
using TAP2017_2018_TravelCompanyInterface;


namespace TAP2017_2018_PlannerImplementation.Utilities
{
    class Checker
    {
        public static void CheckList(List<IReadOnlyTravelCompany> companies)
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

        public static void CheckTc(IReadOnlyTravelCompany company)
        {
            if (company == null)
                throw new ArgumentNullException();
        }

        public static void CheckTransportType(TransportType transportType)
        {
            if (transportType == TransportType.None)
                throw new ArgumentException();
        }

        public static void CheckTrip(string source, string destination, 
            TransportType allowedTransportType)
        {
            CheckString(source);
            CheckString(destination);
            CheckTransportType(allowedTransportType);

        }

        
    }
}
