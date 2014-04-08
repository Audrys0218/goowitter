using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.ServicesContracts;
using System.Configuration;
namespace Business.Services
{
    public class TweetsTokenizer : ITweetsTokenizer
    {
        public IList<Tweet> getTweetsByQueryString(string query)
        {
            return new List<Tweet>();
        }
    }
}
