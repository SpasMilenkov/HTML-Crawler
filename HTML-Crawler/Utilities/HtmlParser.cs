using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.NetworkInformation;
using System.Security.Authentication.ExtendedProtection;
using HTML_Crawler_Prototype;
using Microsoft.VisualBasic.ApplicationServices;
using static System.Net.Mime.MediaTypeNames;

namespace Html_Crawler_Prototype.Utilities;

public class HtmlParser
{
    #region Global Variables
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
    #endregion

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
        try
        {
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
                if (currentNode == null)
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

                    if (nodeEls.Length > 1)
                        for (int j = 1; j < nodeEls.Length; j++)
                            currentNode.Props.Add(nodeEls[j]);
                    _depth++;

                    if (Html[i - 1] == '/')
                    {
                        if (hashed != "selfClosing")
                        {
                            Console.WriteLine($"{currentNode.Tag} invalid closing tag");
                            errorFlag = true;
                            return;
                        }
                        _depth--;
                        _openedTags++;
                        _closedTags++;
                        currentNode.Depth = _depth;
                        currentNode = currentNode.Parent;
                        currentNode.SelfClosing = true;
                        continue;
                    }
                    _openedTags++;
                }
                bool emptyString = true;
                i++;
                if (currentNode.Tag == null)
                    continue;
                while (i <= Html.Length - 1 && Html[i] != '<')
                {
                    if (Html[i] != ' ' && Html[i] != '\t' && Html[i] != '\n')
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
                //get the opening tag and properties of the newly found child element
                if (nodeValue == "html")
                {
                    HtmlTree = currentNode;
                }
                currentNode = child;
            }
        }
        catch (Exception e)
        {
            MessageBox.Show("Parsing Error please. Please check your input again!");
            throw new Exception();
        }
    }
    public void GetOpeningTag(GTree<string>? currentNode)
    {
        if (_indexer >= Html.Length - 1)
            return;
        //moves the indexer past the < symbol
        _indexer++;
        string nodeValue = "";
        char c = Html[_indexer];
        //checks for the end of the opening tag
        //checks for incorrect opening of tags inside the opening tag
        while (c != '>' && _indexer < Html.Length - 1 && c != '<')
        {
            nodeValue += c;
            _indexer++;
            c = Html[_indexer];
        }
        string[] props = Helper.Split(nodeValue, ' ');
        currentNode.Tag = props[0];
        Helper.Slice(props, 1);
        string hashed = _validTags.FindKey(currentNode.Tag);

        if (props.Length > 1)
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
        while (c != '<' && _indexer < Html.Length - 1)
        {
            if (c != ' ' && c != '\t' && c != '\n')
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
    public string PrintInput(string input)
    {
        
        GTree<string> subTree = HtmlTree;
        input = Helper.Slice(input, 2, input.Length - 1);
        string[] path = Helper.Split(input, '/');

        path = Helper.Slice(path, 1);

        string result = "";

        if (path.Length <= 1)
        {
            return PrintNode(HtmlTree, "");
        }
        else
        {
            var nodes = SearchNode(path, subTree);
            var node = nodes.First();

            while (node != null)
            {
                if (node.Value._childNodes.First() == null)
                {
                    result += PrintNode(node.Value, "");
                }
                node = node.Next;
            }
        }
        return result;
    }
    public void SetInput(string path, string input)
    {
        string[] pathSplitted = Helper.Split(Helper.Slice(path, 3, path.Length - 1), '/');
        MyLinkedList<GTree<string>> nodes = SearchNode(pathSplitted, HtmlTree);

        var node = nodes.First();

        while (node != null)
        {
            if (node.Value._childNodes.First() == null)
            {
                node.Value.Value = input;
            }
            node = node.Next;
        }
    }
    public MyLinkedList<GTree<string>> ParseSubTree(string input, int depth)
    {
        MyStack<GTree<string>> parents = new MyStack<GTree<string>>();
        int parentDepth = depth;
        MyLinkedList<GTree<string>> nodes = new MyLinkedList<GTree<string>>();
        GTree<string> currentNode = new GTree<string>();

        for (int i = 0; i < input.Length;)
        {
            string nodeValue = "";
            string value = "";
            if (input[i] == '<' && input[i + 1] == '/')
            {
                currentNode = parents.Pop();
                ClosingTagValidator(ref depth, input, ref i,ref currentNode, value);
                continue;
            }
            else if(input[i] == '<')
            {
                i++;
                while (input[i] != '>')
                {
                    nodeValue += input[i];
                    i++;
                }
                string[] nodeEls = Helper.Split(nodeValue, ' ');
                currentNode.Tag = nodeEls[0];
                string hashed = _validTags.FindKey(currentNode.Tag);

                if (nodeEls.Length > 1)
                    for (int j = 1; j < nodeEls.Length; j++)
                        currentNode.Props.Add(nodeEls[j]);
                depth++;
                currentNode.Depth = depth;
                if (input[i - 1] == '/')
                {
                    if (hashed != "selfClosing")
                    {
                        throw new Exception();
                    }
                    depth--;
                    _openedTags++;
                    _closedTags++;
                    currentNode.Depth = depth;
                    currentNode = currentNode.Parent;
                    currentNode.SelfClosing = true;

                    if (nodes.First().Value.Depth == currentNode.Depth)
                    {
                        nodes.AddLast(currentNode);
                    }

                    continue;
                }
                parents.Push(currentNode);
                _openedTags++;
            }

            i++;
            bool emptyString = true;
            //explodes
            if (currentNode.Tag == null)
                continue;
            if(i <= input.Length - 1)
            {
                while (i <= input.Length - 1 && input[i] != '<')
                {
                    if (input[i] != ' ' && input[i] != '\t' && input[i] != '\n')
                        emptyString = false;
                    value += input[i];
                    i++;
                }

                if (!emptyString)
                {
                    GTree<string> childText = new GTree<string>();
                    childText.Tag = "Text"; //introduce custom text node so that i can keep track of the text in a container
                    childText.Value = value;
                    childText.Parent = currentNode;
                    childText.Depth = depth;
                    currentNode?.AddChild(childText);
                }

                if (input[i + 1] == '/')
                {
                    currentNode = parents.Pop();
                    ClosingTagValidator(ref depth, input, ref i, ref currentNode, value);
                    continue;
                }
            }
  
            GTree<string> child = new GTree<string>();

            if (_openedTags != _closedTags)
            {
                currentNode.AddChild(child);
                currentNode.Value = value;
                currentNode = child;
                continue;
            }
            if (nodes.First() == null)
            {
                GTree<string> gTree = new GTree<string>(currentNode);
                nodes.AddFirst(gTree);
            }
            else
            {
                GTree<string> gTree = new GTree<string>(currentNode);
                nodes.AddLast(gTree);
            }
        }
        return nodes;
    }
    private void ClosingTagValidator(
        ref int depth,
        string input,
        ref int p,
        ref GTree<string> currentNode,
        string value)
    {
        string closingTag = "";
        p += 2;
        while (input[p] != '>')
        {
            closingTag += input[p];
            p++;
        }
        string hashed = _validTags.FindKey(closingTag);

        if (hashed == null || closingTag != currentNode.Tag)
        {
            Console.WriteLine($"{currentNode.Tag}invalid closing tag");
            throw new Exception("pain");
        }
        _closedTags++;
        currentNode.Value = value;
        depth--;
        currentNode.Depth = depth;
        //problem
    }
    public void SetSubtree(string path, string input)
    {
        string[] pathSplitted = Helper.Split(Helper.Slice(path, 3, path.Length - 1), '/');
        MyLinkedList<GTree<string>> parentNodes = SearchNode(pathSplitted, HtmlTree);
        var parent = parentNodes.First();
        MyLinkedList<GTree<string>> nodes = ParseSubTree(Helper.Slice(input, 1, input.Length-1), parent.Value.Depth);
        var node = nodes.First();

        while (parent != null)
        {
            var firstChild = parent.Value._childNodes.First();
            while(firstChild != null)
            {
                parent.Value._childNodes.Remove(firstChild);
                firstChild = firstChild.Next;
            }
            while(node != null)
            {
                if (parent.Value._childNodes.First() == null)
                    parent.Value._childNodes.AddFirst(node.Value);
                else
                    parent.Value._childNodes.AddLast(node.Value);

                node = node.Next;
            }
            parent = parent.Next;
        }
    }
    public void CopyInput(string input)
    {

    }
    private void PropSearch(
       ref MyQueue<GTree<string>> unvisited,
       ref MyLinkedList<GTree<string>> subTrees,
       ref int depth,
       string tag,
       string param,
       int pathLength)
    {
        while (!unvisited.IsEmpty())
        {
            var currentNode = unvisited.Dequeue();
            //if the tag or prop are not matching just go the next tag
            if (tag != currentNode.Tag || !currentNode.HasProp(param))
            {

                //if the next element is one level deeper we
                //reached the next level of the tree so we break of this algorithm
                if (currentNode.Depth != unvisited.Peek()?.Depth && depth < pathLength && !unvisited.IsEmpty())
                    break;

                continue;
            }
            //if we are at the last element of the user path we insert the element in the list and return
            if (depth == pathLength - 1)
            {
                if (subTrees.First() == null)
                {
                    subTrees.AddFirst(currentNode);
                    return;
                }

                subTrees.AddLast(currentNode);
                return;
            }
            //else we just add the children and continue
            var childNode = currentNode._childNodes.First();
            while (childNode != null)
            {
                unvisited.Enqueue(childNode.Value);
                childNode = childNode.Next;
            }
            //same as the check above
            if (currentNode.Depth != unvisited.Peek()?.Depth && depth < pathLength)
            {
                break;
            }
        }
    }
    private void PositionSearch(
        ref MyQueue<GTree<string>> unvisited,
        ref MyLinkedList<GTree<string>> subTrees,
        ref int depth,
        string tag,
        int elementPosition,
        int pathLength)
    {
        //the starting index of the elements gets incremented every time the algorithm finds a matching tag
        //until it aligns with the elementPosition desired by the user
        int index = 1;

        while (!unvisited.IsEmpty())
        {
            var currentNode = unvisited.Dequeue();

            if (tag == currentNode.Tag && index == elementPosition)
            {
                //if it is the end of the path add the element directly to the list and return
                if (depth == pathLength - 1)
                {
                    if (subTrees.First() == null)
                    {
                        subTrees.AddFirst(currentNode);
                        return;
                    }

                    subTrees.AddLast(currentNode);
                    return;
                }
                //if it is not the end of the path add the children of the current element and increase the depth and index;
                var childNode = currentNode._childNodes.First();
                while (childNode != null)
                {
                    unvisited.Enqueue(childNode.Value);
                    childNode = childNode.Next;
                }
                index++;
                depth++;
                return;
            }
            //if the tag is matching but we are not on the desired position just increment the index
            else if (tag == currentNode.Tag && index != elementPosition)
                index++;
        }
    }
    //traverses a subnode (that can be the entire tree if needed) and returns all nodes that 
    //meet the given user path criteria
    private MyLinkedList<GTree<string>> SearchNode(string[] userPath, GTree<string> subtree)
    {
        //contains the results with the nodes that met the crteria
        MyLinkedList<GTree<string>> subTrees = new MyLinkedList<GTree<string>>();

        //contains the nodes that are yet to be visited 
        MyQueue<GTree<string>> unvisited = new MyQueue<GTree<string>>();

        //tracks the depth of the path in order for the checks to work
        int depth = 0;

        unvisited.Enqueue(subtree);
        //main engine of the algorithm
        while (!unvisited.IsEmpty())
        {

            //checks if targument in the path is complex aka p[2] or table[@id='table2']
            if (depth >= userPath.Length)
                break;
            string[] complexInput = Helper.Split(userPath[depth], '[');
            if (complexInput.Length > 1)
            {
                //get the tag of the node
                string tag = complexInput[0];

                //get the second parameter of the complex algorithm
                string param = Helper.Slice(complexInput[1], 0, complexInput[1].Length - 1);

                //the int that contains the position of the parameter if we are searching by position in the document
                int elementPosition = 0;

                if (int.TryParse(param, out elementPosition))
                {
                    PositionSearch(ref unvisited, ref subTrees, ref depth, tag, elementPosition, userPath.Length);
                    continue;
                }
                else //the algorithm that iterates if we are searching by attribute
                {
                    //get the parameter in a form that can be compared with el properties
                    param = Helper.Slice(param, 1, param.Length);
                    PropSearch(ref unvisited, ref subTrees, ref depth, tag, param, userPath.Length);
                    depth++;
                    continue;
                }
            }
            //the main algorithm that runs if we just seach by tags
            //gets the first node in the queue
            GTree<string> currentNode = unvisited.Dequeue();
            //if the element s tag is matching and its not the last part of the user path we just add the children and continue
            if (depth < userPath.Length - 1 &&
                (currentNode.Tag == userPath[depth] || userPath[depth] == "*"))
            {
                var node = currentNode._childNodes.First();
                while (node != null)
                {
                    unvisited.Enqueue(node.Value);
                    node = node.Next;
                }
            }
            //if its the last part of the path we add the node
            //there could be multiple results so we dont return
            if (depth == userPath.Length - 1 &&
               (currentNode.Tag == userPath[depth] || userPath[depth] == "*"))
            {
                if (subTrees.First() == null)
                    subTrees.AddFirst(currentNode);
                
                else
                    subTrees.AddLast(currentNode);
                
            }

            //if the next element in the queue is with a different depth we reached the end of that tree level
            //and go one level down
            if (currentNode.Depth != unvisited.Peek()?.Depth && depth < userPath.Length)
                depth++;

        }
        //return the lists with the nodes that meet the user criteria
        return subTrees;
    }

    private string PrintNode(GTree<string> node, string result)
    {
        var firstChild = node._childNodes.First();
        if (firstChild != null)
        {

            result += $"<{node.Tag}>" + System.Environment.NewLine;
            

            while (firstChild != null)
            {
                if (firstChild.Value._childNodes.First != null)
                {
                    for (int i = 0; i < node.Depth; i++)
                    {
                        result += "   ";
                    }
                    result = PrintNode(firstChild.Value, result);

                }
                else
                {
                    result += $"<{firstChild.Value.Tag}> {firstChild.Value.Value} </{firstChild.Value.Tag}>" + System.Environment.NewLine;
                }
                if (firstChild.Next == null)
                {
                    for (int i = 1; i < node.Depth; i++)
                    {
                        result += "   ";
                    }
                    result += $"</{node.Tag}>" + System.Environment.NewLine;
                }
                firstChild = firstChild.Next;
            }

        }
        else
        {
            result += $"<{node.Tag}> {node.Value} </{node.Tag}>" + System.Environment.NewLine;
        }

        return result;
    }
}