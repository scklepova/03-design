using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace battleships
{
    public class SingleGameResult
    {
        public readonly int shots;
        public readonly bool crashed;
        public readonly int badShots;

        public SingleGameResult(int shots, bool crashed, int badShots)
        {
            this.shots = shots;
            this.crashed = crashed;
            this.badShots = badShots;
           
        }
    }
}
