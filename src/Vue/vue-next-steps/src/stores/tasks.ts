// Pinia store: tasks.
//
// Pinia is Vue's official state-management library. Compared to component
// state, a store is:
//   - App-wide (any component can import and use it)
//   - Persistent for the life of the app (survives navigation)
//   - Testable in isolation (see `src/__tests__/tasks.store.spec.ts`)
//
// There are two styles for defining stores:
//
//   1. Options store:  `defineStore('id', { state, getters, actions })`
//   2. Setup store:    `defineStore('id', () => { ...refs/computed/fns... })`
//
// We use the **setup store** style because it mirrors the Composition API
// used elsewhere in these samples, and TypeScript inference is sharper.
// In a setup store:
//   - `ref` / `reactive` values become the store's *state*
//   - `computed` values become *getters*
//   - plain functions become *actions*
// Whatever you `return` is what consumers see.

import { computed, ref } from 'vue'
import { defineStore } from 'pinia'

export interface Task {
  id: number
  title: string
  done: boolean
}

export const useTasksStore = defineStore('tasks', () => {
  // --- state ---------------------------------------------------------------
  const tasks = ref<Task[]>([
    { id: 1, title: 'Read the Vue Router docs', done: true },
    { id: 2, title: 'Add a Pinia store', done: true },
    { id: 3, title: 'Write a Vitest spec', done: false },
  ])

  // A simple auto-incrementing id. Kept inside the store so tests can observe
  // and reset it through `$reset`-like patterns if needed.
  const nextId = ref(tasks.value.length + 1)

  // --- getters (computed) --------------------------------------------------
  const openCount = computed(() => tasks.value.filter((t) => !t.done).length)
  const doneCount = computed(() => tasks.value.filter((t) => t.done).length)

  // Factory getter: returns a function that looks a task up by id. Getters
  // that take arguments aren't cached, which is fine here.
  const getById = computed(() => {
    return (id: number) => tasks.value.find((t) => t.id === id)
  })

  // --- actions -------------------------------------------------------------
  function addTask(title: string): Task {
    const trimmed = title.trim()
    if (!trimmed) throw new Error('Task title must not be empty')
    const task: Task = { id: nextId.value++, title: trimmed, done: false }
    tasks.value.push(task)
    return task
  }

  function toggleTask(id: number): void {
    const task = tasks.value.find((t) => t.id === id)
    if (task) task.done = !task.done
  }

  function removeTask(id: number): void {
    tasks.value = tasks.value.filter((t) => t.id !== id)
  }

  return {
    // state
    tasks,
    // getters
    openCount,
    doneCount,
    getById,
    // actions
    addTask,
    toggleTask,
    removeTask,
  }
})
