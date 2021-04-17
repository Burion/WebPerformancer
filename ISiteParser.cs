using System;
using System.Collections.Generic;

namespace WebPerformancer
{
    public interface ISiteParser   
    {
        List<string> GetLinks();
    }
}