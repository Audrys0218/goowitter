using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevBridge.Templates.WebProject.DataEntities.Entities;

namespace Models.ServicesContracts
{
    public interface ITweetTokenizer
    {
        List<Tweet> getResult();
        void searchTweetByKeyWords(string[] keywords);
        List<Tweet> getTweetByMaxMinIds(int min, int max);
    }
}
