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