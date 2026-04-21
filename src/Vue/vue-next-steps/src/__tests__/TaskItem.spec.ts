// Component test for TaskItem using @vue/test-utils.
//
// `mount` renders a real component in jsdom and returns a wrapper with
// query/interaction helpers. Because TaskItem uses <router-link>, we either
// need a router instance in the test or to stub the component. Stubbing is
// simpler and keeps the test focused on the component's own behaviour.

import { describe, expect, it } from 'vitest'
import { mount } from '@vue/test-utils'
import TaskItem from '../components/TaskItem.vue'
import type { Task } from '../stores/tasks'

function makeTask(overrides: Partial<Task> = {}): Task {
  return { id: 1, title: 'Sample', done: false, ...overrides }
}

function factory(task: Task) {
  return mount(TaskItem, {
    props: { task },
    global: {
      // Replace <router-link> with a plain <a> so we don't need a real router.
      stubs: { 'router-link': { template: '<a><slot /></a>' } },
    },
  })
}

describe('TaskItem', () => {
  it('renders the task title', () => {
    const wrapper = factory(makeTask({ title: 'Buy milk' }))
    expect(wrapper.text()).toContain('Buy milk')
  })

  it('applies the done class when task.done is true', () => {
    const wrapper = factory(makeTask({ done: true }))
    expect(wrapper.classes()).toContain('done')
  })

  it('emits toggle with the task id when the checkbox changes', async () => {
    const wrapper = factory(makeTask({ id: 42 }))
    await wrapper.get('input[type="checkbox"]').setValue(true)

    expect(wrapper.emitted('toggle')).toEqual([[42]])
  })

  it('emits remove with the task id when the remove button is clicked', async () => {
    const wrapper = factory(makeTask({ id: 7 }))
    await wrapper.get('button.remove').trigger('click')

    expect(wrapper.emitted('remove')).toEqual([[7]])
  })
})
