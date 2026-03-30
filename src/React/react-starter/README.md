# React Starter — Modern React + Vite + TypeScript

A minimal starter project to refamiliarize yourself with React using modern patterns.

## What's Inside

A **todo-style learning tracker** that demonstrates the core modern React concepts:

| Concept | Where | What to learn |
|---|---|---|
| Function components | Every `.tsx` file | Components are just functions returning JSX |
| `useState` hook | `App.tsx`, `AddTodo.tsx` | Replaces `this.state` / `setState` |
| `useEffect` hook | `useLocalStorage.ts` | Replaces lifecycle methods |
| Custom hooks | `hooks/useLocalStorage.ts` | Extract & reuse stateful logic |
| TypeScript props | `TodoItem.tsx`, `TodoList.tsx` | Type-safe component interfaces |
| Lifting state up | `App.tsx` owns state | Children receive callbacks via props |

## Project Structure

```
src/
├── main.tsx               ← Entry point (replaces index.js)
├── App.tsx                ← Root component, owns state
├── index.css              ← Global styles
├── components/
│   ├── AddTodo.tsx        ← Form with controlled input
│   ├── TodoList.tsx       ← List rendering with .map()
│   └── TodoItem.tsx       ← Individual item with typed props
└── hooks/
    └── useLocalStorage.ts ← Custom hook (read this carefully!)
```

## Getting Started

```bash
cd src/react-starter
npm install
npm run dev
```

Then open http://localhost:5173

## Exercises to Try

1. **Add a filter** — show All / Active / Completed todos (practice conditional rendering + state)
2. **Add an edit mode** — double-click a todo to edit its text (practice `useRef` + `useEffect`)
3. **Fetch from an API** — load todos from jsonplaceholder.typicode.com (practice `useEffect` with fetch, then try TanStack Query)
4. **Extract a `useTodos` hook** — move all todo logic out of App into a custom hook
5. **Add routing** — install `react-router-dom` and create separate pages

## Next Steps

Once comfortable here, move to [Next.js](https://nextjs.org/learn) to explore
Server Components, file-based routing, and full-stack React.
