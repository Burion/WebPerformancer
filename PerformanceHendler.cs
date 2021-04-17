using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;

namespace WebPerformancer
{
    public class PerformanceHendler
    {
        readonly List<string> Links;
        public PerformanceHendler(List<string> links)
        {
            Links = links;
        }

        public void CheckPerformanceAndPrint()
        {
            Stopwatch s = new Stopwatch();
            foreach(var l in Links)
            {
                WebClient wc = new WebClient();
                wc.Encoding = System.Text.Encoding.UTF8;
                
                s.Start();
                string robotsContent = wc.DownloadString(l);
                s.Stop();
                Console.WriteLine($"{l} - {s.ElapsedMilliseconds}");
                s.Reset();
            }
        }
    }
}