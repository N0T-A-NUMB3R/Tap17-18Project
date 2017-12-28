using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAP2017_2018_Implementation
{
    
        public static class Utilities
        {
            /*public static readonly Expression<Func<Setting, DescribedEntityDTO>> SettingToDescribedEntityDto = x => new DescribedEntityDTO
            {

                EntityId = x.SettingId,
                Description = x.Description,
                Name = x.Name
            };*/ // Qua convertiro I dtossss

            public static void CheckAnyNull(params object[] objects)
            {
                if (objects.Any(o => o == null))
                {
                    throw new ArgumentNullException();
                }
            }
        }
    }
