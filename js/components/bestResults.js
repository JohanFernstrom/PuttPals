/**
 * Best Results Component (Bästa resultat per bana)
 */

/**
 * Builds the summary table showing best results per course
 * @param {Array} dataArray - Processed data array
 */
function buildSummary(dataArray) {
  if (!dataArray || dataArray.length < 2) return;
  
  const headerRow = dataArray[0]; // ["Anläggning", "Johan", "Jimmie", "Marcus", "Sejaf", "Andreas", "Datum"]
  const persons = headerRow.slice(1, 6); // Persons are in indices 1-5
  
  if (persons.length === 0) return;
  
  // Filter for valid rounds (at least 3 players participated)
  const validRows = dataArray.slice(1).filter(row => {
    return row.slice(1, 6).filter(cell => cell.toString().trim() !== '').length >= 3;
  });
  
  // Group rounds by course
  const groups = groupByCourse(validRows);
  const courses = Object.keys(groups);
  
  // For each person and each course, collect their rounds with date information
  const summary = {}; // summary[person][course] = array of { date, score, rawDate }
  
  persons.forEach((person, pIndex) => {
    summary[person] = {};
    
    courses.forEach(course => {
      let rounds = [];
      
      groups[course].rows.forEach(row => {
        let cell = row[1 + pIndex]; // Persons are in indices 1 to 5
        
        if (cell.toString().trim() !== '') {
          let score = parseFloat(cell);
          
          if (!isNaN(score)) {
            let rawDate = row[6]; // "Datum" column
            let formattedDate = formatDate(rawDate);
            let dateObj = rawDate instanceof Date ? rawDate : new Date(rawDate);
            
            rounds.push({ 
              date: formattedDate, 
              score: score, 
              rawDate: dateObj 
            });
          }
        }
      });
      
      // Sort rounds by score (ascending) and take top 3
      rounds.sort((a, b) => a.score - b.score);
      summary[person][course] = rounds.slice(0, 3);
    });
  });
  
  // For each course and each position (0,1,2), assign points
  const summaryPoints = {}; // summaryPoints[course][person] = array of points for each position
  
  courses.forEach(course => {
    summaryPoints[course] = {};
    
    for (let pos = 0; pos < 3; pos++) {
      let posEntries = [];
      
      persons.forEach(person => {
        let rounds = summary[person][course];
        if (rounds && rounds.length > pos) {
          posEntries.push({ 
            person: person, 
            score: rounds[pos].score 
          });
        }
      });
      
      // Calculate points for this position
      const pointsMapping = calculatePointsForPosition(posEntries);
      
      // Store points for each person at this position
      persons.forEach(person => {
        if (!summaryPoints[course][person]) summaryPoints[course][person] = [];
        summaryPoints[course][person][pos] = pointsMapping[person] || 0;
      });
    }
  });
  
  // Attach points to each summary entry
  persons.forEach(person => {
    courses.forEach(course => {
      if (summary[person][course]) {
        summary[person][course].forEach((entry, idx) => {
          entry.pts = summaryPoints[course][person] ? 
            summaryPoints[course][person][idx] || 0 : 0;
        });
        
        // Sort rounds by points (descending) then date (ascending)
        summary[person][course].sort((a, b) => {
          if (b.pts !== a.pts) return b.pts - a.pts;
          return a.rawDate - b.rawDate;
        });
      }
    });
  });
  
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
    thCourse.textContent = groups[course].name;
    trHeader.appendChild(thCourse);
  });
  
  table.appendChild(trHeader);
  
  // Create a row for each player
  persons.forEach(person => {
    const tr = document.createElement('tr');
    
    // Add player name
    const tdName = document.createElement('td');
    tdName.textContent = person;
    tr.appendChild(tdName);
    
    // Add best results for each course
    courses.forEach(course => {
      const td = document.createElement('td');
      const entries = summary[person][course];
      
      if (entries && entries.length > 0) {
        const resultElements = entries.map(e => {
          // Format date
          const dateStr = e.date;
          
          // Format score with points
          let scoreStr = `<b>${e.score}</b>`;
          
          // Format points with appropriate styling
          let ptsStr = '';
          if (e.pts) {
            // Style points based on position
            if (e.pts === 3) {
              ptsStr = ` <span class="position-1">(${e.pts}p)</span>`;
            } else if (e.pts === 2) {
              ptsStr = ` <span class="position-2">(${e.pts}p)</span>`;
            } else if (e.pts === 1) {
              ptsStr = ` <span class="position-3">(${e.pts}p)</span>`;
            } else {
              ptsStr = ` (${e.pts}p)`;
            }
          }
          
          return `${dateStr} - ${scoreStr}${ptsStr}`;
        });
        td.innerHTML = resultElements.join("<br>");
      } else {
        td.innerHTML = '<span style="color: var(--border);">-</span>';
      }
      
      tr.appendChild(td);
    });
    
    table.appendChild(tr);
  });
  
  // Render the table
  document.getElementById('summaryContainer').innerHTML = "";
  document.getElementById('summaryContainer').appendChild(table);
} 