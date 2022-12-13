using System.Collections.Generic;
using System.Reflection.Metadata;

namespace HTML_Crawler_Prototype;

public class GTree<T>
{
    //holds the node tag
    public T Tag { get; set; }
    //holds the node s props (id, class)
    //TODO: Convert to linked list
    public MyLinkedList<string> Props = new MyLinkedList<string>();
    //keep the connection to the parent element
    public string Value { get; set; }
    public bool SelfClosing = false;
    //hold the child nodes that are connected to this one
    //using linked list is necessary because the html DOM is not binary tree
    //the amount of child nodes are not limited to 2
    public MyLinkedList<GTree<T>> _childNodes = new MyLinkedList<GTree<T>>();

    public GTree()
    {

    }

    public GTree(GTree<T> newNode)
    {
        Tag = newNode.Tag;
        Value = newNode.Value;
        SelfClosing = newNode.SelfClosing;
    }


    //adds new child to the child nodes list
    public void AddChild(GTree<T> child)
    {
        _childNodes.AddLast(child);
    }
    public bool HasProp(string parameter)
    {
        var node = Props.First();
        while (node != null)
        {
            if (node.Value == parameter)
                return true;
            node = node.Next;
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