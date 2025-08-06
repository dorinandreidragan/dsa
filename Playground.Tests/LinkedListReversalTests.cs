namespace Playground.Tests;


// This class contains tests for reversing a singly linked list.
// The reversal operation involves iterating through the list and reversing the direction of the `Next` pointers.
// The algorithm is implemented iteratively and works in O(n) time complexity with O(1) space complexity.
public class LinkedListReversalTests
{
    public class SinglyLinkedListNode(int data)
    {
        public int Data { get; private set; } = data;

        public SinglyLinkedListNode? Next { get; set; }
    }

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

    /// <summary>
    /// Reverses a singly linked list.
    /// The algorithm iterates through the list, reversing the direction of the `Next` pointers.
    /// Example:
    /// Input: 5 -> 4 -> 3 -> 2 -> 1
    /// Output: 1 -> 2 -> 3 -> 4 -> 5
    /// </summary>
    /// <param name="head">The head of the singly linked list.</param>
    /// <returns>The new head of the reversed list.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the input list is null.</exception>
    public SinglyLinkedListNode Reverse(SinglyLinkedListNode head)
    {
        if (head is null) throw new InvalidOperationException("List head must not be null!");

        SinglyLinkedListNode? prev = null;
        var curr = head;

        while (curr is not null)
        {
            var next = curr.Next;
            curr.Next = prev;
            prev = curr;
            curr = next;
        }

        return prev!;
    }

    [Fact]
    public void Reverse_TestCases()
    {
        // Test case: Reversing a list with multiple elements.
        var head = new SinglyLinkedListNodeBuilder()
            .Add(5)
            .Add(4)
            .Add(3)
            .Add(2)
            .Add(1)
            .Build();

        var reverse = Reverse(head);
        Assert.Equal(1, reverse.Data);

        reverse = reverse.Next!;
        Assert.Equal(2, reverse.Data);

        reverse = reverse.Next!;
        Assert.Equal(3, reverse.Data);

        reverse = reverse.Next!;
        Assert.Equal(4, reverse.Data);

        reverse = reverse.Next!;
        Assert.Equal(5, reverse.Data);
    }

    [Fact]
    public void Reverse_EmptyList_ThrowsException()
    {
        // Test case: Reversing an empty list should throw an exception.
        Assert.Throws<InvalidOperationException>(() => Reverse(null!));
    }

    [Fact]
    public void Reverse_SingleElementList()
    {
        // Test case: Reversing a list with a single element.
        var head = new SinglyLinkedListNodeBuilder()
            .Add(1)
            .Build();

        var reverse = Reverse(head);
        Assert.Equal(1, reverse.Data);
        Assert.Null(reverse.Next);
    }
}