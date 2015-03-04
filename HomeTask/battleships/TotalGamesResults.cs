using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace battleships
{
    public class TotalGamesResults
    {
        public string AiName;
        public List<SingleGameResult> Results;

        public TotalGamesResults(string aiName, List<SingleGameResult> results)
        {
            this.AiName = aiName;
            this.Results = results;
        }
    }
}
