<script setup lang="ts">
// Minimal Vue + TypeScript component using the Composition API with <script setup>.
// `ref` creates a reactive value — change `.value` in script, use it directly in the template.
import { ref } from 'vue'

const count = ref(0)

function increment(): void {
  count.value++
}
</script>

<template>
  <h1>Hello, Vue + TypeScript</h1>
  <p>Edit <code>src/App.vue</code> and save to see changes.</p>
  <button @click="increment">count is {{ count }}</button>
</template>
