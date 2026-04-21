export type LessonPriority = 'low' | 'medium' | 'high'
export type SortMode = 'priority' | 'minutes' | 'title'

export interface LessonDraft {
  title: string
  topic: string
  minutes: number
  priority: LessonPriority
  notes: string
  isHandsOn: boolean
}

export interface Lesson extends LessonDraft {
  id: string
  completed: boolean
  createdAt: string
}

export interface StudyGoal {
  learnerName: string
  weeklyMinutes: number
  focusTopic: string
}
