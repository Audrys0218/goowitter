using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.ServicesContracts;
using DevBridge.Templates.WebProject.DataEntities.Entities;
using LinqToTwitter;
namespace Models.Services
{
    public class TweetTokenizer: ITweetTokenizer
    {
        private List<Tweet> result;
        SingleUserAuthorizer auth;
        public TweetTokenizer()
        {
            init();
            result = new List<Tweet>();
        }

        public List<Tweet> getResult()
        {
            return result;
        }

        public async void searchTweetByKeyWords(string[] keywords)
        {
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
                    searchResponse.Statuses.ForEach(tweet =>{
                        result.Add(new Tweet { Text = tweet.Text });
                        Console.WriteLine(tweet.Text);
                        }
                        );
            }
        }

        public List<Tweet> getTweetByMaxMinIds(int min, int max)
        {
            throw new NotImplementedException();
        }

        void init() {
            auth = new SingleUserAuthorizer
            {
                CredentialStore = new SingleUserInMemoryCredentialStore
                {
                    ConsumerKey = "raBdoulry9ggNxaWMtWxBQ",
                    ConsumerSecret = "ynHO9INdtkSAaTtHIhYhRjzZJarETCW9EpoeGbKc",
                    AccessToken = "2367264530-ezlQyI8hj9Eba52lluGpMRkbkh5grvtm5YNKBE2",
                    AccessTokenSecret = "6lQM3YzO6TwTOGZTDCIw60W9A0hdNEmjCo1GzqM7jSq5y"
                }
            };
        }
    }

        //public async static void testMethod()
        //{
        //    var auth = new SingleUserAuthorizer
        //    {
        //        CredentialStore = new SingleUserInMemoryCredentialStore
        //        {
        //            ConsumerKey    = ConfigurationManager.AppSettings["consumerKey"],
        //            ConsumerSecret = ConfigurationManager.AppSettings["consumerSecret"],
        //            AccessToken    = ConfigurationManager.AppSettings["accessToken"],
        //            AccessTokenSecret = ConfigurationManager.AppSettings["accessTokenSecret"]
        //        }
        //    };

            //using (var twitterCtx = new TwitterContext(auth)) { 
            //var searchResponse =
            //    await
            //    (from search in twitterCtx.Search
            //     where search.Type == SearchType.Search &&
            //           search.Query == "\"Sleep\"" &&
            //           search.GeoCode == "-22.912214,-43.230182,1km" &&
            //           search.Count == 20 &&
            //           search.ResultType == ResultType.Mixed
                       
            //     select search)
            //    .SingleOrDefaultAsync();

            //if (searchResponse != null && searchResponse.Statuses != null)
            //    searchResponse.Statuses.ForEach(tweet =>
            //        Console.WriteLine(
            //            "User: {0}, Tweet: {1} Latitute: {2} Longtitute: {3}",
            //            tweet.User.ScreenNameResponse,
            //            tweet.Text,
            //            tweet.Coordinates.Latitude,
            //            tweet.Coordinates.Longitude
            //            ));
        //    using (var twitterCtx = new TwitterContext(auth)) { 
        //    Console.WriteLine("\nStreamed Content: \n");
        //    int count = 0;

        //    await
        //        (from strm in twitterCtx.Streaming
        //         where strm.Type == StreamingType.Filter &&
        //               strm.Track == "twitter" &&
        //               strm.Locations == "-122.75,36.8,-121.75,37.8"
        //         select strm)
        //        .StartAsync(async strm =>
        //        {
        //            Console.WriteLine(strm.Content + "\n");

        //            if (count++ >= 5)
        //                strm.CloseStream();
        //        });
        //    }
        //}

}
