<script setup lang="ts">
// Demonstrates: default + named slots, and `inject` to read shared state.
import { inject } from 'vue'
import { themeKey } from '../keys'

defineProps<{
  name: string
}>()

// Pull the theme ref that App.vue `provide`d.
// Without a default, `inject` returns `Ref<...> | undefined`.
const theme = inject(themeKey)
</script>

<template>
  <!-- :class binding switches styling based on the injected theme.
       Note: refs are auto-unwrapped in templates, so we use `theme` directly. -->
  <article :class="['card', `card--${theme ?? 'light'}`]">
    <header class="card__header">
      <strong>{{ name }}</strong>
    </header>

    <!-- Default slot: content placed between <UserCard>...</UserCard> lands here. -->
    <div class="card__body">
      <slot />
    </div>

    <!-- Named slot: only rendered if the parent provides <template #footer>. -->
    <footer v-if="$slots.footer" class="card__footer">
      <slot name="footer" />
    </footer>
  </article>
</template>

<style scoped>
.card {
  border: 1px solid #cbd5e1;
  border-radius: 0.6rem;
  padding: 0.9rem 1rem;
  margin-top: 0.75rem;
}
.card--dark {
  background: #0f172a;
  color: #e2e8f0;
  border-color: #1e293b;
}
.card__header { margin-bottom: 0.35rem; }
.card__footer { margin-top: 0.5rem; padding-top: 0.5rem; border-top: 1px dashed #cbd5e1; }
</style>
