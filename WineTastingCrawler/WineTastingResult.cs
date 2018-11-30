using System;

namespace WineTastingCrawler
{
    public class WineTastingResult
    {
        public bool NewWineTasting
        {
            get;
            set;
        }

        public DateTime Time
        {
            get;
            set;
        }

        public string Title { get; set; }
        public string Link { get; set; }

        public string Price { get; set; }
        public object Description { get; internal set; }
    }
}