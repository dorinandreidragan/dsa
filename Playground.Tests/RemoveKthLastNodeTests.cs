namespace Playground.Tests;

public class RemoveKthLastNodeTests
{
    /// <summary>
    /// Removes the k-th last node from a singly linked list.
    /// Uses a two-pointer technique to efficiently locate the node to be removed.
    /// </summary>
    /// <param name="head">The head of the singly linked list.</param>
    /// <param name="k">The position from the end of the list to remove.</param>
    /// <returns>The head of the modified list.</returns>
    /// <exception cref="ArgumentException">Thrown if the head is null or k is invalid.</exception>
    private SinglyLinkedListNode RemoveKthLastNode(SinglyLinkedListNode head, int k)
    {
        if (head is null) throw new ArgumentException("head cannot be null");

        // Create a dummy node to simplify edge cases (e.g., removing the head node).
        var dummy = new SinglyLinkedListNode(-1);
        dummy.Next = head;
        var leader = dummy;
        var trailer = dummy;

        // Move the leader pointer k steps forward.
        for (int i = 0; i < k; i++)
        {
            leader = leader!.Next;
            if (leader is null)
                throw new ArgumentException("k must be less than list size");
        }

        // Move both leader and trailer pointers until leader reaches the end.
        while (leader.Next is not null)
        {
            trailer = trailer!.Next;
            leader = leader.Next;
        }

        // Remove the k-th last node.
        trailer!.Next = trailer.Next!.Next;
        return dummy.Next;
    }

    [Fact]
    public void RemoveKthLastNode_TestCase()
    {
        // Test case: Remove the 2nd last node from a list of 5 elements.
        var head = new SinglyLinkedListNodeBuilder()
            .Add(1)
            .Add(2)
            .Add(3)
            .Add(4)
            .Add(5)
            .Build();

        head = RemoveKthLastNode(head, 2);
        Assert.Equal(1, head.Data);

        var node = head.Next;
        Assert.Equal(2, node!.Data);

        node = node.Next;
        Assert.Equal(3, node!.Data);

        node = node.Next;
        Assert.Equal(5, node!.Data);
    }

    [Fact]
    public void RemoveKthLastNode_OnEmptyList()
    {
        // Test case: Attempt to remove a node from an empty list.
        Assert.Throws<ArgumentException>(
            () => RemoveKthLastNode(null, 2));
    }

    [Fact]
    public void RemoveKthLastNode_WhenKthSameWithHead()
    {
        // Test case: Remove the head node when k equals the list size.
        var head = new SinglyLinkedListNodeBuilder()
            .Add(1)
            .Add(2)
            .Add(3)
            .Add(4)
            .Add(5)
            .Build();

        head = RemoveKthLastNode(head, 5);
        Assert.Equal(2, head.Data);

        var node = head.Next;
        Assert.Equal(3, node!.Data);

        node = node.Next;
        Assert.Equal(4, node!.Data);

        node = node.Next;
        Assert.Equal(5, node!.Data);
    }

    [Fact]
    public void RemoveKthLastNode_WhenKthBiggerThanListSize()
    {
        // Test case: Attempt to remove a node when k is larger than the list size.
        var head = new SinglyLinkedListNodeBuilder()
            .Add(1)
            .Add(2)
            .Add(3)
            .Add(4)
            .Add(5)
            .Build();

        Assert.Throws<ArgumentException>(
            () => RemoveKthLastNode(head, 6));
    }

    [Fact]
    public void RemoveKthLastNode_WhenListHasOneElement()
    {
        // Test case: Remove the only node in a single-element list.
        var head = new SinglyLinkedListNodeBuilder()
            .Add(1)
            .Build();

        head = RemoveKthLastNode(head, 1);
        Assert.Null(head);
    }

    [Fact]
    public void RemoveKthLastNode_WhenKIsZero()
    {
        // Test case: Attempt to remove the 0th last node (invalid input).
        var head = new SinglyLinkedListNodeBuilder()
            .Add(1)
            .Add(2)
            .Add(3)
            .Build();

        Assert.Throws<ArgumentException>(
            () => RemoveKthLastNode(head, 0));
    }
}