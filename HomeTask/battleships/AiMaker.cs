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

        public AiMaker(string exe)
        {
            this.exe = exe;
        }

        public Ai MakeAi()
        {
            return new Ai(exe);
        }
    }
}
