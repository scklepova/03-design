using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace battleships
{
    public class AiMaker
    {
        private readonly string exe;
        private int crashes;

        public AiMaker(string exe)
        {
            this.exe = exe;
            this.crashes = 0;
            Game.AiCrashedEvent += OnAiCrashed;
        }

        public Ai MakeAi()
        {
            return new Ai(exe);
        }

        public bool CrashesAllowable(int crashesLimit)
        {
            return crashes <= crashesLimit;
        }

        private void OnAiCrashed()
        {
            crashes++;
        }
    }
}
