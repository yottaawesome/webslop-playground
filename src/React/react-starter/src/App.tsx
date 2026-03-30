import { useState } from 'react'
import { TodoList } from './components/TodoList'
import { AddTodo } from './components/AddTodo'
import { useLocalStorage } from './hooks/useLocalStorage'

export interface Todo {
  id: number
  text: string
  completed: boolean
}

const initialTodos: Todo[] = [
  { id: 1, text: 'Learn React hooks (useState, useEffect)', completed: false },
  { id: 2, text: 'Build something with TypeScript', completed: false },
  { id: 3, text: 'Try TanStack Query for data fetching', completed: false },
  { id: 4, text: 'Explore Next.js App Router', completed: false },
]

export default function App() {
  // Custom hook: persists state to localStorage
  const [todos, setTodos] = useLocalStorage<Todo[]>('todos', initialTodos)
  const [nextId, setNextId] = useState(initialTodos.length + 1)

  const addTodo = (text: string) => {
    setTodos([...todos, { id: nextId, text, completed: false }])
    setNextId(nextId + 1)
  }

  const toggleTodo = (id: number) => {
    setTodos(todos.map(t => t.id === id ? { ...t, completed: !t.completed } : t))
  }

  const deleteTodo = (id: number) => {
    setTodos(todos.filter(t => t.id !== id))
  }

  const remaining = todos.filter(t => !t.completed).length

  return (
    <>
      <h1>React Learning Tracker</h1>
      <h2>{remaining} of {todos.length} items remaining</h2>
      <AddTodo onAdd={addTodo} />
      <TodoList todos={todos} onToggle={toggleTodo} onDelete={deleteTodo} />
    </>
  )
}
