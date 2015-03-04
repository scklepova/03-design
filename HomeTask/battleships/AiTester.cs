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


        public void TestSingleFile(string aiPath, MapGenerator mapGenerator, GameVisualizer gameVisualizer, AiMaker aiMaker, 
            Logger resultsLogger)
        {
            var results = RunGamesSequence(mapGenerator, gameVisualizer, aiMaker);

            var statisticsMaster = new StatisticsMaster(results, resultsLogger, settings);            
            statisticsMaster.WriteScoreStatistics();

        }

        public List<GameResult> RunGamesSequence(MapGenerator generator, GameVisualizer visualizer, AiMaker aiMaker)
        {
            var i = 0;
            var results = Enumerable.Range(0, settings.GamesCount)
                .Select(x => aiMaker.MakeAi())
                .TakeWhile(ai => aiMaker.CrashesAllowable(settings.CrashLimit))
                .Select(ai => new Game(generator.GenerateMap(), ai))
                .Select(game => game.RunGameToEnd(visualizer, settings.Interactive, ++i))
                .Select(result => result.WriteSingleGameResults())
                .ToList();

            return results;
        }

    }


}