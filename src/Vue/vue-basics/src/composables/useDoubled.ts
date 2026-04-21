import { computed, ref } from 'vue'

// A composable is a plain function that uses Vue's reactivity APIs.
// Convention: name it `useXxx` and return refs/computeds/functions.
export function useDoubled(initial = 0) {
  const number = ref(initial)
  const doubled = computed(() => number.value * 2)

  function increment() {
    number.value++
  }

  return { number, doubled, increment }
}
