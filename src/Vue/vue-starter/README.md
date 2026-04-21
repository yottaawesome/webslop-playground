# Vue Starter — Core Vue Concepts in One Sample

A small **Vue 3 + TypeScript + Vite** app built for learning. The sample is a study planner that uses the core Vue concepts in one place so you can read the code, run it, and experiment safely.

## What This Sample Demonstrates

| Concept | Where to look | What it teaches |
| --- | --- | --- |
| `<script setup lang="ts">` | Every `.vue` file | Vue's modern single-file component authoring style |
| `ref()` and `reactive()` | `src/App.vue`, `src/components/LessonForm.vue` | How Vue tracks reactive state |
| `computed()` | `src/App.vue`, `src/components/GoalSummary.vue` | Derive UI state without duplicating data |
| `watch()` | `src/App.vue`, `src/composables/useLocalStorage.ts` | React to changes and run side effects |
| `onMounted()` | `src/App.vue`, `src/components/LessonForm.vue` | Run code when a component is mounted |
| `v-model` | `src/App.vue`, `src/components/LessonForm.vue` | Two-way binding for form inputs |
| `v-if`, `v-show`, `v-for` | `src/components/LessonList.vue`, `src/App.vue` | Conditional rendering and list rendering |
| Props and emits | `src/components/LessonForm.vue`, `src/components/LessonList.vue` | Parent/child communication |
| Slots | `src/components/BasePanel.vue` | Reusable wrapper components with flexible content |
| Provide / inject | `src/App.vue`, `src/components/GoalSummary.vue` | Share data without passing props through every layer |
| Composables | `src/composables/useLocalStorage.ts` | Extract reusable stateful logic |

## Project Structure

```text
src/
├── App.vue                          # Root component that wires the sample together
├── main.ts                          # App entry point
├── style.css                        # Global styling
├── composables/
│   └── useLocalStorage.ts           # Reusable persistence composable
├── components/
│   ├── BasePanel.vue                # Slot-based layout wrapper
│   ├── GoalSummary.vue              # Injected study goal + computed messaging
│   ├── LessonForm.vue               # Form state, v-model, emits, lifecycle hook
│   └── LessonList.vue               # Props, emits, v-if, v-for
├── keys.ts                          # Typed provide/inject keys
└── types.ts                         # Shared TypeScript types
```

## How to Read the Sample

1. Start with `src/App.vue` to see how the whole app fits together.
2. Open `src/components/LessonForm.vue` to learn form handling, `v-model`, and emits.
3. Open `src/components/LessonList.vue` to see list rendering and event bubbling.
4. Read `src/composables/useLocalStorage.ts` to understand a simple reusable composable.
5. Check `src/components/GoalSummary.vue` and `src/keys.ts` for provide/inject.

## Install, Run, and Build

From the repository root:

```bash
cd src/Vue/vue-starter
npm install
npm run dev
```

Then open the local Vite URL shown in the terminal, usually `http://localhost:5173`.

To create a production build:

```bash
cd src/Vue/vue-starter
npm run build
```

To preview the production build locally:

```bash
cd src/Vue/vue-starter
npm run preview
```

## Things to Try

1. Add a new lesson and watch the status message update.
2. Toggle filters to see `computed()` and `watch()` affect the UI.
3. Change the injected study goal in `App.vue` and see `GoalSummary.vue` update.
4. Modify `BasePanel.vue` to add another named slot.
5. Extend the `Lesson` type with a new field and wire it through the app.
