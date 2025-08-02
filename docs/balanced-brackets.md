---
name: balanced brackets
data_structure: stack
canonical: true
difficulty: basic
time_complexity: O(n)
space_complexity: O(n)
description: Check if brackets are balanced using a stack to match opening and closing pairs.
---

# balanced brackets: stack tracks the pairs

Determine if brackets are balanced. Every opening bracket needs a matching closing bracket in the correct order.

## example

Input:  
`()` → YES  
`({[()]})` → YES  
`({[()]` → NO  
`{[(])}` → NO

## how it works

Use a stack to track unmatched opening brackets:

- When you see an opening bracket (`(`, `[`, `{`), push it onto the stack.
- When you see a closing bracket (`)`, `]`, `}`), check if it matches the top of the stack.
- If it matches, pop the stack. If not, the string is unbalanced.
- At the end, the stack must be empty for balanced brackets.

The stack ensures proper nesting order. Last opened bracket must be first closed.

## code

```csharp
public string isBalanced(string s)
{
    var stack = new Stack<char>();

    foreach (char c in s)
    {
        if (c == '(' || c == '[' || c == '{')
        {
            stack.Push(c);
        }
        else
        {
            if (stack.Count == 0) return "NO";

            var open = stack.Pop();

            if (c == ')' && open != '('
            || c == ']' && open != '['
            || c == '}' && open != '{')
                return "NO";
        }
    }

    return stack.Count == 0 ? "YES" : "NO";
}
```

### edge cases

- Empty string is balanced.
- Single bracket is unbalanced.
- Extra opening brackets leave stack non-empty.
- Extra closing brackets return "NO" immediately.
