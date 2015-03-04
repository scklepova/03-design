using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Threading;
using NLog;

namespace battleships
{
	public class Program
	{
		private static void Main(string[] args)
		{
			Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
			if (args.Length == 0)
			{
				Console.WriteLine("Usage: {0} <ai.exe>", Process.GetCurrentProcess().ProcessName);
				return;
			}
			var aiPath = args[0];
			
		    if (File.Exists(aiPath))
		    {
                var settings = new Settings("settings.txt");
                var monitor = new ProcessMonitor(TimeSpan.FromSeconds(settings.TimeLimitSeconds * settings.GamesCount),
                    settings.MemoryLimit);
                Ai.AiProcessStarted += monitor.Register;
                var mapGenerator = new MapGenerator(settings, new Random(settings.RandomSeed));
                var gameVisualizer = new GameVisualizer();
                Logger resultsLogger = LogManager.GetLogger("results");

                var aiMaker = new AiMaker(aiPath);

                var tester = new AiTester(settings);
		        tester.TestSingleFile(aiPath, mapGenerator, gameVisualizer, aiMaker, resultsLogger);
		    }
		    else
		        Console.WriteLine("No AI exe-file " + aiPath);
		}
	}
}