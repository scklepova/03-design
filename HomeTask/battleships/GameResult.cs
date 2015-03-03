using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace battleships
{
    public class GameResult
    {
        public readonly string aiName;
        public readonly List<int> shots;
        public readonly int crashes;
        public readonly int badShots;
        public readonly int gamesPlayed;

        public GameResult(string aiName, List<int> shots, int crashes, int badShots, int gamesNumber)
        {
            this.shots = shots;
            this.crashes = crashes;
            this.badShots = badShots;
            this.aiName = aiName;
            this.gamesPlayed = gamesNumber;
        }
    }
}
