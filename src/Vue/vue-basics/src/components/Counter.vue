<script setup lang="ts">
// Demonstrates: typed props, typed emits, and a local ref that the parent
// stays unaware of (encapsulation).
import { ref, watch } from 'vue'

// `defineProps` declares what this component accepts from its parent.
const props = defineProps<{
  label: string
}>()

// `defineEmits` declares what events this component sends to its parent.
// The parent listens with @change="..." in its template.
const emit = defineEmits<{
  change: [value: number]
}>()

const count = ref(0)

// Whenever count changes, tell the parent about it.
watch(count, newValue => emit('change', newValue))

function increment() {
  count.value++
}

function reset() {
  count.value = 0
}
</script>

<template>
  <div class="row">
    <strong>{{ props.label }}:</strong>
    <span>{{ count }}</span>
    <button class="primary" @click="increment">+1</button>
    <button @click="reset" :disabled="count === 0">Reset</button>
  </div>
</template>
