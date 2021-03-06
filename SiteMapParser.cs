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
        readonly string baselink;
        public SiteMapParser(string link) 
        {
            _link = link;
            baselink = LinkHelper.GetBaseLink(_link);
            Links = new List<string>();
        }

        List<string> SitemapsFromRobots()
        {
            List<string> sitemaps = new List<string>();
            try
            {
                WebClient wc = new WebClient();
                wc.Encoding = System.Text.Encoding.UTF8;
                string robotsContent = wc.DownloadString(LinkHelper.GetBaseLink(_link) + "/robots.txt");
                Regex r = new Regex("[aA-zZ0-9./:-]+.xml");
                foreach(var l in r.Matches(robotsContent))
                {
                    sitemaps.Add(l.ToString());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return sitemaps;
        }


        List<string> ReadSiteMapRecursive(string url)
        {

            List<string> resultLinks = new List<string>();
            WebClient wc = new WebClient();
            wc.Encoding = System.Text.Encoding.UTF8;
            try
            {
                string sitemapString = wc.DownloadString(url);
                XmlDocument urldoc = new XmlDocument();
                urldoc.LoadXml(sitemapString);

                XmlNodeList sitemaps = urldoc.GetElementsByTagName("sitemap");
                foreach (XmlNode node in sitemaps)
                {
                    resultLinks.AddRange(ReadSiteMapRecursive(node["loc"].InnerText));
                }

                XmlNodeList xmlSitemapList = urldoc.GetElementsByTagName("url");
                foreach (XmlNode node in xmlSitemapList)
                {
                    resultLinks.Add(node["loc"].InnerText);
                }
            }
            catch (Exception)
            {
                
            }
            
            return resultLinks;
        }

        public List<string> GetLinks()
        {
            
            List<string> sitemaplinks = SitemapsFromRobots();
            if(!sitemaplinks.Contains(baselink + "/sitemap.xml"))
                sitemaplinks.Add(baselink + "/sitemap.xml");

            if(!sitemaplinks.Contains(baselink + "/sitemap_index.xml"))
                sitemaplinks.Add(baselink + "/sitemap_index.xml");

            if(!sitemaplinks.Contains(baselink + "/sitemapindex.xml"))
                sitemaplinks.Add(baselink + "/sitemapindex.xml");
            
            if(sitemaplinks.Count == 0)
            {
                Console.WriteLine("Sitemap file not found");
            }
            else
            foreach(var l in sitemaplinks)
            {
                try 
                {
                    var smps = ReadSiteMapRecursive(l);
                    if(smps.Count != 0)
                    {
                        foreach(var li in ReadSiteMapRecursive(l))
                        {
                            Links.Add(li);
                        }
                        Console.WriteLine($"Found following sitemap file: {l}");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                
            }
            return Links.Distinct().ToList();
        }
    }
}