using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlmostFramework.Controllers.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    internal class RoutePrefixAttribute : Attribute
    {
        public string RoutePrefix { get; set; }

        public RoutePrefixAttribute(string routePrefix)
        {
            RoutePrefix = routePrefix;
        }
    }
}
