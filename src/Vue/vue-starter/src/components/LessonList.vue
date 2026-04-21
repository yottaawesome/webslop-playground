<script setup lang="ts">
import type { Lesson } from '../types'

const props = defineProps<{
  lessons: Lesson[]
}>()

const emit = defineEmits<{
  toggleLesson: [id: string]
  removeLesson: [id: string]
}>()

// Typed props and emits make component contracts obvious and safe.
function priorityClass(priority: Lesson['priority']) {
  return `priority-pill--${priority}`
}

function formatCreatedAt(isoDate: string) {
  return new Date(isoDate).toLocaleDateString()
}
</script>

<template>
  <div v-if="props.lessons.length === 0" class="empty-state">
    No lessons match the current filter. Try clearing the search or adding another item.
  </div>

  <ul v-else class="lesson-list">
    <li
      v-for="lesson in props.lessons"
      :key="lesson.id"
      :class="['lesson-item', { 'lesson-item--done': lesson.completed }]"
    >
      <div class="lesson-item__content">
        <div class="lesson-item__header">
          <h3>{{ lesson.title }}</h3>

          <div class="lesson-item__badges">
            <span :class="['priority-pill', priorityClass(lesson.priority)]">
              {{ lesson.priority }}
            </span>
            <span v-if="lesson.isHandsOn" class="priority-pill priority-pill--hands-on">
              hands-on
            </span>
          </div>
        </div>

        <p class="lesson-item__meta">
          {{ lesson.topic }} · {{ lesson.minutes }} min · Added {{ formatCreatedAt(lesson.createdAt) }}
        </p>

        <p v-if="lesson.notes" class="lesson-item__notes">{{ lesson.notes }}</p>
      </div>

      <div class="lesson-item__actions">
        <button class="secondary-button" @click="emit('toggleLesson', lesson.id)">
          {{ lesson.completed ? 'Mark active' : 'Mark done' }}
        </button>
        <button class="danger-button" @click="emit('removeLesson', lesson.id)">Remove</button>
      </div>
    </li>
  </ul>
</template>

<style scoped>
.empty-state {
  padding: 1rem;
  border: 1px dashed var(--border);
  border-radius: 1rem;
  color: var(--text-soft);
  background: var(--panel-muted);
}

.lesson-list {
  display: grid;
  gap: 1rem;
  list-style: none;
  margin: 0;
  padding: 0;
}

.lesson-item {
  display: grid;
  gap: 1rem;
  grid-template-columns: minmax(0, 1fr) auto;
  padding: 1rem;
  border: 1px solid var(--border);
  border-radius: 1rem;
  background: #ffffff;
}

.lesson-item--done {
  background: rgba(21, 128, 61, 0.06);
}

.lesson-item__header {
  display: flex;
  gap: 1rem;
  align-items: flex-start;
  justify-content: space-between;
}

.lesson-item__header h3,
.lesson-item__meta,
.lesson-item__notes {
  margin: 0;
}

.lesson-item__badges {
  display: flex;
  gap: 0.5rem;
  flex-wrap: wrap;
}

.lesson-item__meta,
.lesson-item__notes {
  color: var(--text-soft);
}

.lesson-item__notes {
  margin-top: 0.5rem;
}

.lesson-item__actions {
  display: flex;
  gap: 0.75rem;
  align-items: center;
}

.priority-pill {
  display: inline-flex;
  align-items: center;
  padding: 0.25rem 0.65rem;
  border-radius: 999px;
  font-size: 0.8rem;
  font-weight: 700;
  text-transform: capitalize;
}

.priority-pill--high {
  background: rgba(185, 28, 28, 0.12);
  color: var(--danger);
}

.priority-pill--medium {
  background: rgba(180, 83, 9, 0.12);
  color: var(--warning);
}

.priority-pill--low {
  background: rgba(59, 130, 246, 0.12);
  color: var(--accent);
}

.priority-pill--hands-on {
  background: rgba(21, 128, 61, 0.12);
  color: var(--success);
}

.danger-button {
  border: 1px solid rgba(185, 28, 28, 0.25);
  border-radius: 0.85rem;
  background: #ffffff;
  color: var(--danger);
  padding: 0.7rem 0.95rem;
  font-weight: 600;
}

@media (max-width: 760px) {
  .lesson-item {
    grid-template-columns: 1fr;
  }

  .lesson-item__header,
  .lesson-item__actions {
    align-items: flex-start;
    flex-direction: column;
  }
}
</style>
