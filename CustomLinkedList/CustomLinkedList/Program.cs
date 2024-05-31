// See https://aka.ms/new-console-template for more 

using System.Collections;
using System.Globalization;

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

foreach (var item in list)
    Console.WriteLine(item);

Console.ReadKey();
public interface ILinkedList<T> : ICollection<T>
{
    void AddToFront(T item);
    void AddToEnd(T item);
}


public class SinglyLinkedList<T> : ILinkedList<T?>
{
    private Node<T>? _head;
    private int _count;
    public int Count => _count;

    public bool IsReadOnly => false;

    public void Add(T? item)
    {
        AddToEnd(item);
    }

    public void AddToEnd(T? item)
    {
        Node<T> newNode = new Node<T>(item);
        if (_head is null)
            _head = newNode;
        else
        {
            Node<T> tail = GetNodes().Last();
            tail.Next = newNode;
        }

        ++_count;
    }

    public void AddToFront(T? item)
    {
        Node<T> newHead = new Node<T>(item) { Next = _head };
        _head = newHead;
        ++_count;
    }

    public void Clear()
    {
        // Never use the GetEnumarator iteration, e.g. GetNodes(). It will be hard to figure out.
        Node<T>? current = _head;
        while(current != null)
        {
            Node<T>? temporary = current;
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
        throw new NotImplementedException();
    }

    public bool Remove(T? item)
    {
        Node<T>? predecessor = null;
        // Although it is not a good idea to modify the collection during iteration with GetNodes(),
        // since we stop the iteration when we alter the part, this is okay.
        foreach (Node<T> node in GetNodes())
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
        foreach (Node<T> node in GetNodes())
            yield return node.Value;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    private IEnumerable<Node<T>> GetNodes()
    {
        if (_head is null)
            yield break;

        Node<T>? current = _head;
        while(current is not null)
        {
            yield return current;
            current = current.Next;
        }
    }
}

public class Node<T>
{
    public T? Value { get; set; }
    public Node<T>? Next { get; set; }

    public Node(T? value)
    {
        Value = value;
    }

    public override string ToString() =>
     $"Value: {Value}, Next: {(Next is null ? "null" : Next.Value)}";
}
