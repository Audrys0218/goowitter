using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using LinqToTwitter;
using System.Configuration;
namespace Business
{
    class MainClass
    {
        public static int Main(string[] args) {
            testMethod();
            Console.ReadLine();
            testMethod();
            return 0;
        }
        public async static void testMethod()
        {
            var auth = new SingleUserAuthorizer
            {
                CredentialStore = new SingleUserInMemoryCredentialStore
                {
                    ConsumerKey    = ConfigurationManager.AppSettings["consumerKey"],
                    ConsumerSecret = ConfigurationManager.AppSettings["consumerSecret"],
                    AccessToken    = ConfigurationManager.AppSettings["accessToken"],
                    AccessTokenSecret = ConfigurationManager.AppSettings["accessTokenSecret"]
                }
            };

            using (var twitterCtx = new TwitterContext(auth))
            {
                var searchResponse =
                    await
                    (from search in twitterCtx.Search
                     where search.Type == SearchType.Search &&
                           search.Query == "\"Sleep\"" &&
                           search.GeoCode == "-22.912214,-43.230182,1km" &&
                           search.Count == 20 &&
                           search.ResultType == ResultType.Mixed

                     select search)
                    .SingleOrDefaultAsync();

                if (searchResponse != null && searchResponse.Statuses != null)
                    searchResponse.Statuses.ForEach(tweet =>
                        Console.WriteLine(
                            "User: {0}, Tweet: {1} Latitute: {2} Longtitute: {3}",
                            tweet.User.ScreenNameResponse,
                            tweet.Text,
                            tweet.Coordinates.Latitude,
                            tweet.Coordinates.Longitude
                            ));
            }
            //using (var twitterCtx = new TwitterContext(auth)) { 
            //Console.WriteLine("\nStreamed Content: \n");
            //int count = 0;

            //await
            //    (from strm in twitterCtx.Streaming
            //     where strm.Type == StreamingType.Filter &&
            //           strm.Track == "twitter" &&
            //           strm.Locations == "-122.75,36.8,-121.75,37.8"
            //     select strm)
            //    .StartAsync(async strm =>
            //    {
            //        Console.WriteLine(strm.Content + "\n");

            //        if (count++ >= 5)
            //            strm.CloseStream();
            //    });
            //}
        }
    }
}
