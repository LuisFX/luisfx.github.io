import mdx from '@mdx-js/rollup'
import tailwindcss from '@tailwindcss/vite'
import { defineConfig } from 'vite'
import { resolve } from 'path'
import react from '@vitejs/plugin-react'

export default defineConfig({
  plugins: [
    react({include: /\.(jsx|js|mdx|md|tsx|ts)$/}),
    tailwindcss(),
    mdx({
      providerImportSource: '@mdx-js/react',
      remarkPlugins: [],
      rehypePlugins: []
    })
  ],
  root: "./src",
  base: "./",
  build: {
    outDir: "../dist",
    emptyOutDir: true,
    sourcemap: true,
    minify: true,
    rollupOptions: {
      input: {
        main: resolve(__dirname, 'src/index.html'),
      },
    },
  },
  css: {
    postcss: {
      plugins: [
        tailwindcss,
      ]
    }
  },
  server: {
    port: 3000,
    open: true
  },
  assetsInclude: ['**/*.mdx']
})
