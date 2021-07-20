using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smslib.Data.Response
{
    public class Number
    {
      public string id { get; set; }
        public string number { get; set; }
        public string errmsg { get; set; }
        public bool err { get; set; }
    }
}
