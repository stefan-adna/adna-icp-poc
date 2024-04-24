using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;

namespace console_test_app
{
    public class Program
    {
        private const string COMMANDS = """

            Please enter number of command
            1 - Upload document
            2 - Download document
            0 - Exit application
            """;

        public static async Task Main(string[] args)
        {
            // Build configuration
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", false)
                .Build();

            var settings = configuration.GetSection("Settings").Get<Settings>();

            IHost host = Host.CreateDefaultBuilder().ConfigureServices(services =>
            {
                services.AddTransient<IICPConnector, ICPConnector>();
                services.AddSingleton(settings);
            }
            ).Build();

            Console.WriteLine("ICP AssetConnector");
            Console.WriteLine($"CannisterId:{settings.CannisterId}");
            Console.WriteLine($"BaseUrl:{settings.BaseUrl}");

            var connector = host.Services.GetRequiredService<IICPConnector>();
            int command = -1;
            while (command != 0)
            {
                Console.WriteLine(COMMANDS);
                var input = Console.ReadLine();
                if (int.TryParse(input, out var selectedCommand))
                {
                    try
                    {
                        command = selectedCommand;
                        if (command == 1)
                        {
                            await UploadDocument(connector);
                        }
                        else if (command == 2)
                        {
                            await DownloadDocument(connector);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
                else
                {
                    Console.WriteLine("Wrong input");
                }
            }
            Console.WriteLine("Goodbye");
        }

        private static async Task UploadDocument(IICPConnector connector)
        {
            Console.WriteLine("Enter file path:");
            var file = Console.ReadLine().Trim();
            if (File.Exists(file))
            {
                FileInfo f = new FileInfo(file);
                Console.WriteLine($"Upload size:{f.Length}");

                Console.WriteLine("Enter identifier:");
                var identifier = Console.ReadLine().Trim();

                var stopWatch = new Stopwatch();
                stopWatch.Start();
                await connector.UploadDocument(identifier, file);
                stopWatch.Stop();

                Console.WriteLine($"Done. Time elapsed:{stopWatch.Elapsed}");
            }
            else
            {
                Console.WriteLine("File does not exist");
            }
        }

        private static async Task DownloadDocument(IICPConnector connector)
        {
            Console.WriteLine("Enter identifier:");
            var identifier = Console.ReadLine().Trim();
            
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var documentData = await connector.DownloadDocument(identifier);
            stopWatch.Stop();
            Console.WriteLine($"Done. Time elapsed:{stopWatch.Elapsed}");

            if (documentData.Any())
            {
                Console.WriteLine($"Downloaded size:{documentData.Length}");
            }
            else
            {
                Console.WriteLine("No data found");
            }
        }
    }
}
