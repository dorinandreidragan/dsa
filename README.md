# algorithm problem solutions

Optimal algorithms beat brute force every time. This repository proves it.

## 🌐 documentation website

Visit the live documentation: [Coming Soon - GitHub Pages]

Run locally:
```bash
npm install
npm run docs:dev
```

## what lives here

```
├── docs/                   # VitePress documentation site
│   ├── .vitepress/        # Site configuration & theme
│   ├── index.md           # Home page
│   ├── problems.md        # Complete catalog
│   └── *.md               # Problem deep dives
├── Playground.Tests/      # xUnit implementations and test cases
└── package.json          # Node dependencies & scripts
```

## the approach

No shortcuts. No copy-paste solutions. Every algorithm gets:

- **canonical implementation**: industry-standard approaches, not clever hacks
- **complexity analysis**: exact time and space bounds
- **comprehensive testing**: edge cases that break other solutions
- **detailed articles**: why the algorithm works, not just how

## solved problems

Different problems spanning basic to advanced difficulty. [View the complete catalog](docs/problems.md) for full details, complexity analysis, and direct links to articles.

Problems cover core concepts: string manipulation, array algorithms, bit operations, game theory, mathematical optimization, and two-pointer techniques.

## technologies

- **C#** (.NET 9): type safety meets performance
- **xUnit**: battle-tested framework for rigorous validation
- **markdown**: clean documentation that developers actually read

## running the code

```bash
# execute all tests
dotnet test

# target specific problems
dotnet test --filter "ClassName=PalindromeIndexTests"

# verbose output for debugging
dotnet test --logger "console;verbosity=detailed"
```

## why this matters

Implementing algorithms yourself teaches more than reading about them. Testing edge cases reveals assumptions. Documenting solutions solidifies understanding.

The best developers don't just solve problems. They understand why their solutions work and can explain the tradeoffs to others.

---

_Write code like you'll have to debug it at 3 AM. Document it like you'll forget it tomorrow._
