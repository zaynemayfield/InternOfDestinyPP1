Intern of Destiny — Development Plan
Game Concept
Intern of Destiny is a C# console-based skill-and-luck RPG where the player trains their character through short mini-games, builds stats, buys temporary upgrades, and fights increasingly ridiculous bosses.
The core design combines three elements:
Player skill — typing speed, reaction time, accuracy, trivia knowledge, button-mashing, and decision-making.
Character progression — Strength, Intellect, Luck, Lives, Money, and Level.
Randomness — damage rolls, critical hits, random events, and special items.
The core gameplay loop is:
Train → Train → Train → Boss Fight → Rewards → Level Up → Repeat
The player receives three training opportunities per level and may spend them however they want.
---
Chunk 1 — Player Creation and Main Game Loop
The first version establishes the structure of the game.
New Game
Opening:
```text
========================================
          INTERN OF DESTINY
========================================

Congratulations!

Due to an unfortunate clerical error, you have
been selected for MEGA CORP's Hero Internship
Program.

Survival is technically optional.

What is your name?

> Zayne
```
Starting Player Values
Stat	Starting Value
Level	1
Strength	0
Intellect	0
Luck	0
Money	$50
Lives	3
Bosses Defeated	0
Training Remaining	3
Main Menu
```text
========================================
ZAYNE - UNPAID HERO INTERN
========================================
Level:       1
Lives:       ♥ ♥ ♥
Strength:    0
Intellect:   0
Luck:        0
Cash:        $50

Training Sessions Remaining: 3

1. Study
2. Work Out
3. Shop
4. View Stats
5. Fight Boss
6. Quit

Choose:
>
```
Core Logic
The permanent game loop is:
Menu → Activity → Menu → Activity → Boss → Rewards → Next Level → Menu
For the first development milestone, menu options can use placeholder messages.
The goal is simply to get:
Player creation
Player statistics
Main menu
Repeating menu loop
Basic input validation
working correctly.
---
Chunk 2 — Workout: Precision Training
The first playable training activity is Workout.
A full Workout eventually contains three mini-games.
The first mini-game tests typing accuracy.
Precision Training
```text
PRECISION TRAINING

Type the following word EXACTLY:

DUMBBELL

>
```
Scoring
Result	Points
Correct	3
Incorrect	0
The player does not get a retry.
Start with a list of roughly 20 possible words.
Examples:
PUSHUP
MUSCLE
DUMBBELL
SQUAT
POWER
FITNESS
BURPEE
BARBELL
STRENGTH
CARDIO
Difficulty scaling can be added later.
---
Chunk 3 — Workout: Button Mash
The second Workout mini-game tests speed.
Power Training
```text
POWER TRAINING

20 PUSHUPS!

Press SPACE 20 times!

READY...
3
2
1
GO!
```
Level 1 Goal
20 Space presses within 5 seconds
Scoring
Number of Presses	Points
20+	3
15–19	2
10–14	1
Below 10	0
Example result:
```text
23 PUSHUPS!

Time: 4.71 seconds

Score: 3/3
```
---
Chunk 4 — Workout: Reflex Training
The third Workout activity tests reaction speed and accuracy.
Reflex Training
Five random letters appear one at a time.
```text
REFLEX TRAINING

Type each letter as quickly as possible!

READY...

K
> K

R
> R

P
> P
```
Accuracy Scoring
Each correct letter is worth:
1 point
Five letters gives a maximum of:
5 points
Speed Bonus
After all five letters:
Total Time	Bonus
4 seconds or less	+2
6 seconds or less	+1
More than 6 seconds	+0
Maximum Workout Score
Activity	Maximum
Precision	3
Button Mash	3
Reflex Accuracy	5
Reflex Speed Bonus	2
Total	13
Example:
```text
=========================
    WORKOUT COMPLETE
=========================

Precision:       3/3
Power:           3/3
Reflexes:        5/5
Speed Bonus:     +2

TOTAL:           13

Strength: 0 -> 13

Training Sessions Remaining: 2
```
The final Workout score is added directly to the player's Strength.
---
Chunk 5 — Education
Education is the Intellect equivalent of Workout.
Each Education session contains:
5 multiple-choice questions
Each correct answer gives:
+2 Intellect
A perfect score awards:
+3 bonus Intellect
Maximum possible:
13 Intellect
Example Question
```text
================================
       MEGA CORP UNIVERSITY
================================

Question 1 of 5

Which planet is closest to the Sun?

A. Venus
B. Mercury
C. Cleveland
D. Saturn

> B

CORRECT!

+2 Intellect
```
Normal Result
```text
Correct Answers: 4/5

Intellect Earned: +8

Intellect: 0 -> 8
```
Perfect Result
```text
PERFECT SCORE!

MEGA CORP reluctantly acknowledges
that you may actually be intelligent.

+3 PERFECT BONUS

TOTAL INTELLECT: +13
```
---
Chunk 6 — Training Session System
Each level gives the player:
3 Training Sessions
The player may spend them however they want.
Examples:
Workout / Workout / Workout
Study / Study / Study
Workout / Study / Workout
Study / Workout / Study
This allows the player to create different character builds.
When Training Reaches Zero
```text
TRAINING COMPLETE

MEGA CORP has determined that additional
training would cut into shareholder profits.

It's time for your Performance Evaluation.
```
Study and Workout become unavailable until the next level.
The player can still:
Shop
View Stats
Fight the Boss
The boss can also remain available before all three sessions are used.
This creates a risk/reward choice for players who want to fight early.
---
Chunk 7 — First Boss Prototype
Do not build multiple bosses yet.
The first complete boss is:
Gary the Aggressive Goose
Starting Boss Values
Property	Value
HP	100
Boss Timer	60 seconds
Player Starting Lives	3
Gary serves as the vertical slice for the entire combat system.
---
Chunk 8 — Basic Dodge Attack
Gary randomly attacks from one side.
Example:
```text
GARY CHARGES FROM THE LEFT!

DODGE RIGHT!

PRESS R!
```
Or:
```text
GARY CHARGES FROM THE RIGHT!

DODGE LEFT!

PRESS L!
```
Base Reaction Window
1.50 seconds
Success
Player avoids damage.
Failure
Player loses:
1 Life
---
Chunk 9 — Intellect Affects Reaction Time
Intellect represents the character thinking so quickly that combat feels slower.
Base reaction time:
1.50 seconds
Each Intellect point adds:
0.02 seconds
Maximum Intellect time bonus:
+1.50 seconds
Maximum reaction time:
3.00 seconds
Examples
Intellect	Reaction Time
0	1.50 sec
10	1.70 sec
25	2.00 sec
50	2.50 sec
75+	3.00 sec
This gives Intellect a direct effect on actual player difficulty.
---
Chunk 10 — Player Counterattacks
A successful dodge gives the player an opportunity to counterattack.
```text
PERFECT DODGE!

Gary crashes into a garbage can!

COUNTER ATTACK!

TYPE:

GOOSEPUNCH

>
```
Damage consists of three parts:
Random base damage
Strength bonus
Typing-speed bonus
---
Base Damage
Random value:
3–8 damage
---
Strength Bonus
Suggested formula:
Strength ÷ 5
Examples:
Strength	Damage Bonus
10	+2
25	+5
50	+10
100	+20
---
Typing Speed Bonus
Typing Time	Bonus
1.0 sec or less	+10
1.5 sec or less	+8
2.0 sec or less	+6
3.0 sec or less	+3
More than 3 sec	+0
Example:
```text
Base Damage:       6
Strength:         +5
Speed:            +8
--------------------
TOTAL DAMAGE:     19

Gary HP: 81/100
```
This creates the central combat formula:
Luck + Character Stats + Player Skill
---
Chunk 11 — Additional Boss Attacks
Once Gary's basic fight works, add more challenge types.
Dodge Left
```text
DODGE LEFT!

PRESS L!
```
Dodge Right
```text
DODGE RIGHT!

PRESS R!
```
Duck
```text
GARY TAKES FLIGHT!

DUCK!

PRESS D!
```
Block
```text
GARY GOES FOR YOUR EYES!

BLOCK!

TYPE: BLOCK
```
Combo
```text
COMBO ATTACK!

L
R
L
D
```
The player must enter each input in sequence.
More complex bosses can later introduce fake or misleading instructions.
---
Chunk 12 — Boss Timer
Gary begins with:
60 seconds
The player must reduce Gary to 0 HP before time expires.
If the timer reaches zero:
```text
TIME: 0.00

Gary suddenly stops attacking.

He looks at his watch.

"Oh. Would you look at that."

THE UNIVERSE EXPLODES.

YOU DIED.
```
The boss timer is important because it prevents Intellect-heavy builds from becoming unbeatable.
High Intellect
Easier reactions
Lower damage
More attacks required
High Strength
Harder reactions
Higher damage
Fewer attacks required
Balanced Build
Moderate reaction window
Moderate damage
---
Chunk 13 — Win and Lose Conditions
Player Wins When
Boss HP reaches 0 before the timer reaches 0
Player Loses When
Either:
Lives reach 0
or:
Boss timer reaches 0
Death Penalty
For the first version:
-$50
If the player has less than $50:
Money becomes $0
The player may retry the boss.
Avoid permanent death initially.
---
Chunk 14 — Boss Rewards and Leveling
Example victory:
```text
================================
        BOSS DEFEATED!
================================

Gary the Aggressive Goose has fallen.

Probably.

REWARDS

Cash:          +$150
Strength:      +3
Intellect:     +3
Luck:          +1

PROMOTION!

Level 1 -> Level 2

NEW TITLE:

JUNIOR ASSISTANT HERO
```
Boss Victory Effects
Level increases by 1
Bosses Defeated increases by 1
Training Sessions reset to 3
Player receives money
Player receives Strength
Player receives Intellect
Player receives Luck
Next boss unlocks
---
Chunk 15 — Luck
Luck cannot normally be trained.
It primarily increases through:
Boss victories
Random events
Special items
Critical Hit System
Base critical chance:
5%
Each Luck point adds:
+1%
Maximum critical chance:
25%
Critical hit damage:
Normal Damage × 2
Example:
```text
CRITICAL HIT!

Base:        7
Strength:   +5
Speed:      +8

20 DAMAGE x2!

40 DAMAGE!
```
---
Chunk 16 — Money and Shop
Money should provide useful temporary advantages.
Start with five items.
Item	Cost	Effect
Energy Drink	$75	+0.5 sec reaction time during next boss
Protein Shake	$75	+5 damage per attack during next boss
Lucky Sock	$100	+10% critical chance during next boss
Extra Life	$200	+1 life during next boss
Mystery Meat	$50	Random effect
Most shop effects should disappear after the next boss fight.
This keeps permanent progression focused on the main character stats and makes balancing easier.
---
Chunk 17 — Mystery Meat
Mystery Meat costs:
$50
Possible outcomes:
Chance	Effect
20%	+10 temporary Strength
20%	+10 temporary Intellect
20%	+1 temporary Life
15%	+$100
15%	Nothing
10%	-1 temporary Life
Example:
```text
You eat the Mystery Meat.

It tastes like chicken.

Not GOOD chicken.

+10 Strength
```
Negative example:
```text
You eat the Mystery Meat.

Your body immediately files a complaint.

-1 Life
```
---
Chunk 18 — Random Events
After each training session:
25% chance of a random event
Random events add unpredictability and humor.
Wallet
```text
You find a wallet containing $75.

MEGA CORP's ethics training recommends
turning it in.

Fortunately, you haven't taken ethics training.

+$75
```
Wizard
A mysterious wizard challenges the player to:
Rock / Paper / Scissors
Possible reward:
+$100
Possible loss:
-$25
Vending Machine
```text
The vending machine gives you TWO drinks.

Luck +1
```
Goose Sighting
```text
You see Gary staring at you from across
the parking lot.

Nothing happens.

Somehow this is worse.
```
Some events should intentionally have no gameplay effect.
This prevents every random event from feeling like a predictable reward screen.
---
Chunk 19 — Titles and Promotions
Titles provide progression without affecting game balance.
Level	Title
1	Unpaid Hero Intern
2	Junior Assistant Hero
3	Assistant to the Hero
4	Associate Hero
5	Certified Violence Technician
6	Senior Hero
7	Executive Hero
8	Vice President of Punching
9	Chief Combat Officer
10	CEO Candidate
11	CEO of Destiny
---
Chunk 20 — Boss Progression
Only create additional bosses once Gary is fun to fight repeatedly.
Each boss should introduce at least one new mechanic instead of simply increasing HP.
Level	Boss	HP	Time	Main Mechanic
1	Gary the Aggressive Goose	100	60 sec	Basic combat
2	Chad the Gym Bro	140	60 sec	Button mashing
3	Karen, Destroyer of Customer Service	175	55 sec	Fake instructions
4	Professor Skullsworth	210	60 sec	Trivia attacks
5	The IRS Audit Beast	250	55 sec	Number/math challenges
6	Cybernetic Abraham Lincoln	300	50 sec	Combos
7	Jeff, Devourer of Worlds	350	50 sec	Random mechanics
8	The Moon	425	45 sec	Slow but devastating attacks
9	Evil You	Variable	50 sec	Uses player's build
10	THE CEO	600	60 sec	Uses everything
These HP and time values are starting points.
They should be adjusted through playtesting.
---
Chunk 21 — Training Difficulty Scaling
Training becomes harder as levels increase.
Precision Word Length
Levels	Word Length
1–2	4–6 letters
3–5	6–9 letters
6–8	8–12 letters
9–10	10–15 letters
Possible later words might include:
THUNDER
BARBELL
SUPPLEMENTS
BODYBUILDER
UNSTOPPABLE
PHOTOSYNTHESIS
---
Button Mash Scaling
Early:
20 presses / 5 sec
Middle:
25 presses / 5 sec
Later:
30 presses / 5 sec
Possible maximum:
35 presses / 5 sec
Avoid scaling this indefinitely.
At some point increased button-mashing becomes annoying rather than more skillful.
---
Reflex Scaling
Early levels:
Letters only
Later levels:
Letters and numbers
Example:
```text
G

7

K

2

P
```
Possible advanced versions can include short sequences.
---
Chunk 22 — Character Builds
Allowing players to freely choose their three training sessions creates different strategies.
Strength Build
Example:
Strength: 70  
Intellect: 15
Advantages:
Heavy damage
Bosses die quickly
Disadvantages:
Short reaction windows
Greater player skill required
---
Intellect Build
Example:
Strength: 18  
Intellect: 72
Advantages:
Long reaction windows
Easier dodging and typing
Disadvantages:
Low damage
More attacks needed
Greater risk of losing to the boss timer
---
Balanced Build
Example:
Strength: 45  
Intellect: 45
Advantages:
Good reaction time
Good damage
Flexible against different bosses
The goal is for all three approaches to remain viable.
---
Chunk 23 — Saving and Loading
Do not build saving until the core game is fun.
Title Screen
```text
================================
       INTERN OF DESTINY
================================

1. New Game
2. Continue
3. Quit
```
Suggested Save Data
Save:
Player Name
Level
Strength
Intellect
Luck
Money
Bosses Defeated
Training Sessions Remaining
You may also eventually save:
Current title
Purchased permanent unlocks
Game completion status
High scores
Fastest boss kills
---
Recommended Development Order
Build the game in this order:
Player creation, stats, and main menu
Workout — Precision
Workout — Button Mash
Workout — Reflex
Education and trivia
Three-training-session system
Gary boss screen and HP
Timed dodge mechanic
Timed typing attack
Strength affecting damage
Intellect affecting reaction time
Boss countdown timer
Lives and death
Boss victory and level advancement
Luck and critical hits
Money and shop
Temporary items
Mystery Meat
Random events
Titles and promotions
Second boss
Remaining bosses
Training difficulty scaling
Save/load system
Balancing and polish
---
First Major Milestone
The first serious target should be a completely playable Level 1.
That version should include:
New player creation
Stats
Main menu
Three training sessions
Workout
Education
Strength
Intellect
Gary the Aggressive Goose
Dodge challenges
Typing counterattacks
Lives
Boss HP
Boss timer
Victory
Defeat
Level-up rewards
Do not build Boss #2 until fighting Gary repeatedly is actually fun.
Gary should function as the game's vertical slice.
If this loop is enjoyable:
Train → Build Character → Fight Gary → Win → Receive Rewards → Level Up
then the core game design has been proven.
Everything after that is primarily:
New content
New boss mechanics
More randomness
More progression
More polish
---
Initial Balance Targets
The first playtesting goal should be making three Level 1 builds reasonably viable.
Maximum Strength Build
Three perfect Workouts:
39 Strength
Expected strengths:
High damage
Shorter reaction time
Fewer successful attacks required
---
Maximum Intellect Build
Three perfect Education sessions:
39 Intellect
Expected strengths:
Significantly longer reaction windows
Lower damage
More successful attacks required
---
Balanced Build
Example:
2 Workouts
1 Education
or:
1 Workout
2 Education
Expected strengths:
Moderate damage
Moderate reaction time
Safer general-purpose strategy
All three approaches should have a realistic path to defeating Gary.
If one strategy is clearly superior, adjust:
Strength damage scaling
Intellect reaction-time scaling
Gary's HP
Boss timer
Speed bonuses
Critical-hit chance
---
Core Design Principle
Every major combat outcome should ideally involve some combination of:
Player Skill + Character Progression + Luck
For example:
```text
TOTAL DAMAGE =
Random Base Damage
+ Strength Bonus
+ Player Typing Speed Bonus
```
Then Luck may convert that attack into a critical hit.
This prevents the game from becoming purely random, purely stat-based, or purely a typing test.
The player's character matters.
The player's actual performance matters.
And unpredictable moments still happen.
That combination is the foundation of Intern of Destiny.