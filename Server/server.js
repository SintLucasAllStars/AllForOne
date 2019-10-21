var ws = require("nodejs-websocket");

var port = 25565;

//Used for players, creatures and items.
var units = {};

//Used only for players. Also used for checking how many players have joined the game.
var players = {};

var turn = 0;

var maxConnections = 2;

console.log("Created server on port " + port + ".");

var server = ws.createServer(function (conn) 
{
	resetServer();

	function resetServer()
	{
		units = {};
		players = {};
		turn = 0;

		//process.stdout.write('\033c');
		//console.log("Reset server.");
		//console.log("First turn: " + getPlayerSide(turn) + ".");
	}
	
	//Simple function that checks what skins have not been picked yet. No double skins.
	function setPlayerColor(id)
	{
		if(Object.keys(players).length == 1)
			players[id]._playerSide = 0;
		else
			players[id]._playerSide = 1;
	}
	
	//Sends the message to all connected players.
	function updateClient(msg) 
	{
		server.connections.forEach(function (c) 
		{
			c.sendText(msg);
		});				
	}
	
	function getPlayerSide(playerSide)
	{
		if(playerSide == 0)
			return "Red";
		else
			return "Blu";
	}
	
    conn.on("text", function (str) 
	{
		var msg = JSON.parse(str);
		
		if (msg._type == "Player")
		{
			conn.id = msg._guid;
			
			console.log(conn.id);

			for (var index in units)
				conn.sendText(JSON.stringify(units[index]));

			players[conn.id] = msg;
			
			setPlayerColor(conn.id);

			console.log("Client " conn.id + " joined the game. (" + Object.keys(players).length + "/" + maxConnections + ").");

			updateClient(JSON.stringify(players[conn.id]));
			
			return;
		}
		else if (msg._type == "Turn")
		{
			console.log("Next turn: " + getPlayerSide(msg._playerSide) + ".");
		}
		else if(msg._type)
		{
			console.log("Unit moved: " + msg._type + ".");
		}
			
		units[msg._guid] = msg;
			
		updateClient(JSON.stringify(msg));
    });
	
    conn.on("close", function (code, reason) 
	{
		if(!players[conn.id])
			return;
		
		delete players[conn.id];
		
		delete players[conn.id];
		
        console.log("Client " + conn.id + " left the game. (" + Object.keys(players).length + "/" + maxConnections + ").");
		
		resetServer();
    });
}).listen(port);