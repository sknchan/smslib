using Newtonsoft.Json.Linq;
using smslib.Data;
using smslib.Data.Response;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace smslib
{
    public class fivesim : ISms
    {
        public string ApiKey { get; set; }
        public string Country { get; set; }
        public fivesim(string apikey, string country)
        {
            this.Country = country;
            this.ApiKey = apikey;
        }
        public Number GetNumber()
        {
            Number n = new Number();
            try
            {
                string url = string.Format("https://5sim.net/v1/user/buy/activation/{0}/any/kakaotalk", Country);

                string read = "";

                using (WebClient client = new WebClient())
                {
                    client.Headers.Add("Accept", "application/json");
                    client.Headers.Add("Authorization", "Bearer " + ApiKey);
                    client.Encoding = Encoding.UTF8;
                    read = client.DownloadString(url);
                }
                JObject j = JObject.Parse(read);
                n.id = j["id"].ToString();
                n.number = j["phone"].ToString();

                return n;
            }
            catch (WebException ee)
            {
                n.err = true;
                string err = "";
                using (StreamReader sr = new StreamReader(ee.Response.GetResponseStream()))
                {
                    err = sr.ReadToEnd();
                }
                n.errmsg = err;
            }
        }
    }
}
