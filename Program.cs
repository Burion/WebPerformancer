using System;
using System.Collections.Generic;
using System.Net;
using System.Xml;

namespace WebPerformancer
{
    class Program
    {
        static List<string> linksSitemap;
        static List<string> linksWebparser;
        ISiteParser webParser;

        //string url = "https://regex101.com";
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
                    break;

                }
                catch (Exception)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("The url is not found.");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }

            while(true) 
            {
                Console.Clear();
                Console.WriteLine("Select the option (press appropriate key):");
                Console.WriteLine("1. Show links is sitemaps.");
                Console.WriteLine("2. Show links from site parsing.");
                Console.WriteLine("3. Merge links.");
                Console.WriteLine("4. Show unique links in sitemap.");
                Console.WriteLine("5. Show unique links from site parsing.");
                Console.WriteLine("6. Estimate performance of all links.");
                Console.WriteLine("7. Exit.");

                var k = Console.ReadKey();
                
                Console.Clear();
                switch(k.Key)
                {
                    case ConsoleKey.D1: 
                        
                        Console.WriteLine("Found in sitemap.xml:");
                        foreach(var l in linksSitemap)
                        {
                            Console.WriteLine($"{l}");
                        }
                        Console.ReadLine();
                    break;

                    case ConsoleKey.D2: 
                        Console.WriteLine("Found in page:");
                        foreach(var l in linksWebparser)
                        {
                            Console.WriteLine($"{l}");
                        }
                        Console.ReadLine();
                    break;

                    case ConsoleKey.D3: 
                        
                        Console.WriteLine("Found in sitemap.xml:");
                        foreach(var l in linksSitemap)
                        {
                            Console.WriteLine($"{l}");
                        }
                        Console.ReadLine();
                    break;
                    case ConsoleKey.D4: 
                        
                        Console.WriteLine("Unique in sitemap:");
                        foreach(var l in ListAssembler.UniqueItemsInOriginal(linksSitemap, linksWebparser))
                        {
                            Console.WriteLine(l);
                        }
                        Console.ReadLine();
                    break;
                    case ConsoleKey.D5: 
                        
                         Console.WriteLine("Unique in web page:");
                        foreach(var l in ListAssembler.UniqueItemsInOriginal(linksWebparser, linksSitemap))
                        {
                            Console.WriteLine(l);
                        }
                        Console.ReadLine();
                    break;
                    case ConsoleKey.D6: 
                        
                        PerformanceHendler ph = new PerformanceHendler(ListAssembler.Merge(linksSitemap, linksWebparser));
                        ph.CheckPerformanceAndPrint();
                        Console.ReadLine();
                    break;
                    case ConsoleKey.D7: 
                        Environment.Exit(0);
                    break;
                    default:

                    break;

                }
            }

            
            
            

           

            
        }
    }
}
