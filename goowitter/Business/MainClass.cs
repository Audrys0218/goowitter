using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Xml;
namespace Business
{
    class MainClass
    {
        public static int Main(string[] args) {
            string barer = "AAAAAAAAAAAAAAAAAAAAALUKWgAAAAAAJrCy7FbyI3REmljlHhzmtz%2B6To8%3D1ZqiF6uVgyY9kTJr7mJPLTbp0yBAdII9w1vwpTsdVrkFHPrRA1";

            HttpWebRequest request = WebRequest.Create(
              "https://api.twitter.com/1.1/search/tweets.json?q=%40twitterapi") as HttpWebRequest;
            // If required by the server, set the credentials.
            //            request.Credentials = CredentialCache.DefaultCredentials;
            // Get the response.
            request.Headers.Add("Authorization", "Bearer AAAAAAAAAAAAAAAAAAAAALUKWgAAAAAAJrCy7FbyI3REmljlHhzmtz%2B6To8%3D1ZqiF6uVgyY9kTJr7mJPLTbp0yBAdII9w1vwpTsdVrkFHPrRA1");
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            // Display the status.

            //DataContractSerializer jsonSerializer = new DataContractSerializer(typeof(Response));
            //object objResponse = jsonSerializer.ReadObject(response.GetResponseStream());
            //Response jsonResponse = objResponse as Response;

            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            // Get the stream containing content returned by the server.
            Stream dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            string responseFromServer = reader.ReadToEnd();
            // Display the content.
            Console.WriteLine(responseFromServer);
            // Clean up the streams and the response.
            reader.Close();
            response.Close();



            Console.ReadLine();
            //string jsonString = 
            return 0;
        }
    }
}
