using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IocContainer
{
    public static class Container
    {
        private static Dictionary<Type, InstanceInfo> _instances;

        static Container()
        {
            _instances = new Dictionary<Type, InstanceInfo>();
        }

        public static void Register(Type type, Type implType, Lifetime lifeTime = Lifetime.Transient)
        {
            if (_instances.ContainsKey(type))
            {
                _instances.Remove(type);
            }

            InstanceInfo instanceInfo = new InstanceInfo(implType, lifeTime);
            _instances.Add(type, instanceInfo);
        }

        public static void Register<T, TImpl>(Lifetime lifetime = Lifetime.Transient)
            where T : class
            where TImpl : class
        {
            Register(typeof(T), typeof(TImpl), lifetime);
        }

        public static void RegisterFactory<T>(Func<T> factory, Lifetime lifetime = Lifetime.Transient)
        {
            Type type = typeof(T);

            if (_instances.ContainsKey(type))
            {
                _instances.Remove(type);
            }

            InstanceInfo instanceInfo = new InstanceInfo(() => factory(), lifetime);
            _instances.Add(type, instanceInfo);
        }

        public static T Resolve<T>() => (T)Resolve(typeof(T));

        public static object Resolve(Type type)
        {
            if (!_instances.ContainsKey(type))
            {
                return null;
            }

            InstanceInfo instanceInfo = _instances[type];

            if (instanceInfo == null)
            {
                return null;
            }

            if (instanceInfo.Instance != null)
            {
                return instanceInfo.Instance;
            }

            var obj = GetInstance(instanceInfo);

            if (instanceInfo.Lifetime == Lifetime.Singleton)
            {
                instanceInfo.Instance = obj;
            }

            return obj;
        }
        
        private static object GetInstance(InstanceInfo instanceInfo)
        {
            if (instanceInfo.Factory != null)
            {
                return instanceInfo.Factory.Invoke();
            }

            object[] args = instanceInfo
                .ConstructorParameters
                .Select(p => Resolve(p.ParameterType))
                .ToArray();

            return Activator.CreateInstance(instanceInfo.ImplementationType, args);
        }
    }

}
