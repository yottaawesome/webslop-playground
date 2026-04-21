import { ref, watch } from 'vue'

// Composables are plain functions that bundle reusable reactive logic.
export function useLocalStorage<T>(key: string, initialValue: T) {
  const existingValue = window.localStorage.getItem(key)
  const state = ref(existingValue ? (JSON.parse(existingValue) as T) : initialValue)

  // This watcher keeps browser storage in sync whenever the reactive value changes.
  watch(
    state,
    newValue => {
      window.localStorage.setItem(key, JSON.stringify(newValue))
    },
    { deep: true },
  )

  return state
}
