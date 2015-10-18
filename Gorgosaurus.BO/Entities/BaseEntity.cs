using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gorgosaurus.BO.Extensions;
using System.Reflection;

namespace Gorgosaurus.BO.Entities
{
    public abstract class BaseEntity
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

        public virtual string GetPropertiesAsCsv(bool onlyCurrentTypeProperties = false)
        {
            var res = new StringBuilder();
            PropertyInfo[] properties = this.GetType().GetProperties();

            string typeName = this.GetType().Name;
            foreach (PropertyInfo property in properties)
            {
                if (onlyCurrentTypeProperties && !property.DeclaringType.Name.Equals(typeName))
                    continue;

                if (!property.CanWrite ||
                    property.IsEnumerable() ||
                    property.CustomAttributes.Any(a => a.AttributeType.Name == "NotColumn"))
                    continue;

                res.Append(typeName + "." + property.Name.ToUpperInvariant() + ",");
            }

            res.Length -= 1;

            return res.ToString();
        }
    }
}
