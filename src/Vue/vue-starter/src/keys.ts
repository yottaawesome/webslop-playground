import type { InjectionKey } from 'vue'
import type { StudyGoal } from './types'

// A typed symbol makes provide/inject safer than sharing a plain string key.
export const studyGoalKey: InjectionKey<StudyGoal> = Symbol('study-goal')
