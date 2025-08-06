---
name: linked list reversal
data_structure: singly linked list
canonical: true
difficulty: intermediate
time_complexity: O(n)
space_complexity: O(1)
description: Reverse a singly linked list by iteratively updating the Next pointers.
---

# linked list reversal

Reversing a singly linked list is a fundamental problem in computer science. It involves reversing the direction of the `Next` pointers in the list so that the last node becomes the head and the first node becomes the tail.

## problem statement

Given the head of a singly linked list, reverse the list and return the new head.

### example

Input:

```
5 -> 4 -> 3 -> 2 -> 1
```

Output:

```
1 -> 2 -> 3 -> 4 -> 5
```

## solution

### approach

The reversal is performed iteratively by traversing the list and updating the `Next` pointers of each node. The algorithm uses three pointers:

- `prev`: Tracks the previous node.
- `curr`: Tracks the current node.
- `next`: Temporarily stores the next node to avoid losing reference during pointer updates.

### pseudocode

```
function reverse(head):
    prev = null
    curr = head

    while curr is not null:
        next = curr.next
        curr.next = prev
        prev = curr
        curr = next

    return prev
```

### complexity

- **Time Complexity**: O(n), where n is the number of nodes in the list.
- **Space Complexity**: O(1), as the reversal is performed in-place.

### canonical solution

The iterative approach is the canonical way to reverse a singly linked list. It is efficient and widely used in practice.

## implementation

The implementation in C# is as follows:

```csharp
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
```

## test cases

### basic tests

1. Reversing a list with multiple elements:

   - Input: `5 -> 4 -> 3 -> 2 -> 1`
   - Output: `1 -> 2 -> 3 -> 4 -> 5`

2. Reversing a list with a single element:

   - Input: `1`
   - Output: `1`

3. Reversing an empty list:
   - Input: `null`
   - Output: Exception (`InvalidOperationException`)

## alternative solution

### recursive approach

The reversal can also be performed recursively. This approach breaks the problem into smaller subproblems, reversing the rest of the list first and then adjusting the `Next` pointer of the current node.

### pseudocode

```
function reverseRecursive(head):
    if head is null or head.next is null:
        return head

    newHead = reverseRecursive(head.next)
    head.next.next = head
    head.next = null

    return newHead
```

### complexity

- **Time Complexity**: O(n), where n is the number of nodes in the list.
- **Space Complexity**: O(n), due to the recursion stack.

### implementation

The implementation in C# is as follows:

```csharp
public SinglyLinkedListNode? ReverseRecursive(SinglyLinkedListNode head)
{
    if (head is null || head.Next is null) return head;

    var newHead = ReverseRecursive(head.Next);
    head.Next.Next = head;
    head.Next = null;
    return newHead;
}
```

## comparison of approaches

### iterative vs recursive

- **Space Efficiency**: The iterative approach is more space-efficient, using O(1) space, while the recursive approach uses O(n) space due to the recursion stack.
- **Performance**: Both approaches have the same time complexity of O(n), but the iterative approach is preferred for large lists to avoid stack overflow.
- **Readability**: The recursive approach is concise and elegant, making it easier to understand for those familiar with recursion.
- **Canonical Solution**: The iterative approach is considered canonical due to its better space efficiency and widespread use in practice.

### when to use

- Use the iterative approach for production code or when working with large lists.
- Use the recursive approach for educational purposes or when recursion is preferred for simplicity.
