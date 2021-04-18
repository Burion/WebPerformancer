using System;
using System.Text.RegularExpressions;

namespace WebPerformancer
{
    public static class LinkHelper
    {
        public static string GetBaseLink(string link)
        {
            return Regex.Match(link, "http[s]?://[aA-zZ0-9.-]+").Value;
        }
    }
}