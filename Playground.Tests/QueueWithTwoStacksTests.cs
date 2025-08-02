/// <summary>
/// This class contains unit tests for the TwoStackQueue implementation.
/// The TwoStackQueue simulates a queue using two stacks, ensuring FIFO behavior.
/// </summary>
public class QueueWithTwoStacksTests
{
    /// <summary>
    /// Tests that Peek throws an exception when the queue is empty.
    /// </summary>
    [Fact]
    public void PeeksNothing_When_NoElements()
    {
        var queue = new TwoStackQueue();
        Assert.Throws<InvalidOperationException>(() => queue.Peek());
    }

    /// <summary>
    /// Tests that Peek returns the first element without removing it.
    /// </summary>
    [Fact]
    public void PeeksFirst()
    {
        var queue = new TwoStackQueue();
        queue.Enqueue(1);
        queue.Enqueue(2);
        queue.Enqueue(3);
        Assert.Equal(1, queue.Peek());
    }

    /// <summary>
    /// Tests that Dequeue removes and returns elements in FIFO order.
    /// </summary>
    [Fact]
    public void DequeuesMultipleTimes()
    {
        var queue = new TwoStackQueue();
        queue.Enqueue(1);
        queue.Enqueue(2);
        queue.Enqueue(3);

        Assert.Equal(1, queue.Dequeue());
        Assert.Equal(2, queue.Dequeue());
        Assert.Equal(3, queue.Dequeue());
    }

    /// <summary>
    /// Tests that Dequeue throws an exception when the queue is empty.
    /// </summary>
    [Fact]
    public void DequeuesNothing_When_NoElements()
    {
        var queue = new TwoStackQueue();
        Assert.Throws<InvalidOperationException>(() => queue.Dequeue());
    }

    /// <summary>
    /// Tests the behavior of the queue with a single element.
    /// </summary>
    [Fact]
    public void HandlesSingleElement()
    {
        var queue = new TwoStackQueue();
        queue.Enqueue(42);
        Assert.Equal(42, queue.Peek());
        Assert.Equal(42, queue.Dequeue());
        Assert.Throws<InvalidOperationException>(() => queue.Peek());
    }

    /// <summary>
    /// Tests the queue's performance and correctness with a large number of elements.
    /// </summary>
    [Fact]
    public void HandlesLargeNumberOfElements()
    {
        var queue = new TwoStackQueue();
        const int largeCount = 10000;

        for (int i = 0; i < largeCount; i++)
        {
            queue.Enqueue(i);
        }

        for (int i = 0; i < largeCount; i++)
        {
            Assert.Equal(i, queue.Dequeue());
        }

        Assert.Throws<InvalidOperationException>(() => queue.Dequeue());
    }

    /// <summary>
    /// Implementation of a queue using two stacks.
    /// </summary>
    public class TwoStackQueue
    {
        private readonly Stack<int> stack1 = new();
        private readonly Stack<int> stack2 = new();

        /// <summary>
        /// Returns the element at the front of the queue without removing it.
        /// Throws InvalidOperationException if the queue is empty.
        /// </summary>
        public int Peek()
        {
            if (stack1.Count == 0 && stack2.Count == 0)
            {
                throw new InvalidOperationException();
            }
            TransferIfNeeded();
            return stack2.Peek();
        }

        /// <summary>
        /// Adds an element to the end of the queue.
        /// </summary>
        public void Enqueue(int item)
        {
            stack1.Push(item);
        }

        /// <summary>
        /// Removes and returns the element at the front of the queue.
        /// Throws InvalidOperationException if the queue is empty.
        /// </summary>
        internal int Dequeue()
        {
            if (stack1.Count == 0 && stack2.Count == 0)
            {
                throw new InvalidOperationException();
            }
            TransferIfNeeded();
            return stack2.Pop();
        }

        /// <summary>
        /// Transfers elements from stack1 to stack2 if stack2 is empty.
        /// Ensures FIFO order for the queue.
        /// </summary>
        private void TransferIfNeeded()
        {
            if (stack2.Count == 0)
            {
                while (stack1.Count != 0)
                {
                    stack2.Push(stack1.Pop());
                }
            }
        }
    }
}