using System.Reflection;
using IocContainer;
using AlmostFramework.Controllers;
using AlmostFramework.Controllers.Attributes;

namespace AlmostFramework
{
    internal class Api
    {
        private Dictionary<string, (MethodInfo, object)> _controllersMethods;
        private readonly object[] _controllers;

        public Api()
        {
            _controllers = new object[]
            {
                Container.Resolve<IUsersController>(),
                Container.Resolve<ISubscriptionController>(),
            };
            _controllersMethods = new Dictionary<string, (MethodInfo, object)>(StringComparer.OrdinalIgnoreCase);
            GetMethods();
        }

        public void CallEndpoint(string route, params object[] args)
        {
            if (!_controllersMethods.ContainsKey(route))
            {
                Console.WriteLine("Not Found - 404");
                return;
            }

            var methodToInvoke = _controllersMethods[route];
            object? returnedValue = methodToInvoke.Item1.Invoke(methodToInvoke.Item2, args);

            if (returnedValue is System.Collections.IEnumerable en)
            {
                Console.WriteLine($"Method {methodToInvoke.Item1.Name} returned: ");

                foreach (var val in en)
                {
                    Console.WriteLine(val);
                }
            }
            else
            {
                Console.WriteLine($"Method {methodToInvoke.Item1.Name} returned {returnedValue}");
            }
        }

        private void GetMethods()
        {
            var result = _controllers
                .Select(it => new { 
                    routePrefix = it.GetType().GetCustomAttribute<RoutePrefixAttribute>()?.RoutePrefix ?? string.Empty, 
                    methods = it.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public), 
                    controller = it 
                })
                .SelectMany(it => it.methods.Select(m => new { 
                    route = $"{it.routePrefix}{m.GetCustomAttribute<RouteAttribute>()?.Route ?? ""}", 
                    method = m, 
                    controller = it.controller 
                }))
                .Where(it => !string.IsNullOrEmpty(it.route) && it.method.DeclaringType != typeof(object))
                .ToArray();

            foreach (var i in result)
            {
                _controllersMethods.Add(i.route, new(i.method, i.controller));
            }
        }
    }
}
