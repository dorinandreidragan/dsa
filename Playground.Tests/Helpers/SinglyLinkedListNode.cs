namespace Playground.Tests.Helpers;

public class SinglyLinkedListNode(int data)
{
    public int Data { get; private set; } = data;

    public SinglyLinkedListNode? Next { get; set; }
}