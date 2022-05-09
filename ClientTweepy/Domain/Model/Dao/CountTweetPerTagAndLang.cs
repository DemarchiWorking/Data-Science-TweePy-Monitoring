using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Dao
{
    public class CountTweetPerTagAndLang
    {
        public string lang { get; set; }
        public string tag { get; set; }
        public int count { get; set; }
    }
}



