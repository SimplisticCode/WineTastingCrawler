using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace WineTastingCrawler
{
    public class WineCrawler 
    {
    public WineCrawler(string url)
        {
        client = new HttpClient
            {
            BaseAddress = new Uri("https://www.foetex.dk")
                };
            foetexUrl = url;
            monthsDanish = new List<string>
            {
                "januar",
                "februar",
                "marts",
                "april",
                "maj",
                "juni",
                "juli",
                "august",
                "september",
                "oktober",
                "november",
                "december"
                };
            
            }
        
        private readonly HttpClient client;
        
        public string foetexUrl
        {
        get;
            set;
            }
        
        private readonly List<string> monthsDanish;
        
        public async Task<WineTastingResult> PerformCrawl()
        {
            var content = await client.GetStringAsync(foetexUrl);
            content = content.Trim();

            var wineTastingEvent = new WineTastingResult();
            wineTastingEvent.NewWineTasting = content.Contains("Den Gamle By, Aarhus") ? true : false;
            var contentSplitted_before = content.Split("Den Gamle By")[0].Split(">");
            if (!wineTastingEvent.NewWineTasting) return wineTastingEvent;
            var lengthOfArray = contentSplitted_before.Length;

            DateTime time = FindDate(contentSplitted_before, lengthOfArray);
            wineTastingEvent.Time = time;
            var link = contentSplitted_before[lengthOfArray - 2].Split('"')[1];
            wineTastingEvent.Link = link;
            var text_section = new List<string>(content.Split("text_section"));
            var event_details = text_section[text_section.Count - 1];
            wineTastingEvent.Title = event_details.Split("<strong>")[1].Split('<')[0];
            wineTastingEvent.Price = event_details.Split("<strong>")[2].Split('<')[0];
            return wineTastingEvent;
        }

        private DateTime FindDate(string[] contentSplitted, int lengthOfArray)
        {
            var dateOfEvent = contentSplitted[lengthOfArray - 1].Replace(',', ' ').Replace('.', ' ').Trim().Split(" ");
            int month = monthsDanish.IndexOf(dateOfEvent[3].ToLower()) + 1;
            var day = int.Parse(dateOfEvent[1]);
            int hour = int.Parse(dateOfEvent[6]);
            var minute = int.Parse(dateOfEvent[7]);
            var time = new DateTime(DateTime.Now.Year, month, day, hour, minute, 0);
            return time;
        }
    }
}
