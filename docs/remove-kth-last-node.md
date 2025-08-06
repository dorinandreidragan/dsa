---
name: remove k-th last node
data_structure: singly linked list
canonical: true
difficulty: intermediate
time_complexity: O(n)
space_complexity: O(1)
description: Remove the k-th last node from a singly linked list using the two-pointer technique.
---

# remove k-th last node: two-pointer technique simplifies traversal

Remove the k-th last node from a singly linked list efficiently without using extra space.

## example

Input:  
Linked list: `1 -> 2 -> 3 -> 4 -> 5`  
`k = 2`

Output:  
Modified linked list: `1 -> 2 -> 3 -> 5`

## how it works

Use the two-pointer technique to locate the k-th last node:

- Create a dummy node to simplify edge cases.
- Move the `leader` pointer `k` steps forward.
- Move both `leader` and `trailer` pointers simultaneously until `leader` reaches the end.
- The `trailer` pointer will be positioned at the node preceding the k-th last node.
- Update the `Next` reference of the `trailer` node to skip the k-th last node.

This approach avoids the need for extra space and ensures efficient traversal.

## code

```csharp
private SinglyLinkedListNode RemoveKthLastNode(SinglyLinkedListNode head, int k)
{
    if (head is null) throw new ArgumentException("head cannot be null");

    var dummy = new SinglyLinkedListNode(-1);
    dummy.Next = head;
    var leader = dummy;
    var trailer = dummy;

    for (int i = 0; i < k; i++)
    {
        leader = leader!.Next;
        if (leader is null)
            throw new ArgumentException("k must be less than list size");
    }

    while (leader.Next is not null)
    {
        trailer = trailer!.Next;
        leader = leader.Next;
    }

    trailer!.Next = trailer.Next!.Next;
    return dummy.Next;
}
```

### edge cases

- Removing the head node when `k` equals the size of the list.
- Removing the only node in a single-element list.
- Invalid inputs such as `k` being zero or greater than the list size.
