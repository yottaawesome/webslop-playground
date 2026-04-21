<script setup lang="ts">
// Detail view for a single task. Two ways to get the id are demonstrated:
//
//   1. Declared as a prop (see `props: true` in router/index.ts). This is the
//      cleanest option and what we use here.
//   2. `const route = useRoute(); route.params.id` — handy for deeper access
//      to the matched route, query params, etc.
//
// Route params are always strings, so we parse to a number before using it
// as a task id.

import { computed } from 'vue'
import { useRouter } from 'vue-router'
import { useTasksStore } from '../stores/tasks'

const props = defineProps<{ id: string }>()

const router = useRouter()
const store = useTasksStore()

// Compute-derived reactive lookup. If the task is deleted, this becomes
// undefined and the template renders the "not found" branch.
const task = computed(() => store.getById(Number(props.id)))

function goBack() {
  // `router.back()` is roughly `history.back()` but router-aware; if the user
  // landed here directly we fall back to the tasks list.
  if (window.history.length > 1) router.back()
  else router.push({ name: 'tasks' })
}
</script>

<template>
  <section v-if="task">
    <h2>{{ task.title }}</h2>
    <p>
      Status:
      <strong>{{ task.done ? 'Done' : 'Open' }}</strong>
    </p>
    <button @click="store.toggleTask(task.id)">
      Mark as {{ task.done ? 'open' : 'done' }}
    </button>
    <button @click="goBack">Back</button>
  </section>

  <section v-else>
    <h2>Task not found</h2>
    <p>No task with id <code>{{ id }}</code>.</p>
    <router-link :to="{ name: 'tasks' }">Back to list</router-link>
  </section>
</template>
