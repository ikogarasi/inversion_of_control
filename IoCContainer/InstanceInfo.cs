using System.Reflection;

namespace IocContainer
{
    public class InstanceInfo
    {
        public Lifetime Lifetime { get; }
        public Type ImplementationType { get; }
        public ConstructorInfo DefaultConstructor { get; }
        public ParameterInfo[] ConstructorParameters { get; }
        public Func<object> Factory { get; set; }
        public object Instance { get; set; }

        public InstanceInfo(Type implementationType, Lifetime lifetime)
        {
            ImplementationType = implementationType;
            Lifetime = lifetime;

            DefaultConstructor = implementationType.GetConstructors()[0];
            ConstructorParameters = DefaultConstructor.GetParameters();
        }

        public InstanceInfo(Func<object> factory, Lifetime lifetime)
        {
            Factory = factory;
            Lifetime = lifetime;
        }
    }
}
