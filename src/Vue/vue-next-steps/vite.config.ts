/// <reference types="vitest" />
import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'

// https://vite.dev/config/
export default defineConfig({
  plugins: [vue()],
  // Vitest reads config from vite.config.ts. `environment: 'jsdom'` gives us
  // a DOM implementation so @vue/test-utils can mount components.
  test: {
    environment: 'jsdom',
    globals: false, // use explicit imports from 'vitest' for clarity
    include: ['src/**/*.spec.ts'],
  },
})
