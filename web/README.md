# LuisFX Personal Website

This repository contains the source code for my personal website, built with F#, Fable, Feliz, and modern web technologies.

## 🛠️ Tech Stack

- **F#** - Main programming language
- **Fable** - F# to JavaScript compiler
- **Feliz** - React wrapper for F#
- **Elmish** - State management
- **Tailwind CSS** - Utility-first CSS framework
- **DaisyUI** - Component library for Tailwind
- **Vite** - Build tool and dev server

## 🚀 Features

- Modern, responsive design
- Interactive UI components
- Theme switching
- WebAssembly demos
- AI chatbot integration (coming soon)
- Interactive visualizations
- Blog with MDX support (coming soon)

## 🏗️ Development

### Prerequisites

* [.NET SDK](https://www.microsoft.com/net/download/core) v7.0 or higher
* [Node.js](https://nodejs.org) v18+ LTS
* [dotnet fable](https://www.nuget.org/packages/Fable/) tool

### Getting Started

1. Install the .NET tools:
   ```bash
   dotnet tool restore
   ```

2. Install the npm dependencies:
   ```bash
   npm install
   ```

3. Start the development server:
   ```bash
   npm start
   ```

4. Open your browser and navigate to http://localhost:3000

### Building for Production

To build the website for production:
```bash
npm run build
```

This will create optimized files in the `dist` directory.

## 📂 Project Structure

- `src/` - F# source code
  - `Pages/` - Individual page components
  - `Components.fs` - Reusable UI components
  - `Layouts.fs` - Main layout components
  - `Theme.fs` - Theme management
  - `State.fs` - Elmish state management
  - `App.fs` - Main application component
  - `Main.fs` - Entry point
  - `styles.css` - Tailwind and custom styles

## 🚢 Deployment

This website is automatically deployed to GitHub Pages when changes are pushed to the main branch. The deployment workflow is defined in `.github/workflows/deploy.yml`.

## 📝 License

MIT

## 🙏 Acknowledgements

- [Fable](https://fable.io)
- [Feliz](https://zaid-ajaj.github.io/Feliz)
- [Tailwind CSS](https://tailwindcss.com)
- [DaisyUI](https://daisyui.com)
- [Vite](https://vitejs.dev)