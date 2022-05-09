using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Response
{
    public class Response 
    {
        public string Title { get; set; }
        public int Status { get; set; }
        public bool isSuccess { get; set; }
        public bool isEmpty { get; set; }
        public IEnumerable<dynamic> List { get; set; }  
    }
}
