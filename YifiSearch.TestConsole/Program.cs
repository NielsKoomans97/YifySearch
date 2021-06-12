using System;
using YifySearch.Models;
using YifySearch.Enums;
using YifySearch.Events;
using YifySearch;
using System.IO;
using System.Threading.Tasks;

namespace YifiSearch.TestConsole
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            while (true)
            {
                var searchClient = new SearchClient();

                if (File.Exists($"{AppDomain.CurrentDomain.BaseDirectory}\\config"))
                    searchClient = SearchClient.LoadConfig($"{AppDomain.CurrentDomain.BaseDirectory}\\config");

                Console.Write("Search $>");
                var line = Console.ReadLine();

                searchClient.Query = line;
                searchClient.SearchCompleted += (s, e) =>
                {
                    foreach (var movie in e.Movies)
                    {
                        Console.WriteLine(movie.Title);
                    }
                };

                await searchClient.SearchAsync(1);
            }
        }
    }
}