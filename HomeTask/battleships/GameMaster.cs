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
        private int badShots;
        private int crashes;
        private readonly List<int> shots = new List<int>();
        private readonly Settings settings;
        private readonly AiMaker aiMaker;

        public GameMaster(AiMaker maker, Settings settings)
        {
            this.aiMaker = maker;
            this.settings = settings;
            badShots = 0;
            crashes = 0;
        }

        public GameResult RunGamesSequence()
        {
            var gen = new MapGenerator(settings, new Random(settings.RandomSeed));
            var vis = new GameVisualizer();

            var ai = aiMaker.MakeAi();
            for (var gameIndex = 0; gameIndex < settings.GamesCount; gameIndex++)
            {
                var map = gen.GenerateMap();
                var game = new Game(map, ai);
                game.RunGameToEnd(vis, settings.Interactive);
                badShots += game.BadShots;
                if (game.AiCrashed)
                {
                    crashes++;
                    if (crashes > settings.CrashLimit) break;
                    ai = aiMaker.MakeAi();
                }
                else
                    shots.Add(game.TurnsCount);

                if (settings.Verbose)
                {
                    WriteGameResults(game, gameIndex);
                }
            }
            ai.Dispose();
            return new GameResult(ai.Name, shots, crashes, badShots, settings.GamesCount);
        }


        private static void WriteGameResults(Game game, int gameNumber)
        {
            Console.WriteLine(
                "Game #{3,4}: Turns {0,4}, BadShots {1}{2}",
                game.TurnsCount, game.BadShots, game.AiCrashed ? ", Crashed" : "", gameNumber);
        }


    }
}
