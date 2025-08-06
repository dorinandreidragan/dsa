namespace Playground.Tests.Helpers;

public class SinglyLinkedListNodeBuilder
{
    private SinglyLinkedListNode? head;
    private SinglyLinkedListNode? current;

    public SinglyLinkedListNodeBuilder Add(int data)
    {
        if (head is null)
        {
            head = new SinglyLinkedListNode(data);
            current = head;
        }
        else
        {
            current!.Next = new SinglyLinkedListNode(data);
            current = current.Next;
        }
        return this;
    }

    public SinglyLinkedListNode Build()
    {
        if (head is null) throw new InvalidOperationException("At least one node should be added.");
        return head;
    }
}