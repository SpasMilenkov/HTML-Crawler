using System.Collections.Generic;

namespace HTML_Crawler_Prototype;

public class GTree<T>
{
    //holds the node tag
    public T Tag { get; set; }
    //holds the node s props (id, class)
    //TODO: Convert to linked list
    public MyLinkedList<string> Props = new MyLinkedList<string>();
    //keep the connection to the parent element
    public GTree<T> Parent { get; set; }
    public string Value { get; set; }
    public int Depth { get; set; }
    public bool SelfClosing = false;
    //hold the child nodes that are connected to this one
    //using linked list is necessary because the html DOM is not binary tree
    //the amount of child nodes are not limited to 2
    public MyLinkedList<GTree<T>> _childNodes = new MyLinkedList<GTree<T>>();

    public GTree()
    {

    }

    public GTree (GTree<T> newNode)
    {
        Tag = newNode.Tag;  
        Parent = newNode.Parent;
        Value = newNode.Value;
        Depth = newNode.Depth;
        SelfClosing = newNode.SelfClosing;
        var child = newNode._childNodes.First();

        while( child != null)
        {
            this.AddChild(child.Value);
            child = child.Next;
        }
    }


    //adds new child to the child nodes list
    public void AddChild(GTree<T> child)
    {
        if (_childNodes.First() == null)
        {
            _childNodes.AddFirst(child);
            return;
        }

        _childNodes.AddLast(child);
    }
    public bool HasProp(string parameter)
    {
        for (int i = 0; i < Props.Count; i++)
        {
            if (Props[i] == parameter)
                return true;
        }
        return false;
    }

    public string PropsString()
    {
        if (Props.Count() == 0)
            return "";
        var node = Props.First();
        string props = " ";
        while (node != null)
        {
            props += node.Value + ' ';
            node = node.Next;
        }
        return props;
    }
}