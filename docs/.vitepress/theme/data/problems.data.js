import { readdirSync, readFileSync } from 'fs'
import { join, dirname } from 'path'
import { fileURLToPath } from 'url'
import matter from 'gray-matter'

const __dirname = dirname(fileURLToPath(import.meta.url))

export default {
    load() {
        const docsPath = join(__dirname, '../../../')
        const files = readdirSync(docsPath).filter(f => f.endsWith('.md') && f !== 'problems.md' && f !== 'index.md')

        return files.map(file => {
            const content = readFileSync(join(docsPath, file), 'utf-8')
            const { data } = matter(content)
            return {
                filename: file.replace('.md', ''),
                ...data
            }
        }).sort((a, b) => a.name.localeCompare(b.name))
    }
}
