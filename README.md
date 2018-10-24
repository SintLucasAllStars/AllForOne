AllForOne Assignment --- Mechanic Fever
DESCRIPTION
On this assignment everyone makes the same game "Mechanic Fever". How you make it is what matters... The assignment is focused on code elegance and production value. You do not need to add all the mechanics described, but prioritize carfully so that you end up with a game that makes sense and is playable.

GENERAL MECHANICS
You will build a 3D turn-based digital "board game" in which every turn is played as a third person action game. The game is to be played by 2 players. They can be both on the same computer (Chair shuffle) or if you feel super confident you can make it networked (optional). It is recomended to go for chair shuffle first and THEN adapt to networked.

The game starts with a top view map of a corridor system.
Every player has an initial point budget of 100 points to buy and place units anywhere on the map. They do this in turn. meaning that player 1 "buys" and places 1 unit in the game area, then player 2 places one unit, then player 1 again, until both either run out of points or press a "Done" button.
Units are humanoid 3D characters. They have a set of stats that amount to:
Health
Strength
Speed
Defense
Each player will have a color - Player1 is RED, player 2 is BLUE. All units of each player must have an element of that color. That could be clothing, skin, a light or a overhead floating object, aura etc. Up to you.
When buying a unit the price depends on the balance of the stats, and that will also influence how many units each player is going to have. So stratigically one player could choose to set up a single unit with full stats and play with only one "super soldier" or have 10 weak units.
When all units are placed, the game starts.
On the begining of each turn, one power up or weapon will randomly spawn on the play area.
The current player will then choose one unit to control by clicking it.
Once the player clicks the unit the camera flies into 3rd person mode, a 10 second timer starts and the player now has full control of the character and can (more details on each of these below):
Run
Jump
Attack / use weapon (if you have one)
Pick up power ups and weapons
Use power ups
Fortify
Once the 10 seconds are finished the turn is over.
The game ends when one of the players has no more units alive. Obviously the other player wins.
LEVEL DESIGN
The play area will be partly indoor, partly outdoor, there will be one or more single floor buildings made out of winding corridors with doors and windows and Props inside and outside. There will be multiple doors that lead to the outdoor part of the level so that players can use them as shortcuts.

PRE-GAME MECHANICS
This is the initial phase where the players buy and place units on the game area. The flow is as follows:

Player gets a Unit character that is displayed on the screen with randomized body (this can be a different scene or a overlay).
The stats of the unit are shown and the current price of the unit as well. These are randomly assigned by default but around the mid-range.
The player can choose any value for each stats. That will cause the price to be recalculated (see balancing).
When the player is ready to buy then he should hit the "Hire" button. This takes the game to the top view of the game area and the player will then click a location where the Unit should spawn.
Balancing
A unit with full stats (100% on all stats), will cost 100 points
A unit with the minimum skill (1% on all stats) should cost about 10 points.
Each skill has different cost, they are distributed in this order:
Health and Speed (Same price / Most expensive)
Strength and Defense (Same price / Slightly cheaper)
BOARD MECHANICS DETAIL
The full game area should be visible on the top view.
First thing that happens when a turn starts is that a power up spawns randomly on the scene. This should be clearly visible.
There should be a clear element that tells which player's turn it is.
The color of the units should also be clearly visible.
ACTION MECHANICS DETAIL
When the player starts, the counter should be clearly visible and counting down from 10.
The player can:
Run / Jump anywhere on the game area
Attack / use weapon: All players start without weapon and they can only punch. They can then pick up weapons that give them more range or hit points. (See attack/defense mechanic)
Pick up power ups and weapons.
Use power ups (see description of powerups below)
Fortify: at any point during the 10 second turn (action view), the player can choose to fortify. Fortify will put this unit in a defensive position and he will remain this way. The unit stays fortified until it gets picked to play again. (See attack/defense mechanic)
If the 10 seconds run out and the unit is outdoors, then that unit dies instantly.
ATTACK / DEFENSE MECHANIC
An attack that hits an enemy unit will remove health from that unit in proportion to the strength of the character multiplied by the damage of the weapon it is using.
Each attack will take some time to complete, some weapons make this time longer.
If the attacked unit is fortified, it will take less damage proportionally to it's defense stat.
If the attacked unit is not fortified, the defence stat is useless.
POWER UPS
The player, during the 10 second round, can at any moment use one of the power ups it has picked up before.
There is a button to cycle through the current power ups. It should be clear on the screen what is the currently selected power up.
If the player uses the power up it gets spent.
Adrenaline
The player gets a 10 second 50% boost in speed.

Rage
The player gets a 5 second 10% boost in strength

Time machine
The player can freeze the 10 second counter for 3 seconds

WEAPONS
No weapon / Punch
damage : 1 
speed : 10 
range : 0 

PowerPunch (like a glove or brass knuckles or boxing wraps)
damage : 2 
speed : 10 
range : 0 

knife
damage : 3 
speed : 8 
range : 0 

WarHammer
damage : 8 
speed : 4 
range : 1 (extends the range by a radius of one Unity unit). 

Gun
damage : 5 
speed : 3 
range : about 1/3 of the diagonal of the playarea. 

ASSETS
Scyfi interior 
https://assetstore.unity.com/packages/3d/environments/sci-fi/sci-fi-styled-modular-pack-82913 

Skybox 
https://assetstore.unity.com/packages/2d/textures-materials/sky/skybox-series-free-103633 
https://assetstore.unity.com/packages/3d/environments/sci-fi/real-stars-skybox-116333 

Corridors 
https://assetstore.unity.com/packages/3d/environments/urban/simple-corridors-96435 

Procedural male character 
https://assetstore.unity.com/packages/3d/characters/humanoids/mcs-male-45805 

Procedural female character 
https://assetstore.unity.com/packages/3d/characters/humanoids/mcs-female-45807 

Character 
https://assetstore.unity.com/packages/3d/characters/humanoids/combat-character-pack-soldier-96594 

Animation General 
https://assetstore.unity.com/packages/3d/animations/huge-fbx-mocap-library-part-1-19991 
https://assetstore.unity.com/packages/3d/animations/huge-fbx-mocap-library-part-2-20282 
https://assetstore.unity.com/packages/3d/animations/huge-fbx-mocap-library-part-3-20285 

Animation Fighting 
https://assetstore.unity.com/categories/3d/animations/?k=price%3A0&order_by=relevance&q=category%3A23&q=price%3A0&rows=42 

Animation Movement 
https://assetstore.unity.com/packages/3d/animations/move-motion-free-pack-25900 

Adam photogrametry pack 
https://assetstore.unity.com/packages/templates/packs/adam-ep2-ep3-photogrammetry-pack-115786 

Delivery & Grading:
You will deliver the assignment by forking this repository, developing the game on your fork and sending a pull request. On the pull request description Should be your name and a small description of the features that are there or missing.

The deadline for the pull requests is the 24th of October 2018.

Grading distribution
Code / architecture quality - 25 points max
Features implemented - 25 points max
Visual quality - 20 points max
Overall game quality - 30 points max
Grading ranges:
(0 - 40) - Onvoldoende
(41 - 60) - Voldoende
(61 - 85) - Goed
(86 - 100) - Excelent