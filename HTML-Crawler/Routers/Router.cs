using Html_Crawler_Prototype.Utilities;
using HTML_Crawler_Prototype;
using System.Text;

namespace HTML_Crawler.Routers
{
    public class Router
    {
        private HtmlParser _parser;
        public string Directory;

        public Router()
        {
            _parser = new HtmlParser();
        }
        public string ParseInput(string input)
        {
            string command = IdentifyWord(input, 0);
            

            switch (command)
            {
                case "PRINT":
                    return _parser.PrintInput(IdentifyWord(input, command.Length + 1));
                case "SET":

                    string path = IdentifyWord(input, command.Length + 1);
                    string filler = Helper.Slice(input, path.Length + 2 + command.Length);

                    if (filler[1] == '<')
                    {
                        _parser.SetSubtree(path, filler);
                        return _parser.PrintInput("\"//\"");
                    }
                    _parser.SetInput(path, Helper.Slice(filler, 1, filler.Length - 1));
                    return _parser.PrintInput("\"//\"");
                case "COPY":

                    string copyTo = IdentifyWord(input, 5);
                    string copyFrom = IdentifyWord(input, 6 + copyTo.Length);

                    copyTo = Helper.Slice(copyTo,2, copyTo.Length - 1);
                    copyFrom = Helper.Slice(copyFrom, 2, copyFrom.Length - 1);

                    _parser.Copy(Helper.Split(copyFrom, '/'), Helper.Split(copyTo, '/'));

                    return _parser.PrintInput("\"//\"");
                case "//":
                    return _parser.PrintInput(input);
                default:
                    return "Invalid input";
            }
        }
        private string IdentifyWord(string input, int i)
        {
            string word = "";
            for (; i < input.Length; i++)
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
        public void SaveDocument(Stream saveFile)
        {
            byte[] html = new UTF8Encoding(true).GetBytes(_parser.PrintInput("\"//\""));
            saveFile.Write(html, 0, html.Length);
        }
        public GTree<string> GetTree() => _parser.HtmlTree;
    }
}
