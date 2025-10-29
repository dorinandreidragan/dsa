import { defineConfig } from 'vitepress'
import { readdirSync, readFileSync } from 'fs'
import { join, dirname } from 'path'
import { fileURLToPath } from 'url'
import matter from 'gray-matter'

const __dirname = dirname(fileURLToPath(import.meta.url))

interface ProblemData {
    filename: string
    name: string
    difficulty?: string
    data_structure?: string
    time_complexity?: string
    space_complexity?: string
    description?: string
    canonical?: boolean
}

// Helper to get all problem files
function getProblems(): ProblemData[] {
    const docsPath = join(__dirname, '../')
    const files = readdirSync(docsPath).filter((f: string) => f.endsWith('.md') && f !== 'problems.md' && f !== 'index.md')

    return files.map((file: string) => {
        const content = readFileSync(join(docsPath, file), 'utf-8')
        const { data } = matter(content)
        return {
            filename: file.replace('.md', ''),
            ...data
        } as ProblemData
    })
}

// Group problems by difficulty
function groupByDifficulty() {
    const problems = getProblems()
    const groups: Record<string, ProblemData[]> = {
        basic: [],
        intermediate: [],
        advanced: []
    }

    problems.forEach((p: ProblemData) => {
        const difficulty = (p.difficulty || 'basic').toLowerCase()
        if (groups[difficulty]) {
            groups[difficulty].push(p)
        }
    })

    // Sort alphabetically within each group
    Object.keys(groups).forEach(key => {
        groups[key].sort((a, b) => a.name.localeCompare(b.name))
    })

    return groups
}

export default defineConfig({
    title: 'DSA Problems',
    description: 'Data structures and algorithms problem documentation with solutions',

    themeConfig: {
        logo: '/logo.svg',

        nav: [
            { text: 'Home', link: '/' },
            {
                text: 'By Difficulty',
                items: [
                    { text: 'Basic', link: '/#basic' },
                    { text: 'Intermediate', link: '/#intermediate' },
                    { text: 'Advanced', link: '/#advanced' }
                ]
            }
        ],

        sidebar: [
            {
                text: 'Basic',
                collapsed: false,
                items: groupByDifficulty().basic.map(p => ({
                    text: p.name,
                    link: `/${p.filename}`
                }))
            },
            {
                text: 'Intermediate',
                collapsed: false,
                items: groupByDifficulty().intermediate.map(p => ({
                    text: p.name,
                    link: `/${p.filename}`
                }))
            },
            {
                text: 'Advanced',
                collapsed: false,
                items: groupByDifficulty().advanced.map(p => ({
                    text: p.name,
                    link: `/${p.filename}`
                }))
            }
        ],

        socialLinks: [
            { icon: 'github', link: 'https://github.com/dorinandreidragan/dsa' }
        ],

        search: {
            provider: 'local'
        },

        footer: {
            message: 'Released under the MIT License.',
            copyright: 'Copyright © 2025-present'
        }
    },

    // Ensure markdown features are enabled
    markdown: {
        theme: 'github-dark',
        lineNumbers: true
    }
})
