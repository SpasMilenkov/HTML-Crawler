namespace HTML_Crawler_Prototype;

struct KeyValuePair
{
    public string Key;
    public string Value;
}
class Dictionary
{

    class LinkedListItem
    {
        public KeyValuePair KeyValuePair;
        public LinkedListItem Next;
    }

    private LinkedListItem[] _records;

    public Dictionary(int size)
    {
        _records = new LinkedListItem[size];
    }

    public int Hash(string key)
    {
        //hash function
        var index = 0;
        for (int i = 0; i < key.Length; i++)
        {
            index += key[i] * 27;
            index %= _records.Length;
        }
        //Gospodi pomogni mi bojeeeeeeee
        //Unhandled Exception
        return index;

    }

    public void Add(string key, string value)
    {
        var index = Hash(key);

        var current = _records[index];

        while (current != null && current.KeyValuePair.Key != key)
        {
            current = current.Next;
        }

        if (current == null)
        {
            current = new LinkedListItem
            {
                KeyValuePair = new KeyValuePair
                {
                    Key = key,
                    Value = value
                },
                Next = _records[index]
            };
            _records[index] = current;
        }
    }

    public string this[string key] // indexer
    {
        get
        {
            var index = Hash(key);
            var current = _records[index];
            while (current != null && current.KeyValuePair.Key != key)
                current = current.Next; // traverse the linked list of the bucket

            return current?.KeyValuePair.Value ?? "normalClose";

        }
    }
}