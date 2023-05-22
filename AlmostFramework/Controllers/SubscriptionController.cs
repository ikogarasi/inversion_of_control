using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlmostFramework.Controllers.Attributes;
using AlmostFramework.Data;

namespace AlmostFramework.Controllers
{
    [RoutePrefix("subscription")]
    internal class SubscriptionController : ISubscriptionController
    {
        private IDataStore _dataStore;

        public SubscriptionController(IDataStore dataStore)
        {
            _dataStore = dataStore;
        }

        [Route("/get")]
        public List<int> GetSubscriptions()
        {
            return _dataStore.Subscribers;
        }

        [Route("/post")]
        public void CreateSubscription(int subscriptionId)
        {
            _dataStore.Subscribers.Add(subscriptionId);
        }
    }
}
