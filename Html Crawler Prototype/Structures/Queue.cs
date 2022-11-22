namespace HTML_Crawler_Prototype;

public class Queue<T>
{
    private LinkedList<T> _container = new LinkedList<T>();
    
    public void Push(T value)
    {
        _container.AddFirst(value);
    }

    public T Pop()
    {
        if (!IsEmpty())
            throw new Exception("Queue underflow");
        
        var firstEl = _container.First();
        _container.Remove(firstEl);
        return firstEl.Value;
    }
    public bool IsEmpty() =>  _container.First() == null;
}