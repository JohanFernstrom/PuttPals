/**
 * Round Count Component (Antal rundor)
 */

/**
 * Builds the count table showing number of rounds played by each player
 * @param {Array} dataArray - Processed data array
 */
function buildCountTable(dataArray) {
  if (!dataArray || dataArray.length < 2) return;
  
  const headerRow = dataArray[0];
  const persons = headerRow.slice(1, 6); // Persons are in indices 1 to 5
  
  if (persons.length === 0) return;
  
  // Initialize count tracking
  const counts = {};
  persons.forEach(person => { counts[person] = {}; });
  
  // Initialize course grouping
  const groups = {};
  
  // Count rounds for each person at each course
  for (let i = 1; i < dataArray.length; i++) {
    let row = dataArray[i];
    
    // Skip if not a valid round (fewer than 3 players)
    if (row.slice(1, 6).filter(cell => cell.toString().trim() !== '').length < 3) continue;
    
    // Skip if no course specified
    let rawCourse = row[0].toString().trim();
    if (rawCourse === '') continue;
    
    // Normalize course name
    let key = rawCourse.toLowerCase();
    if (!groups[key]) {
      groups[key] = rawCourse.charAt(0).toUpperCase() + rawCourse.slice(1).toLowerCase();
    }
    
    // Count rounds for each person
    persons.forEach((person, pIndex) => {
      let cellValue = row[1 + pIndex];
      if (cellValue.toString().trim() !== '') {
        if (!counts[person][key]) counts[person][key] = 0;
        counts[person][key]++;
      }
    });
  }
  
  const courses = Object.keys(groups);
  
  // Build the HTML table
  const table = document.createElement('table');
  
  // Create header row
  const trHeader = document.createElement('tr');
  const thName = document.createElement('th');
  thName.textContent = "Namn";
  trHeader.appendChild(thName);
  
  // Add course headers
  courses.forEach(course => {
    const thCourse = document.createElement('th');
    thCourse.textContent = groups[course];
    trHeader.appendChild(thCourse);
  });
  
  // Add total header
  const thTotal = document.createElement('th');
  thTotal.textContent = "Total";
  trHeader.appendChild(thTotal);
  table.appendChild(trHeader);
  
  // Create a row for each player
  persons.forEach(person => {
    const tr = document.createElement('tr');
    
    // Add player name
    const tdName = document.createElement('td');
    tdName.textContent = person;
    tr.appendChild(tdName);
    
    // Count total rounds for this player
    let totalCount = 0;
    
    // Add count for each course
    courses.forEach(course => {
      const td = document.createElement('td');
      let count = counts[person][course] || 0;
      totalCount += count;
      td.textContent = count > 0 ? count : "-";
      tr.appendChild(td);
    });
    
    // Add total count
    const tdTotal = document.createElement('td');
    tdTotal.innerHTML = "<b>" + totalCount + "</b>";
    tr.appendChild(tdTotal);
    
    table.appendChild(tr);
  });
  
  // Render the table
  document.getElementById('countContainer').innerHTML = "";
  document.getElementById('countContainer').appendChild(table);
} 