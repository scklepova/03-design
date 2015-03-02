using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace battleships
{
    public abstract class SettingsBase
    {
        public int CrashLimit;
        public int GamesCount;
        public int Height;
        public bool Interactive;
        public int MemoryLimit;
        public int RandomSeed;
        public int[] Ships;
        public int TimeLimitSeconds;
        public bool Verbose;
        public int Width;
    }
}
