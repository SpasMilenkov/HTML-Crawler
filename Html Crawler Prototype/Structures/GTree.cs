namespace HTML_Crawler_Prototype;

public class GTree<T>
{
    //holds the node tag
    public T Tag { get; set; }
    //holds the node s props (id, class)
    public string[] Props { get; set; }
    //keep the connection to the parent element
    public GTree<T> Parent { get; set; }
    public  string Value { get; set; }

    //hold the child nodes that are connected to this one
    //using linked list is necessary because the html DOM is not binary tree
    //the amount of child nodes are not limited to 2
    public LinkedList<GTree<T>> _childNodes = new LinkedList<GTree<T>>();
    
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