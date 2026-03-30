import { TodoItem } from './TodoItem'
import type { Todo } from '../App'

interface TodoListProps {
  todos: Todo[]
  onToggle: (id: number) => void
  onDelete: (id: number) => void
}

export function TodoList({ todos, onToggle, onDelete }: TodoListProps) {
  if (todos.length === 0) {
    return <p style={{ color: '#888', textAlign: 'center' }}>No items yet. Add one above!</p>
  }

  return (
    <ul style={{ listStyle: 'none' }}>
      {todos.map(todo => (
        <TodoItem key={todo.id} todo={todo} onToggle={onToggle} onDelete={onDelete} />
      ))}
    </ul>
  )
}
