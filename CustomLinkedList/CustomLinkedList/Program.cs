// See https://aka.ms/new-console-template for more 

using System.Collections;

SinglyLinkedList<string> list = new SinglyLinkedList<string>();
list.AddToFront("a");
list.AddToFront("b");
list.AddToFront("c");
list.AddToEnd("d");
list.Add("e");

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
        throw new NotImplementedException();
    }

    public bool Contains(T? item)
    {
        throw new NotImplementedException();
    }

    public void CopyTo(T?[] array, int arrayIndex)
    {
        throw new NotImplementedException();
    }

    public bool Remove(T? item)
    {
        throw new NotImplementedException();
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
