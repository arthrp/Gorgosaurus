﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Gorgosaurus.DA
{
    public static class ReflectionHelper
    {
        public static bool IsEnumerable(this PropertyInfo prop)
        {
            var typeName = prop.PropertyType.Name;
            return typeName.Contains("IEnumerable");
        }
    }
}
