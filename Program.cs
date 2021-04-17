using System;
using System.Net;
using System.Xml;

namespace WebPerformancer
{
    class Program
    {
        static void Main(string[] args)
        {
            // ISiteParser siteMap = new SiteMapParser("https://writemaps.com");
            // foreach(var s in siteMap.GetLinks())
            // {
            //     Console.WriteLine(s);
            // }
            string url = "https://writemaps.com/blog/how-to-find-your-sitemap/";
            ISiteParser sitemapParser = new SiteMapParser(url);
            ISiteParser webParser = new WebSiteParser(url); 
            var linksSitemap = sitemapParser.GetLinks();
            var linksWebparser = webParser.GetLinks();
            Console.WriteLine("Found in sitemap.xml:");
            foreach(var l in linksSitemap)
            {
                Console.WriteLine($"{l}");
            }

            Console.WriteLine("Found in page:");
            foreach(var l in linksWebparser)
            {
                Console.WriteLine($"{l}");
            }
        }
    }
}
