<template>
  <div class="problem-meta">
    <DifficultyBadge v-if="difficulty" :difficulty="difficulty" />
    <span v-if="dataStructure" class="badge data-structure">{{ dataStructure }}</span>
    <span v-if="timeComplexity" class="badge complexity">⏱️ {{ timeComplexity }}</span>
    <span v-if="spaceComplexity" class="badge complexity">💾 {{ spaceComplexity }}</span>
    <span v-if="canonical" class="badge complexity">✓ Canonical</span>
  </div>
</template>

<script setup lang="ts">
import DifficultyBadge from './DifficultyBadge.vue'

defineProps<{
  difficulty?: string
  dataStructure?: string
  timeComplexity?: string
  spaceComplexity?: string
  canonical?: boolean
}>()
</script>
