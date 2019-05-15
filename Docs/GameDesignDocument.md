# Game Design Document

![Alpha Commit logo](./pictures/ac_logo_small.png)  
`A L P H A // C O M M I T`

### General information
**Team name:** “Alpha Commit”  
**Team members:** Baratto Diego (919538), Costella Alessandro (920031), Liggio Giorgio Maria (905471)

---

## 1. Changelog
| Author        | Date          | Description |
| ------------- | ------------- | ----- |
| Costella Alessandro | 2019-04-15 | Added logline |
| Costella Alessandro | 2019-04-15 | Added last interfaces |
| Costella Alessandro | 2019-04-15 | Added main menu, shop, cards and in-game interfaces |
| Costella Alessandro | 2019-04-15 | Added game modes |
| Costella Alessandro | 2019-04-15 | Added assets list |
| Costella Alessandro | 2019-04-15 | Added card rules and level RCG |
| Costella Alessandro | 2019-04-15 | Added rules, wincons, controls, flowchart |
| Baratto Diego | 2019-04-15 | Updated project subdivision |
| Costella Alessandro | 2019-04-12 | Added Gameplay 5.1 and 5.2 |
| Costella Alessandro | 2019-04-12 | Added character design, removed Game World |
| Baratto Diego | 2019-04-11 | Fixed image issue |
| Baratto Diego | 2019-04-11 | Inserted image into document |
| Costella Alessandro | 2019-04-03 | Audience chapter complete, removed Legal Analysis. |
| Costella Alessandro | 2019-04-03 | Vision Statement  |
| Liggio Giorgio | 2019-04-03 | Push first game design writing |

## 2. Vision Statement
*Sunday Knights: Magic & Mayhem* is a marriage of *Candy Crush* + *Clash Royale*. It's a fast puzzle game where two opponents play against each other. The game setting is a medieval fantasy world with various locations. The game will feel partly serious and exuberant. In each game, the player will face an AI opponent or a real-life user in multiplayer mode. There are game modifiers that come in the form of playing cards, to gain points more quickly or slow down the opponent.

There's huge emphasis on the multiplayer game pacing, with head-to-head fast matches where anything can happen due to special actions. Many in-game rewards such as game cards, resources, and currency are designed to convey a sense of accomplishment and achievement.

### 2.1. Game Logline
*“Embark on a journey to fight back the forces of evil, one puzzle at a time.”*

### 2.2. Gameplay Synopsis

#### 2.2.1. Uniqueness
*Sunday Knights: Magic & Mayhem* is a Match 3 game featuring fast-paced action and CCG (Collectible Card Game) elements. It stands out from other titles in the category because it combines puzzle games with a 1v1 player-vs-player experience; as a matter of fact, the design focuses on the long-term engagement of users thanks to the social gaming aspect.

#### 2.2.2. Functioning & Core Mechanics
> This section is intentionally descriptive. For further information and implementation details, please refer to Chapter 4 ("*Gameplay*") of this document.

The fight between Good and Evil takes place on a square grid filled with tiles in the form of coloured gemstones. Although winning conditions may vary, the core mechanics stay the same across all game modes.

The game goal is to gain points and resources by clearing tiles from the game grid. Players can swap tiles with nearby ones; when a group of tiles satisfies the matching rules, they are removed from the grid. Finally, remaining tiles fall downwards, and new tiles spawn from the top of the grid columns.

Combined ("combos") and cascade matches award more points. Certain tiles can behave differently, like matching with any colour or performing special actions.

Players can also perform special actions by consuming an action card; these produce the same effects activated by special tiles, but they can be invoked at any time.

Players face some constraints while playing, depending on the game mode. There may be a maximum number of moves or a time limit before the game is over.

As non-exhaustive, non-final examples, we can estimate the following values:

* Single player game, easy difficulty
  * Move limit: **15 to 25 moves**
  * Time limit: **no limit**
  * Card limit: **4 cards**
* Multiplayer game
  * Move limit: **30 moves**
  * Time limit: **2 minutes**
  * Card limit: **4 cards**

#### 2.2.3. Setting
The game takes place in an archetypical medieval fantasy era, in the peaceful Kingdom of Ludenia. The first impression the player gets is one of peace and carefreeness since Ludenians are happy and cheery people. Ludenia has many views to offer; beautiful landscapes hold majestic castles and quiet villages, the woods are home to many animals, and the mountains contrast a sea stretching as far as the eye can see.

