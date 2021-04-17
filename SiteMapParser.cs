using System;
using System.Linq;
using System.Collections.Generic;
using System.Net;
using System.Xml;

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

        public List<string> GetLinks()
        {
            //TODO different sitemap locations
            WebClient wc = new WebClient();
            wc.Encoding = System.Text.Encoding.UTF8;
            string sitemapString = wc.DownloadString(_link + "/sitemap.xml");
            XmlDocument urldoc = new XmlDocument();
            urldoc.LoadXml(sitemapString);
            XmlNodeList xmlSitemapList = urldoc.GetElementsByTagName("url");
            foreach (XmlNode node in xmlSitemapList)
            {
                Links.Add(node["loc"].InnerText);
                
            }
            return Links;
        }
    }
}