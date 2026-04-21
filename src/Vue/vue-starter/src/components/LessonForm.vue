<script setup lang="ts">
import { onMounted, reactive, ref } from 'vue'
import type { LessonDraft, LessonPriority } from '../types'

const emit = defineEmits<{
  addLesson: [draft: LessonDraft]
}>()

const titleInput = ref<HTMLInputElement | null>(null)
const formMessage = ref('')

// This reactive object powers the form through v-model bindings in the template.
const form = reactive<LessonDraft>({
  title: '',
  topic: 'Reactivity',
  minutes: 20,
  priority: 'medium',
  notes: '',
  isHandsOn: true,
})

const priorityOptions: LessonPriority[] = ['low', 'medium', 'high']
const topicOptions = ['Reactivity', 'Templates', 'Components', 'Forms', 'Routing']

// Template refs become useful once the element exists in the DOM.
onMounted(() => {
  titleInput.value?.focus()
})

function submitForm() {
  const trimmedTitle = form.title.trim()
  const safeMinutes = Number.isFinite(form.minutes) ? Math.max(5, form.minutes) : NaN

  if (trimmedTitle.length === 0) {
    formMessage.value = 'Please add a lesson title before submitting.'
    titleInput.value?.focus()
    return
  }

  if (Number.isNaN(safeMinutes)) {
    formMessage.value = 'Please enter a valid number of minutes.'
    return
  }

  emit('addLesson', {
    title: trimmedTitle,
    topic: form.topic,
    minutes: safeMinutes,
    priority: form.priority,
    notes: form.notes.trim(),
    isHandsOn: form.isHandsOn,
  })

  formMessage.value = 'Lesson added. Scroll down to see it in the list.'
  resetForm()
}

function resetForm() {
  form.title = ''
  form.topic = 'Reactivity'
  form.minutes = 20
  form.priority = 'medium'
  form.notes = ''
  form.isHandsOn = true
  titleInput.value?.focus()
}
</script>

<template>
  <form class="lesson-form" @submit.prevent="submitForm">
    <div class="lesson-form__grid">
      <label class="field field--wide">
        <span>Lesson title</span>
        <input
          ref="titleInput"
          v-model="form.title"
          placeholder="Example: Build a tiny counter with ref()"
          type="text"
        />
      </label>

      <label class="field">
        <span>Topic</span>
        <select v-model="form.topic">
          <option v-for="topic in topicOptions" :key="topic" :value="topic">
            {{ topic }}
          </option>
        </select>
      </label>

      <label class="field">
        <span>Minutes</span>
        <input v-model.number="form.minutes" min="5" step="5" type="number" />
      </label>

      <label class="field">
        <span>Priority</span>
        <select v-model="form.priority">
          <option v-for="priority in priorityOptions" :key="priority" :value="priority">
            {{ priority }}
          </option>
        </select>
      </label>

      <label class="field field--wide">
        <span>Notes</span>
        <textarea
          v-model="form.notes"
          placeholder="What do you want to practice or remember?"
          rows="3"
        ></textarea>
      </label>
    </div>

    <label class="toggle">
      <input v-model="form.isHandsOn" type="checkbox" />
      <span>Mark this as a hands-on exercise</span>
    </label>

    <div class="lesson-form__actions">
      <button class="primary-button" type="submit">Add lesson</button>
      <p class="lesson-form__hint">
        The title field is auto-focused with a template ref inside <code>onMounted()</code>.
      </p>
    </div>

    <p v-if="formMessage" class="lesson-form__message">{{ formMessage }}</p>
  </form>
</template>

<style scoped>
.lesson-form {
  display: grid;
  gap: 1rem;
}

.lesson-form__grid {
  display: grid;
  gap: 1rem;
  grid-template-columns: repeat(2, minmax(0, 1fr));
}

.field--wide {
  grid-column: 1 / -1;
}

.lesson-form__actions {
  display: flex;
  gap: 1rem;
  align-items: center;
  justify-content: space-between;
}

.primary-button {
  border: none;
  border-radius: 0.85rem;
  background: var(--accent);
  color: #ffffff;
  padding: 0.8rem 1.15rem;
  font-weight: 700;
}

.lesson-form__hint {
  margin: 0;
  color: var(--text-soft);
}

.lesson-form__message {
  margin: 0;
  color: var(--text-soft);
  font-size: 0.95rem;
}

@media (max-width: 700px) {
  .lesson-form__grid {
    grid-template-columns: 1fr;
  }

  .lesson-form__actions {
    align-items: flex-start;
    flex-direction: column;
  }
}
</style>
