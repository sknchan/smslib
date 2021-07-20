using smslib.Data.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smslib
{
    public interface ISms
    {
        string ApiKey { get; set; }
        string Country { get; set; }
        Number GetNumber();

    }
}
