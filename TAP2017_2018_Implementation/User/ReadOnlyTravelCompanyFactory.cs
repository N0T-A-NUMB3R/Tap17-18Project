﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAP2017_2018_TravelCompanyInterface;

namespace TAP2017_2018_Implementation
{
    public class ReadOnlyTravelCompanyFactory : IReadOnlyTravelCompanyFactory
    {
        private string dbConnection;

        public ReadOnlyTravelCompanyFactory(string dbConnection)
        {
            this.dbConnection = dbConnection;
        }

        public IReadOnlyTravelCompany Get(string name)
        {
            throw new NotImplementedException();
            //Carica i dati di una TC già inizializzata

        }
    }
}

