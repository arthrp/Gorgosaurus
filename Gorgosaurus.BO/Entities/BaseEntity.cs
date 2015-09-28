using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gorgosaurus.BO.Extensions;

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
                return CreatedOnUnix.GetDateTimeFromUnixTimestamp();
            }
        }

        public long ModifiedOnUnix { get; set; }

        public DateTime ModifiedOn
        {
            get
            {
                return ModifiedOnUnix.GetDateTimeFromUnixTimestamp();
            }
        }

        public long? CreatedByUserId { get; set; }
    }
}
