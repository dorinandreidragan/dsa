import DefaultTheme from 'vitepress/theme'
import type { Theme } from 'vitepress'
import './style.css'
import DifficultyBadge from './components/DifficultyBadge.vue'
import ProblemMeta from './components/ProblemMeta.vue'
import ComplexityTag from './components/ComplexityTag.vue'

export default {
    extends: DefaultTheme,
    enhanceApp({ app }: { app: any }) {
        app.component('DifficultyBadge', DifficultyBadge)
        app.component('ProblemMeta', ProblemMeta)
        app.component('ComplexityTag', ComplexityTag)
    }
} satisfies Theme
