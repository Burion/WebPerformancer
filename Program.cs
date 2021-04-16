using System;
using System.Net;
using System.Xml;

namespace WebPerformancer
{
    class Program
    {
        static void Main(string[] args)
        {
            SiteParser siteMap = new SiteMapParser("https://writemaps.com");
            foreach(var s in siteMap.GetLinks())
            {
                Console.WriteLine(s);
            }
        }
    }
}
