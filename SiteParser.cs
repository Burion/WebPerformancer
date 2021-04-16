using System;
using System.Collections.Generic;

namespace WebPerformancer
{
    public interface SiteParser   
    {
        List<string> GetLinks();
    }
}