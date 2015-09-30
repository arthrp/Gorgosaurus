using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Gorgosaurus.DA.Extensions
{
    public static class PropertyInfoExtensions
    {
        public static bool IsNumeric(this PropertyInfo propInfo)
        {
            Type propType = propInfo.GetType();
            switch (Type.GetTypeCode(propType))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return true;
                default:
                    return false;
            }
        }

        public static string GetStringValue(this PropertyInfo propInfo, object obj)
        {
            var res = propInfo.GetValue(obj);

            if (res == null)
                return "null";

            return (propInfo.IsNumeric()) ?
                        propInfo.GetValue(obj).ToString() : "'" + propInfo.GetValue(obj) + "'";
        }
    }
}
