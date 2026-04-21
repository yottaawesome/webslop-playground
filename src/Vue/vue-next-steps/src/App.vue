<script setup lang="ts">
// The root component. In a router-based app, App.vue is usually a thin shell
// that renders a persistent layout (nav, header, footer) around a <RouterView>
// — the placeholder that swaps in the component for the current route.
//
// `RouterLink` is the navigation primitive; it renders an <a> but intercepts
// clicks so the router handles them without a full page reload. The
// `active-class` / automatic `router-link-active` class on the rendered anchor
// lets us style the current route.

import { RouterLink, RouterView } from 'vue-router'
import { useTasksStore } from './stores/tasks'

// Pinia stores are called as functions inside `setup`. The returned object is
// reactive — accessing `store.openCount` in the template auto-tracks.
const tasks = useTasksStore()
</script>

<template>
  <div class="app">
    <header class="app-header">
      <h1>Vue Next Steps</h1>
      <nav>
        <!--
          RouterLink renders to <a href="/"> but intercepts navigation.
          Vue Router automatically adds `router-link-active` /
          `router-link-exact-active` classes based on the current URL.
        -->
        <RouterLink to="/">Home</RouterLink>
        <RouterLink to="/tasks">Tasks ({{ tasks.openCount }})</RouterLink>
        <RouterLink to="/about">About</RouterLink>
      </nav>
    </header>

    <main>
      <!--
        RouterView is where the component matched by the current route gets
        mounted. Everything around it stays put between navigations.
      -->
      <RouterView />
    </main>

    <footer class="app-footer">
      <small>Demonstrates Vue Router, Pinia, and Vitest.</small>
    </footer>
  </div>
</template>
