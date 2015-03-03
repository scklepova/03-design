using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using NLog;

namespace battleships
{
    public class AiTester
    {
        private static readonly Logger resultsLog = LogManager.GetLogger("results");
        private readonly Settings settings;

        public AiTester(Settings settings)
        {
            this.settings = settings;
        }



        public void TestSingleFile(string exe)
        {
            var monitor = new ProcessMonitor(TimeSpan.FromSeconds(settings.TimeLimitSeconds*settings.GamesCount),
                settings.MemoryLimit);
            Ai.AiProcessStarted += monitor.Register;

            var aiMaker = new AiMaker(exe);
            var master = new GameMaster(aiMaker, settings);
            var results = master.RunGamesSequence();
            var statisticsMaster = new StatisticsMaster(results, resultsLog, settings);
            statisticsMaster.WriteTotal();

        }

    }


}