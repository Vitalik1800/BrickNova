# BrickNova v1.0

## 1. About

BrickNova is a classic brick-breaker game developed for Windows.

The objective of the game is to destroy all bricks, earn points, complete levels, and reach the final level.

## 2. System Requirements

* Windows
* .NET 8 compatible environment
* Keyboard
* Display capable of running a Windows Forms application

## 3. How to Start

1. Extract the BrickNova release package.
2. Open the `BrickNova` folder.
3. Run:

```text
BrickNova.exe
```

The game initializes its local SQLite database automatically when required.

## 4. Main Menu

The main menu provides access to the available game functions.

Available options include:

* New Game
* Continue
* High Scores
* Help
* About
* Settings
* Reset Progress
* Exit

## 5. Controls

| Key       | Action              |
| --------- | ------------------- |
| `A` / `←` | Move paddle left    |
| `D` / `→` | Move paddle right   |
| `Space`   | Start / Resume      |
| `Esc`     | Pause               |
| `N`       | New Game            |
| `C`       | Continue            |
| `R`       | Restart             |
| `M`       | Return to Main Menu |

## 6. Gameplay

The player controls the paddle and must prevent the ball from leaving the play area.

The ball destroys bricks when collisions occur.

Destroyed bricks award points.

Completing all bricks in a level advances the game to the next level.

## 7. Lives

The player starts a new game with three lives.

When the ball is lost:

* one life is removed;
* the ball is reset;
* the paddle is reset;
* the game continues if lives remain.

When all lives are lost, the game enters the Game Over state.

## 8. Levels

BrickNova contains multiple levels.

Completing the current level loads the next level automatically.

Completing the final level results in the Victory state.

## 9. Score

The player's score increases when bricks are destroyed.

Completed scores can be saved to the High Scores database.

## 10. Continue

BrickNova can save the current game progress.

Saved progress includes:

* Current level
* Score
* Remaining lives
* Update timestamp

Select `Continue` from the main menu to restore available saved progress.

If no valid saved progress exists, Continue cannot start a saved game.

## 11. Settings

The Settings section contains application audio configuration.

Available audio settings include:

* Sound Enabled
* Master Volume

The settings are stored locally and restored when the application starts.

## 12. Audio

BrickNova provides sound effects for gameplay and game-state events.

Audio may be configured through the Settings menu.

The available audio controls include enabling/disabling sound and changing the master volume.

## 13. High Scores

Completed scores can be stored locally.

The High Scores section displays saved score records.

## 14. Database

BrickNova uses SQLite for local application data.

The database stores information such as:

* Game progress
* High Scores
* Settings

The application initializes the required database structure automatically.

## 15. Release Files

A standard BrickNova release package contains:

```text
BrickNova/
├── BrickNova.exe
├── *.dll
└── README.txt
```

The SQLite database may be created automatically by the application on first launch.

## 16. Troubleshooting

### The game does not start

Make sure the application is being run on a supported Windows environment and that all files from the release package remain together.

### Continue is unavailable

Continue requires valid saved game progress.

If no saved progress exists, start a New Game.

### Sound does not play

Open Settings and verify that:

```text
Sound Enabled = On
```

Also check the Master Volume setting.

### Game progress was reset

Game progress is stored in the local SQLite database. Removing or replacing the database can remove locally stored progress.

## 🎮 Gameplay Demo

Watch the BrickNova v1.0 gameplay demonstration.

📸 **Screenshots:** [screenshots/](https://github.com/Vitalik1800/BrickNova/tree/main/screenshoots)

🎥 **Video:** [video/](https://github.com/Vitalik1800/BrickNova/tree/main/video)

## 17. Version

**BrickNova v1.0**

Release type:

**Stable Release**

## 18. Credits

BrickNova was developed as a standalone Windows game using C# and .NET 8.
