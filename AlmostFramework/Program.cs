using IocContainer;
using AlmostFramework;
using AlmostFramework.Controllers;
using AlmostFramework.Data;


Container.Register<IDataStore, DataStore>(Lifetime.Singleton);
Container.Register<IUsersController, UsersController>(Lifetime.Transient);
Container.Register<ISubscriptionController, SubscriptionController>(Lifetime.Transient);

var api = new Api();
api.CallEndpoint("/get");
api.CallEndpoint("/post", "Oleh");
api.CallEndpoint("/post", "Mykhailo");
api.CallEndpoint("/put");
api.CallEndpoint("/get");
api.CallEndpoint("/delete", 0);
api.CallEndpoint("/get");
api.CallEndpoint("subscription/post", 1);
api.CallEndpoint("subscription/get");