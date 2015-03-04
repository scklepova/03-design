using System;
using System.Collections.Generic;
using System.Configuration;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

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

        public List<SingleGameResult> RunGamesSequence(MapGenerator generator, GameVisualizer visualizer)
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
