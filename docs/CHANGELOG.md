# Changelog

## v1.0.8 - 2025-03-29

### Förbättrad Hero Banner
- Uppdaterat hero banner med ny SVG-bild i mörkt tema
- Lagt till diskgolfspecifika designelement inklusive en korg och en disk
- Skapat en diskbana med streckad linje för att visa diskens flygbana
- Förbättrat textläsligheten med justeringar av textstorlek och positionering
- Lagt till subtil gradient-bakgrund för ökat djup och visuellt intresse

## v1.0.7 - 2025-03-29

### Hero Banner och förbättringar
- Lagt till en hero banner-sektion ovanför innehållet för bättre visuell presentation
- Implementerat responsiv höjd för olika skärmstorlekar
- Skapat en bildplats med överlappande text och gradient-överlappning
- Upprättat en images-mapp med standardbild för framtida bildbyten
- Tagit bort hover-effekter från hero banner för renare gränssnitt
- Säkerställt att hero banner fungerar väl både i ljust och mörkt läge
- Anpassat bannertext för olika skärmstorlekar genom media queries

## v1.0.6 - 2025-03-29

### Added Patch Notes Modal
- Added a "Versionshistorik" link in the footer for quick access to version history
- Implemented a modal dialog that displays the complete version history in Swedish
- Organized patch notes by version number with detailed sections for features and improvements
- Refactored the modal system to support multiple modals with shared code
- Created a reusable `initModalHandlers` function to handle modal interactions
- Added proper accessibility support including keyboard navigation and background scroll locking
- Updated documentation to include information about the patch notes feature

## v1.0.5 - 2025-03-29

### Added Rules Modal
- Added a "Regler" button in the header to provide quick access to league rules
- Implemented a modal dialog that displays the complete league rules
- Modal includes information about basic rules, point system, season ranking, and player eligibility
- Added smooth animations and transitions for opening/closing the modal
- Implemented keyboard navigation support (Escape key to close)
- Ensured the modal is fully responsive on all device sizes
- Added focus trap and background scroll locking for improved user experience
- Updated documentation to reflect the new feature

## v1.0.4 - 2025-03-29

### Mobile Responsiveness Improvements
- Improved responsive design for better mobile and small screen compatibility
- Added tiered media queries for different device sizes (phones, tablets, desktops)
- Optimized table layouts with horizontal scrolling on small screens
- Improved header and button layouts for narrow screens
- Added sticky table headers for better usability when scrolling
- Enhanced text readability with appropriate sizing and spacing
- Implemented responsive adjustments for very small screens (320px and below)

## v1.0.3 - 2025-03-29

### Cleanup and Optimization
- Removed redundant custom hot reload implementation (hotReload.js)
- Standardized on browser-sync for development hot reloading
- Removed unnecessary script reference from index.html
- Streamlined development workflow

## v1.0.2 - 2025-03-29

### Development Improvements
- Added basic hot reload functionality that watches for file changes
- Created a simple client-side file watcher (hotReload.js) that checks file timestamps
- Added package.json for project information and future dependency management
- Improved development workflow with automatic page refreshing

## v1.0.1 - 2025-03-29

### Compatibility Improvements
- Converted ES modules to standard JavaScript files for direct browser loading without a server
- Removed all `import`/`export` statements for better browser compatibility
- Updated the script loading order in index.html to ensure proper dependency resolution
- Made all utility functions globally available

## v1.0.0 - 2025-03-29

### Code Refactoring

#### Architecture Changes
- Restructured the codebase from a single monolithic HTML file to a modular structure
- Created a clear separation of concerns with HTML, CSS, and JavaScript in separate files
- Implemented ES modules for better code organization and maintainability
- Established a clear file hierarchy:
  - `/css` - Contains all styling
  - `/js` - Contains all JavaScript logic
    - `/js/components` - UI component modules
    - `/js/utils` - Utility functions

#### CSS Improvements
- Extracted all styles from inline `<style>` tag to external `styles.css` file
- Maintained all existing styling and responsive design

#### JavaScript Improvements
- Modularized JavaScript code into logical components:
  - `dataProcessor.js` - Core data processing utilities
  - `standings.js` - "Ställning" table component
  - `bestResults.js` - "Bästa resultat per bana" table component
  - `allRounds.js` - "Alla tidigare rundor" table component
  - `roundCount.js` - "Antal rundor" table component
  - `main.js` - Application initialization and coordination
- Added proper JSDoc comments for all functions
- Renamed variables and functions to better reflect their purpose:
  - Changed "venue" to "course" to align with discgolf terminology
  - Used more descriptive parameter and variable names
- Improved error handling throughout the application
- Extracted common functionality into reusable utility functions

#### HTML Improvements
- Simplified HTML structure
- Used external CSS and JavaScript files
- Added proper module type for ES modules

### Terminology Updates
- Updated all references from "minigolf" to "discgolf"
- Changed "venue" terminology to "course" throughout the code
- Changed "strokes" terminology to "throws" in documentation

### Documentation
- Created comprehensive documentation in the `/docs` directory
- Maintained and updated all existing documentation to reflect code changes
- Added comments throughout the codebase for better code understanding

### Bug Fixes
- Fixed potential issues with date formatting
- Improved error handling for data loading
- Added more robust validation of input data

### Performance Improvements
- Optimized data processing by extracting reusable functions
- Reduced code duplication across components
- Improved file loading by organizing code logically 