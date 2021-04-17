using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;

namespace WebPerformancer 
{
    public class WebSiteParser: ISiteParser
    {
        public List<string> Links {get;set;}
        public string _link;
        public WebSiteParser(string link) 
        {
            _link = link;
        }

        public List<string> GetLinks()
        {
            WebClient wc = new WebClient();
            wc.Encoding = System.Text.Encoding.UTF8;
            string html = wc.DownloadString(_link);

            string baselink = Regex.Match(_link, "http[s]?://[aA-zZ0-9.]+").Value;
            Console.WriteLine(baselink);
            Regex rx = new Regex($"{baselink}[/aA-zZ0-9/.]+");
            foreach(var m in rx.Matches(html))
            {
                Console.WriteLine(m);
            }
            return new List<string>();
        
        }
    }
}