
# Program Organization

![Context Diagram](https://github.com/grayg11/POOSW-Project/blob/master/Design/Context%20Diagram%20(1).png)

A user will access Imperial Assault from an executable file. Once the executable is run, the game will load into the Main Menu Screen.

User Story: 3	As a user I want a main menu screen so I can navigate through my options	Acceptance Criteria: Given I am a user, when the game starts, then there is a main menu screen loaded

![Container Diagram](https://github.com/grayg11/POOSW-Project/blob/master/Design/Container%20Diagram.png)

A user will access Imperial Assault from an executable file. Once the executable is run, the game will load into the Main Menu Screen. 
The main menu will have options for selecting game modes, loading game saves, rules, and exit game option. Once the user has selected a game mode the character selection scene is loaded. Here the user gets to choose a part of 4 charatcers. Once all party members are chosen, according to what was selected in the main menu, the appropriate game level will be loaded.

User Stories: 3, 4, 9, 10, 12.


![Component Diagram](https://github.com/grayg11/POOSW-Project/blob/master/Design/Component%20Diagram.png)

The main components of each level are borken down into a Game State Controller (FSM), Camera controller, and a UI controller. The camer and UI controllers provide all player interaction with the game, while the game state controller provides the level creation and holds all states for the game loop. The game loop will continue until eithe the win or lose state is called. From both the win or lose state the user is directed back to the main menu screen.

User Stories: 1, 2, 3, 4, 9, 12, 13, 16

# Major Classes

![Class Diagram](https://github.com/grayg11/POOSW-Project/blob/master/Design/Class%20Diagram.png)

The State Machine class has a method to change a state, each state is an abstract class which game state class uses. The GameStateController implements state machine and holds references to all other main classes. The camera controller has methods to read in user input to change the camera position. The UI controller holds methods for each interactable button in the scene. The Level Gen has methods that create a map visually, and in data to be used in pathfinding. It also spawns the playable and non playable characters. Each character has a unit script that holds all statistical data for itself and also has a method to pathfind for movement.

User Stories: 1, 2, 4, 5, 6, 7, 8, 11, 12, 14, 16

# Data Design

All data will be stored internally in a Data Manager script This script holds all level unique information. All in game information is stored in the GameStateController, Data MAnager, or Unit scripts.

# User Interface Design

## Main Menu

 ![Main Menu](https://github.com/grayg11/POOSW-Project/blob/master/Design/main%20menu.PNG)

The purpose of the main menu is to allow users the options of selecting a game mode, loading a game save, reading the rules, and exiting the game. The Game Mode button leads to another UI called **Game Modes**

## Game Modes

![Game Modes](https://github.com/grayg11/POOSW-Project/blob/master/Design/game%20modes.PNG)

The Game Modes UI lists the types of games available to select from. These are Skirmish, Campaign, and Raid, each with their own button that leads to a unique **Mode Selection Screen**

## Mode Selection Screen

![Campaign](https://github.com/grayg11/POOSW-Project/blob/master/Design/campaign%20screen.PNG)

The Mode Selection Screen will have a description of the game mode, individual levels of each game mode, mode difficulty buttons and a start button that loads the **Character Select Scene**

## Character Select Scene

![Char select](https://github.com/grayg11/POOSW-Project/blob/master/Design/char%20select.PNG)

The Character Select Scene has button to scroll through all the playable characters as well as a visual to who has already been selected. When you select a character you will be prompted to confirm or go back. Once the party is full the level will load.

## In Game Main

![Player Main](https://github.com/grayg11/POOSW-Project/blob/master/Design/player%20main.PNG)

The in game menu will show the active players name alon with all their stats. There is also a button for each type of action. Move will generate possible move spaces to select from, attack will allow the player to select an enemy to fight and loads the **Combat UI**, use ability opens a menu that lists possible abilities, and rest will heal the player.

## In Game Combat

![Combat](https://github.com/grayg11/POOSW-Project/blob/master/Design/combat.PNG)

The Combat UI has places for both attacker and defender, with associated stats. The UI shows what dice are rolled and has a few texts explaining the attack. There is a modify button if a player can modify the dice roll, and a confirm button that ends the attack and applies all damage, returning to the previous players main state.


## Supporting User Stories

| ID | User Story | Related Component |
|----|------------|-------------------|
| 1 | As a user I want cantrols so I can interact with the game | All buttons |
| 2 | As a user I want a camera so I can see the game | In Game Main |
| 3 | As a user I want a main menu screen so I can navigate through my options. | Main Menu |
| 4 | As a user I want multiple characters to play with so I have more options available to me | Character Selection |
| 5 | As a user I want a character to be able to move | In Game Move Button |
| 6 | As a user I want a character to be able to fight | In Game Attack Button, Combat UI |
| 7 | As a user I want a character to be able to heal | In Game Rest Button |
| 8 | As a user I want a character to have special abilities | In Game Use Ability Button |
| 9 | As a user I want a character selection screen so I can choose the characters I want to play | Character Selection |
| 10 | As a user I want multiple difficulty levels so I can adjust the game to my skill level | Mode Selection Screen |
| 12 | As a user I want multiple levels to play on | Mode Selection Screen |
| 13 | As a user I want a UI so I can interpret the gameplay | All Screens |
| 14 | As a user I want player stats so I know my health, etc. | In Game UI and Combat UI |
| 15 | As a user I want dice-based combat | Combat UI |


# Resource Management

All resource management will be controlled through Unity and GitHub

# Security

As SWIA will be distributed by an executable file, and with no user accounts, we will not be implementing any security outside of what Unity already provides.

# Performance

Users should not encounter any performance issues if they are using a computer made in the last decade.

# Scalability

The scalability in this game is only limited by our imagination. There will always be room for more players, enemies, weapons, maps, etc. With the use of state macines and classes for each important game aspect, future integration should be relatively easy.

# Interoperability

This game will not interact with anything outside of the executable file.

# Internationalization/Localization

This product is made for use of English speaking users only, with a North American target base.

# Input/Output

The majority of IO handled will be from the user, database, or external sites via API
- Inputs:
  - Mouse
  - Keyboard
 - Outputs
  - Error Logging via Unity
 
# Error Processing
- All errors after Build will be dealth with via the Unity Crash Handler.
  
# Fault Tolerance

Any known faults during the development phase will be corrected during testing and not published to the repo until it is fixed.

# Architectural Feasibility

We should encounter minimal problems in this area due to the small footprint the game has. Each level will be its own container and not be able to affect anything outside of it.

# Overengineering

Over engineering can become a problem in the future due to the many game expansions for Imperial Assault the board game, but we plan on only implementing the core box gameplay before considering any expansions.

# Build-vs-Buy Decisions

- Unity 3D for implementation
- Unity Asset store for audio assets
- SW Imperial Assault the board game, and Tabltop Simulator for other assets.

# Reuse

The only thing being reused in this project is the Star Wars IP and the rules and likeness of the board game.

# Change Strategy

We will implement an iterative approach to design using AGILE. Weekly Sprints with precise objectives will help integrate changes along the way.
