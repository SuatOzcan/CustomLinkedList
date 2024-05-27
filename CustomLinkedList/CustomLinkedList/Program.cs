// See https://aka.ms/new-console-template for more information
public interface ILinkedList<T> : ICollection<T>
{
    void AddToFront(T item);
    void AddToEnd(T item);
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
