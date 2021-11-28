Requirments

| ID # | Type | Description | Rationale | Fit Criterion | Priority | Dep |
| --- | --- | --- | --- | --- | --- | --- |
| 0001 | Functional | The game must allow the user to start the game | Allow the game to even begin | The player is now in the game | 1 |
| 0002 | Functional | The game must allow the user to control the player character | It is a single-player game so the user should be the player | The user input affects the player character | 1 | 0001 |
| 0003 | Functional | The game must allow the player to customize their character&#39;s looks | Players should be able to choose what they look like | In-game their character will look like what they choose. | 3 |
| 0004 | Functional | The game must allow the user to save the game | Continuation of game | Saved game state matches game state when the user saved the game. | 2 |
| 0004-1 | Security | Modifications to save files will be recorded and a non-modified un-editable backup of the save file should be created. | Some users like to modify their game and this would allow for them to revert back to an unmodified version. | Backup save file appears in the list of game saves | 3 | 0004 |
| 0005 | Functional | The game must allow for the user to select 1 of 3 difficulty modes (easy, normal, hard) | Different players prefer different levels of difficulty | Base stats and stat modifier values for NPCs and player are changed to pre-determined values for the difficulty | 2 |
| 0006 | Functional | The game must allow the user to change settings such as video settings, audio settings, and difficulty. | Players have different preferences when playing games | When a player changes the setting it is noticeable when they play the game | 2 | 0005 |
| 0007 | Functional | The game must allow the user to load a saved game | Continuation of the game | The game starts at the point where it was when the user saved the game | 2 | 0001 0004 |
| 0008 | Functional | The game must have a main menu to load game saves, change settings, and start a new game | Every video game needs a main menu in order to easily get to the actual game play they want to. | When the game application starts it first shows the main menu and a player can load a saved game, create a new game, or change the settings. | 1 | 0001 0004 0005 0006 0007|
| 0009 | Functional | The game must have multiple types of non-playable characters (NPCs), such as merchants, guards, farmers, fishermen, builders, soldiers, bandits, thieves, blacksmiths, alchemists, etc. | Game world will be empty without them. | Different types of NPCs show up. | 2 | 0005 |
| 0010 | Functional | The game must have a variety of items. These include things such as weapons, armor, food, potions, and miscellaneous items. | Every role-playing game needs a variety of items otherwise it can get boring. | Multiple types and variety of each type of items show up in the game | 2|
| 0011 | Functional | The game must provide the player and each NPC have an inventory to store items.| The player and NPCs need to store items | The player and NPCs will have space for items to be stored in. | 2 | 0009|
| 0012 | Functional | The game must allow the user to pick up items | Most traditional RPGs allow the player to pick up items | Once an item is picked up it will disappear from its position and end up in the inventory of the player | 2 | 0010 0011|
| 0013 | Functional | The game must allow the player to create weapons, armor, food, potions, and certain miscellaneous items. | Adds variety to play style, which increases replayability. | At each respective station, players can exchange crafting materials for a complete item. | 3 | 0002 0010 0011|
| 0014 | Functional | The game must allow characters to interact with each other | Without interactions, the game is literally useless | The player can do things such as talk or fight with NPCs, and NPCs do the same with other NPCs. | 2 | 0002 0005 |
| 0015 | Functional | The game must allow NPCs and Quest boards to give out quests | Quests give a goal to the player and add changes to the world when they get completed | Certain NPCs and quest boards will have a quest indicator above them and when interacting with them the player will see information about the quest. | 2 | 0014 |
| 0016 | Functional | The game must allow players and NPCs to accept quests | Quests give a goal to the player and add changes to the world when they get completed | Player and each NPC that can accept quest will have a log and the quest will appear on that log when they accept it and the quest giver will not give out the quest again unless it is failed | 2 | 0014 |
| 0017 | Functional | The game must have multiple locations | Adds diversity to the game | Multiple distinct locations appear in the game | 2 |
| 0018 | Functional | The game must have multiple weather effects | Adds diversity to the game | In the game, the weather will change over time, indicated by whether it is raining, sunny, cloudy, snowy, or windy. | 3 |
| 0019 | Functional | The game must have multiple types of vegetation | Adds diversity to the game | Different types of trees, bushes, flowers, and grasses will be in the game. | 3 |
| 0020 | Functional | The game must have multiple biomes | Adds diversity to the game | Different locations and areas in the game will have different weather patterns and vegetation from each other | 3 | 0017 0018 0019 |
| 0021 | Functional | The game must have a common currency such as coins | You need currency to buy things. | The player can obtain currency which can then be used in shops | 2 | 0010 |
| 0022 | Functional | The game must allow the player and NPCs to buy/sell property and items | Adds diversity and a money-making aspect to the game | Ownership of property or item goes to the buyer and if it&#39;s an item it goes into the inventory, while money exchanged goes to the seller. | 2 | 0010 0014 0021|
| 0023 | Functional | The game must allow the player to personalize their property | Adds diversity to the game | The player can change the structures on the property and the designation of the property (home, shop, etc). | 3 | 0010 0022 |
| 0024 | Functional | The game must display the player&#39;s stats to them | Allows players to keep track of what their stats are | The player can see their stats after opening up the stats page. | 2 | 0001 |
| 0025 | Functional | The game must allow the player to level up his or her stats | Players have different play styles | Once a player accumulates enough experience from actions and quest they can increase his or her stats some. | 3 | 0024 |
| 0026 | Functional | The game must detect collision between Objects | There needs to be collision between objects. | Objects should not pass through each other. | 1 |
| 0027 | Functional | The game must update health when the character is attacked or heals | Needs to check if the character is dead. | Health goes down when attacked and increases when the character heals | 2 | 0014 |
| 0028 | Functional | The game must allow the player to quit the game | The player has to stop at some point | The game closes when the player quits | 1 | 0001 |
| 0029 | Functional | The game must have an in-game menu to save the game, load previous saves, change settings, and quit the game. | Every game has an in-game menu. | When the escape button is pressed the in-game menu opens and pauses the game. | 1 | 0004 0005 0006 0007 0028 |
| 0030 | Functional | The game must have multiple types of quests: fetch, kill, transport, rescue, etc. | Adds diversity to the game | Multiple types of quests appear in the game | 2 |
| 0031 | Functional | The game must have consequences to quests | Every action has an effect | Once a quest is completed or fails it will change something in the world according to what type of quest it is. | 2 | 0015 0016 0030 |
| 0032 | Look and Feel | The game will have both first and third person views.| Different players have different preferences on point of view in video games. | The player can swap between first and third person view. | 1 | 0002 |
| 0033 | Security-Access | The game will be offline so there will be no connection with the internet while playing | Doesn&#39;t require the internet to play | Runs with no internet connect. | 1 | 0001|
| 0034 | Performance-speed | The game will load within a minute when getting into the game world | Load times need to be fast to enjoy the game rather than watching a loading screen. | Load times are under or approximately equal to one minute | 3 |
| 0035 | Usability-Accessibility | A copy of the game will be accessible to anyone that asks for it | This is my first complete game so it is just to boost my experience and for a grade | If someone asks for a copy of the game I will give them a copy. | 3 |
| 0036 | Usability-Ease of Use | The game will be run from the executable after it is installed | No point in having a complicated start procedure | Running the executable will start the game. | 1 |
| 0037 | Usability | The game will run on Windows 10 at minimum. | This is the operating system that is running while the game is being made so it should run. | The game runs properly when using Windows 10. | 1 |

Dep = Dependencies

NPCs = Non-player characters which include humanoid characters and non-humanoid characters that are controlled by ai.

RPG = Role-Playing Game