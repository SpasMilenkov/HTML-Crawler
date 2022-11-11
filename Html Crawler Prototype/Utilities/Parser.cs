
namespace HTML_Crawler_Prototype;

public class Parser
{
    //the reason for my existence
    public GTree<string> HtmlTree = new GTree<string>();
    public static string Html = "";
    //checks if a tag is closed
    public bool Closed = false;
    //checks if a tag is opened
    public bool Opened = false;
    //keeps the total number of opened tags
    public int TotalOpened = 0;
    //keeps the total number of closed tags
    public int TotalClosed = 0;
    //keeps temporary indexing data
    private static int _indexer = 0;
    public void ParseHtml(int begin, int end, GTree<string> parent)
    {
        if (begin >= end)
        {
                 return;
        }

        // html = Helper.Trim(html);
        string[] data = GetOpeningTag(begin, end);
        string props = "";
        for (int i = 1; i < data.Length; i++)
            props += data[i];
             
        // html = Helper.Slice(html, data[0].Length + 2);
        string value = GetValue(_indexer, data[0]);

        // html = Helper.Slice(html, 0, html.Length - (data[0].Length+3));
        if (Closed)
        {
            GTree<string> node = new GTree<string>();
            node.SetGTree(data[0], props, value, parent);
            parent.AddChild(node);
            // ParseHtml(html, node);
        }
        else
            Console.WriteLine("HTML parsing error");
             
    }
    public string[] GetOpeningTag(int begin, int end)
    {
        //start looking < and > to get the opening html tag
        string nodeData = "";

        for (int i = begin; i < end; i++)
        {
            char c = Html[i];
                
            if (c == '>')
            {
                Closed = false;
                i++;
                _indexer = i;
                return Helper.Split(nodeData, ' ');
            }
                
            if (Closed)
                nodeData += c;
                
            if (c == '<')
                Closed = true;
        }
        throw new Exception("Opening Tag Error");
    }

    public string GetValue(int begin, string tag)
    {   
        string val = "";
        for (int i = begin; i < Html.Length -1 ; i++)
        {
            char c = Html[i];
            if (c == '<' && Html[i + 1] == '/')
            {
                GetClosingTag(i + 2);
                // if (tag == closingTag)
                // {
                //     Closed = true;
                //     return val;
                // } 
            }
            val += c;
        }
        return val;
    }
    public static void GetClosingTag(int begin)
    {
        int i = begin;
        for (; i < Html.Length; i++)
        {
            char c = Html[i];
            if (c != '<')
            {
                _indexer = i;
                break;
            }
        }
    }
}