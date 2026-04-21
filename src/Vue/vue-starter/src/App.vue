<script setup lang="ts">
import { computed, onMounted, provide, reactive, ref, watch } from 'vue'
import BasePanel from './components/BasePanel.vue'
import GoalSummary from './components/GoalSummary.vue'
import LessonForm from './components/LessonForm.vue'
import LessonList from './components/LessonList.vue'
import { useLocalStorage } from './composables/useLocalStorage'
import { studyGoalKey } from './keys'
import type { Lesson, LessonDraft, LessonPriority, SortMode, StudyGoal } from './types'

// Seed data keeps the very first render interesting and gives the learner data to inspect.
const starterLessons: Lesson[] = [
  {
    id: 'intro-reactivity',
    title: 'Compare ref() and reactive()',
    topic: 'Reactivity',
    minutes: 20,
    priority: 'high',
    notes: 'Update the filter and toggles to see Vue react immediately.',
    isHandsOn: true,
    completed: true,
    createdAt: '2026-01-12T09:00:00.000Z',
  },
  {
    id: 'component-events',
    title: 'Trace props and emits between components',
    topic: 'Components',
    minutes: 25,
    priority: 'high',
    notes: 'Follow LessonForm and LessonList to see parent/child communication.',
    isHandsOn: true,
    completed: false,
    createdAt: '2026-01-13T09:00:00.000Z',
  },
  {
    id: 'template-practice',
    title: 'Experiment with v-if, v-show, and v-for',
    topic: 'Templates',
    minutes: 15,
    priority: 'medium',
    notes: 'Clear items or change filters to see conditional rendering in action.',
    isHandsOn: false,
    completed: false,
    createdAt: '2026-01-14T09:00:00.000Z',
  },
]

// The composable hides the localStorage plumbing so App can focus on business logic.
const lessons = useLocalStorage<Lesson[]>('vue-starter-lessons', starterLessons)

// `ref()` is ideal for standalone primitive values and array references.
const filterText = ref('')
const statusMessage = ref('Preparing the Vue learning sample...')
const mountedAt = ref('')

// `reactive()` is handy when a few related UI values naturally belong together.
const ui = reactive({
  showCompletedOnly: false,
  showNotes: true,
  sortBy: 'priority' as SortMode,
})

// Provide a reactive object so nested components can consume it without prop drilling.
const studyGoal = reactive<StudyGoal>({
  learnerName: 'Curious Developer',
  weeklyMinutes: 90,
  focusTopic: 'Vue fundamentals',
})

provide(studyGoalKey, studyGoal)

const priorityWeight: Record<LessonPriority, number> = {
  high: 0,
  medium: 1,
  low: 2,
}

// Computed values derive useful state without duplicating data.
const completedCount = computed(() => lessons.value.filter(lesson => lesson.completed).length)

const completedMinutes = computed(() =>
  lessons.value
    .filter(lesson => lesson.completed)
    .reduce((total, lesson) => total + lesson.minutes, 0),
)

const visibleLessons = computed(() => {
  const searchTerm = normalizeText(filterText.value)

  return [...lessons.value]
    .filter(lesson => {
      const matchesSearch =
        searchTerm.length === 0 ||
        normalizeText(lesson.title).includes(searchTerm) ||
        normalizeText(lesson.topic).includes(searchTerm) ||
        normalizeText(lesson.notes).includes(searchTerm)

      const matchesCompletion = !ui.showCompletedOnly || lesson.completed

      return matchesSearch && matchesCompletion
    })
    .sort(sortLessons)
})

const progressPercent = computed(() => {
  if (studyGoal.weeklyMinutes === 0) {
    return 0
  }

  return Math.min(100, Math.round((completedMinutes.value / studyGoal.weeklyMinutes) * 100))
})

// Watches are best for side effects or "react over time" behavior.
watch(filterText, newValue => {
  const trimmedValue = newValue.trim()

  statusMessage.value =
    trimmedValue.length > 0
      ? `Filtering lessons for "${trimmedValue}".`
      : 'Showing every lesson again.'
})

watch(
  () => ({
    total: lessons.value.length,
    completed: completedCount.value,
  }),
  (next, previous) => {
    if (next.total > previous.total) {
      statusMessage.value = 'A new lesson was added to the plan.'
      return
    }

    if (next.total < previous.total) {
      statusMessage.value = 'A lesson was removed from the plan.'
      return
    }

    if (next.completed > previous.completed) {
      statusMessage.value = 'Nice work - you completed another lesson.'
      return
    }

    if (next.completed < previous.completed) {
      statusMessage.value = 'That lesson is active again.'
    }
  },
)

