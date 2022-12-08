using HTML_Crawler_Prototype;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTML_Crawler.Router
{
    internal class Router
    {
        public void ParseInput(string input)
        {
            string command = "";

            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == ' ')
                {
                    Helper.Slice(input, i);
                    break;
                }
                command += input[i];
            }

            switch (command)
            {
                case "PRINT":
                    break;
                case "SET":
                    break;
                case "Copy":
                    break;
                case "//":
                    break;
            }
        }
    }
}
