using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ServicesContracts
{
    public interface ITweetsTokenizer
    {
        IList<Tweet> getTweetsByQueryString(string query);
    }
}
