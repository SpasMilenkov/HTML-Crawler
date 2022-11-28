using System.Security.Authentication.ExtendedProtection;
using HTML_Crawler_Prototype;

namespace Html_Crawler_Prototype.Utilities;

public class HtmlParser
{
    //the reason for my existence
    public GTree<string> HtmlTree = new GTree<string>();
    public string Html = "";
    //keeps temporary indexing data
    private static int _indexer = 0;
    //error flag
    private static bool errorFlag = false;
    private int _closedTags = 0;
    private int _openedTags = 0;
    //a hashmap containing all valid html tags
    private static Dictionary _validTags = new Dictionary(130);
    private int _depth = 1;
    
    public void ParseHtml()
    {
        LoadTagsDB();

        char c = Html[0];
        GTree<string> node = new GTree<string>();
        if (c != '<')
        {
            Console.WriteLine("Invalid Document");
            return;
        }

        // GetOpeningTag(HtmlTree);
        IterativeParse();
        if (_closedTags != _openedTags)
        {
            Console.WriteLine("Invalid document! Tags are missing");
        }
    }
    public void IterativeParse()
    {
        // try
        // {
            GTree<string> currentNode = new GTree<string>();
            Html.TrimStart();
            Html.TrimEnd();
            if (Html[0] != '<')
            {
                Console.WriteLine("Invalid Document");
                return;
            }
            for (int i = 0; i < Html.Length;)
            {
                string nodeValue = "";
                string value = "";
                if(currentNode == null)
                    return;
                if (Html[i] == '<')
                {
                    i++;
                    while (Html[i] != '>')
                    {
                        nodeValue += Html[i];
                        i++;
                    }
                    string[] nodeEls = Helper.Split(nodeValue, ' ');
                    currentNode.Tag = nodeEls[0];
                    string hashed = _validTags.FindKey(currentNode.Tag);

                    if(nodeEls.Length > 1)
                        for (int j = 1; j < nodeEls.Length; j++)
                            currentNode.Props.Add(nodeEls[j]);
                    
                    if (Html[i-1] == '/')
                    {
                        if (hashed != "selfClosing")
                        {
                            Console.WriteLine($"{currentNode.Tag} invalid closing tag");   
                            errorFlag = true;
                            return;
                        }
                        currentNode.Parent.AddChild(currentNode);
                        currentNode = currentNode.Parent;
                        continue;
                    }
                    _openedTags++;
                }
                bool emptyString = true;
                i++;
                if(currentNode.Tag == null)
                    continue;
                while (i <= Html.Length-1 && Html[i] != '<' )
                {
                    if (Html[i] != ' ' && Html[i] != '\t' && Html[i] !='\n')
                        emptyString = false;
                    value += Html[i];
                    i++;
                }
                string closingTag = "";
                if (Html[i + 1] == '/')
                {
                    i += 2;
                    while (Html[i] != '>')
                    {
                        closingTag += Html[i];
                        i++;
                    }
                    string hashed = _validTags.FindKey(closingTag);
            
                    if (hashed == null || closingTag != currentNode.Tag)
                    {
                        Console.WriteLine($"{currentNode.Tag}invalid closing tag");
                        errorFlag = true;
                        throw new Exception("pain");
                    }
                    _closedTags++;
                    currentNode.Value = value;
                    _depth--;
                    currentNode.Depth = _depth;
                    currentNode = currentNode.Parent;
                    continue;
                }
                if (!emptyString)
                {
                    GTree<string> childText = new GTree<string>();
                    childText.Tag = "Text"; //introduce custom text node so that i can keep track of the text in a container
                    childText.Value = value;
                    childText.Parent = currentNode;
                    childText.Depth = _depth;
                    currentNode?.AddChild(childText);
                }
                GTree<string> child = new GTree<string>();

                child.Parent = currentNode;
                currentNode.AddChild(child);
                currentNode.Value = value;
                _depth++;
                //get the opening tag and properties of the newly found child element
                if (nodeValue == "html")
                {
                    HtmlTree = currentNode;
                }
                currentNode = child;
            }
        // }
        // catch (Exception e)
        // {
        //     Console.WriteLine("Parsing Error please. Please check your input again!");
        // }
    }
    public void GetOpeningTag(GTree<string>? currentNode)
    {
        if(_indexer >= Html.Length - 1)
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
        Helper.Slice(props, 1);
        string hashed = _validTags.FindKey(currentNode.Tag);
        
        if(props.Length > 1)
            for (int j = 1; j < props.Length; j++)
                currentNode.Props.Add(props[j]);
        
        if (hashed == null)
        {
            Console.WriteLine($"{currentNode.Tag}invalid tag");   
            errorFlag = true;
            return;
        }
        currentNode.Depth = _depth;
        if (Html[_indexer - 1] == '/')
        {
            if (hashed != "selfClosing")
            {
                Console.WriteLine($"{currentNode.Tag}invalid closing tag");   
                errorFlag = true;
                return;
            }
            currentNode.Parent.AddChild(currentNode);
            GetValue(currentNode.Parent);
            return;
        }
        _openedTags++;
        GetValue(currentNode);
    }

