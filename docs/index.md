---
layout: home

hero:
  name: DSA Problems
  text: Master data structures and algorithms
  tagline: Clear explanations, optimal solutions, production-ready code
  actions:
    - theme: brand
      text: Browse Problems
      link: /balanced-brackets
    - theme: alt
      text: View on GitHub
      link: https://github.com/dorinandreidragan/dsa

features:
  - icon: 📚
    title: 20 Problems Solved
    details: Comprehensive collection covering basic to advanced difficulty levels with canonical solutions
  - icon: ⚡
    title: Time & Space Complexity
    details: Every solution includes detailed complexity analysis for optimal performance understanding
  - icon: 🎯
    title: Real Problem Scenarios
    details: Problems sourced from HackerRank with practical applications in technical interviews
  - icon: 💻
    title: Production Code
    details: Clean C# implementations with proper edge case handling and test coverage
  - icon: 🔍
    title: Pattern Recognition
    details: Learn to identify problem patterns and choose the right data structures
  - icon: 📖
    title: Clear Explanations
    details: Step-by-step breakdowns of how algorithms work with visual examples
---

<script setup>
import { data as problems } from './.vitepress/theme/data/problems.data.js'

const basicCount = problems.filter(p => p.difficulty === 'basic').length
const intermediateCount = problems.filter(p => p.difficulty === 'intermediate').length
const advancedCount = problems.filter(p => p.difficulty === 'advanced').length

const basicProblems = problems.filter(p => p.difficulty === 'basic')
const intermediateProblems = problems.filter(p => p.difficulty === 'intermediate')
const advancedProblems = problems.filter(p => p.difficulty === 'advanced')
</script>

## Problems by difficulty

<div class="difficulty-grid">
  <div class="difficulty-card basic">
    <h3>Basic</h3>
    <p class="count">{{ basicCount }} problems</p>
    <p class="description">Foundation concepts and common patterns</p>
    <a href="#basic" class="card-link">Explore →</a>
  </div>
  
  <div class="difficulty-card intermediate">
    <h3>Intermediate</h3>
    <p class="count">{{ intermediateCount }} problems</p>
    <p class="description">Advanced techniques and optimizations</p>
    <a href="#intermediate" class="card-link">Explore →</a>
  </div>
  
  <div class="difficulty-card advanced">
    <h3>Advanced</h3>
    <p class="count">{{ advancedCount }} problems</p>
    <p class="description">Complex algorithms and dynamic programming</p>
    <a href="#advanced" class="card-link">Explore →</a>
  </div>
</div>

## Basic

<div class="problem-list">
  <a v-for="problem in basicProblems" :key="problem.filename" :href="`/${problem.filename}`" class="problem-item">
    <h3>{{ problem.name }}</h3>
    <p class="meta">
      <span class="badge">{{ problem.data_structure }}</span>
      <span class="complexity">{{ problem.time_complexity }}</span>
    </p>
    <p class="description">{{ problem.description }}</p>
  </a>
</div>

## Intermediate

<div class="problem-list">
  <a v-for="problem in intermediateProblems" :key="problem.filename" :href="`/${problem.filename}`" class="problem-item">
    <h3>{{ problem.name }}</h3>
    <p class="meta">
      <span class="badge">{{ problem.data_structure }}</span>
      <span class="complexity">{{ problem.time_complexity }}</span>
    </p>
    <p class="description">{{ problem.description }}</p>
  </a>
</div>

## Advanced

<div class="problem-list">
  <a v-for="problem in advancedProblems" :key="problem.filename" :href="`/${problem.filename}`" class="problem-item">
    <h3>{{ problem.name }}</h3>
    <p class="meta">
      <span class="badge">{{ problem.data_structure }}</span>
      <span class="complexity">{{ problem.time_complexity }}</span>
    </p>
    <p class="description">{{ problem.description }}</p>
  </a>
</div>

<style scoped>
.difficulty-grid {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(250px, 1fr));
  gap: 1.5rem;
  margin-top: 2rem;
  margin-bottom: 3rem;
}

.difficulty-card {
  padding: 1.5rem;
  border-radius: 8px;
  border: 1px solid var(--vp-c-divider);
  transition: all 0.3s ease;
}

.difficulty-card:hover {
  transform: translateY(-4px);
  box-shadow: 0 8px 16px rgba(0, 0, 0, 0.1);
}

.difficulty-card.basic {
  border-left: 4px solid #10b981;
}

.difficulty-card.intermediate {
  border-left: 4px solid #f59e0b;
}

.difficulty-card.advanced {
  border-left: 4px solid #ef4444;
}

.difficulty-card h3 {
  margin-top: 0;
  margin-bottom: 0.5rem;
  font-size: 1.5rem;
}

.difficulty-card .count {
  font-size: 2rem;
  font-weight: bold;
  margin: 0.5rem 0;
  opacity: 0.9;
}

.difficulty-card .description {
  color: var(--vp-c-text-2);
  margin-bottom: 1rem;
  font-size: 0.9rem;
}

.difficulty-card .card-link {
  display: inline-block;
  color: var(--vp-c-brand-1);
  text-decoration: none;
  font-weight: 500;
}

.difficulty-card .card-link:hover {
  text-decoration: underline;
}

.problem-list {
  display: grid;
  grid-template-columns: repeat(auto-fill, minmax(300px, 1fr));
  gap: 1.5rem;
  margin: 2rem 0 3rem 0;
}

.problem-item {
  display: block;
  padding: 1.5rem;
  border: 1px solid var(--vp-c-divider);
  border-radius: 8px;
  text-decoration: none;
  color: inherit;
  transition: all 0.3s ease;
}

.problem-item:hover {
  transform: translateY(-2px);
  box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1);
  border-color: var(--vp-c-brand-1);
}

.problem-item h3 {
  margin: 0 0 0.5rem 0;
  font-size: 1.25rem;
  color: var(--vp-c-brand-1);
  text-transform: capitalize;
}

.problem-item .meta {
  display: flex;
  gap: 0.5rem;
  align-items: center;
  margin: 0.5rem 0;
}

.problem-item .badge {
  display: inline-block;
  padding: 0.125rem 0.5rem;
  background-color: var(--vp-c-brand-soft);
  color: var(--vp-c-brand-1);
  border-radius: 4px;
  font-size: 0.75rem;
  text-transform: capitalize;
}

.problem-item .complexity {
  font-family: 'Courier New', monospace;
  font-size: 0.875rem;
  color: var(--vp-c-text-2);
}

.problem-item .description {
  margin: 0.75rem 0 0 0;
  color: var(--vp-c-text-2);
  font-size: 0.9rem;
  line-height: 1.5;
}
</style>