// Lifecycle hooks let you run code at key moments in a component's life.
onMounted(() => {
  mountedAt.value = new Date().toLocaleTimeString()
  statusMessage.value = 'The app is mounted. Try the form, filters, and list actions.'
})

function addLesson(draft: LessonDraft) {
  lessons.value = [
    ...lessons.value,
    {
      ...draft,
      id: crypto.randomUUID(),
      completed: false,
      createdAt: new Date().toISOString(),
    },
  ]
}

function toggleLesson(id: string) {
  lessons.value = lessons.value.map(lesson =>
    lesson.id === id ? { ...lesson, completed: !lesson.completed } : lesson,
  )
}

function removeLesson(id: string) {
  lessons.value = lessons.value.filter(lesson => lesson.id !== id)
}

function clearCompleted() {
  lessons.value = lessons.value.filter(lesson => !lesson.completed)
  statusMessage.value = 'Completed lessons cleared.'
}

function normalizeText(value: string) {
  return value.trim().toLowerCase()
}

function sortLessons(a: Lesson, b: Lesson) {
  switch (ui.sortBy) {
    case 'minutes':
      return b.minutes - a.minutes
    case 'title':
      return a.title.localeCompare(b.title)
    case 'priority':
    default:
      return priorityWeight[a.priority] - priorityWeight[b.priority]
  }
}
</script>

<template>
  <main class="app-shell">
    <header class="hero">
      <p class="eyebrow">Vue 3 + TypeScript learning sample</p>
      <h1>Learn the core Vue concepts in one small app</h1>
      <p class="hero__text">
        This study planner stays intentionally small, but it demonstrates the features you will
        reach for in most real Vue applications.
      </p>
    </header>

    <section class="panel-grid">
      <BasePanel
        title="1. Reactive state and computed values"
        subtitle="This section uses ref, reactive, computed, watch, v-model, and a few common directives."
      >
        <template #actions>
          <span class="stat-chip">{{ completedCount }} / {{ lessons.length }} complete</span>
        </template>

        <div class="stats-row">
          <article class="mini-stat">
            <span class="mini-stat__label">Visible lessons</span>
            <strong>{{ visibleLessons.length }}</strong>
          </article>
          <article class="mini-stat">
            <span class="mini-stat__label">Completed minutes</span>
            <strong>{{ completedMinutes }}</strong>
          </article>
        </div>

        <div class="control-grid">
          <label class="field">
            <span>Search lessons</span>
            <input
              v-model="filterText"
              type="search"
              placeholder="Try 'component' or 'forms'"
            />
          </label>

          <label class="field">
            <span>Sort lessons</span>
            <select v-model="ui.sortBy">
              <option value="priority">Priority</option>
              <option value="minutes">Minutes</option>
              <option value="title">Title</option>
            </select>
          </label>
        </div>

        <div class="toggle-row">
          <label class="toggle">
            <input v-model="ui.showCompletedOnly" type="checkbox" />
            <span>Only show completed lessons</span>
          </label>

          <label class="toggle">
            <input v-model="ui.showNotes" type="checkbox" />
            <span>Show inline learning notes</span>
          </label>
        </div>

        <p v-show="ui.showNotes" class="inline-note">
          This block uses <code>v-model</code> for form state, <code>v-show</code> for quick
          visibility toggling, and the computed <code>visibleLessons</code> list for filtering.
        </p>

        <template #footer>
          <span>{{ statusMessage }}</span>
          <span>Mounted at {{ mountedAt }}</span>
        </template>
      </BasePanel>

      <BasePanel
        title="2. Typed props and emits"
        subtitle="LessonForm owns temporary input state and emits a strongly typed payload back to App."
      >
        <template #actions>
          <span class="stat-chip stat-chip--accent">{{ studyGoal.focusTopic }}</span>
        </template>

        <LessonForm @add-lesson="addLesson" />
      </BasePanel>

      <BasePanel
        title="3. Provide / inject and slots"
        subtitle="GoalSummary injects shared data from App, while BasePanel demonstrates named slots."
      >
        <GoalSummary
          :completed-count="completedCount"
          :completed-minutes="completedMinutes"
          :progress-percent="progressPercent"
          :total-count="lessons.length"
        />
      </BasePanel>

      <BasePanel
        title="4. Conditional and list rendering"
        subtitle="LessonList uses v-if for the empty state, v-for for items, and emits actions upward."
      >
        <template #actions>
          <button
            class="secondary-button"
            :disabled="completedCount === 0"
            @click="clearCompleted"
          >
            Clear completed
          </button>
        </template>

        <LessonList
          :lessons="visibleLessons"
          @toggle-lesson="toggleLesson"
          @remove-lesson="removeLesson"
        />
      </BasePanel>
    </section>
  </main>
</template>
