# Vue Boilerplate — Hello World

A minimal **Vue 3 + TypeScript + Vite** starting point. Scaffolded with
`npm create vite@latest` using the `vue-ts` template, then trimmed down to the
essentials so you can build on top of it without deleting anything first.

For richer learning examples, see the sibling
[`vue-basics`](../vue-basics/README.md) (concept-by-concept demos) and
[`vue-starter`](../vue-starter/README.md) (small app) projects.

## What's Included

- **Vue 3** with the Composition API and `<script setup lang="ts">` SFCs
- **TypeScript** with `vue-tsc` type-checking on build
- **Vite** for dev server, HMR, and production builds
- A single `App.vue` with a click counter — enough to confirm reactivity works
- Minimal global styles in `src/style.css`

## Project Structure

```text
vue-boilerplate/
├── index.html                  # Vite entry HTML
├── package.json
├── tsconfig.json               # Extends @vue/tsconfig, references tsconfig.app/node
├── tsconfig.app.json           # Config for app source
├── tsconfig.node.json          # Config for Vite's own files
├── vite.config.ts              # Vite + @vitejs/plugin-vue
└── src/
    ├── App.vue                 # Root component — the "hello world"
    ├── main.ts                 # createApp(App).mount('#app')
    ├── style.css               # Global styles
    └── vite-env.d.ts           # Vite's TS ambient types
```

## Install, Run, Build

```bash
cd src/Vue/vue-boilerplate
npm install
npm run dev       # start dev server (usually http://localhost:5173)
npm run build     # type-check (vue-tsc) + production build to dist/
npm run preview   # preview the production build locally
```

## Next Steps

- Add a `components/` folder and break `App.vue` into pieces.
- Add a `composables/` folder for reusable reactive logic.
- Install a router: `npm install vue-router@4`.
- Install a state store: `npm install pinia`.
- Add testing: `npm install -D vitest @vue/test-utils jsdom`.
