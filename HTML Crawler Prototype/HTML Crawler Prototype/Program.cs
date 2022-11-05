
//dear programmer as of writing this program only i and God knew how the thing worked
//with the passing of time however now only God knows
//after you are done bumping your head in the wall trying to fix this 
//increment the counter below so the next dreaded individual knows what awaits him 
//hours wasted on this: 17

using System.Diagnostics;

namespace HTML_Crawler_Prototype
{
    public class Program 
    {
        //<div> </div>
        //<img></img>
        //<img/>
        //<br/>
        //<br> </br>
        //<div/>
        
        //HARDCODED REMOVE LATER
        private static string _Path = "/home/spasmilenkov/Documents/SAA-uni/small-test.txt";
        //the reason for my existence
        private static GTree<string> HtmlTree = new GTree<string>();
        //allows access to all string manipulation commands
        private static Helper _helper = new Helper();
        static string _html = "";
        //checks if a tag is closed
        private static bool _closed = false;
        //checks if a tag is opened
        private static bool _openTag = false;
        //keeps the total number of opened tags
        private static int _totalOpened = 0;
        //keeps the total number of closed tags
        private static int _totalClosed = 0;
        
        public static void Main(string[] args)
        {
            LoadUi();
        }
        //the simple command line interface rendering
        public static void LoadUi()
        {
            char command = ' ';
            do
            {
                Console.WriteLine("---------------------------------------------------------------------------------------"+
                                  "\nWelcome to the HTML Crawler. Here are the commands:\n" +
                                  "0: Exit the program\n"+
                                  "1: Load a file from the file system\n" +
                                  "2: Print node\n" +
                                  "3: Save html file\n" +
                                  "Enter command");
                command = Console.ReadKey().KeyChar;
                Console.WriteLine("\n---------------------------------------------------------------------------------------");
                switch (command)
                {
                    case '1':
                        LoadFile();
                        break;
                    case '2':
                        PrintNode();
                        break;
                    case '3':
                        SaveDocument();
                        break;
                    case '0':
                        break;
                    // case '1':
                    //     break;
                    default:
                        Console.WriteLine("Invalid Input!");
                        break;
                }
            } while (command != '0');
        }
        public static void LoadFile()
        {
            try
            {
                using (StreamReader sr = new StreamReader(_Path))
                {
                    string line;

                    while ((line = sr.ReadLine()) != null)
                    {
                        _html = _html += line;
                        Console.WriteLine(line);
                    }
                }
            }
            catch (Exception e)
            {
                // Let the user know what went wrong.
                Console.WriteLine(_Path);
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
            // create the tree s first node
            string[] data = GetOpeningTag(_html);
            string props = "";
            for (int i = 1; i < data.Length; i++)
                props += data[i];
            _html = _helper.Slice(_html, data[0].Length + 2);
            string value = GetValue(_html, data[0]);
            value = _helper.Slice(_html, 0, _html.Length - (data[0].Length + 3));
            HtmlTree.SetGTree(data[0], "", value, null);
            
            ParseHtml(value, HtmlTree);
        }
        public static void ParseHtml(string html, GTree<string> parent)
         {
             if (html.Length == 0)
             {
                 return;
             }

             html = _helper.Trim(html);
             string[] data = GetOpeningTag(html);
             string props = "";
             for (int i = 1; i < data.Length; i++)
                 props += data[i];
             
             html = _helper.Slice(html, data[0].Length + 2);
             string value = GetValue(html, data[0]);

             html = _helper.Slice(html, 0, html.Length - (data[0].Length+3));

             if (_closed == true)
             {
                 GTree<string> node = new GTree<string>();
                 node.SetGTree(data[0], props, value, parent);
                 parent.AddChild(node);
                 ParseHtml(html, node);
             }
             else
                Console.WriteLine("HTML parsing error");
             
         }
        public static string[] GetOpeningTag(string html)
        {
            //start looking < and > to get the opening html tag
            string nodeData = "";

            for (int j = 0; j < html.Length; j++)
            {
                char c = html[j];
                
                if (c == '>')
                {
                    _openTag = false;
                    return _helper.Split(nodeData, ' ');
                }
                
                if (_openTag)
                    nodeData += c;
                
                if (c == '<')
                    _openTag = true;
            }
            throw new Exception("Internal Error");
        }

        public static string GetValue(string text, string tag)
        {   
            string val = "";
            for (int j = 0; j < text.Length; j++)
            {
                char c = text[j];
                if (c == '<' && text[j + 1] == '/')
                {
                    string closingTag = GetClosingTag(text, j+2);
                    if (tag == closingTag)
                    {
                        _closed = true;
                        return val;
                    } 
                }
                val += c;
            }
            return val;
        }
        public static string GetClosingTag(string html, int i)
        {
            char c = html[i];
            string tag = "";
            while (c != '>' && i <html.Length - 1)
            {
                tag += c;
                i++;
                c = html[i];
            }

            return tag;
        }
         public static void PrintNode()
         {
             
         }
         public static void SaveDocument()
         {
             
         }
    }
}