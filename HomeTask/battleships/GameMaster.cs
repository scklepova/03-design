using System;
using System.Collections.Generic;
using System.Configuration;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace battleships
{
    public class GameMaster
    {
        private readonly Settings settings;
        private readonly AiMaker aiMaker;

        public GameMaster(AiMaker maker, Settings settings)
        {
            this.aiMaker = maker;
            this.settings = settings;
        }

        public TotalGamesResults RunGamesSequence(MapGenerator generator, GameVisualizer visualizer)
        {
            var crashes = 0;
            var results = new List<SingleGameResult>();
            var ai = aiMaker.MakeAi();
            for (var gameIndex = 0; gameIndex < settings.GamesCount; gameIndex++)
            {
                var map = generator.GenerateMap();
                var game = new Game(map, ai);
                results.Add(game.RunGameToEnd(visualizer, settings.Interactive, settings.Verbose, results.Count));
                if (game.AiCrashed)
                {
                    crashes++;
                    if (crashes > settings.CrashLimit) break;
                    ai = aiMaker.MakeAi();
                }
            }
            ai.Dispose();
            return new TotalGamesResults(ai.Name, results);
        }


       

    }
}
