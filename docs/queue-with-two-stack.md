---
name: queue with two stacks
data_structure: stack
canonical: true
difficulty: intermediate
time_complexity: O(1) amortized per operation
space_complexity: O(n)
description: Implement a queue using two stacks.
---

# queue with two stacks: simulate a queue

Use two stacks to implement a queue. One stack handles enqueue operations, and the other handles dequeue operations.

## example

Operations:  
Enqueue(1), Enqueue(2), Dequeue()

Steps:

1. Push 1 to stack1.
2. Push 2 to stack1.
3. Move all elements from stack1 to stack2, pop from stack2.

Result: 1 is dequeued.

## how it works

- Enqueue: Push elements onto stack1.
- Dequeue:
  1. If stack2 is empty, move all elements from stack1 to stack2.
  2. Pop from stack2.

This ensures the oldest element is always dequeued first.

## code

```csharp
public class QueueWithTwoStacks<T>
{
    private Stack<T> stack1 = new Stack<T>();
    private Stack<T> stack2 = new Stack<T>();

    public void Enqueue(T item)
    {
        stack1.Push(item);
    }

    public T Dequeue()
    {
        if (stack2.Count == 0)
        {
            while (stack1.Count > 0)
            {
                stack2.Push(stack1.Pop());
            }
        }

        if (stack2.Count == 0)
        {
            throw new InvalidOperationException("Queue is empty.");
        }

        return stack2.Pop();
    }
}
```

### edge cases

- Dequeue from an empty queue throws an exception.
- Enqueue and dequeue interleaved operations maintain correct order.
