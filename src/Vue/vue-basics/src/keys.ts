import type { InjectionKey, Ref } from 'vue'

// A typed InjectionKey lets provide/inject share data with full type safety.
// The value stored under this key is a Ref<'light' | 'dark'> — see App.vue.
export const themeKey: InjectionKey<Ref<'light' | 'dark'>> = Symbol('theme')
