<script setup lang="ts">
defineProps<{
  title: string
  subtitle: string
}>()
</script>

<template>
  <section class="panel">
    <header class="panel__header">
      <div>
        <h2>{{ title }}</h2>
        <p>{{ subtitle }}</p>
      </div>

      <!-- Named slots let the parent decide what extra UI belongs in the header. -->
      <div v-if="$slots.actions" class="panel__actions">
        <slot name="actions" />
      </div>
    </header>

    <div class="panel__body">
      <!-- The default slot is where each panel's main content is rendered. -->
      <slot />
    </div>

    <footer v-if="$slots.footer" class="panel__footer">
      <slot name="footer" />
    </footer>
  </section>
</template>

<style scoped>
.panel {
  display: grid;
  gap: 1rem;
  padding: 1.35rem;
  border: 1px solid var(--border);
  border-radius: 1.35rem;
  background: var(--panel);
  box-shadow: var(--shadow);
}

.panel__header,
.panel__footer {
  display: flex;
  gap: 1rem;
  align-items: center;
  justify-content: space-between;
}

.panel__header h2 {
  margin: 0 0 0.35rem;
  font-size: 1.2rem;
}

.panel__header p,
.panel__footer {
  margin: 0;
  color: var(--text-soft);
}

.panel__body {
  display: grid;
  gap: 1rem;
}

@media (max-width: 700px) {
  .panel__header,
  .panel__footer {
    align-items: flex-start;
    flex-direction: column;
  }
}
</style>
