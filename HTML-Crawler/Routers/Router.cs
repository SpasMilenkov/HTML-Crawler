using Html_Crawler_Prototype.Utilities;
using HTML_Crawler_Prototype;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace HTML_Crawler.Routers
{
    public class Router
    {
        private HtmlParser _parser;

        public Router()
        {
            _parser = new HtmlParser();
        }
        public void ParseInput(string input)
        {
            string command = IdentifyWord(input);

            input = Helper.Slice(input, command.Length);
            

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
        private string IdentifyWord(string input)
        {
            string word = "";
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == ' ')
                {
                    input = Helper.Slice(input, i);
                    break;
                }
                word += input[i];
            }
            return word;
        }

        public void LoadDocument(string path)
        {
            string html = File.ReadAllText(path);
            _parser.Html = html;
            _parser.ParseHtml();
        }
    }
}
