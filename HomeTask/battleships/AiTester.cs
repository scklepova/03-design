using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using NLog;

namespace battleships
{
    public class AiTester
    {
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

            var mapGenerator = new MapGenerator(settings, new Random(settings.RandomSeed));
            var gameVisualizer = new GameVisualizer();
            Logger resultsLog = LogManager.GetLogger("results");

            var aiMaker = new AiMaker(exe);
            var master = new GameMaster(aiMaker, settings);
            var results = master.RunGamesSequence(mapGenerator, gameVisualizer);
            var statisticsMaster = new StatisticsMaster(results, resultsLog, settings);
            
            statisticsMaster.WriteScoreStatistics();

        }

    }


}