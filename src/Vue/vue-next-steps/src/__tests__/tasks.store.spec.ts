// Unit tests for the Pinia store.
//
// Key testing idiom for Pinia:
//   1. Call `setActivePinia(createPinia())` in `beforeEach`. This gives every
//      test a fresh, isolated Pinia instance so tests don't leak state.
//   2. Call the composable (`useTasksStore()`) *inside* each test, after the
//      active Pinia has been set.
//
// Because setup stores are just functions that return refs + actions, you can
// treat them like any other module under test — no component mounting needed.

import { beforeEach, describe, expect, it } from 'vitest'
import { createPinia, setActivePinia } from 'pinia'
import { useTasksStore } from '../stores/tasks'

describe('tasks store', () => {
  beforeEach(() => {
    setActivePinia(createPinia())
  })

  it('seeds with the initial tasks', () => {
    const store = useTasksStore()
    expect(store.tasks).toHaveLength(3)
    expect(store.openCount).toBe(1)
    expect(store.doneCount).toBe(2)
  })

  it('adds a new task and increments openCount', () => {
    const store = useTasksStore()
    const before = store.openCount
    const task = store.addTask('  Write docs  ')

    expect(task.title).toBe('Write docs') // trims whitespace
    expect(task.done).toBe(false)
    expect(store.openCount).toBe(before + 1)
  })

  it('throws when adding an empty task', () => {
    const store = useTasksStore()
    expect(() => store.addTask('   ')).toThrow(/must not be empty/)
  })

  it('toggles task completion', () => {
    const store = useTasksStore()
    const first = store.tasks[0]
    const wasDone = first.done

    store.toggleTask(first.id)
    expect(first.done).toBe(!wasDone)
  })

  it('removes a task', () => {
    const store = useTasksStore()
    const target = store.tasks[0]
    store.removeTask(target.id)

    expect(store.tasks.find((t) => t.id === target.id)).toBeUndefined()
  })

  it('looks tasks up via getById', () => {
    const store = useTasksStore()
    expect(store.getById(1)?.title).toMatch(/Vue Router/)
    expect(store.getById(9999)).toBeUndefined()
  })
})
