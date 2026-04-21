<script setup lang="ts">
// Simple landing page. A "view" in router parlance is just a Vue component
// that happens to be mounted at a route. There's nothing special about the
// folder name `views/` — it's a convention to separate routed components
// from reusable leaf components.
</script>

<template>
  <section>
    <h2>Welcome</h2>
    <p>
      This sample builds on <code>vue-boilerplate</code> by adding the three
      "next steps" most Vue apps reach for:
    </p>
    <ul>
      <li><strong>Vue Router</strong> for client-side navigation.</li>
      <li><strong>Pinia</strong> for shared, reactive state.</li>
      <li><strong>Vitest</strong> for unit testing stores and components.</li>
    </ul>
    <p>Head to the <router-link to="/tasks">Tasks</router-link> page to try the store.</p>
  </section>
</template>
