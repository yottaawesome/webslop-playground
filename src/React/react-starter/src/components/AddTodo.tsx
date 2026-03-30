import { useState, type FormEvent } from 'react'

interface AddTodoProps {
  onAdd: (text: string) => void
}

export function AddTodo({ onAdd }: AddTodoProps) {
  const [text, setText] = useState('')

  const handleSubmit = (e: FormEvent) => {
    e.preventDefault()
    const trimmed = text.trim()
    if (!trimmed) return
    onAdd(trimmed)
    setText('')
  }

  return (
    <form onSubmit={handleSubmit} style={styles.form}>
      <input
        type="text"
        value={text}
        onChange={e => setText(e.target.value)}
        placeholder="Add a learning goal..."
        style={styles.input}
      />
      <button type="submit" style={styles.button}>Add</button>
    </form>
  )
}

const styles = {
  form: {
    display: 'flex',
    gap: '0.5rem',
    marginBottom: '1.5rem',
  },
  input: {
    flex: 1,
    padding: '10px 12px',
    border: '1px solid #ccc',
    borderRadius: '4px',
    fontSize: '1rem',
  },
  button: {
    padding: '10px 20px',
    background: '#0078d4',
    color: 'white',
    border: 'none',
    borderRadius: '4px',
    fontSize: '1rem',
    cursor: 'pointer',
  },
} as const
