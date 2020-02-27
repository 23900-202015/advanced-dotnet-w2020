using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsoleApp1
{

    class Program
    {
        private static readonly HttpClient client = new HttpClient();

        static async Task Main(string[] args)
        {
            try
            {
                await GetSiteContentAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Main Catch: {ex.Message}");
            }
            Console.WriteLine("Back To main, now press any key to quit!");
            Console.ReadKey();

            Console.WriteLine("Done");
        }

        private static async Task GetSiteContentAsync()
        {
            ////Error generating list
            //var siteList = new List<string> { "yahoo", "Not a website","google", "msn", "reddit", "stackoverflow", "wired" };

            var siteList = new List<string> { "yahoo", "google", "msn", "reddit", "stackoverflow", "wired" };

            foreach (string site in siteList)
            {
                var task = client.GetStringAsync($"http://{site}.com");

                await task;
                Console.WriteLine($"{site} content length is {task.Result.Length}");
            }

            List<Task<string>> taskList = (from site in siteList select client.GetStringAsync($"http://{site}.com")).ToList();
            int sumLength = 0;
            Console.WriteLine("Starting While Loop!!!!!");
            while (taskList.Any())
            {
                var firstToFinish = await Task.WhenAny(taskList);
                Console.WriteLine($" content length is {firstToFinish.Result.Length}");
                sumLength += firstToFinish.Result.Length;
                taskList.Remove(firstToFinish);

            }

            Console.WriteLine($"Total length is: {sumLength}");
        }
    }
}
