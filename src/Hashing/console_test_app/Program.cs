using console_test_app.Clients.HashingClient.Models;
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
            1 - Store hash
            2 - Retrieve hash
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

            Console.WriteLine("Console Test App");
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
                    command = selectedCommand;
                    if (command == 1)
                    {
                        await StoreHash(connector);
                    }
                    else if (command == 2)
                    {
                        await RetrieveHash(connector);
                    }
                }
                else
                {
                    Console.WriteLine("Wrong input");
                }
            }
            Console.WriteLine("Goodbye");
        }

        private static async Task StoreHash(IICPConnector connector)
        {
            Console.WriteLine("Enter root hash (0x...):");
            var rootHash = Console.ReadLine().Trim();

            Console.WriteLine("Enter hashed clientid (0x...):");
            var clientIdHash = Console.ReadLine().Trim();

            var transaction = new Transaction()
            {
                ClientIdHash = clientIdHash,
                TransactionId = Guid.NewGuid().ToString(),
                CreateDateTime = DateTime.Now.Ticks
            };

            var stopWatch = new Stopwatch();
            stopWatch.Start();
            await connector.StoreRootHash(rootHash, transaction);
            stopWatch.Stop();

            Console.WriteLine($"Done. Time elapsed:{stopWatch.Elapsed}");
        }

        private static async Task RetrieveHash(IICPConnector connector)
        {
            Console.WriteLine("Enter root hash (0x...):");
            var rootHash = Console.ReadLine().Trim();

            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var result = await connector.GetRootHash(rootHash);
            stopWatch.Stop();

            Console.WriteLine($"Done. Time elapsed:{stopWatch.Elapsed}");

            Console.WriteLine($"Found {result.Count} entries.");
            foreach (var entry in result.OrderBy(o => o.CreateDateTime))
            {
                Console.WriteLine($"TransactionId: {entry.TransactionId}, ClientIdHash: {entry.ClientIdHash}, DateTime:{new DateTime((long)entry.CreateDateTime)}");
            }
        }
    }
}
