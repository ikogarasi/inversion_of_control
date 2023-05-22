using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlmostFramework.Controllers
{
    internal interface ISubscriptionController
    {
        List<int> GetSubscriptions();
        void CreateSubscription(int subscriptionId);
    }
}
