# vue-next-steps

A Vue 3 + TypeScript sample that layers on the three most common "next steps"
a boilerplate app reaches for:

| Concept            | Library / Tool    | Shows up in                                  |
| ------------------ | ----------------- | -------------------------------------------- |
| Client-side routing | `vue-router@4`    | `src/router/index.ts`, `src/App.vue`         |
| State management   | `pinia`           | `src/stores/tasks.ts`, `src/views/TasksView.vue` |
| Unit testing       | `vitest` + `@vue/test-utils` | `src/__tests__/*.spec.ts`         |

It's a tiny "tasks" app: a list, a form to add items, and a detail page per
task — enough surface area for the three pieces above to be useful without
drowning them in features.

## Prerequisites

* Node 20+ (Vite 8 requirement)
* npm (or swap in pnpm / yarn if you prefer)

## Install

```powershell
cd src/Vue/vue-next-steps
npm install
```

## Run

```powershell
npm run dev       # start the Vite dev server with HMR
npm run build     # type-check (vue-tsc) + production build
npm run preview   # serve the production build locally
```

## Test

```powershell
npm test          # single run (CI-style)
npm run test:watch
npm run test:ui   # interactive Vitest UI in the browser
```

Tests live in `src/__tests__/`:

* `tasks.store.spec.ts` — exercises the Pinia store in isolation. Uses
  `setActivePinia(createPinia())` in `beforeEach` so each test sees a fresh
  store.
* `TaskItem.spec.ts` — mounts a component with `@vue/test-utils`, stubs out
  `<router-link>`, and asserts on rendered output + emitted events.

## Project structure

```
vue-next-steps/
├── index.html
├── package.json
├── vite.config.ts            # Vite + Vitest config (test: { environment: 'jsdom' })
├── tsconfig*.json
└── src/
    ├── main.ts               # createApp + app.use(createPinia()) + app.use(router)
    ├── App.vue               # Layout shell with <RouterLink> nav + <RouterView />
    ├── style.css
    ├── router/
    │   └── index.ts          # Route table, history mode, lazy-loaded detail view
    ├── stores/
    │   └── tasks.ts          # Setup-style Pinia store (state / getters / actions)
    ├── views/                # Components mounted at a route
    │   ├── HomeView.vue
    │   ├── TasksView.vue
    │   ├── TaskDetailView.vue
    │   └── AboutView.vue
    ├── components/
    │   └── TaskItem.vue      # Leaf component with typed props + emits
    └── __tests__/
        ├── tasks.store.spec.ts
        └── TaskItem.spec.ts
```

## Concepts worth exploring

### Vue Router

* **Route records** — `{ path, name, component }` entries in `router/index.ts`.
* **Dynamic segments** — `/tasks/:id` exposes `route.params.id` (always a
  string). We use `props: true` so the id arrives as a typed prop instead.
* **Lazy loading** — `component: () => import('../views/TaskDetailView.vue')`
  code-splits that view into its own chunk.
* **`<RouterLink>` vs `<a>`** — `<RouterLink>` intercepts the click, prevents
  a full page reload, and applies `.router-link-active` / `-exact-active`
  classes automatically.
* **Programmatic navigation** — `const router = useRouter(); router.push(...)`.

### Pinia

* **Setup stores** — `defineStore('id', () => { ... })` lets you use refs,
  `computed`, and plain functions (state / getters / actions). Whatever you
  return is what consumers see.
* **`storeToRefs`** — destructures state + getters while keeping reactivity.
  Don't plain-destructure store state; you'll lose reactivity.
* **Testability** — because a setup store is just a function, you test it by
  calling it after `setActivePinia(createPinia())`. No component needed.

### Vitest

* Reads its config from `vite.config.ts` under the `test` key, so plugins
  (like `@vitejs/plugin-vue`) and aliases Just Work.
* `environment: 'jsdom'` provides a DOM for component tests.
* Use `@vue/test-utils`' `mount()` to render components; query with
  `wrapper.get()`, interact with `trigger` / `setValue`, and inspect emitted
  events via `wrapper.emitted()`.

## Exercises to try

1. Add a route guard (`router.beforeEach`) that logs navigations to the
   console.
2. Persist `tasks` to `localStorage` — Pinia plugins are a good fit.
3. Add a search input bound to a `computed` filter over the task list.
4. Write a test that mounts `TasksView` with a real Pinia instance and the
   router's `memory` history (`createMemoryHistory`).

## Where to go next

* Where we came from: `vue-boilerplate` (minimal), `vue-basics` (one concept
  per demo), `vue-starter` (integrated study-planner app).
* Beyond this sample: SSR with [Nuxt](https://nuxt.com), component libraries
  like [PrimeVue](https://primevue.org) or [Vuetify](https://vuetifyjs.com),
  E2E testing with [Playwright](https://playwright.dev).
