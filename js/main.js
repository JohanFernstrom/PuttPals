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
  // Load saved theme preference
  loadSavedTheme();
  
  // Initialize app components
  initApp();
  initThemeToggle();
});

/**
 * Main application initialization
 */
function initApp() {
  // Show loading animation
  showLoading('Laddar data...');
  
  // Fetch data and build all tables
  fetchExcelFile(fileURL)
    .then(dataArray => {
      // Add slight delays between building each table for better perceived performance
      setTimeout(() => buildStallning(dataArray), 100);
      setTimeout(() => buildSummary(dataArray), 300);
      setTimeout(() => buildDetailedTable(dataArray), 500);
      setTimeout(() => buildCountTable(dataArray), 700);
      
      // Hide loading message with a slight delay
      setTimeout(() => {
        hideLoading();
        // Add animation class to tables
        document.querySelectorAll('.table-container').forEach((table, index) => {
          setTimeout(() => {
            table.style.opacity = '0';
            table.style.transform = 'translateY(20px)';
            table.style.transition = 'opacity 0.5s ease, transform 0.5s ease';
            
            // Force reflow
            void table.offsetWidth;
            
            table.style.opacity = '1';
            table.style.transform = 'translateY(0)';
          }, index * 150);
        });
      }, 800);
    })
    .catch(error => {
      console.error('Error initializing application:', error);
      showError(`Ett fel uppstod: ${error.message}`);
    });
}

/**
 * Initialize theme toggle functionality
 */
function initThemeToggle() {
  // Add theme toggle button to the header
  const headerButtons = document.querySelector('.buttons');
  const themeButton = document.createElement('button');
  themeButton.className = 'btn theme-toggle';
  
  // Set initial icon based on current theme
  const isDarkMode = document.body.classList.contains('light-mode');
  themeButton.innerHTML = isDarkMode ? 
    '<i class="fas fa-sun fa-fw"></i>' : 
    '<i class="fas fa-moon fa-fw"></i>';
    
  themeButton.setAttribute('title', 'Växla mellan mörkt och ljust läge');
  themeButton.style.minWidth = '44px';
  
  themeButton.addEventListener('click', toggleTheme);
  headerButtons.appendChild(themeButton);
}

/**
 * Toggle between light and dark themes
 */
function toggleTheme() {
  const body = document.body;
  const isDarkMode = body.classList.contains('light-mode');
  
  if (isDarkMode) {
    body.classList.remove('light-mode');
    document.querySelector('.theme-toggle i').className = 'fas fa-moon fa-fw';
    localStorage.setItem('theme', 'dark');
  } else {
    body.classList.add('light-mode');
    document.querySelector('.theme-toggle i').className = 'fas fa-sun fa-fw';
    localStorage.setItem('theme', 'light');
  }
}

/**
 * Show loading animation
 * @param {string} message - Loading message to display
 */
function showLoading(message) {
  const messageEl = document.getElementById('message');
  messageEl.innerHTML = `
    <div class="loading-container">
      <div class="loading"><div></div><div></div><div></div><div></div></div>
      <span>${message}</span>
    </div>
  `;
  messageEl.style.display = 'block';
}

/**
 * Hide loading animation
 */
function hideLoading() {
  const messageEl = document.getElementById('message');
  messageEl.style.opacity = '0';
  messageEl.style.transition = 'opacity 0.3s ease';
  setTimeout(() => {
    messageEl.innerHTML = '';
    messageEl.style.opacity = '1';
    messageEl.style.display = 'none';
  }, 300);
}

/**
 * Show error message
 * @param {string} message - Error message to display
 */
function showError(message) {
  const messageEl = document.getElementById('message');
  messageEl.innerHTML = `
    <div class="error-message">
      <i class="fas fa-exclamation-triangle"></i>
      <span>${message}</span>
    </div>
  `;
  messageEl.style.display = 'block';
}

/**
 * Load saved theme preference from localStorage
 */
function loadSavedTheme() {
  const savedTheme = localStorage.getItem('theme');
  
  if (savedTheme === 'light') {
    document.body.classList.add('light-mode');
  }
} 