namespace HTML_Crawler_Prototype;

public class GTree<T>
{
    //hold the data of the node
    private T? _data;
    //holds the node tag
    private T _tag;
    //holds the node s props (id, class)
    private T _props;
    //keep the connection to the parent element
    private GTree<T> _parent;

    //hold the child nodes that are connected to this one
    //using linked list is necessary because the html DOM is not binary tree
    //the amount of child nodes are not limited to 2
    private LinkedList<GTree<T>> _childNodes;

    public void SetGTree(T tag, T props,T data, GTree<T> parent)
    {
        _tag = tag;
        _props = props;
        _parent = parent;
        _childNodes = new LinkedList<GTree<T>>();
    }
    //adds new child to the child nodes list
    public void AddChild(GTree<T> child)
    {
        _childNodes.AddFirst(child);
    }
    //gets a child from the node s child list based on a parameter
    //WORK IN PROGRESS
    public GTree<T> GetChild(int i)
    {
        var current = _childNodes.First();
        while (current != null)
        {
            current = current.Next;
        }
        return null;
    }
}