Things change when the story antagonists show up. The Dreary Army is driven only by the need for stealing everyone's happiness, and this reflects in a duller mood in the environments. Wherever they go, life withers and joy disappears, nature recedes, and desolation overcomes everything.

A hero must save the realm from becoming another wasteland.

#### 2.2.4. Look and Feel

![Example of a normal match screen](pictures/Look-n-feel.png)

## 3. Audience, Platform, and Marketing
Audience analysis and definition is based on, but is not limited to, the core demographics of leading titles in the genre; other factors such as business objectives and mobile market tendencies were also considered.

> The following paragraphs are about the game audience. For a more comprehensive comparison with other competitors, please refer to paragraphs 3.3 ("*Top Performers*") and 3.4 ("*Feature Comparison*") of this chapter.

### 3.1. Market Insights
Puzzle games have no well-defined target audience, due to the high flexibility of both gameplay and cosmetic elements; this means that marketing choices, such as defining a target audience, heavily influence their core design.

A user analysis by Newzoo [1] on King's *Candy Crush*, a top performer on both Google Play and Apple Store, shows that the actual player base is broader than the intended audience: although King's core demographic is mostly women, a significant portion of players (40%) are male. In particular, *Candy Crush* appeals to a demographic with 42% of players between the age of 21-35 and almost 40% above 35.

The same analysis also highlights some features of Supercell's *Clash Royale* core players: they are predominantly male (77%), and over half of them fall into the 21-35 age category with only 23% of players over the age of 35. We account for these insights due to the presence of fast-action elements in our game, considered in titles not belonging to the classic puzzle game genre.

Finally, most *Candy Crush* players enjoy the game on mobile devices, and define themselves as "casual gamers" (45%); they mainly play games to pass the time and don’t invest a lot of money in them. On the other hand, *Clash Royale* players are at least "mid-core", playing games on a regular basis and being also open to try different type of games (16%) such as a puzzle game.

Of *Candy Crush Saga*’s players, males (54%), are more likely to pay for mobile games.

These consideration are still valid when considering other titles for comparison purposes, and not restricted to the *Candy Crush Saga* and *Clash Royale* titles only.

### 3.2 Target Audience
The study shows how the public is diverse among some genres. Age is not a restriction since a large share of users can enjoy the same contents and themes in games; gender is blatantly relevant for similar reasons.

The intended audience for our game consists of adults aged 25-35 (spread ±5), with a part-time or full-time occupation (e.g. job, school). They are casual players and enjoy playing games in their spare time, to take a break from the daily routine, or while travelling. Our target has no specific gender identity; while the core game is suitable to anyone, additional content may be appealing to a particular gender group or subgroup.

### 3.3. Top Performers
Candy Crush, Toon Blast, Matchington Mansion, Farm Heroes.

### 3.4. Feature Comparison
Competitors do not feature PvP, while they usually include longer and more complex levels and puzzl-ish mechanics, and the slower pace that comes with them.

### 3.5. Target Platform
Mobile Android devices, as the market for this kind of games flourishes on mobile platforms. That is where the potential audience of the game would look for it. Match-3 gameplay is experienced at its best on a touch screen.

### 3.6. System Requirements
Android 5.0+, as Android 5 constitutes 89% of the Android market right now.

### 3.7. Business Model

## 4. Gameplay

### 4.1. Overview
The game is about matching groups of 3 or more faces on a grid by swapping couples of them. Only adjacent faces can be swapped, vertically or horizontally. Furthermore, the game includes playable cards that can change the state of the game for the player or their opponent.

### 4.2. Gameplay description

#### 4.2.1. Mechanics

##### Faces
The game is about making faces disappear ("freeing" them from the evil curse) by matching groups of them. The main part of the in-match GUI consists of a grid of 6x7 faces of random colours (6 columns x 7 rows), as is described in further detail in the Interfaces section. The player can swap couples of adjacent faces by "spending" a move - meaning their move count diminishes by one for each swap they perform. A swap is EFFECTIVE only if it actually "groups" 3 or more faces of the same colour, meaning if a swap doesn't cause any face to disappear, it is automatically reverted by the game. In this case, the move is not spent.

##### Moves
Every player starts the match with a set number of moves and cannot usually gain any (some cards cause exceptions to this rule). PvP matches grant 30 starting moves, campaign scenarios grant a different number of moves based on the single level.

##### Score
Each time faces disappear, they grant score based on the size of the group of faces matched. 3-faces matches grant 30 points, 4-faces grant 60, 5-faces grant 100, 7-faces grant 300

##### Score Threshold
Each level has a minimum score the player must reach in order to achieve victory. This number varies throughout the campaign, while in PvP matches it's a fixed 1200.

