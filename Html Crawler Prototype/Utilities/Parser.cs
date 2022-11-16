
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
        //moves the indexer past the < symbol
        _indexer++;
        string nodeValue = "";
        char c = Html[_indexer];
        //checks for the end of the opening tag
        //checks for incorrect opening of tags inside the opening tag
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
        //if current node is null that means we reached the last lvl of depth of the html
        //then we reach the bottom of the recursion
        if (_indexer >= Html.Length - 1 && currentNode == null) 
            return;
        //need to move past the > char 
        _indexer++;
        char c = Html[_indexer];
        string textValue = "";
        while (c != '<' && _indexer < Html.Length-1)
        {
            textValue += c;
            _indexer++;
            c = Html[_indexer];
        }
        //checking if the next thing beginning with < is a closing tag and another node
        if (Html[_indexer + 1] == '/')
        {
            currentNode.Value = textValue;
            TotalClosed++;
            _indexer += currentNode.Tag.Length + 2;
            GetValue(currentNode.Parent); //get one level deeper into the html
        }
        //parsing text as a child node for elements that have more than one child
        // covers the case when:
        //<div>
        //      Some text        <--------------------------.
        //      <p>Paragraph text</p>                       |
        //      another text            (if we ignore the paragraph and take this as a string value
        //</div>                        we will end up with "Some text another text" appended on first position)
        GTree<string> childText = new GTree<string>();
        childText.Tag = "Text"; //introduce custom text node so that i can keep track of the text in a container
        childText.Value = textValue;
        childText.Parent = currentNode;
        currentNode.AddChild(childText);
        
        //create a new node for the next html container and add it to the parent
        GTree<string> child = new GTree<string>();
        child.Parent = currentNode;
        currentNode.AddChild(child);
        //get the opening tag and properties of the newly found child element
        GetOpeningTag(child);

    }
    //currently unneeded probably gonna remove later
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