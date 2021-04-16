using System;
using System.Collections.Generic;

namespace WebPerformancer 
{
    public class WebSiteParser: SiteParser
    {
        public List<string> Links {get;set;}
        public WebSiteParser(string link) 
        {
            //TODO logic
        }

        public List<string> GetLinks()
        {
            throw new NotImplementedException();
        }
    }
}