    public void GetValue(GTree<string>? currentNode)
    {
        //if current node is null that means we reached the last lvl of depth of the html
        //then we reach the bottom of the recursion
        if (_indexer >= Html.Length - 1)
            return;

        //need to move past the > char 
        _indexer++;
        char c = Html[_indexer];
        string textValue = "";
        string closingTag = "";
        bool emptyString = true;
        while (c != '<' && _indexer < Html.Length-1)
        {
            if (c != ' ' && c != '\t' && c!= '\n')
                emptyString = false;
            textValue += c;
            _indexer++;
            c = Html[_indexer];
        }
        //checking if the next thing beginning with < is a closing tag and another node
        if (Html[_indexer + 1] == '/')
        {
            _indexer += 2;
            c = Html[_indexer];
            while (c != '>')
            {
                closingTag += c;
                _indexer++;
                c = Html[_indexer];
            }
            string hashed = _validTags.FindKey(closingTag);
            
            if (hashed == null || closingTag != currentNode.Tag)
            {
                Console.WriteLine($"{currentNode.Tag}invalid closing tag");
                errorFlag = true;
                return;
            }
            _closedTags++;
            currentNode.Value = textValue;
            _depth--;
            currentNode.Depth = _depth;
            GetValue(currentNode.Parent); //get one level deeper into the html
            return;
        }
        //parsing text as a child node for elements that have more than one child
        // covers the case when:
        //<div>
        //      Some text        <--------------------------.
        //      <p>Paragraph text</p>                       |
        //      another text            (if we ignore the paragraph and take this as a string value
        //</div>                        we will end up with "Some text another text" appended on first position)
        if (!emptyString)
        {
            GTree<string> childText = new GTree<string>();
            childText.Tag = "Text"; //introduce custom text node so that i can keep track of the text in a container
            childText.Value = textValue;
            childText.Parent = currentNode;
            childText.Depth = _depth;
            currentNode?.AddChild(childText);
        }
        //create a new node for the next html container and add it to the parent
        GTree<string> child = new GTree<string>();

        child.Parent = currentNode;
        currentNode.AddChild(child);
        
        _depth++;
        //get the opening tag and properties of the newly found child element
        GetOpeningTag(child);
    }
    //parse the TagTable file and load the correct html tags in the 
    public void LoadTagsDB()
    {
        using (StreamReader sr = new StreamReader("TagTable.txt"))
        {
            string line;
    
            while ((line = sr.ReadLine()) != null)
            {
                string[] split = Helper.Split(line, ' ');
                _validTags.Add(split[0], split[1]);
            }
        }
    }
    
    //Parse the user query
    public void ParseInput(string input)
    {
        // switch ()
        // {
        //     case "PRINT":
        //         PrintNode(SearchNode());
        //         break;
        //     case "SET":
        //         SetNode(SearchNode());
        //         break;
        //     case "COPY":
        //         CopyNode(SearchNode());
        //         break;
        // }
    }
    private GTree<string> SearchNode()
    {
        throw new NotImplementedException();
    }
    private string PrintNode(GTree<string> node)
    {
        string result = "";
        var unvisited = new MyStack<GTree<string>>();
        unvisited.Push(node);
        while (unvisited != null)
        {
            var visiting = unvisited.Pop();
           
        }
        return result;
    }
    private void SetNode(GTree<string> node)
    {
        throw new NotImplementedException();
    }
    private void CopyNode(GTree<string> node)
    {
        throw new NotImplementedException();
    }
}