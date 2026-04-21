// Router configuration.
//
// vue-router@4 is the Vue 3 router. Two pieces need to be chosen:
//
//   - History mode:
//       * createWebHistory()      – clean URLs (`/tasks/1`). Needs a server
//                                   that serves index.html for unknown paths.
//       * createWebHashHistory()  – hash-based URLs (`/#/tasks/1`). Works on
//                                   any static host with no server config.
//     We use web history here; Vite's dev server handles the SPA fallback
//     automatically during `npm run dev`.
//
//   - Route definitions: an array of { path, name, component } objects.
//
// Lazy-loading (dynamic `import()`) splits each view into its own JS chunk,
// so the initial bundle only contains what's needed for the first route.
// We use static imports here for simplicity and because the app is tiny,
// but the `TaskDetail` view shows the lazy-import pattern as a reference.

import { createRouter, createWebHistory, type RouteRecordRaw } from 'vue-router'

import HomeView from '../views/HomeView.vue'
import TasksView from '../views/TasksView.vue'
import AboutView from '../views/AboutView.vue'

const routes: RouteRecordRaw[] = [
  {
    path: '/',
    name: 'home',
    component: HomeView,
  },
  {
    path: '/tasks',
    name: 'tasks',
    component: TasksView,
  },
  {
    // Dynamic segment: `:id` is captured as `route.params.id` (always a string).
    path: '/tasks/:id',
    name: 'task-detail',
    // Lazy-loaded view — the chunk is fetched only when this route is visited.
    component: () => import('../views/TaskDetailView.vue'),
    // `props: true` passes route params as component props, which is cleaner
    // than reaching into `useRoute()` inside the component.
    props: true,
  },
  {
    path: '/about',
    name: 'about',
    component: AboutView,
  },
  {
    // Catch-all for unknown paths. The `:pathMatch(.*)*` syntax captures the
    // full unmatched URL, but we don't use it here — we just redirect home.
    path: '/:pathMatch(.*)*',
    redirect: { name: 'home' },
  },
]

export const router = createRouter({
  // `import.meta.env.BASE_URL` reflects Vite's `base` config so the app works
  // when deployed under a sub-path.
  history: createWebHistory(import.meta.env.BASE_URL),
  routes,
})
