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

        public void CheckPerformanceAndPrint() => PrintPerformance(GetPerformance());

        public Dictionary<string, int> GetPerformance()
        {
            Dictionary<string, int> Result = new Dictionary<string, int>();
            Stopwatch s = new Stopwatch();
            foreach(var l in Links)
            {
                WebClient wc = new WebClient();
                wc.Encoding = System.Text.Encoding.UTF8;
                s.Start();
                string robotsContent = wc.DownloadString(l);
                s.Stop();
                Result[l] = (int)s.ElapsedMilliseconds;
                s.Reset();
            }
            return Result;
        }

        public void PrintPerformance(Dictionary<string, int> Results)
        {
            foreach(var v in Results)
            {
                Console.WriteLine($"{v.Key} - {v.Value}");
            }
        }
    }
}