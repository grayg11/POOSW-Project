
# Program Organization

![System Context](https://github.com/Joseph-Kocis/Playlist-Authority/blob/master/Artifacts/Data%20Design/System%20Context.PNG)

![Context Diagram](https://github.com/grayg11/POOSW-Project/blob/master/Design/Context%20Diagram%20(1).png)

A user will access Playlist authority via their web browser. Playlist Authority will allow the user to connect to Spotify and Apple music in order to import and export playlists, as well as a database to store all user information, history, etc.

![Container Diagram](https://github.com/Joseph-Kocis/Playlist-Authority/blob/master/Artifacts/Data%20Design/Container%20Diagram.jpg)

A User access our web application when they visit PlaylistAuthority.org. The web application returns to the users browser, the single page application, which has all the functionality of the services provided. The single page app makes API calls to the internal API application which houses the import/export functionalities. The API application can then read/write to the database and access Spotify and Apple music.

![Component Diagram](https://github.com/Joseph-Kocis/Playlist-Authority/blob/master/Artifacts/Data%20Design/Component%20Diagram.jpg)

The main components of the Signle page application are the Sign in controller and the account controller. The sign in controller ses a security component to safely access the user credentials and database. The Account controller uses facades for both Spotify and Apple music in order to use their funtionalities.

![Sequence Diagram](https://docs.google.com/drawings/d/1NEmfRJ-Wo-9ial2MltMwgyQim834q1XObPhoLf7_sVQ/edit?usp=sharing)

![Event Storming](https://github.com/Joseph-Kocis/Playlist-Authority/blob/master/Artifacts/Data%20Design/Event%20Storming%20Drawing.png)

# Major Classes

![Class Diagram](https://github.com/Joseph-Kocis/Playlist-Authority/blob/master/Artifacts/Data%20Design/Class%20Diagram.png)

The User class, will contain all account handling like registration, deletion, log in/out, and log in credentials. The playlist class will be able to add songs, remove songs, create new playlists, and delete playlists. Each API class will have a link, import and export method.

# Data Design

![Data ERD](https://github.com/Joseph-Kocis/Playlist-Authority/blob/master/Artifacts/Data%20Design/Database%20ERD.png)

The data Playlist Authority maintains is divided into four sub-categories for each User account. These categories are **Account Info**, **Friends**, **History**, and **Playlist**. 

### Account Info
The Account Info is accessed by a unique User ID. It will store all user specific information such as the User's name, email, and password. Any streaming service keys that the User has linked are also stored here, as well as a pointer to the User's friend list. With this pointer we can access the Friends sub category

### Friends
The Friends data will simply store and list each Friend User ID and Name. The Friend User ID is also accessed by the Shared History.

### History
All data stored in History is sub-catergorized by a type; either Exported, Shared, or Saved. This stores pointers to each sub-catergory. Each of these types hold a Playlist ID that can be used to get any specific playlist data stored in the database.

* #### Export History
Export History stores the Playlist ID, which service the playlist was exported to and the date time group.

* #### Shared History
Shared History stores the Playlist ID, a boolean for "Shared To" or "Shared From", the Friend User ID, and the date time group.

* #### Saved History
Saved History stores the Playlist ID and the date time group.

### Saved Playlist
Saved Playlist is accessed by a Playlist ID and stores the playlist name, the number of songs and a pointer to a list of songs associated to that playlist.

* #### Playlist Songs
Playlist Songs stores each Song ID and Song Name for the corresponding playlist.
 
 # Business Rules

* Application must be accessable to all internet users.
* Users accounts must be secure.
* Application must keep a history of all user interations.

# User Interface Design

## Home Page

 ![Home Page](https://github.com/Joseph-Kocis/Playlist-Authority/blob/master/Artifacts/Data%20Design/User%20Interface/HomePageUI.PNG)

The purpose of Playlist Authority's Home Page is to allow users to log in or register to use our services. There is also an *About Us* section that will have a detailed description of the services we provide. The user can interact with the home page by either entering their existing login credentials and selecting *Log In*, which will direct them to the **User Dash** page or by choosing the "Sign Up" button, which will direct them to a generic site registration page.
## User Dash

![User Dash](https://github.com/Joseph-Kocis/Playlist-Authority/blob/master/Artifacts/Data%20Design/User%20Interface/UserDashUI.PNG)

The User Dash is a page that has an organized template where the user has access to the majority of Playlist Authority's services. On the left of the page, with exception to *Create New Playlist*, are buttons that will populate a list of corresponding playlists in the shaded area on the right. When the shaded area has a list of playlists the user can select one and choose *Export*, which will prompt them which streaming service to export the selected playlist to, or the user can *Edit* the playlist, which directs them to the **Playlist Page**. The *Create New Playlist* option on the left also leads to the **Playlist Page**. In the top right of the User Dash are user account options where the user can edit their profile information and friends, link their streaming service accounts, and log out of their account. *Log Out* will direct the user back to the **Home Page**. *Link Service* buttons will generate pop-up windows with that services log in screen.
## Playlist Page

![User Dash](https://github.com/Joseph-Kocis/Playlist-Authority/blob/master/Artifacts/Data%20Design/User%20Interface/PlaylistPageUI.PNG)

The Playlist Page allows users to view or edit a playlist. The shaded middle of the page will show a list of the chosen playlist's songs, or it will be empty if the user was directed here from *Create New Playlist*. The User can edit a playlist by either *Add* or *Remove* song options. When the User is done editing the playlist they choose from the handling options at the bottom of the page. The User can *Save* the playlist to our internal database, *Delete* the playlist from our internal database or its associated streaming service, or *Export* the playlist to a streaming service. 

## Export Popup

![Export Popup](https://github.com/Joseph-Kocis/Playlist-Authority/blob/master/Artifacts/Data%20Design/User%20Interface/ExportPageUI.PNG)

The purpose of the Export Popup is to give the User the option to name or rename a playlist and to confirm the export to the streaming service. The *Cancel* option closes the popup and takes the User back to the page they are on, and the *Export* option indicates the playlist has been exported and redirects the User to the **User Dash**.


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
