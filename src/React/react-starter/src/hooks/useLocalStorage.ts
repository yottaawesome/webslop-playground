import { useState, useEffect } from 'react'

/**
 * A hook that syncs state with localStorage.
 * Demonstrates custom hooks — one of the most powerful React patterns.
 */
export function useLocalStorage<T>(key: string, initialValue: T): [T, (value: T) => void] {
  const [storedValue, setStoredValue] = useState<T>(() => {
    try {
      const item = window.localStorage.getItem(key)
      return item ? (JSON.parse(item) as T) : initialValue
    } catch {
      return initialValue
    }
  })

  useEffect(() => {
    try {
      window.localStorage.setItem(key, JSON.stringify(storedValue))
    } catch {
      // Storage full or unavailable — silently ignore
    }
  }, [key, storedValue])

  return [storedValue, setStoredValue]
}
