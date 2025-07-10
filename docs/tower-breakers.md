---
name: tower breakers
data_structure: none (game theory)
canonical: true
difficulty: intermediate
time_complexity: O(1)
space_complexity: O(1)
description: Determine the winner in a two-player tower reduction game.
---

# tower breakers: win or lose in one glance

Two players. Towers stand tall. On your turn, pick a tower. Drop its height from `x` to `y`, where `y < x` and `y` divides `x`. If you can't move, you lose. No second chances.

Example: Three towers, each height 12. Player 1 can pick any tower and reduce it to 6, 4, 3, 2, or 1 (since all these divide 12). If all towers are height 1, no moves are possible.

## who wins, and why

- height 1? No moves. First player loses.
- even number of towers? Second player mirrors every move. First player can't win.
- odd towers, height > 1? First player breaks symmetry. Second player can't keep up.

## example: two towers, height 6

Start: `[6, 6]`

Player 1: `[3, 6]`  
Player 2: `[3, 3]` (mirrors)  
Player 1: `[1, 3]`  
Player 2: `[1, 1]` (mirrors)  
Player 1: no moves. Player 2 wins.

## code

```csharp
public static int towerBreakers(int n, int m)
{
    if (m == 1) return 2;           // no moves possible
    if (n % 2 == 0) return 2;       // mirroring wins
    return 1;                       // symmetry breaking wins
}
```

O(1) time. O(1) space. Game theory crushes brute force.
