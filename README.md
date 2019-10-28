# AllForOne

## DESCRIPTION
On this assignment everyone makes the same game **"Mechanic Fever"**. How you make it is what matters...
The assignment is focused on code elegance and production value. You do not need to add all the mechanics described, but prioritize carfully so that you end up with a game that makes sense and is playable.

## GENERAL MECHANICS

You will build a 3D turn-based digital "board game" in which every turn is played as a third person action game.
The game is to be played by 2 players. They can be both on the same computer (Chair shuffle) or if you feel super confident you can make it networked (optional). It is recomended to go for chair shuffle first and THEN adapt to networked.

* The game starts with a top view map of a corridor system.
* Every player has an initial point budget of 100 points to buy and place units anywhere on the map. They do this in turn. meaning that player 1 "buys" and places 1 unit in the game area, then player 2 places one unit, then player 1 again, until both either run out of points or press a "Done" button.
* Units are humanoid 3D characters. They have a set of stats that amount to:
  - Health
  - Strength
  - Speed
  - Defense
* Each player will have a color - Player1 is RED, player 2 is BLUE. All units of each player must have an element of that color. That could be clothing, skin, a light or a overhead floating object, aura etc. Up to you.
* When buying a unit the price depends on the balance of the stats, and that will also influence how many units each player is going to have. So stratigically one player could choose to set up a single unit with full stats and play with only one "super soldier" or have 10 weak units.
* When all units are placed, the game starts.
* On the begining of each turn, one power up or weapon will randomly spawn on the play area.
* The current player will then choose one unit to control by clicking it.
* Once the player clicks the unit the camera flies into 3rd person mode, a 10 second timer starts and the player now has full control of the character and can (more details on each of these below):
  - Run
  - Jump
  - Attack / use weapon (if you have one)
  - Pick up power ups and weapons
  - Use power ups
  - Fortify
* Once the 10 seconds are finished the turn is over.
* The game ends when one of the players has no more units alive. Obviously the other player wins.
