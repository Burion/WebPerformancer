using System;
using System.Collections.Generic;
using System.Net;
using System.Xml;
using System.Linq;

namespace WebPerformancer
{
    class Program
    {
        static List<string> linksSitemap;
        static List<string> linksWebparser;

        static void Main(string[] args)
        {
            Console.WriteLine("2021 - Buriak Vladyslav");
            
            while(true)
            {
                Console.WriteLine("Please, enter the url:");
                var url = Console.ReadLine();
                Console.Clear();
                Console.WriteLine("Please, wait...");

                try 
                {
                    ISiteParser sitemapParser = new SiteMapParser(url);
                    ISiteParser webParser = new WebSiteParser(url); 
                    linksSitemap = sitemapParser.GetLinks();
                    linksWebparser = webParser.GetLinks();
                    Console.WriteLine("Press Enter.");
                    Console.ReadLine();
                    break;


                }
                catch (Exception)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("The url is not found.");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            
            PerformanceHandler ph = new PerformanceHandler(ListAssembler.Merge(linksSitemap, linksWebparser));
            Console.WriteLine("Please, wait...");
            var dict = ph.GetPerformance();
            
            List<LinkRecordModel> records = new List<LinkRecordModel>();
            foreach(var kv in dict.OrderBy(r => r.Value).ToDictionary(r => r.Key, r => r.Value))
            {
                int takenFrom = ListAssembler.UniqueItemsInOriginal(linksWebparser, linksSitemap).Contains(kv.Key) 
                ? (int)Sources.WebSite 
                : ListAssembler.UniqueItemsInOriginal(linksSitemap, linksWebparser).Contains(kv.Key) 
                ? (int)Sources.Sitemaps
                : (int)Sources.Both;

                records.Add(new LinkRecordModel() { Link = kv.Key, AnswerTimeMills = kv.Value, TakenFrom = takenFrom });
            }
            TablePrinter tp = new TablePrinter(records, 150);
            tp.Print();
            Console.ReadLine();
        
        }
    }
}
