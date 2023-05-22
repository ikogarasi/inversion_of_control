using IocContainer;
using IocContainer.Models;

Container.Register(typeof(IDataStore), typeof(DataStore), Lifetime.Singleton);
Container.RegisterFactory<IService>(() => new Service(), Lifetime.Transient);

var dataStore1 = Container.Resolve<IDataStore>();
var dataStore2 = Container.Resolve<IDataStore>();

Console.WriteLine($"This should be true - ${dataStore1 == dataStore2}");

var service1 = Container.Resolve(typeof(IService));
var service2 = Container.Resolve(typeof(IService));

Console.WriteLine($"This should be false - ${service1 == service2}");
