using System;
using System.Linq;
using System.Collections.Generic;
using System.Net;
using System.Xml;
using System.Text.RegularExpressions;

namespace WebPerformancer 
{
    public class SiteMapParser: ISiteParser
    {
        public List<string> Links {get;set;}
        readonly string _link;
        public SiteMapParser(string link) 
        {
            _link = link;
            Links = new List<string>();
        }

        List<string> SitemapsFromRobots()
        {
            List<string> sitemaps = new List<string>();
            try
            {
                WebClient wc = new WebClient();
                wc.Encoding = System.Text.Encoding.UTF8;
                string robotsContent = wc.DownloadString(_link + "/robots.txt");
                Regex r = new Regex("[aA-zZ0-9./:]+.xml");
                foreach(var l in r.Matches(robotsContent))
                {
                    sitemaps.Add(l.ToString());
                }
            }
            catch (Exception)
            {
                Console.WriteLine($"No robots.txt file for {_link}");
            }
            return sitemaps;
        }
        public List<string> GetLinks()
        {
            List<string> sitemaplinks = SitemapsFromRobots();
            sitemaplinks.Add(_link + "/sitemap.xml");
            sitemaplinks.Add(_link + "/sitemap_index.xml");
            sitemaplinks.Add(_link + "/sitemapindex.xml");

            foreach(var l in sitemaplinks)
            {
                try 
                {
                    WebClient wc = new WebClient();
                    wc.Encoding = System.Text.Encoding.UTF8;
                    string sitemapString = wc.DownloadString(l);
                    XmlDocument urldoc = new XmlDocument();
                    urldoc.LoadXml(sitemapString);
                    XmlNodeList xmlSitemapList = urldoc.GetElementsByTagName("url");
                    foreach (XmlNode node in xmlSitemapList)
                    {
                        Links.Add(node["loc"].InnerText);
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine($"No sitemap found for {l}");
                }
                
            }
            return Links;
        }
    }
}