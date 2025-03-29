# ToggleBoys Ligan 2025 - Product Requirements Document

## Overview
ToggleBoys Ligan is a discgolf league tracking system that allows players to record their scores across different courses, calculates points based on performance, and displays various statistics and rankings.

## Target Users
- League players (Johan, Jimmie, Marcus, Sejaf, Andreas)
- League administrators

## Features

### Data Collection
- Players can register new rounds via Google Forms
- Administrators can manually enter data via Google Sheets
- Each round includes: course, scores for each player, and date

### Visualization & Statistics
1. **Ställning (Standings)**
   - Shows total points per player across all courses
   - Displays points earned at each course
   - Sorts players by total points (highest to lowest)

2. **Bästa resultat per bana (Best results per course)**
   - Shows up to 3 best scores per player for each course
   - Displays date, score, and points earned for each result
   - Sorted by points earned then date

3. **Alla tidigare rundor (All previous rounds)**
   - Comprehensive list of all rounds played
   - Includes course, scores for all players, and date

4. **Antal rundor (Number of rounds)**
   - Counts total rounds played by each player
   - Breaks down counts by course

### User Interface Features
1. **Hero Banner**
   - Prominent banner at the top of the content area
   - Displays league name and brief description
   - Responsive height adapts to different screen sizes
   - Overlay with gradient background for improved text readability
   - Serves as a visual identifier for the application
   - Placeholder image with ability to update in the future

2. **Rules Modal**
   - Accessible via "Regler" button in the header
   - Displays league rules and point calculations
   - Provides quick reference for scoring system and player eligibility
   - Responsive design adapts to all screen sizes

3. **Patch Notes Modal**
   - Accessible via "Versionshistorik" link in the footer
   - Displays complete version history in Swedish
   - Shows features, improvements, and bug fixes from all versions
   - Organized by version number and date
   - Allows users to track application evolution over time

4. **Theme Toggle**
   - Allows switching between light and dark themes
   - Preference is saved for future visits

### Technical Requirements
- Responsive design that works on both desktop and mobile devices
- Dark mode UI for better visibility
- Automatic data refresh when loading the page
- Ability to fetch data from Google Sheets

## Future Enhancements
- User authentication
- Ability to add new players
- Photo gallery for each course
- Personal statistics and improvement tracking
- Season-based record keeping 