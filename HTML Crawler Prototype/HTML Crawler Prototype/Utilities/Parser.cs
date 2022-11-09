namespace HTML_Crawler_Prototype;

public class Parser
{
    //the reason for my existence
    public GTree<string> HtmlTree = new GTree<string>();
    public string Html = "";
    //checks if a tag is closed
    public bool Closed = false;
    //checks if a tag is opened
    public bool Opened = false;
    //keeps the total number of opened tags
    public int TotalOpened = 0;
    //keeps the total number of closed tags
    public int TotalClosed = 0;
    //indexes the document
    public static int _indexer = 0;
    public void ParseHtml(string html, GTree<string> parent)
    {
        if (html.Length == 0)
        {
                 return;
        }

        // html = Helper.Trim(html);
        string[] data = GetOpeningTag(html);
        string props = "";
        for (int i = 1; i < data.Length; i++)
            props += data[i];
             
        // html = Helper.Slice(html, data[0].Length + 2);
        string value = GetValue(html, data[0]);

        // html = Helper.Slice(html, 0, html.Length - (data[0].Length+3));
        if (Closed)
        {
            GTree<string> node = new GTree<string>();
            node.SetGTree(data[0], props, value, parent);
            parent.AddChild(node);
            ParseHtml(html, node);
        }
        else
            Console.WriteLine("HTML parsing error");
             
    }
    public string[] GetOpeningTag(string html)
    {
        //start looking < and > to get the opening html tag
        string nodeData = "";

        for (; _indexer < html.Length; _indexer++)
        {
            char c = html[_indexer];
                
            if (c == '>')
            {
                Closed = false;
                _indexer++;
                return Helper.Split(nodeData, ' ');
            }
                
            if (Closed)
                nodeData += c;
                
            if (c == '<')
                Closed = true;
        }
        throw new Exception("Opening Tag Error");
    }

    public string GetValue(string text, string tag)
    {   
        string val = "";
        for (; _indexer < text.Length; _indexer++)
        {
            char c = text[_indexer];
            if (c == '<' && text[_indexer + 1] == '/')
            {
                //skips the </
                _indexer += 2;
                string closingTag = GetClosingTag(text);
                if (tag == closingTag)
                {
                    Closed = true;
                    return val;
                } 
            }
            val += c;
        }
        return val;
    }
    public static string GetClosingTag(string html)
    {
        char c = html[_indexer];
        string tag = "";
        while (c != '>' && _indexer <html.Length - 1)
        {
            tag += c;
            _indexer++;
            c = html[_indexer];
        }

        return tag;
    }
}