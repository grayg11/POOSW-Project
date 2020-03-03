
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


## In Game Combat

![Combat](https://github.com/grayg11/POOSW-Project/blob/master/Design/combat.PNG)


## Supporting User Stories

| ID | User Story | Related Component |
|----|------------|-------------------|
| 000 | As a User I want to be able to link my account with Spotify so that I can export playlists. | User Dash - Link Spotify |
| 001 | As a User I want to have a private account so that I can use the product securely | Home Page - Sign Up / User Dash - Account Info |
| 002 | As a User I want to be able to link my account with Apple Music so that I can export playlists. | User Dash - Link Apple Music |
| 003 | As a user I want to be able to browse my playlists so that I can select them for export. | User Dash - Poplated List of selected playlists |
| 004 | As a user I want to see my history so that I know what playlists have been transferred and to where. | User Dash - Explore Playlists |
| 005 | As a user I want to connect with other Playlist Authority users so that I can send them playlists | User Dash - Friends |
| 006 | As a user I want to be able to log on to the site so that I can use it | Home Page - Log In |
| 007 | As a user I want to be able to log off from the site so that I know I have safely disconnected | User Dash - Log Off |
| 008 | As a user I want to be able to play music from the website | -Not Implemented- |
| 009 | As a user I want the ability to create a new playlist on the site so that I don't have to log off to create a new playlist| User Dash - Create New Playlist / Playlist Page |
| 010 | As a user I want the ability to edit playlists on the site so that I don't have to log off to edit playlists as I'm getting ready to send them| User Dash - Edit / Playlist Page |
| 011 | As a user I want the ability to delete my Playlist Authority account so that I can have peace of mind if I change my mind about using the service| User Dash - Account Info |
| 012 | As a user I want to be able to access the site with a url so that I can access the site| Home Page |
| 015 | As a User I want to be able to link my account with Spotify so that I can import playlists. | User Dash - Link Spotify |
| 016 | As a User I want to be able to link my account with Apple Music so that I can import playlists. | User Dash - Apple Music |
| 017 | As a user I want to be able to browse my playlists so that I can select them for import. | User Dash - Spotify/Apple Music Playlists and Poplated List of selected playlists | 

# Resource Management

All resource management will be controlled by AWS

# Security
* We Plan to enforce a Strong Password Policy. (min 10 characters and 1 non alpha)
* We plan to encrypt our Login Page with SSL encryption.
* We plan to use a Secure Host and will back up our data to a secure AWS EC2 server.

# Performance

Since we are using AWS EC2 we do not forsee performance issues.

# Scalability

We may decied to include other external music sites in our product, which would only requires minor additions, otherwise the scalabiility of our product will directly depend on the limits of AWS EC2 free tier.

# Interoperability

Our product will use individual API connections for each external service, per account.

# Internationalization/Localization

This product is made for use of English speaking users only, with a North American target base.

# Input/Output

The majority of IO handled will be from the user, database, or external sites via API
- Inputs:
  - Mouse
  - Keyboard
  - API's
 - Outputs
 
 
# Error Processing
- Front End Error:
  * If an error occurs due to user input, then we will catch it, revert the user inputs, and then display a pop-up explaining what the user did wrong. For instance, if a user inputs an incorrect password, the website will refresh the inout window for the password and then inform the user to enter a new password.
- Back End Error
  * If an error occurs due to faulty code or by other hidden proccesses, then we will halt the server processes and display a blank page with an error code and basic description of the error.
  
# Fault Tolerance

When encountering User IO faults, Playlist authority will simply deny the request and prompt the ser to try again.
For external API faults, PA will display the error to the user and information about it
For any system faults, the server will shut down and/or restart.

# Architectural Feasibility

We should encounter minimal problems in this area due to designing around AWS EC2 free tier specs.

# Overengineering

Since our product is a web application we are designing to deliver at the most basic level. We will provide the service in the most basic way that works, which means any future problems that deny service to the user will be dealt with when encountered.

# Build-vs-Buy Decisions

- Spotify API
- Apple Music API
- Amazon Web Service
- other third party dependencies

# Reuse

The use of a MERN stack architecture will be used for product implementation.
Playlist Authority will not use any pre-exsisting software, test cases, etc.

# Change Strategy

We will implement an iterative approach to design using AGILE. Weekly Sprints with precise objectives will help integrate changes along the way.
