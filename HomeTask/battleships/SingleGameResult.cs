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

        public SingleGameResult(int shots, bool crashed, int badShots)
        {
            this.Shots = shots;
            this.Crashed = crashed;
            this.BadShots = badShots;
           
        }
    }
}
