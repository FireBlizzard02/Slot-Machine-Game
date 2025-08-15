# Unity Slot Machine Game

A simple yet fun **Slot Machine** game built in Unity with smooth reel animations, sound effects, and interactive lever controls.


## Play the Game
  **[Play on GitHub Pages](https://fireblizzard02.github.io/Slot-Machine-Game/)**


## Game Overview
This is a slot machine simulation where you pull a lever to spin reels, complete with:
- Smooth reel spin animations
- Realistic lever handle mechanic
- Sound effects for spins and results
- Simple win/lose detection

## Bonus Features

 - Realistic lever animation linked to reel spins.
 - Audio cues for improved feedback.
 - Fully responsive WebGL build for desktop browsers.

## Development Approach
1. **Core Mechanics** – Built spinning reels, random symbol selection, and win detection.  
2. **UI & Animation Integration** – Added handle animations and smooth reel transitions.  
3. **Audio & Feedback** – Integrated spin/start/stop audio for better immersion.  
4. **WebGL Optimization** – Tweaked settings for smooth browser play and organized assets into structured folders.

## Run WebGL Build Locally (Python Method)

   Unity WebGL builds cannot be run by double-clicking the `index.html` file due to browser security restrictions.  
   You must serve the build files via a local web server.
   
     Steps:
   
   1. Download or Clone this Repository
      ```bash
      git clone https://github.com/YourUsername/YourRepoName.git
   
   2. Navigate to the WebGL Build Folder
   Replace Build/WebGL with the actual path to your build folder.
   
     cd Build/WebGL
   
   3. Start a Local Web Server (Python 3 Required)
   
     python -m http.server 8080
   
   4. Open the Game in Your Browser

     Visit:
     http://localhost:8080
   5. Stop the Server
     Press CTRL + C in the terminal to stop serving.

