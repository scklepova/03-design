using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Threading;
using Ninject;


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
			var settings = new Settings("settings.txt");


            var kernel = new StandardKernel();
            kernel.Bind<SettingsBase>()
                .To<Settings>()
                .WithConstructorArgument("settings.txt");
		    kernel.Bind<IMapGenerator>()
		        .To<MapGenerator>()
                .WithConstructorArgument("random", new Random(settings.RandomSeed));
            kernel.Bind<IGameVisualizer>().To<GameVisualizer>();
            kernel.Bind<ProcessMonitor>()
                .To<ProcessMonitor>()
                .WithConstructorArgument("timeLimit", TimeSpan.FromSeconds(settings.TimeLimitSeconds * settings.GamesCount))
                .WithConstructorArgument("memoryLimit", (long)settings.MemoryLimit);

            var tester = kernel.Get<AiTester>();
		    

			if (File.Exists(aiPath))
				tester.TestSingleFile(aiPath);
			else
				Console.WriteLine("No AI exe-file " + aiPath);
		}
	}
}