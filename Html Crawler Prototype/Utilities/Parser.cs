
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
    //keeps temporary indexing data
    private static int _indexer = 0;
    public void ParseHtml()
    {
        for (int i = 0; i < Html.Length; i++)
        {
            char c = Html[i];
            if (c == '<')
            {
                GetOpeningTag(HtmlTree);
            }
        }
    }
    public void GetOpeningTag(GTree<string> currentNode)
    {
        if(currentNode == null || _indexer >= Html.Length - 1)
            return;
        _indexer++;
        string nodeValue = "";
        char c = Html[_indexer];
        while (c != '>' && _indexer < Html.Length-1 && c != '<')
        {

            nodeValue += c;
            _indexer++;
            c = Html[_indexer];
        }
        string[] props = Helper.Split(nodeValue, ' ');
        currentNode.Tag = props[0];
        Helper.Slice(props, 1, props.Length);
        GetValue(currentNode);

    }

    public void GetValue(GTree<string> currentNode)
    {
        if (_indexer >= Html.Length - 1 && currentNode == null)
            return;
        _indexer++;
        char c = Html[_indexer];
        string textValue = "";
        while (c != '<' && _indexer < Html.Length-1)
        {
            textValue += c;
            _indexer++;
            c = Html[_indexer];
        }
        if (Html[_indexer + 1] == '/')
        {
            currentNode.Value = textValue;
            TotalClosed++;
            _indexer += currentNode.Tag.Length + 2;
            GetValue(currentNode.Parent);
        }
        GTree<string> childText = new GTree<string>();
        childText.Tag = "Text";
        childText.Value = textValue;
        childText.Parent = currentNode;
        currentNode.AddChild(childText);
        
        GTree<string> child = new GTree<string>();
        child.Parent = currentNode;
        currentNode.AddChild(child);
        GetOpeningTag(child);

    }
    public void GetClosingTag(int begin)
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