using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace battleships
{
    public class StatisticsMaster
    {
        private readonly List<GameResult> results ;
        private readonly Logger resultsLog;
        private readonly Settings settings;

        public StatisticsMaster(List<GameResult> results , Logger logger, Settings settings)
        {
            this.results = results;
            this.resultsLog = logger;
            this.settings = settings;
        }

        public void WriteScoreStatistics()
        {
            var shots = GetListOfShots();
            var badShots = GetTotalBadShots();
            var crashes = GetTotalCrashes();

			if (shots.Count == 0) shots.Add(1000 * 1000);
			shots.Sort();
			var median = shots.Count % 2 == 1 ? shots[shots.Count / 2] : (shots[shots.Count / 2] + shots[(shots.Count + 1) / 2]) / 2;
			var mean = shots.Average();
			var sigma = Math.Sqrt(shots.Average(s => (s - mean) * (s - mean)));
			var badFraction = (100.0 * badShots) / shots.Sum();
			var crashPenalty = 100.0 * crashes / settings.CrashLimit;
			var efficiencyScore = 100.0 * (settings.Width * settings.Height - mean) / (settings.Width * settings.Height);
			var score = efficiencyScore - crashPenalty - badFraction;
			var headers = FormatTableRow(new object[] { "AiName", "Mean", "Sigma", "Median", "Crashes", "Bad%", "Games", "Score" });
			var message = FormatTableRow(new object[] { results.ElementAt(0).AiName, mean, sigma, median, crashes, badFraction, results.Count, score });
			resultsLog.Info(message);

			Console.WriteLine();
			Console.WriteLine("Score statistics");
			Console.WriteLine("================");
			Console.WriteLine(headers);
			Console.WriteLine(message);
		}

        public List<int> GetListOfShots()
        {
            return results.Select(result => result.Shots).ToList();
        }

        public int GetTotalBadShots()
        {
            return results.Sum(result => result.BadShots);
        }

        public int GetTotalCrashes()
        {
            return results.Count(result => result.Crashed);
        }

		private string FormatTableRow(object[] values)
		{
			return FormatValue(values[0], 15) 
				+ string.Join(" ", values.Skip(1).Select(v => FormatValue(v, 7)));
		}

		private static string FormatValue(object v, int width)
		{
			return v.ToString().Replace("\t", " ").PadRight(width).Substring(0, width);
		}
	}
    
}
