import type { Todo } from '../App'

interface TodoItemProps {
  todo: Todo
  onToggle: (id: number) => void
  onDelete: (id: number) => void
}

export function TodoItem({ todo, onToggle, onDelete }: TodoItemProps) {
  return (
    <li style={styles.item}>
      <label style={styles.label}>
        <input
          type="checkbox"
          checked={todo.completed}
          onChange={() => onToggle(todo.id)}
          style={styles.checkbox}
        />
        <span style={todo.completed ? styles.completedText : styles.text}>
          {todo.text}
        </span>
      </label>
      <button onClick={() => onDelete(todo.id)} style={styles.deleteBtn} title="Delete">
        ✕
      </button>
    </li>
  )
}

const styles = {
  item: {
    display: 'flex',
    alignItems: 'center',
    justifyContent: 'space-between',
    padding: '10px 12px',
    background: 'white',
    borderRadius: '4px',
    marginBottom: '6px',
    boxShadow: '0 1px 2px rgba(0,0,0,0.06)',
  },
  label: {
    display: 'flex',
    alignItems: 'center',
    gap: '10px',
    flex: 1,
    cursor: 'pointer',
  },
  checkbox: {
    width: '18px',
    height: '18px',
    cursor: 'pointer',
  },
  text: {
    fontSize: '0.95rem',
  },
  completedText: {
    fontSize: '0.95rem',
    textDecoration: 'line-through' as const,
    color: '#999',
  },
  deleteBtn: {
    background: 'none',
    border: 'none',
    color: '#ccc',
    fontSize: '1rem',
    cursor: 'pointer',
    padding: '4px 8px',
    borderRadius: '4px',
  },
} as const
