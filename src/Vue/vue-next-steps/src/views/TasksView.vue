<script setup lang="ts">
// The Tasks list view. Pulls data from the Pinia store, renders a form to
// add new tasks, and links each row to a detail route.
//
// Notes:
//   - `storeToRefs` destructures store state/getters while keeping reactivity.
//     Plain destructuring (`const { tasks } = store`) would *break* reactivity
//     for state refs. Actions can be destructured directly (they're functions).
//   - `v-model` on the input is two-way bound to a local `ref`.

import { ref } from 'vue'
import { storeToRefs } from 'pinia'
import { useTasksStore } from '../stores/tasks'
import TaskItem from '../components/TaskItem.vue'

const store = useTasksStore()
const { tasks, openCount, doneCount } = storeToRefs(store)
const { addTask, toggleTask, removeTask } = store

const draft = ref('')

function submit() {
  if (!draft.value.trim()) return
  addTask(draft.value)
  draft.value = ''
}
</script>

<template>
  <section>
    <h2>Tasks</h2>
    <p class="meta">{{ openCount }} open · {{ doneCount }} done</p>

    <!--
      `.prevent` is an event modifier that calls event.preventDefault() so the
      form doesn't reload the page. Equivalent to writing it inside `submit`.
    -->
    <form @submit.prevent="submit" class="task-form">
      <input
        v-model="draft"
        placeholder="What needs doing?"
        aria-label="New task title"
      />
      <button type="submit" :disabled="!draft.trim()">Add</button>
    </form>

    <ul class="task-list">
      <!--
        v-for over a ref<T[]> auto-unwraps. Always provide a stable `:key`
        so Vue can efficiently patch the list.
      -->
      <TaskItem
        v-for="task in tasks"
        :key="task.id"
        :task="task"
        @toggle="toggleTask"
        @remove="removeTask"
      />
    </ul>
  </section>
</template>
