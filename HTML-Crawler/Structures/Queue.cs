using System.ComponentModel;

namespace HTML_Crawler_Prototype;

public class MyQueue<T>
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
    public T? Peek()
    {
        T t = default(T);
        if (IsEmpty())
            return t;
        return _container[0];
    }
    public bool IsEmpty() => _container.Count == 0;
}