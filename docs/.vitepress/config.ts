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
    title: 'dsa problems',
    description: 'data structures and algorithms problem documentation with solutions',
    base: '/dsa/',

    themeConfig: {
        logo: '/logo.svg',

        nav: [
            { text: 'home', link: '/' },
            {
                text: 'by difficulty',
                items: [
                    { text: 'basic', link: '/#basic' },
                    { text: 'intermediate', link: '/#intermediate' },
                    { text: 'advanced', link: '/#advanced' }
                ]
            }
        ],

        sidebar: [
            {
                text: 'basic',
                collapsed: false,
                items: groupByDifficulty().basic.map(p => ({
                    text: p.name,
                    link: `/${p.filename}`
                }))
            },
            {
                text: 'intermediate',
                collapsed: false,
                items: groupByDifficulty().intermediate.map(p => ({
                    text: p.name,
                    link: `/${p.filename}`
                }))
            },
            {
                text: 'advanced',
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
            message: 'released under the MIT license',
            copyright: 'copyright © 2025-present'
        }
    },

    // Ensure markdown features are enabled
    markdown: {
        theme: 'github-dark',
        lineNumbers: true
    }
})
