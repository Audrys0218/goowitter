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
            //testMethod();
            testMethod2();          //finds posts in an interval with specific keyword
            //testMethod3();
            Console.ReadLine();
            return 0;
        }

        public async static void testMethod3()
        {
           /* using (var twitterctx = new twittercontext(auth))
            {
                console.writeline("\nstreamed content: \n");
                int count = 0;

                await
                    (from strm in twitterctx.streaming
                     where strm.type == streamingtype.filter &&
                           strm.track == "twitter" &&
                           strm.locations == "-122.75,36.8,-121.75,37.8"
                     select strm)
                    .startasync(async strm =>
                    {
                        console.writeline(strm.content + "\n");

                        if (count++ >= 5)
                            strm.closestream();
                    });
            }*/
        }

        public async static void testMethod2()
        {
            var auth = new SingleUserAuthorizer
            {
                CredentialStore = new SingleUserInMemoryCredentialStore
                {
                    ConsumerKey = ConfigurationManager.AppSettings["consumerKey"],
                    ConsumerSecret = ConfigurationManager.AppSettings["consumerSecret"],
                    AccessToken = ConfigurationManager.AppSettings["accessToken"],
                    AccessTokenSecret = ConfigurationManager.AppSettings["accessTokenSecret"]
                }
            };
            //Data
            DateTime tfrom = new DateTime(2014,04,18), tto = new DateTime(2014,04,19);
            String querry = "empathy";
            ulong idfrom = 0, idto = 0;
            
            using (var twitterCtx = new TwitterContext(auth))
            {
                //Getting oldest tweet id from DateTime(tfrom)
                //veikia nevisai tiksliai, nes reikia nurodyti pirma seniausia tweet'a 
                //po tam tikros datos o nurodomas pirmas seniausias atitinkantis eilute: 
                //su retesniais zodziais didesne paklaida (netikslus alltweets),
                //nes nera(as bent neradau) kaip gauti pati seniausia pagal data bet 
                //nenurodant eilutes
                var searchResponse = await
                    (from search in twitterCtx.Search
                     where search.Type == SearchType.Search &&
                           search.Query == "\"" + querry + "\"" &&
                           search.Count == 1 &&
                           search.Until == tfrom
                     select search).SingleOrDefaultAsync();
                if (searchResponse != null && searchResponse.Statuses != null)
                    idfrom = searchResponse.Statuses.First().StatusID;

                //Getting newest tweet id from DateTime(tto)
                searchResponse = await
                    (from search in twitterCtx.Search
                     where search.Type == SearchType.Search &&
                           search.Query == "\"" + querry + "\"" &&
                           search.Count == 1 &&
                           search.Until == tto
                     select search).SingleOrDefaultAsync();
                if (searchResponse != null && searchResponse.Statuses != null)
                    idto = searchResponse.Statuses.First().StatusID;

                ulong ourtweets = 0, alltweets = idto - idfrom;   //statistic data

                Console.WriteLine("To:{0}", idto);

                //Getting tweets that match search string
                do
                {
                    searchResponse = await
                        (from search in twitterCtx.Search
                         where search.Type == SearchType.Search &&
                               search.Query == "\"" + querry + "\"" &&
                               search.Count == 100 &&
                               search.SinceID == idfrom &&
                               search.MaxID == idto &&
                               search.GeoCode == "40.7142700,-74.0059700,10km"
                         select search).SingleOrDefaultAsync();
                    if (searchResponse != null && searchResponse.Statuses != null)
                    {
                        searchResponse.Statuses.ForEach(tweet =>
                            Console.WriteLine(
                                "User:{0, 15},Date:{1, 21},ID:{2},\nLoc:{3},Coord{4}",
                                tweet.User.ScreenNameResponse,
                                tweet.CreatedAt,
                                tweet.StatusID,
                                tweet.User.Location,
                                tweet.Coordinates
                                ));
                        if (searchResponse.Statuses.Count != 0 
                            && idto != searchResponse.Statuses.Last().StatusID)
                        {
                            idto = searchResponse.Statuses.Last().StatusID;
                            ourtweets = ourtweets + (ulong)searchResponse.Statuses.Count;
                        }
                        else { break; }
                    }
                    
                    System.Threading.Thread.Sleep(2000);        // 450/15min = 1/2s 
                } while (searchResponse.Statuses.Count != 0);
                Console.WriteLine("Fr:{0}",idfrom);
                Console.WriteLine("Tweets found: {0}/{1}", ourtweets, alltweets);
                Console.WriteLine("Tweets according to string corresponds to {0}%", ourtweets/alltweets*100);
                Console.WriteLine("DONE!!!");
            }
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
        }
    }
}
