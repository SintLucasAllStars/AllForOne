var ws = require("nodejs-websocket");

var port = 25565;

var objects = {};

var maxPlayers = 4;

console.log("Created server on port " + port + ".");

var server = ws.createServer(function (conn) 
{
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
	
    conn.on("text", function (str) 
	{
		var msg = JSON.parse(str);		

		if (msg.GameData.Type.includes("Player"))
		{
			if(objects[msg.GameData.Guid] == undefined)
			{
				console.log("Client " + msg.GameData.Guid + " joined the game. (" + server.connections.length + "/" + maxPlayers + ").");

				conn.id = msg.GameData.Guid;

				if(server.connections.length == 1)
				{
					msg.GameData.IsMasterClient = true;
					console.log("Made client " + msg.GameData.Guid + " master client.");
				}
				
				msg.GameData.Type = "Player_0" + server.connections.length;
				
				for (var index in objects) 
				{
					conn.sendText(JSON.stringify(objects[index]));
				}

				objects[conn.id] = msg;
				
				updateClient(msg);
			}
			else
			{
				objects[msg.GameData.Guid] = msg;
					
				updateClient(msg);
			}			
		}
		else
		{
			objects[msg.GameData.Guid] = msg;
					
			updateClient(msg);			
		}
    });
	
    conn.on("close", function (code, reason) 
	{		
        console.log("Client " + conn.id + " left the game. (" + server.connections.length + "/" + maxPlayers + ").");
	
		objects[conn.id].GameData.Active = false;

		if(server.connections.length == 0)
		{
			console.log("All players left. Resetting server");
			objects = {};
			return;
		}
		
		/*
		if(objects[conn.id].GameData.IsMasterClient && server.connections.length > 0)
		{
			var keys = Object.keys(objects);
			var index = getRandomInt(keys.length);
			objects[keys[index]].GameData.IsMasterClient = true;
			console.log("Client " + conn.id + " left and was master client. Made client " + objects[keys[index]].GameData.Guid + " master client.");
			c.sendText(JSON.stringify(objects[keys[index]]));			
		}
		*/
	
		server.connections.forEach(function (c) 
		{
			c.sendText(JSON.stringify(objects[conn.id]));
		});
		
		delete objects[conn.id];
    });
	
}).listen(port);