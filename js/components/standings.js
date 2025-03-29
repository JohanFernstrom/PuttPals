/**
 * Standings Component (Ställning)
 */

/**
 * Builds the standings table showing total points per player across all courses
 * @param {Array} dataArray - Processed data array
 */
function buildStallning(dataArray) {
  if (!dataArray || dataArray.length < 2) return;
  
  const headerRow = dataArray[0]; // ["Anläggning", "Johan", "Jimmie", "Marcus", "Sejaf", "Andreas", "Datum"]
  const persons = headerRow.slice(1, 6); // Persons are in indices 1 to 5
  
  if (persons.length === 0) return;
  
  // Filter for valid rounds (at least 3 players participated)
  const validRounds = dataArray.slice(1).filter(row => {
    return row.slice(1, 6).filter(cell => cell.toString().trim() !== '').length >= 3;
  });
  
  // Group rounds by course
  const groups = groupByCourse(validRounds);
  const courses = Object.keys(groups);
  
  // For each person and each course, collect available scores (sorted ascending) – up to 3
  const personCourseScores = {};
  persons.forEach((person, idx) => {
    personCourseScores[person] = {};
    courses.forEach(course => {
      let scores = [];
      groups[course].rows.forEach(row => {
        let cell = row[1 + idx]; // Persons are in indices 1 to 5
        if (cell.toString().trim() !== '') {
          let score = parseFloat(cell);
          if (!isNaN(score)) {
            scores.push(score);
          }
        }
      });
      scores.sort((a, b) => a - b);
      personCourseScores[person][course] = scores.slice(0, 3);
    });
  });
  
  // For each course and for each position (0,1,2), rank persons and assign points
  const coursePoints = {};
  courses.forEach(course => {
    coursePoints[course] = {};
    
    for (let pos = 0; pos < 3; pos++) {
      let posEntries = [];
      persons.forEach(person => {
        let scores = personCourseScores[person][course];
        if (scores && scores.length > pos) {
          posEntries.push({ person: person, score: scores[pos] });
        }
      });
      
      // Calculate points for this position
      const pointsMapping = calculatePointsForPosition(posEntries);
      
      // Add points to total for each person
      persons.forEach(person => {
        if (!coursePoints[course][person]) coursePoints[course][person] = 0;
        if (pointsMapping[person]) {
          coursePoints[course][person] += pointsMapping[person];
        }
      });
    }
  });
  
  // Sum total points per person across courses
  const totalPoints = {};
  persons.forEach(person => {
    totalPoints[person] = 0;
    courses.forEach(course => {
      totalPoints[person] += (coursePoints[course][person] || 0);
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
  
  // Add total header
  const thTotal = document.createElement('th');
  thTotal.textContent = "Total";
  trHeader.appendChild(thTotal);
  table.appendChild(trHeader);
  
  // Sort players by total points (highest to lowest)
  let rows = persons.map(person => ({ person: person, total: totalPoints[person] }));
  rows.sort((a, b) => b.total - a.total);
  
  // Create a row for each player
  rows.forEach(row => {
    const tr = document.createElement('tr');
    
    // Add player name
    const tdName = document.createElement('td');
    tdName.textContent = row.person;
    tr.appendChild(tdName);
    
    // Add points per course
    courses.forEach(course => {
      const td = document.createElement('td');
      let pts = coursePoints[course][row.person] || 0;
      td.innerHTML = pts > 0 ? "<b>" + pts + "</b>" : '<b>-</b>';
      tr.appendChild(td);
    });
    
    // Add total points
    const tdTotal = document.createElement('td');
    tdTotal.innerHTML = "<b>" + row.total + "</b>";
    tr.appendChild(tdTotal);
    
    table.appendChild(tr);
  });
  
  // Render the table
  document.getElementById('stallningContainer').innerHTML = "";
  document.getElementById('stallningContainer').appendChild(table);
} 