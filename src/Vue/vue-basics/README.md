# Vue Basics — One Concept per Demo

A tiny **Vue 3 + TypeScript + Vite** sample. Each section of the page isolates
one core Vue concept so you can read the code, see it run, and experiment.

If you want a larger, more app-like example, see the sibling
[`vue-starter`](../vue-starter/README.md) project.

## Concepts Covered

| # | Concept | Where |
|---|---|---|
| 1 | `ref()` and `{{ }}` interpolation | `src/App.vue` |
| 2 | `v-model` two-way binding | `src/App.vue` |
| 3 | `computed()` derived values | `src/App.vue` |
| 4 | `watch()` side effects | `src/App.vue` |
| 5 | `v-for` list rendering (with `:key`) | `src/App.vue` |
| 6 | `v-if` vs `v-show` conditional rendering | `src/App.vue` |
| 7 | Props & emits between components | `src/components/Counter.vue` |
| 8 | Composables (reusable reactive logic) | `src/composables/useDoubled.ts` |
| 9 | Slots (default + named) and `provide`/`inject` | `src/components/UserCard.vue`, `src/keys.ts` |
| 10 | Lifecycle hooks (`onMounted`) | `src/App.vue` |

Also demonstrated throughout: `<script setup lang="ts">` single-file components,
event modifiers (e.g. `@keyup.enter`), attribute binding (`:class`, `:key`),
and typed `defineProps` / `defineEmits`.

## Project Structure

```text
src/
├── App.vue                       # Root component — 10 numbered demo sections
├── main.ts                       # App entry point
├── style.css                     # Global styles
├── keys.ts                       # Typed InjectionKey for provide/inject
├── composables/
│   └── useDoubled.ts             # Example composable
└── components/
    ├── Counter.vue               # Props + emits
    └── UserCard.vue              # Slots + inject
```

## Install, Run, Build

```bash
cd src/Vue/vue-basics
npm install
npm run dev       # start dev server (usually http://localhost:5173)
npm run build     # type-check + production build
npm run preview   # preview the built app locally
```

## Suggested Exercises

1. Add an 11th demo that uses `watchEffect` instead of `watch`.
2. Make `Counter.vue` support a `step` prop (e.g. `+2` per click).
3. Add a second named slot to `UserCard.vue` (e.g. `#avatar`).
4. Extract the fruits list into a new composable `useList<T>()`.
5. Persist `fruits` to `localStorage` using a `watch` with `{ deep: true }`.
