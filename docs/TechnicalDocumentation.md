# ToggleBoys Ligan 2025 - Technical Documentation

## Architecture Overview

The ToggleBoys Ligan application is a client-side web application that uses the following technologies:

- HTML5 for structure
- CSS3 for styling (responsive dark theme)
- JavaScript for data processing and UI rendering
- SheetJS library for Excel file processing
- Google Sheets as the data backend

## Data Flow

1. User loads the web page
2. Application fetches Excel data from Google Sheets using the export URL
3. SheetJS library processes the Excel data
4. Data is transformed and used to build four different tables
5. Tables are rendered to the DOM

## Core Components

### Data Fetching

```javascript
const fileURL = 'https://docs.google.com/spreadsheets/d/1bw6pXc641nv13V9ECxuwjTPzJq1z3ubpMsTQSVQewVs/export';

function fetchExcelFile(url) {
  fetch(url)
    .then(response => response.arrayBuffer())
    .then(data => {
      const workbook = XLSX.read(data, { type: 'array', cellDates: true });
      // Process data...
    });
}
```

### Data Processing

The application processes the raw spreadsheet data in several ways:

1. **Data Cleaning**
   - Removes empty rows
   - Removes timestamp column
   - Formats dates

2. **Data Grouping**
   - Groups rounds by course

3. **Score Collection**
   - Collects up to 3 best scores per player per course

4. **Point Calculation**
   - Ranks players for each position at each course
   - Assigns points based on rank (3 for 1st, 2 for 2nd, 1 for 3rd)
   - Handles ties by giving same points to tied players
   - Sums points across positions and courses

### UI Rendering

Four main tables are generated dynamically:

1. **Ställning (Standings)** - `buildStallning()`
   - Shows point totals by player and course

2. **Bästa resultat per bana (Best results per course)** - `buildSummary()`
   - Shows best scores with date and points earned

3. **Alla tidigare rundor (All previous rounds)** - `buildDetailedTable()`
   - Shows raw data from the spreadsheet

4. **Antal rundor (Number of rounds)** - `buildCountTable()`
   - Shows count of rounds played by each player at each course

### UI Components

#### Modal System

The application includes a generic modal system that supports multiple modals:

```javascript
function initModal() {
  // Initialize rules modal
  initModalHandlers('rulesModal', 'rulesBtn');
  
  // Initialize patch notes modal
  initModalHandlers('patchNotesModal', 'patchNotesBtn');
}

function initModalHandlers(modalId, btnId) {
  const modal = document.getElementById(modalId);
  const btn = document.getElementById(btnId);
  const closeBtn = modal.querySelector('.close-button');
  
  // Open modal when the button is clicked
  btn.addEventListener('click', function(event) {
    event.preventDefault(); // Prevent default link behavior
    modal.classList.add('active');
    document.body.style.overflow = 'hidden'; // Prevent background scrolling
  });
  
  // Additional handlers for closing the modal...
}
```

The application currently has two modal instances:

1. **Rules Modal** - Accessible from the header, shows league rules
2. **Patch Notes Modal** - Accessible from the footer, shows version history in Swedish

Each modal shares the same CSS styling and interaction behavior, creating a consistent user experience.

The modals are styled using CSS with transitions for smooth animations:

```css
.modal {
  display: none;
  position: fixed;
  z-index: 1000;
  left: 0;
  top: 0;
  width: 100%;
  height: 100%;
  background-color: rgba(0, 0, 0, 0.7);
  opacity: 0;
  transition: opacity 0.3s ease;
}

.modal.active {
  opacity: 1;
  display: block;
}

.modal-content {
  background-color: var(--surface);
  margin: 5% auto;
  padding: 24px;
  border-radius: 8px;
  width: 90%;
  max-width: 800px;
  max-height: 80vh;
  overflow-y: auto;
  box-shadow: 0 10px 20px rgba(0,0,0,0.19), 0 6px 6px rgba(0,0,0,0.23);
  transform: translateY(-20px);
  transition: transform 0.3s ease;
}

.modal.active .modal-content {
  transform: translateY(0);
}
```

#### Hero Banner

The application features a responsive hero banner at the top of the content area:

```html
<div class="container">
    <div class="hero-banner">
        <img src="images/hero-placeholder.svg" alt="ToggleBoys Discgolf League">
        <div class="hero-banner-overlay">
            <h2 class="hero-banner-title">ToggleBoys Discgolf League 2025</h2>
            <p class="hero-banner-subtitle">Tracking scores and standings across multiple courses</p>
        </div>
    </div>
</div>
```

The hero banner uses responsive sizing with a height of 40% of the viewport (40vh) and adapts to different screen sizes using media queries:

```css
.hero-banner {
  width: 100%;
  height: 40vh;
  max-height: 400px;
  background-color: var(--surface);
  margin-bottom: var(--spacing-lg);
  border-radius: var(--radius-md);
  overflow: hidden;
  position: relative;
  box-shadow: var(--shadow-md);
}

.hero-banner img {
  width: 100%;
  height: 100%;
  object-fit: cover;
}

.hero-banner-overlay {
  position: absolute;
  bottom: 0;
  left: 0;
  width: 100%;
  padding: var(--spacing-lg);
  background: linear-gradient(to top, rgba(0, 0, 0, 0.7), transparent);
  color: white;
}
```

The banner includes a text overlay with a gradient background for improved readability. For smaller screens, both the banner height and text size are reduced for optimal viewing:

```css
@media (max-width: 768px) {
  .hero-banner {
    height: 30vh;
  }
  
  .hero-banner-title {
    font-size: 1.5rem;
  }
}
```

Images for the banner are stored in the `/images` directory, making it easy to update or change the banner image in the future.

#### Theme Toggle

// ... existing code ...

## Google Integration

### Data Source

The application pulls data from a Google Spreadsheet with the ID:
`1bw6pXc641nv13V9ECxuwjTPzJq1z3ubpMsTQSVQewVs`

### Data Input Methods

The application provides two buttons to allow data input:

1. **Manuell Inmatning** - Direct link to the Google Sheet for manual data entry
2. **Registrera ny runda** - Link to a Google Form for submitting new rounds

## Responsive Design

The application uses responsive CSS with media queries to adapt to different screen sizes:

```css
@media (max-width: 600px) {
  body {
    padding: 10px;
    font-size: 16px;
  }

  .header-container,
  table {
    width: 100%;
  }

  .btn {
    font-size: 16px;
    padding: 10px 14px;
  }
}
```

## Deployment

The application is a static website that can be hosted on any web server. No server-side processing is required as all data processing happens in the browser. 