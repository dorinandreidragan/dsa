<template>
  <span :class="['badge', 'difficulty', difficulty.toLowerCase()]">
    {{ difficulty }}
  </span>
</template>

<script setup lang="ts">
defineProps<{
  difficulty: string
}>()
</script>

<style scoped>
.badge.difficulty {
  text-transform: capitalize;
  font-weight: 600;
  letter-spacing: 0.025em;
}
</style>
