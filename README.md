# MultiPlayer-Tic-Tac-Game-Using-Socket-Programming

###	Introduction 
In this project we have implemented a multiplayer tic tac toe game using socket programming in C# in which a server creates the game in local area network. The players in LAN can connect to the server by using the IP address of the server. We have used the concept of thread. When a new client connection arrives to server, new thread will be created and user will be able to play against the opponent player. Rules of the game: One by one the players have to click on buttons, ‘O’ or ‘X’. A player can win the game when either of the diagonals have the same key i.e., ‘O’ or ‘X’ or any of the rows or the columns have the same key otherwise the game will result in a draw. User can also play as a single player against an undefeatable AI.
###	Scope and Modules
#### Scope:
-	User can play as single player against AI
-	User can play as multiplayer against opponents through LAN
-	User can connect to server by entering their name
-	User can choose from list of available players to play with.
-	Users can chat with each other through chat system
-	Users can view their scores
#### Modules:
1. <b>Client-side modules</b>
   - MainWindow
   - MultiPlayer
   - SinglePlayer
2. <b>Server-side modules</b>
   - MainWindow
