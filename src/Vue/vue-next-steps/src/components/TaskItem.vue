<script setup lang="ts">
// A single row in the task list. Kept deliberately small so the unit test is
// easy to read: one prop in, two events out.
//
// Typed `defineProps<T>()` / `defineEmits<T>()` are compile-time only — the
// Vue compiler strips the generics and synthesizes the runtime options.

import type { Task } from '../stores/tasks'

defineProps<{ task: Task }>()

const emit = defineEmits<{
  toggle: [id: number]
  remove: [id: number]
}>()

function onToggle(id: number) {
  emit('toggle', id)
}

function onRemove(id: number) {
  emit('remove', id)
}
</script>

<template>
  <li class="task-item" :class="{ done: task.done }">
    <!--
      Checkbox bound to `task.done`. We don't use v-model here because the
      source of truth lives in the Pinia store; we emit an event and let the
      parent call the store action.
    -->
    <input
      type="checkbox"
      :checked="task.done"
      :aria-label="`Toggle ${task.title}`"
      @change="onToggle(task.id)"
    />

    <router-link :to="{ name: 'task-detail', params: { id: task.id } }">
      {{ task.title }}
    </router-link>

    <button class="remove" @click="onRemove(task.id)" aria-label="Remove task">
      ✕
    </button>
  </li>
</template>
