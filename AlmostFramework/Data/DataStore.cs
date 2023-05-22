using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlmostFramework.Data
{
    internal class DataStore : IDataStore
    {
        public List<string> Users { get; set; }
        public List<int> Subscribers { get; set; }

        public DataStore()
        {
            Users = new List<string>();
            Subscribers = new List<int>();
        }
    }
}
