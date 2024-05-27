﻿// See https://aka.ms/new-console-template for more 

using System.Collections;

SinglyLinkedList<string> list = new SinglyLinkedList<string>();
list.AddToFront("a");
list.AddToFront("b");
list.AddToFront("c");

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
        throw new NotImplementedException();
    }

    public void AddToEnd(T? item)
    {
        throw new NotImplementedException();
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
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new NotImplementedException();
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
