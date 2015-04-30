using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gorgosaurus.BO.Entities
{
    public class BaseEntity
    {
        public long Id { get; set; }

        public long CreatedOnUnix { get; set; }

        public DateTime CreatedOn 
        { 
            get 
            { 
                var epoch = new DateTime(1970,1,1,0,0,0,0, DateTimeKind.Utc);
                return epoch.AddSeconds(CreatedOnUnix).ToLocalTime();
            }
        }
    }
}
