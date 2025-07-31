using System.Text;

public class MergeTwoSortedLinkedListsTests
{
    public class SinglyLinkedListNode
    {
        public int data;
        public SinglyLinkedListNode? next;
    }

    /// <summary>
    /// 3 3 7
    /// 1 2
    /// </summary>
    /// <param name="head1"></param>
    /// <param name="head2"></param>
    /// <returns></returns>
    public static SinglyLinkedListNode mergeLists(SinglyLinkedListNode head1, SinglyLinkedListNode head2)
    {
        var dummyHead = new SinglyLinkedListNode(); // Dummy node to simplify edge cases
        var current = dummyHead;

        // Traverse both lists until one is exhausted
        while (head1 != null && head2 != null)
        {
            if (head1.data <= head2.data)
            {
                current.next = head1;
                head1 = head1.next;
            }
            else
            {
                current.next = head2;
                head2 = head2.next;
            }
            current = current.next;
        }

        // Attach the remaining nodes from the non-exhausted list
        current.next = head1 ?? head2;

        return dummyHead.next ?? new SinglyLinkedListNode(); // Return the merged list, skipping the dummy node
    }

    public string PrintList(SinglyLinkedListNode? head)
    {
        var output = new List<int>();
        while (head != null)
        {
            output.Add(head.data);
            head = head.next;
        }
        return string.Join(" ", output);
    }

    [Theory]
    [MemberData(nameof(MergeListsData))]
    public void MergeLists_TestCases(SinglyLinkedListNode head1, SinglyLinkedListNode head2, string expected)
    {
        var merged = mergeLists(head1, head2);
        Assert.Equal(expected, string.Join(" ", PrintList(merged)));
    }

    public static IEnumerable<object[]> MergeListsData =>
    [
        [
            new SinglyLinkedListNode {
                data = 1, next = new SinglyLinkedListNode {
                    data = 3, next = new SinglyLinkedListNode {
                        data = 7, next = null
                    }
                }
            },
            new SinglyLinkedListNode {
                data = 1, next = new SinglyLinkedListNode {
                    data = 2, next = null
                }
            },
            "1 1 2 3 7"
        ]
    ];
}