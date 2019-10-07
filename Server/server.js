var ws = require("nodejs-websocket");

var port = 25565;

//Used for players, creatures and items.
var objects = {};

//Used only for players. Also used for checking how many players have joined the game.
var players = {};

var maxConnections = 4;

var hasMasterClient = false;

resetServer();

function resetServer()
{
	objects = {};
	players = {};

	hasMasterClient = false;

	//process.stdout.write('\033c');
	console.log("Created server on port " + port + ".");
}

var server = ws.createServer(function (conn) 
{
	//Simple function that checks what skins have not been picked yet. No double skins.
	function setPlayerSkin()
	{
		var playerSkins = ["Player_01", "Player_02", "Player_03", "Player_04"];		
		
		for (var index in players) 
		{
			for (var i in playerSkins) 
			{
				if (players[index].GameData.Type == playerSkins[i]) 
				{
					playerSkins.splice(i, 1); 		
				}					
			}
		}
		return playerSkins[0];
	}
	
	//Sends the message to all connected players.
	function updateClient(msg) 
	{
		server.connections.forEach(function (c) 
		{
			c.sendText(JSON.stringify(objects[msg.GameData.Guid]));
		});				
	}
	
	function getRandomInt(max) 
	{
		return Math.floor(Math.random() * Math.floor(max));
	}
	
	//Function to make player master client.
	function makeMasterClient(msg)
	{
		msg.GameData.IsMasterClient = true;
		hasMasterClient = true;
		console.log("Made client " + msg.GameData.PlayerName + " master client.");
		return msg;		
	}
	
    conn.on("text", function (str) 
	{
		var msg = JSON.parse(str);

		if (msg.GameData.Type == "Player")
		{
			//Kick out player when it has reached the player limit.
			if(Object.keys(players).length + 1 > maxConnections)
			{
				conn.close(1000, "Player limit reached.");
				return;
			}
			
			conn.id = msg.GameData.Guid;

			//MasterClient detection. Player is masterclient to make monsters walk and spawn new objects, rather than letting every player handle it.
			if(!hasMasterClient)
			{
				msg = makeMasterClient(msg);
			}
				
			msg.GameData.Type = setPlayerSkin();
				
			for (var index in objects)
				conn.sendText(JSON.stringify(objects[index]));

			objects[conn.id] = msg;
			players[conn.id] = msg;
				
			console.log("Client " + msg.GameData.PlayerName + " joined the game. (" + Object.keys(players).length + "/" + maxConnections + ").");

			updateClient(msg);		
		}
		else if(msg.GameData.Type != undefined)
		{
			objects[msg.GameData.Guid] = msg;	
			
			updateClient(msg);
		}
		else
			conn.close();
    });
	
    conn.on("close", function (code, reason) 
	{
		if(!players[conn.id])
			return;
		
		delete players[conn.id];
		
		objects[conn.id].GameData.IsConnected = false;
		objects[conn.id].GameData.IsAlive = false;
		
		//Giving MasterClient to a different player if the player who left was MasterClient.
		if(objects[conn.id].GameData.IsMasterClient && Object.keys(players).length > 0)
		{
			hasMasterClient = false;
			
			var guidArray = [];
			
			for (var index in players) 
				guidArray.push(players[index].GameData.Guid);
			
			var index = getRandomInt(guidArray.length);

			objects[guidArray[index]] = makeMasterClient(objects[guidArray[index]]);

			//Let everyone know we have a new MasterClient.
			server.connections.forEach(function (c) 
			{
				c.sendText(JSON.stringify(objects[guidArray[index]]));
			});
			
			hasMasterClient = true;
		}
		
		objects[conn.id].GameData.IsMasterClient = false;		

		server.connections.forEach(function (c) 
		{
			c.sendText(JSON.stringify(objects[conn.id]));
		});
		
		delete objects[conn.id];
		
        console.log("Client " + conn.id + " left the game. (" + Object.keys(players).length + "/" + maxConnections + ").");
	
		//If there are no more players, reset server.
		if(Object.keys(players).length == 0)
			resetServer();
    });
}).listen(port);