<template>
  <code class="complexity-tag">{{ complexity }}</code>
</template>

<script setup lang="ts">
defineProps<{
  complexity: string
}>()
</script>

<style scoped>
.complexity-tag {
  font-family: 'Courier New', monospace;
  font-size: 0.9em;
  padding: 0.125rem 0.5rem;
  background-color: var(--vp-c-bg-soft);
  border-radius: 4px;
  color: var(--vp-c-brand-1);
}
</style>
