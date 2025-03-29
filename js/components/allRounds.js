/**
 * All Rounds Component (Alla tidigare rundor)
 */

/**
 * Builds the detailed table showing all previously played rounds
 * @param {Array} dataArray - Processed data array
 */
function buildDetailedTable(dataArray) {
  if (!dataArray || dataArray.length === 0) {
    document.getElementById('tableContainer').textContent = "No data to display.";
    return;
  }
  
  const table = document.createElement('table');
  
  // Process each row of data
  dataArray.forEach((row, rowIndex) => {
    const tr = document.createElement('tr');
    
    row.forEach((cellValue, colIndex) => {
      // Create appropriate cell type (th for header row, td for data rows)
      const cell = document.createElement(rowIndex === 0 ? 'th' : 'td');
      
      // Format "Datum" column (header "Datum" is at index 6)
      if (rowIndex !== 0 && dataArray[0][colIndex] === "Datum") {
        cellValue = formatDate(cellValue);
      }
      
      // For score cells (persons are in indices 1 to 5) display in bold
      if (rowIndex !== 0 && colIndex >= 1 && colIndex <= 5) {
        cell.innerHTML = cellValue.toString().trim() === '' ? 
          '<b>-</b>' : '<b>' + cellValue + '</b>';
      } else {
        cell.innerHTML = cellValue.toString().trim() === '' ? '<b>-</b>' : cellValue;
      }
      
      tr.appendChild(cell);
    });
    
    table.appendChild(tr);
  });
  
  // Render the table
  document.getElementById('tableContainer').innerHTML = "";
  document.getElementById('tableContainer').appendChild(table);
} 