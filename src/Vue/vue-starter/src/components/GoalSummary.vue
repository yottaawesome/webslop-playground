<script setup lang="ts">
import { computed, inject } from 'vue'
import { studyGoalKey } from '../keys'

const props = defineProps<{
  completedCount: number
  completedMinutes: number
  progressPercent: number
  totalCount: number
}>()

// Inject gives this component access to shared data without extra prop plumbing.
const studyGoal = inject(studyGoalKey)

if (!studyGoal) {
  throw new Error('GoalSummary must be rendered under a provider for studyGoalKey.')
}

const remainingMinutes = computed(() =>
  Math.max(0, studyGoal.weeklyMinutes - props.completedMinutes),
)

const encouragement = computed(() => {
  if (props.progressPercent >= 100) {
    return 'You reached the weekly target. Try raising the goal or adding a stretch lesson.'
  }

  if (props.progressPercent >= 60) {
    return 'You are close to the goal. One more focused session should do it.'
  }

  return 'Keep going - each completed lesson will move the progress bar.'
})
</script>

<template>
  <div class="goal-summary">
    <p class="goal-summary__lead">
      {{ studyGoal.learnerName }} is focusing on <strong>{{ studyGoal.focusTopic }}</strong> with
      a weekly target of <strong>{{ studyGoal.weeklyMinutes }} minutes</strong>.
    </p>

    <div class="goal-summary__grid">
      <article class="summary-card">
        <span>Completed lessons</span>
        <strong>{{ completedCount }} / {{ totalCount }}</strong>
      </article>

      <article class="summary-card">
        <span>Study progress</span>
        <strong>{{ progressPercent }}%</strong>
      </article>

      <article class="summary-card">
        <span>Minutes remaining</span>
        <strong>{{ remainingMinutes }}</strong>
      </article>
    </div>

    <div
      class="progress-bar"
      :aria-valuemax="100"
      :aria-valuemin="0"
      :aria-valuenow="progressPercent"
      aria-label="Weekly study progress"
      role="progressbar"
    >
      <div class="progress-bar__fill" :style="{ width: `${progressPercent}%` }"></div>
    </div>

    <p class="goal-summary__message">{{ encouragement }}</p>
  </div>
</template>

<style scoped>
.goal-summary {
  display: grid;
  gap: 1rem;
}

.goal-summary__lead,
.goal-summary__message {
  margin: 0;
  color: var(--text-soft);
}

.goal-summary__grid {
  display: grid;
  gap: 1rem;
  grid-template-columns: repeat(3, minmax(0, 1fr));
}

.summary-card {
  padding: 1rem;
  border: 1px solid var(--border);
  border-radius: 1rem;
  background: var(--panel-muted);
}

.summary-card span {
  display: block;
  color: var(--text-soft);
  font-size: 0.9rem;
}

.summary-card strong {
  display: block;
  margin-top: 0.25rem;
  font-size: 1.4rem;
}

.progress-bar {
  overflow: hidden;
  height: 0.9rem;
  border-radius: 999px;
  background: var(--panel-muted);
}

.progress-bar__fill {
  height: 100%;
  border-radius: inherit;
  background: linear-gradient(90deg, var(--accent), #60a5fa);
  transition: width 0.2s ease;
}

@media (max-width: 700px) {
  .goal-summary__grid {
    grid-template-columns: 1fr;
  }
}
</style>
