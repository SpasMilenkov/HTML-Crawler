
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
    public void ParseHtml(string html, GTree<string> parent)
    {
        if (html.Length == 0)
        {
                 return;
        }

        html = Helper.Trim(html);
        string[] data = GetOpeningTag(html);
        string props = "";
        for (int i = 1; i < data.Length; i++)
            props += data[i];
             
        html = Helper.Slice(html, data[0].Length + 2);
        string value = GetValue(html, data[0]);

        html = Helper.Slice(html, 0, html.Length - (data[0].Length+3));

        if (Closed == true)
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

        for (int j = 0; j < html.Length; j++)
        {
            char c = html[j];
                
            if (c == '>')
            {
                Closed = false;
                return Helper.Split(nodeData, ' ');
            }
                
            if (Closed)
                nodeData += c;
                
            if (c == '<')
                Closed = true;
        }
        throw new Exception("Internal Error");
    }

    public string GetValue(string text, string tag)
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
                    Closed = true;
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
}