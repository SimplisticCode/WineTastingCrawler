using System;
using System.Collections.Generic;
using System.Threading;
using WineTastingCrawler;

namespace TestProject
{
    class Program
    {
        private const string baseurl = "https://www.foetex.dk";
        private const string url = "/vin-spiritus/vores-egen-vinkaelder/kommende-vinarrangementer/c/kommende-vin-arrangementer";

        static void Main(string[] args)
        {
            var crawler = new WineCrawler(baseurl,url);
            while (true)
            {
                var wineEvent = crawler.PerformCrawl().GetAwaiter().GetResult();
                var mailClient = new WineEventMailer();
                var addresses = new List<string>();
                if (wineEvent.NewWineTasting)
                {
                    Console.WriteLine($"There is wine tasting on {wineEvent.Time.ToLongDateString()} at {wineEvent.Time.ToLongTimeString()}");
                    Console.WriteLine($"Tema/Title: {wineEvent.Title} and price: {wineEvent.Price}");
                    Console.WriteLine($"Link to the event: {wineEvent.Link}");
                    mailClient.SendMail(wineEvent, addresses);

                }
                else
                    Console.Write("There is no wine tasting");

                Thread.Sleep(15000);
            }

        }
    }
}
