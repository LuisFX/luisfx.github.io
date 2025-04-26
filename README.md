# LuisFX Portfolio

A modern, interactive portfolio website built with F# and Fable, showcasing skills, projects, and experience with smooth animations and responsive design.


## Technologies

- **F#** - Type-safe functional programming language
- **Fable** - F# to JavaScript compiler
- **Feliz** - React DSL for F#
- **Feliz.Router** - Client-side routing for Feliz
- **Tailwind CSS** - Utility-first CSS framework
- **DaisyUI** - Component library for Tailwind CSS

## Features

- 🚀 Fully responsive design that works across all device sizes
- 🎨 Modern UI with animations and smooth transitions
- 🌙 Light/dark theme support
- 📱 Mobile-friendly navigation
- ⚡ Fast client-side routing with Feliz.Router
- 💻 Interactive code snippets with typing animations
- 🔄 State management with Elmish

## Development Journal

A detailed development journal is available at [web/JOURNAL.md](web/JOURNAL.md), which tracks progress, fixed issues, and future plans.

## Getting Started

### Prerequisites

- .NET 6.0 SDK or higher
- Node.js 16.x or higher
- npm or yarn

### Installation

1. Clone the repository:
   ```
   git clone https://github.com/luisfx/luisfx.github.io.git
   cd luisfx.github.io
   ```

2. Install dependencies:
   ```
   cd web
   npm install
   ```

3. Start the development server:
   ```
   npm start
   ```

4. Open your browser at `http://localhost:8080`

## Project Structure

```
luisfx.github.io/
├── web/
│   ├── src/               # F# source code
│   │   ├── Pages/         # Page components
│   │   ├── App.fs         # Main application component
│   │   ├── State.fs       # Application state and routing
│   │   ├── Layouts.fs     # Layout components
│   │   └── Theme.fs       # Theme handling
│   ├── public/            # Static assets
│   │   └── img/           # Images
│   ├── package.json       # npm dependencies
│   └── JOURNAL.md         # Development journal
└── README.md              # Project documentation
```

## Roadmap

### Phase 1: Foundation ✅
- [x] Set up project structure with F# and Fable
- [x] Implement responsive layout with Tailwind CSS
- [x] Create main navigation and mobile menu
- [x] Design and implement Home page
- [x] Add client-side routing with Feliz.Router
- [x] Create typing animation for code snippets

### Phase 2: Core Pages 🚧
- [ ] Complete About page with biography and personal information
- [ ] Design and implement Projects page with project showcase
- [ ] Create Experience page with timeline of work history
- [ ] Build Contact page with contact form
- [ ] Add Skills page with detailed skills breakdown

### Phase 3: Enhanced Features 📋
- [ ] Implement blog functionality
- [ ] Add project filtering on Projects page
- [ ] Create project detail pages
- [ ] Integrate FontAwesome for improved icons
- [ ] Add smooth scroll animations
- [ ] Implement search functionality

### Phase 4: Optimization and Deployment 🔮
- [ ] Optimize images and assets
- [ ] Improve loading performance
- [ ] Add SEO metadata
- [ ] Set up GitHub Actions for CI/CD
- [ ] Deploy to GitHub Pages

## Contributing

Contributions are welcome! Feel free to open an issue or submit a pull request.

## License

This project is licensed under the MIT License - see the LICENSE file for details.

## Contact

- GitHub: [@luisfx](https://github.com/luisfx)
- LinkedIn: [Luis Fontoura Torres](https://www.linkedin.com/in/luisfontouratorres/)
