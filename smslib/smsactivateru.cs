using smslib.Data.Response;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace smslib
{
    public class smsactivateru : ISms
    {
        public string ApiKey { get; set; }
        public string Country { get; set; }
        public smsactivateru(string apikey, string country)
        {
            this.Country = country;
            this.ApiKey = apikey;

        }
        public Number GetNumber()
        {
            Number n = new Number();
            try
            {

                string url = string.Format("https://sms-activate.ru/stubs/handler_api.php?api_key={0}&action=getNumber&service=kt&country={1}", ApiKey, Country);
                string read = "";
                using (WebClient client = new WebClient())
                {
                    client.Encoding = Encoding.UTF8;
                    read = client.DownloadString(url);
                }
                if (read.Contains("NO_NUMBERS"))
                {
                    n.err = true;
                    n.errmsg = "번호없음";
                }
                else if (read.Contains("NO_BALANCE"))
                {
                    n.err = true;
                    n.errmsg = "잔액부족";
                }
                else if (read.Contains("ACCESS_NUMBER"))
                {
                    n.number = read.Split(':')[2];
                    n.id = read.Split(':')[1];
                    n.err = false;


                }
                else
                {
                    n.err = true;
                }
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
            return n;
        }

    }
}
