# PuttPals - ToggleBoys Ligan 2025

A disc golf league tracking application for the ToggleBoys Ligan 2025. This web application helps track scores, calculate points, and maintain league standings across multiple courses.

## Features

- 🏆 Professional hero banner with discgolf theme and responsive design
- 📊 Track scores across multiple disc golf courses
- 🏆 Calculate points based on player performance
- 📈 Display standings, best results, and round statistics
- 📖 Quick access to league rules via modal dialog
- 📝 Version history tracking through "Versionshistorik" modal
- 📱 Responsive design for mobile and desktop
- 🌓 Toggle between dark and light themes
- 🎨 Modern UI with animations and transitions
- 💯 Position highlighting and ranking visualization
- ⚡ Real-time data from Google Sheets

## Live Demo

Visit the [PuttPals application](https://github.com/JohanFernstrom/PuttPals) to see the application in action!

## Getting Started

### Prerequisites

- Node.js (for development)
- Modern web browser

### Installation

1. Clone the repository:
   ```
   git clone https://github.com/JohanFernstrom/PuttPals.git
   cd PuttPals
   ```

2. Install dependencies:
   ```
   npm install
   ```

3. Start the development server:
   ```
   npm start
   ```

4. Open your browser to http://localhost:3000

## Project Structure

```
PuttPals/
├── css/                  # Stylesheets
├── docs/                 # Documentation files
├── images/               # Image assets including hero banner
├── js/                   # JavaScript files
│   ├── components/       # UI components
│   └── utils/            # Utility functions
├── index.html            # Main HTML file
├── package.json          # Project configuration
└── .gitignore            # Git ignore file
```

## Key Features

### Hero Banner
- Custom SVG-based hero banner with discgolf theme
- Responsive design adapts to different screen sizes
- Features a discgolf basket, disc, and flight path
- Gradient overlay for improved text readability

### Interactive Modals
- **Rules Modal**: Access comprehensive league rules directly from the header
- **Version History Modal**: Track application updates in Swedish via footer link
- Keyboard navigation support (Escape to close)
- Background scroll locking for improved user experience

### League Statistics
- Standings with total points
- Best results per course with dates and scores
- Comprehensive history of all rounds
- Round counts per player and course

## Design Features

- **Modern UI**: Sleek, responsive interface with smooth transitions and animations
- **Dark/Light Mode**: Toggle between dark and light themes based on preference
- **Responsive Tables**: Optimized table display for all screen sizes
- **Position Highlighting**: Visual indicators for top rankings
- **Loading Animation**: Improved user feedback during data loading
- **CSS Variables**: Easily customize the color scheme and spacing

## Dependencies

- [SheetJS](https://github.com/SheetJS/sheetjs) - Excel file parsing
- [Google Fonts](https://fonts.google.com/) - Montserrat and Open Sans fonts
- [Font Awesome](https://fontawesome.com/) - Icons for improved UI

## Documentation

For more detailed information, please check the [docs](./docs) directory:

- [Installation Guide](./docs/INSTALLATION.md)
- [Product Requirements Document](./docs/PRD.md)
- [Rules](./docs/Rules.md)
- [Technical Documentation](./docs/TechnicalDocumentation.md)
- [Changelog](./docs/CHANGELOG.md)

## Data Sources

- [Google Spreadsheet](https://docs.google.com/spreadsheets/d/1bw6pXc641nv13V9ECxuwjTPzJq1z3ubpMsTQSVQewVs/edit?usp=sharing) - The data source for the application
- [Register New Round](https://forms.gle/MShZKyWr9vmKfCz68) - Submit a new round via Google Forms

## Recent Updates

- Added custom SVG hero banner with discgolf elements
- Implemented rules modal accessible from header
- Added version history modal (Versionshistorik) in footer
- Improved responsiveness across all device sizes
- Refactored modal system for better code organization

## Contributing

1. Fork the repository
2. Create your feature branch: `git checkout -b feature/amazing-feature`
3. Commit your changes: `git commit -m 'Add some amazing feature'`
4. Push to the branch: `git push origin feature/amazing-feature`
5. Open a Pull Request

## License

This project is licensed under the MIT License. 