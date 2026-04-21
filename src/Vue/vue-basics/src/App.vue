<script setup lang="ts">
// ─────────────────────────────────────────────────────────────────────────────
// App.vue — the root component for the "vue-basics" learning sample.
//
// Each <section class="demo"> below isolates one core Vue concept so you can
// read the code, see it run, and tweak it in isolation.
// ─────────────────────────────────────────────────────────────────────────────
import { computed, onMounted, provide, ref, watch } from 'vue'
import Counter from './components/Counter.vue'
import UserCard from './components/UserCard.vue'
import { useDoubled } from './composables/useDoubled'
import { themeKey } from './keys'

// 1. REACTIVE STATE — `ref()` wraps a value so Vue knows when it changes.
//    Read/write via `.value` in <script>, but use it directly in <template>.
const name = ref('Learner')

// 2. TWO-WAY BINDING — `v-model` in the template binds an input to this ref.
const message = ref('')

// 3. COMPUTED — a value derived from other reactive data. Cached until deps change.
const greeting = computed(() => `Hello, ${name.value || 'stranger'}!`)

// 4. WATCHER — run a side effect whenever a value changes.
const log = ref<string[]>([])
watch(message, (next, prev) => {
  log.value.unshift(`message changed: "${prev}" → "${next}"`)
  if (log.value.length > 5) log.value.pop()
})

// 5. LIST RENDERING — an array we'll iterate over with v-for.
const fruits = ref(['apples', 'oranges', 'bananas'])
const newFruit = ref('')
function addFruit() {
  const trimmed = newFruit.value.trim()
  if (!trimmed) return
  fruits.value.push(trimmed)
  newFruit.value = ''
}

// 6. CONDITIONAL RENDERING — toggle a flag; v-if and v-show react to it.
const showTip = ref(true)

// 7. PROPS/EMITS — `Counter.vue` receives a prop and emits an event back.
const counterTotal = ref(0)
function handleCounterChange(value: number) {
  counterTotal.value = value
}

// 8. COMPOSABLE — reusable reactive logic extracted into a plain function.
const { number, doubled, increment } = useDoubled(3)

// 9. PROVIDE / INJECT — share data with descendants without prop-drilling.
const theme = ref<'light' | 'dark'>('light')
provide(themeKey, theme)

// 10. LIFECYCLE — run code after the component mounts to the DOM.
const mountedAt = ref('')
onMounted(() => {
  mountedAt.value = new Date().toLocaleTimeString()
})
</script>

<template>
  <h1>Vue Basics</h1>
  <p class="intro">
    Ten tiny demos covering the core Vue concepts. Open <code>src/App.vue</code>
    alongside the page and match each section to its code.
  </p>

  <!-- 1. ref + text interpolation with {{ }} -->
  <section class="demo">
    <span class="demo__tag">1. ref &amp; interpolation</span>
    <h2>Reactive text</h2>
    <!-- `name` is a ref; Vue auto-unwraps it inside templates. -->
    <p>Current name: <strong>{{ name }}</strong></p>
    <!-- @click is shorthand for v-on:click -->
    <button @click="name = 'Ada'">Set name to Ada</button>
    <button @click="name = ''">Clear</button>
  </section>

  <!-- 2. v-model two-way binding -->
  <section class="demo">
    <span class="demo__tag">2. v-model</span>
    <h2>Two-way binding</h2>
    <input v-model="message" placeholder="Type anything..." />
    <p class="muted">You typed: {{ message || '(nothing yet)' }}</p>
  </section>

  <!-- 3. computed -->
  <section class="demo">
    <span class="demo__tag">3. computed</span>
    <h2>Derived values</h2>
    <!-- `greeting` recomputes only when `name` changes. -->
    <p>{{ greeting }}</p>
  </section>

  <!-- 4. watch -->
  <section class="demo">
    <span class="demo__tag">4. watch</span>
    <h2>Side effects on change</h2>
    <p class="muted">Change the input in demo #2 and this log updates.</p>
    <ul>
      <li v-for="entry in log" :key="entry">{{ entry }}</li>
    </ul>
    <p v-if="log.length === 0" class="muted">(no changes yet)</p>
  </section>

  <!-- 5. v-for -->
  <section class="demo">
    <span class="demo__tag">5. v-for</span>
    <h2>List rendering</h2>
    <ul>
      <!-- :key helps Vue track list items efficiently. -->
      <li v-for="fruit in fruits" :key="fruit">{{ fruit }}</li>
    </ul>
    <div class="row">
      <!-- @keyup.enter is an event modifier — fires only on the Enter key. -->
      <input v-model="newFruit" placeholder="Add a fruit" @keyup.enter="addFruit" />
      <button class="primary" @click="addFruit">Add</button>
    </div>
  </section>

  <!-- 6. v-if vs v-show -->
  <section class="demo">
    <span class="demo__tag">6. v-if / v-show</span>
    <h2>Conditional rendering</h2>
    <button @click="showTip = !showTip">Toggle tip</button>
    <!-- v-if removes the element; v-show toggles CSS display. -->
    <p v-if="showTip">💡 <strong>v-if</strong> removes this element from the DOM entirely.</p>
    <p v-show="showTip" class="muted">
      (v-show would keep it in the DOM and just hide it — useful for frequent toggles.)
    </p>
  </section>

  <!-- 7. Props & emits (child component) -->
  <section class="demo">
    <span class="demo__tag">7. props &amp; emits</span>
    <h2>Child components</h2>
    <!--
      :label is a bound prop (passes a string down).
      @change is a listener for an event the child emits back up.
    -->
    <Counter :label="'Clicks'" @change="handleCounterChange" />
    <p class="muted">Parent sees total: {{ counterTotal }}</p>
  </section>

  <!-- 8. Composable (reusable reactive logic) -->
  <section class="demo">
    <span class="demo__tag">8. composable</span>
    <h2>Reusable logic with composables</h2>
    <p>number = {{ number }} · doubled = {{ doubled }}</p>
    <button @click="increment">+1</button>
    <p class="muted">Logic lives in <code>composables/useDoubled.ts</code>.</p>
  </section>

  <!-- 9. Slots + provide/inject -->
  <section class="demo">
    <span class="demo__tag">9. slots &amp; provide/inject</span>
    <h2>Slots and shared state</h2>
    <div class="row">
      <button @click="theme = theme === 'light' ? 'dark' : 'light'">
        Toggle theme (currently {{ theme }})
      </button>
    </div>
    <!--
      Anything between <UserCard>...</UserCard> that isn't in a named slot
      goes into the default slot inside UserCard.vue.
    -->
    <UserCard name="Grace Hopper">
      <p>Computer scientist who popularised machine-independent programming languages.</p>
      <!-- Named slot: this content is placed where UserCard declares <slot name="footer" />. -->
      <template #footer>
        <span class="muted">(injected theme: {{ theme }})</span>
      </template>
    </UserCard>
  </section>

  <!-- 10. Lifecycle hook -->
  <section class="demo">
    <span class="demo__tag">10. lifecycle</span>
    <h2>onMounted</h2>
    <p>App mounted at: <strong>{{ mountedAt }}</strong></p>
  </section>
</template>