##### Cards
Cards are items the player can use at the expense of moves, with the cost depending on the single card. The player can unlock them by playing the campaign or by buying packs with coins they gain through PvP matches. Cards are either blue, if they provide a bonus or help for the player, or red, if they damage the opponent. Cards are played by tapping on them.

######Card List

>Blue cards

1. Shuffle!

The Shuffle! card allows the player to change their side's layout. By activating the card, all the faces will flip and become of a random color. This card must not generate "automatic" matches by creating 3-or-more groups of faces.

![The Shuffle! card](pictures/cards_Shuffle!.png)

2. Move on!

The Move on! card increases the Remaining Moves counter of the user by 3 upon activation.

![The Move on! card](pictures/cards_Moveon!.png)

3. Da Bath Bomb!

The Da Bath Bomb! card requires the player to target a specific face cell. Upon targeting, the card will "demolish" the selected face (by making it happy, of course) and the 8 faces in the square around it, in an explosion of soap foam. This will grant the player 150 points.

![The Da Bath Bomb! card](pictures/cards_DaBathBomb!.png)

4. Gimme that juice!

The Gimme that juice! bomb covers all the faces in sparkly raspberry juice, making it so they provide double points for 5 seconds.

![The Gimme that juice! card](pictures/cards_GimmeThatJuice!.png)


###### Coins
After every PvP match, the player will be granted coins: 15 if they win, 5 if they lose. Coins can be gained either this way or through spending real money. They can only be "lost" by buying packs. Cards can be either common or rare.
	Card example: Bomb

##### Packs
There are 2 card packs that can be bought in the Shop (see Interfaces for the Shop), one providing 1 card for 100 coins and one providing 3 cards for 250 coins. Cards are randomized based on their rarity: each card has 95% of being common and 5% of being rare.

### 4.3. Controls
The game is controlled through the phone's touch screen.

**Choosing the cards before a game:** tapping a card on the grid will move it into the "selected cards" area. Tapping on it in the selected cards area will move it back into the grid.

**Swapping:** swapping 2 faces is done by "dragging" one in a particular direction, either vertical or horizontal. The first point of pressure pinpoints the face to move, moving the finger towards another face selects it as the second one to swap.

**Using non-targeting cards:** a card can be used by double tapping on it. The first tap will highlight the card, while the second will "confirm" the selection and activate the effect. Tapping anywhere else after the first tap will cancel the card selection.

**Using targeting cards:** a card that needs to target a cell can be activated by tapping on it once. An arrow will appear from it towards the center of the board. The second tap, that is to be performed on a face, will activate the effects on the selected face. Tapping outside of the board will cancel the card selection.

#### 4.3.1. Interfaces
For all interfaces, the phone is considered to be in portrait mode, vertically held.

##### Main Menu
The main screen features the necessary buttons, the details of which will be further described in the assets section, on a static background. The buttons are, as they appear from top to bottom:

* Campaign
* Duel
* Shop
* Settings

The main menu screen will also feature a red X button to quit the game on the top right side of the screen.

##### Shop Screen
The shop will feature Robin on the bottom left of the screen, filling the whole bottom part with a conversazion balloon containing "Hey! Nothing better than some new gear to go into battle again!". Above him the screen will show 3 different kinds of card packs, with their pricing and the "BUY!" button.
When the player taps on a pack, a small balloon will appear from it. From top to bottom, the packs will be:

* Card Pack - 60 gold - Balloon: "Contains 3 new cards!"
* BIG Pack - 120 gold - Balloon: "Contains 7 new cards, convenient!"
* THE PACK-O - 300 gold - Balloon: "Contains 20 new cards! HUGE DEAL!"

The top left corner shows a little insertion with the player's money account, with a small "+" button on the side. Tapping on the money or the + will open the "buy coins with real money" pop-up. The exchange rate is €1 - 100 gold, with bigger bundles to be decided upon.

##### Card Selection
The card selection pop-up appears before queuing for a game or going into a campaign level. It holds a square grid of all the cards the player owns on the top, the square's side being half of the screen's height, and a bottom section of 4 slots to see what cards have been chosen so far.

##### In-Game UI
The in-game interface is as the look-and-feel section shows. In addition to those elements, the "ME" and "OPPO" sections feature a small bar on the bottom of their respective squares that empties to the left in 30 seconds, resetting everytime the assigned player makes a move. That bar is the inactivity bar, that makes the player lose the game if it empties completely. There is also a main time bar, showing how much time is left, below the 2 squares, just as broad as the 2 squares together.

