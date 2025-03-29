/**
 * Main Application Entry Point for ToggleBoys Ligan 2025
 */

// Google Sheets export URL
const SPREADSHEET_ID = '1bw6pXc641nv13V9ECxuwjTPzJq1z3ubpMsTQSVQewVs';
const fileURL = `https://docs.google.com/spreadsheets/d/${SPREADSHEET_ID}/export`;

/**
 * Initialize the application when the DOM is loaded
 */
document.addEventListener('DOMContentLoaded', function() {
  initApp();
});

/**
 * Main application initialization
 */
function initApp() {
  // Show loading message
  document.getElementById('message').textContent = 'Loading data...';
  
  // Fetch data and build all tables
  fetchExcelFile(fileURL)
    .then(dataArray => {
      buildStallning(dataArray);
      buildSummary(dataArray);
      buildDetailedTable(dataArray);
      buildCountTable(dataArray);
      
      // Clear loading message
      document.getElementById('message').textContent = '';
    })
    .catch(error => {
      console.error('Error initializing application:', error);
      document.getElementById('message').textContent = `Error: ${error.message}`;
    });
} 