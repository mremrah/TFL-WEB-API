using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TflApiClient.DTO;
using TflApiClient.Utils;
using Microsoft.Extensions.Configuration;
using TflApiClient.Interface;
using System.IO;
using TflApiClient.Services;

namespace TflApiClient
{
    public class TflClient
    {
        private void PrintUsageHelp()
        {
            Console.WriteLine($"TFL Road query Service client. It will query and shows the status of entered road from TFL official web services.");
            Console.WriteLine($"Usage :");
            Console.WriteLine($"\tRoadStatus theRoadNameToQuery");
            Console.WriteLine($"Examples:");
            Console.WriteLine($"\t-To Query A2 road: 'RoadStatus A2<enter-key>'");
            Console.WriteLine($"\t-To Query A3 road: 'RoadStatus A3<enter-key>'");
            Console.WriteLine($"NB: If the query is successfull last exit will be 0, otherwise 1 {Environment.NewLine} ");
            Console.WriteLine("and exit code can be queried by entering:");
            Console.WriteLine($"echo $lastexitcode<enter-key>");
            Console.WriteLine($"Thank you very much.");
        }

        public void Setup()
        {
            //Setup configuration
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            Configuration = builder.Build();

            //Setup IoC container
            ServiceProvider = new ServiceCollection()
                    .AddSingleton<ILogger, FileLogger>()
                    .AddSingleton<IConfigurationRoot>(Configuration)
                    .AddTransient<IServiceEndpoint<RoadCorridor>, RoadService>()
                    .BuildServiceProvider();
        }

        public IConfigurationRoot Configuration { get; private set; }
        public ServiceProvider ServiceProvider { get; set; }

        static async Task<int> Main(string[] args)
        {
            TflClient program = new TflClient();
            if (args == null || (args.Length == 0 || args.Length > 1))
            {
                program.PrintUsageHelp();
                return 0;
            }
            program.Setup();
            try
            {
                
                var routeService = program.ServiceProvider.GetService<IServiceEndpoint<RoadCorridor>>();

                var (roadCorridor, apiError) = await routeService.Get(args[0]);
                if (roadCorridor != null)
                {
                    Console.WriteLine(roadCorridor.ToString());

                    return roadCorridor.ExitCode;
                }
                if (apiError != null)
                {
                    Console.WriteLine(apiError.ToString());
                    return 1;
                }

                Console.WriteLine("Unknown Error! Please contact with developer!");

                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Oooppss!! Eyes are full open! Un-handled exception! How it is come! we didn't except this happen! Sorry for it.");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }

            return 1;

        }


    }
}
