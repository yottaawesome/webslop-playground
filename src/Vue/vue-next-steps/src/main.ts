// Application entry point.
//
// In the boilerplate sample this file was a one-liner. Here we install two
// Vue plugins before mounting the app:
//
//   1. Pinia   – the official state management library for Vue 3.
//   2. Router  – `vue-router@4` gives us client-side navigation.
//
// Each plugin is an object with an `install(app)` method; calling `app.use()`
// runs it. Order only matters if one plugin depends on another — here it
// doesn't, but Pinia is typically registered first so stores can be used
// inside route guards / components from the very first render.

import { createApp } from 'vue'
import { createPinia } from 'pinia'

import App from './App.vue'
import { router } from './router'
import './style.css'

const app = createApp(App)

app.use(createPinia())
app.use(router)

app.mount('#app')
