namespace HTML_Crawler_Prototype;

public class MyStack<T>
{
    private MyLinkedList<T> _container;

    public MyStack()
    {
        _container = new MyLinkedList<T>();
    }

    public void Push(T value)
    {
        _container.AddLast(value);
    }

    public T Pop()
    {
        if (IsEmpty())
            throw new Exception("Stack underflow");
        
        var lastEl = _container.Last();
        _container.Remove(lastEl);

        return lastEl.Value;
    } 
    public T Peek()
    {
        if (IsEmpty())
            throw new Exception("Stack underflow");

        var lastEl = _container.Last();

        return lastEl.Value;
    }
    public bool IsEmpty() => _container.Count() == 0;
}