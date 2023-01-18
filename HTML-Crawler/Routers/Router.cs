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
        public string ParseInput(string input)
        {
            string command = IdentifyWord(input);

            input = Helper.Slice(input, command.Length + 1);
            

            switch (command)
            {
                case "PRINT":
                    return _parser.PrintInput(input);
                case "SET":

                    string path = IdentifyWord(input);
                    input = Helper.Slice(input, path.Length + 1);

                    if (input[1] == '<')
                    {
                        _parser.SetSubtree(path, input);
                        return _parser.PrintInput("\"//\"");
                    }
                    _parser.SetInput(path, input);
                    return _parser.PrintInput("\"//\"");
                case "Copy":
                    _parser.CopyInput(input);
                    return _parser.PrintInput("\"//\"");
                case "//":
                    return _parser.PrintInput(input);
                default:
                    return "Invalid input";
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
            string[] pathSplit = Helper.Split(path, '\\');

            for (int i = 0; i < pathSplit.Length-1; i++)
            {
                Directory += pathSplit[i] + '\\';
            }
            _parser.ParseHtml();
        }
        public void SaveDocument(string path)
        {
            using(var fs = File.Create(path))
            {
                byte[] html = new UTF8Encoding(true).GetBytes(_parser.PrintInput("\"//\""));
                fs.Write(html, 0, html.Length);
            }
        }
        public GTree<string> GetTree() => _parser.HtmlTree;
    }
}
