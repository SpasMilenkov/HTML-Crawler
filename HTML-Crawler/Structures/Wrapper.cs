using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTML_Crawler.Structures
{
    public class Wrapper<T>
    {
        public T Value;
        public int Depth;

        public Wrapper(T node,int depth)
        {
            Value = node;
            Depth = depth;
        }
    }
}
