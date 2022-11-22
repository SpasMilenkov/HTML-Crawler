namespace HTML_Crawler_Prototype;

public class Stack<T>
{
    private List<T> _container;
    
    public void Push(T value)
    {
        _container.Add(value);
    }

    public T Pop()
    {
        if (!IsEmpty())
            throw new Exception("Stack underflow");
        
        var lastEl = _container[_container.Count - 1];
        _container.RemoveAt(_container.Count - 1);

        return lastEl;
    }
    public bool IsEmpty() => _container.Count == 0;
}