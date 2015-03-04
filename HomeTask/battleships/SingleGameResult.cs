using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace battleships
{
    public class SingleGameResult
    {
        
        public readonly int Shots;
        public readonly bool Crashed;
        public readonly int BadShots;
        public readonly string AiName;
        public readonly int GameNumber;
        

        public SingleGameResult(int shots, bool crashed, int badShots, string aiName, int gameNumber)
        {
            this.Shots = shots;
            this.Crashed = crashed;
            this.BadShots = badShots;
            this.AiName = aiName;
            this.GameNumber = gameNumber;
        }

        public SingleGameResult WriteSingleGameResults()
        {
            Console.WriteLine(
                "Game #{3,4}: Turns {0,4}, BadShots {1}{2}",
                Shots, BadShots, Crashed ? ", Crashed" : "", GameNumber);
            return this;
        }
    }
}
