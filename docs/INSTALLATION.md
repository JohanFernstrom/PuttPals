# PuttPals Installation Guide

This guide explains how to install, configure, and deploy the PuttPals application.

## Local Development Setup

### Prerequisites

- [Node.js](https://nodejs.org/) (v14 or higher)
- [npm](https://www.npmjs.com/) (v6 or higher)
- A modern web browser (Chrome, Firefox, Safari, or Edge)

### Installation Steps

1. **Clone the repository**

   ```bash
   git clone https://github.com/yourusername/PuttPals.git
   cd PuttPals
   ```

2. **Install dependencies**

   ```bash
   npm install
   ```

3. **Start the development server**

   ```bash
   npm run dev
   ```

   This will start a local development server with hot reloading at http://localhost:3000.

## Configuration

### Google Sheets Integration

PuttPals reads data from a Google Spreadsheet. To configure your own data source:

1. Create a Google Spreadsheet with the proper format (see Data Format section below)
2. Make sure the spreadsheet is shared with appropriate visibility settings
3. Update the `SPREADSHEET_ID` in `js/main.js` with your spreadsheet ID:

   ```javascript
   const SPREADSHEET_ID = 'your-spreadsheet-id';
   ```

   The spreadsheet ID is the part of your Google Sheets URL between `/d/` and `/edit`:
   `https://docs.google.com/spreadsheets/d/SPREADSHEET_ID/edit`

### Data Format

The spreadsheet should have the following columns:

1. Timestamp (automatically removed by the app)
2. Course/Anläggning
3. Player 1 score
4. Player 2 score
5. Player 3 score
6. Player 4 score
7. Player 5 score
8. Date

## Deployment

### Option 1: Basic Web Server Hosting

1. Build the project (if needed):

   ```bash
   npm run build
   ```

2. Upload all files to your web hosting service via FTP or their upload interface.

3. Ensure your hosting service can serve HTML, CSS, and JavaScript files.

### Option 2: GitHub Pages

1. Create a GitHub repository for your project
2. Push your code to the repository
3. Go to repository settings, select "Pages" from the sidebar
4. Configure GitHub Pages to use your main branch
5. GitHub will provide a URL where your site is published

### Option 3: Netlify

1. Create an account on [Netlify](https://www.netlify.com/)
2. Connect your GitHub repository
3. Configure build settings if needed (usually not required for this project)
4. Netlify will deploy your site and provide a URL

## Troubleshooting

### CORS Issues

If you're getting CORS errors when fetching data from Google Sheets:

1. Make sure your spreadsheet is set to "Anyone with the link can view"
2. Consider using a CORS proxy if issues persist

### Performance Optimization

If you have a large dataset and the application is running slowly:

1. Consider limiting the data loaded at once
2. Implement pagination in the tables
3. Cache results locally using localStorage

## Customization

### Theme Customization

The application uses CSS variables for easy theming. Edit the `:root` section in `css/styles.css` to change colors and other design elements:

```css
:root {
  --primary: #4CAF50;  /* Change this to your preferred primary color */
  --secondary: #2196F3;  /* Change this to your preferred secondary color */
  /* ... other variables ... */
}
```

### Adding New Features

To extend the application with new features:

1. Create new JavaScript files in the appropriate directories (`js/components` for new UI components)
2. Import and initialize them in `js/main.js`
3. Add corresponding HTML elements if needed

## Support

For issues, feature requests, or questions, please create an issue in the GitHub repository or contact the maintainers directly. 