##### Settings Menu
The settings menu has the same look of the main menu. The background and X button stay the same, will the menu button change into:

* Music
* Sound effects
* Credits

Music and Sound effects button, if tapped, will grey out and turn music or sound effects off. If tapped on again, the effect will be reverted.
The Credits button, if tapped, will show our team's and respective names - yay!

##### Level Selection
The campaign level selection screen features a "path" connecting the levels, which are represented with buttons with their numbers above them. The background shows the land of Ludenia.
All completed levels will appear as blue, smiling faces, the next level to complete will appear as a sad, red face. Unlocked levels will appear as sad, stone-grey faces.

#### 4.3.2. Rules
The number of possible moves is set for each game. Time is set only in multiplayer games.

It is not possible to swap 2 faces if this does not cause any match to happen. The swap will be reversed and the move will not be spent.

A PvP match can result in a player's victory or a draw. A draw happens if both players hit the same score or if no player can reach the score threshold.

The player has to make at least one move every 30 seconds. Otherwise, victory will be granted to the opponent due to inactivity. If this happens, it will not be possible for the inactive player to join a game for 3 minutes.

The card the player can use are to be selected through an ad-hoc screen before entering the game (or level). The same card cannot be used again without making 10  9 other moves first.

If a player disconnects from the game more than 30 seconds from the end, their time will deplete naturally and they will lose the game. If a player disconnects during the last 30 seconds, the will just result inactive for that last part of the match, with no mechanical consequences. The winner will be computed regularly.

#### 4.3.3. Scoring/winning conditions
Campaign: the level is won if the player can achieve a level-depending score withing the given number of moves.

PvP: the player who, having reached the threshold, reaches the highest score, wins the game. Alternatively, if a player does not make a move for 30 seconds straight, their opponent wins the game.

### 4.4. Game Modes and Other Features
**Campaign:** a series of levels to be played in a sequence. A level cannot be accessed if the previous one has not been defeated. The campaign is a single player mode of 30 different levels, with different mechanics emerging with higher levels. This mode awards coins and cards with the rules described above. In this mode, griefing cards (the ones that hinder the opponent's gameplay) are not usable.

**PvP:** the core of the game. This mode consists of 1v1 matches, not longer than 3 minutes. In this mode, all cards are allowed. This mode rewards coins as described above.

### 4.5. Levels
Levels are generated through an Random Content Generation algorithm, with due restrictions (for example on the number of face colours present in the level) based on their "position" in the campaign.

### 4.6. Flowchart

![Flowchart](pictures/flowchart.png)

## 5. Game Characters

### 5.1. Character Design

##### Zeely
The player's guide is a trust-inspiring, colorful little parrot. It has a yellow body, red chest and light blue underwings and head, and green eyes. He is always cheerful and full of energy, never appearing without smiling eyes. The anatomy is a standard Forpus' parrot one.

##### Robin
The shop-keeper is a short boy, wearing robes a bit too big for him, but not so much to make him look goofy. He has short, brown hair, big blue eyes and a big pair of round glasses on them. His clothes are mostly Risultati immagini per blue color 2661F7 blue, with ocra yellow adornments on the edges. As an apprentice wizard, he also wears a big blue pointy hat of the same making, so big he has to keep a hand on it to prevent it from falling. Just like Zeely, he is always cheerful, presenting himself with an open smile every time he appears.

All the characters described above are NPC. They only appear to tell the player something, or to adobe the shop's screen. They have hence no A.I.

## 6. Media List

>Faces

![Faces' sketch before and after getting matches](pictures/face_colorless.png)

Faces apppear in 5 different colours: red (E00201), green (rgb 10, 255, 60), yellow (FFFF33), violet (710193) and blue (0038a8). As seen in the example above, they have 2 stages, a sad one before being matched, and a happy one after being matched.

*	Score board
*	Timers
*	In-game background
*	Loading screen
*	Board
*	Buttons
*	Zeely
*	Robin

>Faces animation

Faces have a specific animation when they get matched, turning their frown into a smile in 0.5 seconds and then popping away towards their center. This is accompanied by a "pop" sound.

*	Cards animations
*	Menu music
*	In-game music
*	Match-3 sound effect
*	Zeely entering sound
*	Robin entering sound (shop)
*	Victory fanfare

>Defeat riff

This is what the player heards upon defeat. It's a riff of 4 notes, on descending demitones, from Bb (or Sib) to G (or Sol), with a long tremble on the last one.

*	Button sound
