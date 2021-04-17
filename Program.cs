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
            ISiteParser webparser = new WebSiteParser("https://stackoverflow.com");
            webparser.GetLinks();
        }
    }
}
