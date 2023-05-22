using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlmostFramework.Controllers
{
    internal interface IUsersController
    {
        List<string> Get();
        int Post(string user);
        public void Delete(int id);
    }
}
