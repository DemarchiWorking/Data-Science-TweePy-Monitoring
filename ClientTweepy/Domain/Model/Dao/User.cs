using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Dao
{
    public class User
    {
        public string idUser { get; set; }
        public string username { get; set; }
        public int followersCount { get; set; }
        public int followedCount { get; set; }

    }
}



