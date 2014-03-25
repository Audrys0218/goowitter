using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class Authentication
    {
        private string key = "raBdoulry9ggNxaWMtWxBQ";
        private string secret = "ynHO9INdtkSAaTtHIhYhRjzZJarETCW9EpoeGbKc";
        private static string BearerToken;

        public int Authenticate()
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create("https://api.twitter.com/oauth2/token");
                request.Method = "POST";

                var authHeader = string.Format("Basic {0}", Convert.ToBase64String(
                    Encoding.UTF8.GetBytes(Uri.EscapeDataString(key) + ":" + Uri.EscapeDataString((secret)))));

                request.Headers.Add("Authorization", authHeader);
                request.ContentType = "application/x-www-form-urlencoded;charset=UTF-8";

                using (Stream stream = request.GetRequestStream())
                {
                    byte[] content = ASCIIEncoding.ASCII.GetBytes("grant_type=client_credentials");
                    stream.Write(content, 0, content.Length);
                }

                var response = (HttpWebResponse)request.GetResponse();
                string responseData = new StreamReader(response.GetResponseStream()).ReadToEnd();

                BearerToken = responseData;  //fancy extracting....
            }
            catch (WebException e)
            {
                return (int)e.Status;
            }

            return 0;
        }

        public string GetBearerToken()
        {
            return BearerToken;
        }

    }
}
