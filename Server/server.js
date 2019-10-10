var ws = require("nodejs-websocket");

var port = 25565;

//Used for players, creatures and items.
var units = {};

//Used only for players. Also used for checking how many players have joined the game.
var players = {};

var maxConnections = 2;

resetServer();

function resetServer()
{
	units = {};
	players = {};

	//process.stdout.write('\033c');
	console.log("Created server on port " + port + ".");
}

var server = ws.createServer(function (conn) 
{
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
	
    conn.on("text", function (str) 
	{
		var msg = JSON.parse(str);
		
		if (msg._type == undefined)
		{
			//Kick out player when it has reached the player limit.
			if(Object.keys(players).length + 1 > maxConnections)
			{
				conn.close(1000, "Player limit reached.");
				return;
			}

			players[conn.id] = msg;
			
			setPlayerColor(conn.id);

			console.log("Client" + " joined the game. (" + Object.keys(players).length + "/" + maxConnections + ").");

			updateClient(JSON.stringify(players[conn.id]));
		}
		else
		{
			console.log("unit joined " + msg._type);
			
			console.log(msg);
			
			units[msg._guid] = msg;
			
			updateClient(JSON.stringify(msg));
		}
    });
	
    conn.on("close", function (code, reason) 
	{
		if(!players[conn.id])
			return;
		
		delete players[conn.id];
		
		delete players[conn.id];
		
        console.log("Client " + conn.id + " left the game. (" + Object.keys(players).length + "/" + maxConnections + ").");
    });
}).listen(port);