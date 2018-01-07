﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAP2017_2018_PlannerInterface;

namespace TAP2017_2018_PlannerImplementation
{
    public class PlannerFactory : IPlannerFactory
    {
        public IPlanner CreateNew()
        {
            return new Planner();
        }
    }
}
