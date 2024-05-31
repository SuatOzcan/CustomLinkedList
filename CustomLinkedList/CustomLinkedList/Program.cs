// See https://aka.ms/new-console-template for more 

using System.Collections;

SinglyLinkedList<string> list = new SinglyLinkedList<string>();
list.AddToFront("a");
list.AddToFront("b");
list.AddToFront("c");
list.AddToEnd("d");
list.Add("e");
list.Remove("c");

Console.WriteLine("Contains \"b\"? {0} ",list.Contains("b"));
Console.WriteLine("Contains \"c\"? {0}", list.Contains("c"));
Console.WriteLine("Contains \"QWE\" {0}",list.Contains("QWE"));
//list.Clear();

Console.WriteLine("Values in the collection are:");
foreach (var item in list)
    Console.WriteLine(item);
Console.WriteLine();

string[] arr = new string[10];

list.CopyTo(arr, 0);

Console.WriteLine("Values in the array are:");

foreach (string item in arr)
{ Console.WriteLine(item); }

//var o = arr.Select(x =>
//            {
//                Console.WriteLine(x);
//                return x;
//            });
//foreach (string item in o) { }

Console.WriteLine("Press any key to close the application.");
Console.ReadKey(true);

public interface ILinkedList<T> : ICollection<T>
{
    void AddToFront(T item);
    void AddToEnd(T item);
}


public class SinglyLinkedList<T> : ILinkedList<T?>
{
    private Node? _head;
    private Node? _tail;
    private int _count;
    public int Count => _count;

    public bool IsReadOnly => false;

    public void Add(T? item)
    {
        AddToEnd(item);
    }

    public void AddToEnd(T? item)
    {
        Node newNode = new Node(item);
        if (_head is null)
            _head = newNode;
        else
        {
            if(_tail is null)
                _tail = GetNodes().Last();
            _tail.Next = newNode;
        }
        ++_count;
    }

    public void AddToFront(T? item)
    {
        Node newHead = new Node(item) { Next = _head };
        _head = newHead;
        ++_count;
    }

    public void Clear()
    {
        // Never use the GetEnumarator iteration, e.g. GetNodes(). It will be hard to figure out.
        Node? current = _head;
        while(current != null)
        {
            Node? temporary = current;
            current = current.Next;
            temporary.Next = null;
        }

        _head = null;
        _count = 0;
    }

    public bool Contains(T? item)
    {
        if (item is null)
            return GetNodes().Any(node => node.Value is null);
        else
            return GetNodes().Any(node => item.Equals(node.Value));

        //return GetNodes().Any(node => node.Value.Equals(item)); // Gives a "Value may be null here." warning.
    }
    public void CopyTo(T?[] array, int arrayIndex)
    {
        if(array is null)
            throw new ArgumentNullException(nameof(array));
        if(arrayIndex < 0 || arrayIndex >= array.Length)
            throw new ArgumentOutOfRangeException(nameof(arrayIndex));
        if (Count + arrayIndex > array.Length)
        //if (GetNodes().Count() + arrayIndex > array.Length)
            throw new ArgumentException("Array is not long enough to store the collection.");
        foreach (Node node in GetNodes())
        {
            if(array.Length >= GetNodes().Count())
            array[arrayIndex] = node.Value;
            ++arrayIndex;
        }
    }

    public bool Remove(T? item)
    {
        Node? predecessor = null;
        // Although it is not a good idea to modify the collection during iteration with GetNodes(),
        // since we stop the iteration when we alter the part, this is okay.
        foreach (Node node in GetNodes())
        {
            if ((node.Value is null && item is null) || (node.Value is not null && node.Value.Equals(item)))
            {
                if (predecessor is null)
                {
                    _head = node.Next;
                }
                else
                {
                    predecessor.Next = node.Next;
                }
                --_count;
                return true;
                // We stop the iteration here.
            }
            predecessor = node;
        }
        return false;
    }
    public IEnumerator<T?> GetEnumerator()
    {
        foreach (Node node in GetNodes())
            yield return node.Value;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    private IEnumerable<Node> GetNodes()
    {
        if (_head is null)
            yield break;

        Node? current = _head;
        while(current is not null)
        {
            yield return current;
            current = current.Next;
        }
    }

    private class Node
    {
        public T? Value { get; set; }
        public Node? Next { get; set; }

        public Node(T? value)
        {
            Value = value;
        }

        public override string ToString() =>
         $"Value: {Value}, Next: {(Next is null ? "null" : Next.Value)}";
    }
}


