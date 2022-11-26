namespace HTML_Crawler_Prototype;

public class Queue<T>
{
    private List<T> _container = new List<T>();
    
    public void Enqueue(T value)
    {
        _container.Add(value);
    }

    public T Dequeue()
    {
        if (IsEmpty())
            throw new Exception("Queue underflow");
        
        var firstEl = _container[0];
        _container.RemoveAt(0);
        return firstEl;
    }
    public bool IsEmpty() =>  _container.Count == 0;
}