using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlmostFramework.Controllers.Attributes;
using AlmostFramework.Data;
using IocContainer;

namespace AlmostFramework.Controllers
{
    internal class UsersController : IUsersController
    {
        private readonly IDataStore _dataStore;

        public UsersController(IDataStore dataStore) 
        {
            _dataStore = dataStore;
        }

        [Route("/get")]
        public List<string> Get()
        {
            return _dataStore.Users;
        }

        [Route("/post")]
        public int Post(string user)
        {
            _dataStore.Users.Add(user);
            return _dataStore.Users.Count - 1;
        }

        [Route("/delete")]
        public void Delete(int id)
        {
            if (_dataStore.Users.Count > id)
            {
                _dataStore.Users.RemoveAt(id);
            }
        }
    }
}
