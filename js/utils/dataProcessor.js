/**
 * Utility functions for processing data from Google Sheets
 */

/**
 * Fetches Excel file from Google Sheets and processes the data
 * @param {string} url - URL to the Google Sheet export
 * @returns {Promise<Array>} - Promise resolving to the processed data array
 */
function fetchExcelFile(url) {
  document.getElementById('message').textContent = 'Loading Excel file...';
  return fetch(url)
    .then(response => {
      if (!response.ok) {
        throw new Error("Network response was not ok: " + response.statusText);
      }
      return response.arrayBuffer();
    })
    .then(data => {
      const workbook = XLSX.read(data, { type: 'array', cellDates: true });
      const firstSheetName = workbook.SheetNames[0];
      const worksheet = workbook.Sheets[firstSheetName];
      let dataArray = XLSX.utils.sheet_to_json(worksheet, { header: 1, defval: '' });
      
      // Process the data
      return cleanDataArray(dataArray);
    })
    .catch(error => {
      console.error("Error fetching or processing the Excel file:", error);
      document.getElementById('message').textContent = "Error: " + error.message;
      throw error;
    });
}

/**
 * Cleans and processes the raw data array from Google Sheets
 * @param {Array} dataArray - Raw data array from Google Sheets
 * @returns {Array} - Processed data array
 */
function cleanDataArray(dataArray) {
  console.log("Original Data Array:", dataArray);
  
  // Remove rows that are completely empty (except header)
  dataArray = dataArray.filter((row, i) => i === 0 || row.some(cell => cell.toString().trim() !== ''));
  
  // Remove the first column ("Tidstämpel") from every row
  dataArray = dataArray.map(row => row.slice(1));
  
  // Now, header becomes: [ "Anläggning", "Johan", "Jimmie", "Marcus", "Sejaf", "Andreas", "Datum" ]
  console.log("Adjusted Data Array:", dataArray);
  
  return dataArray;
}

/**
 * Groups data rows by course/venue (case-insensitive)
 * @param {Array} rows - Array of data rows to group
 * @returns {Object} - Object with course names as keys and arrays of rows as values
 */
function groupByCourse(rows) {
  const groups = {};
  
  rows.forEach(row => {
    let rawCourse = row[0].toString().trim();
    let key = rawCourse.toLowerCase();
    
    if (!groups[key]) {
      groups[key] = { 
        name: rawCourse.charAt(0).toUpperCase() + rawCourse.slice(1).toLowerCase(), 
        rows: [] 
      };
    }
    
    groups[key].rows.push(row);
  });
  
  return groups;
}

/**
 * Calculates points based on ranked positions
 * @param {Array} entries - Array of entries to rank
 * @returns {Object} - Object mapping person names to points
 */
function calculatePointsForPosition(entries) {
  entries.sort((a, b) => a.score - b.score);
  
  const pointsMapping = {};
  let uniqueRank = 1;
  let i = 0;
  
  while (i < entries.length) {
    let start = i;
    
    // Find all entries with the same score (ties)
    while (i < entries.length && entries[i].score === entries[start].score) {
      i++;
    }
    
    // Assign points based on rank
    let pts = 0;
    if (uniqueRank === 1) pts = 3;
    else if (uniqueRank === 2) pts = 2;
    else if (uniqueRank === 3) pts = 1;
    else pts = 0;
    
    // Apply points to all tied players
    for (let j = start; j < i; j++) {
      let person = entries[j].person;
      if (!pointsMapping[person]) pointsMapping[person] = 0;
      pointsMapping[person] += pts;
    }
    
    uniqueRank++;
  }
  
  return pointsMapping;
}

/**
 * Formats a date value consistently
 * @param {Date|string|number} dateValue - Date value to format
 * @returns {string} - Formatted date string
 */
function formatDate(dateValue) {
  if (dateValue instanceof Date) {
    return dateValue.toLocaleDateString('sv-SE', { day: '2-digit', month: 'short' });
  } else if (typeof dateValue === 'number') {
    return XLSX.SSF.format("dd-mmm", dateValue);
  } else {
    const dateObj = new Date(dateValue);
    if (!isNaN(dateObj)) {
      return dateObj.toLocaleDateString('sv-SE', { day: '2-digit', month: 'short' });
    }
    return dateValue.toString();
  }
} 