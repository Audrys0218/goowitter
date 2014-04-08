using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Services;
using Models.ServicesContracts;
using DevBridge.Templates.WebProject.DataEntities.Entities;
namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
            ITweetTokenizer tokenizer = new TweetTokenizer();
            tokenizer.searchTweetByKeyWords(new [] {"Sleep"});
            List<Tweet> result = tokenizer.getResult();
            Console.WriteLine(result.Count);
            foreach (var tweet in result) {
                Console.WriteLine(tweet.Text);
            }
            Console.ReadLine();
        }
    }
}
