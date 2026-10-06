using System.Diagnostics;

namespace InternOfDestiny
{
    internal class CombatManager
    {
        private Player player;
        private Boss boss;
        private Random random = new Random();
        private Stopwatch stopwatch = new Stopwatch();

        public CombatManager(Player player, Boss boss)
        {
            this.player = player;
            this.boss = boss;
        }

        internal void startFight()
        {
            stopwatch.Start();
            while(player.getLives() > 0 && boss.getCurrentHealth() > 0 && stopwatch.ElapsedMilliseconds < boss.getTimeLimitMS())
            {
                Console.WriteLine($"\n{boss.getName()} has {boss.getCurrentHealth()} health remaining.");
                Console.WriteLine($"You have {player.getLives()} lives remaining.\n");
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey(true);
                if (chooseBossAttack())
                {
                    choosePlayerAttack();
                }

            }
            if (player.getLives() <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nYou have been defeated by the boss!");
                Console.ResetColor();
                // Handle player defeat by resetting player stats to begging of level stats.
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey(true);
                player.playerDefeated();
                
            }
            else if (boss.getCurrentHealth() <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nYou have defeated {boss.getName()}!");
                Console.ResetColor();
                // Handle boss defeat (e.g., rewards, next level, etc.)
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey(true);
                player.levelUp();
                
            }
            else if (stopwatch.ElapsedMilliseconds >= boss.getTimeLimitMS())
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nTime's up! {boss.getName()} has won the battle!");
                Console.ResetColor();
                // Handle time limit exceeded (e.g., boss wins, player loses, etc.)
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey(true);
                player.playerDefeated();
                
                
            }
            
            
        }

        // This method will eventually choose a random attack for the boss to perform but based on the level, with each boss level more attack options open up.
        internal bool chooseBossAttack()
        {
            return runDodgeAttack();
        }
        // This method will eventually choose a random attack for the player to perform but based on the level, with each player level more attack options open up.
        internal bool choosePlayerAttack()
        {
            return runTypeAttack();
        }

        internal bool runDodgeAttack()
        {
            // Randomly choose a direction for the boss attack then player must respond with "D" if boss attacks from the left, "A" if boss attacks from the right and "S" when he pokes your eyes.
            Console.Clear();
            Console.WriteLine("Dodge left with a and right with d and duck with s\nAre you ready?");
            Console.WriteLine("\nPress any key to start...");
            Console.ReadKey(true);
            int direction = random.Next(1, 3); // Randomly choose a direction (1-3)
            Stopwatch stopwatch = new Stopwatch();
            
            if (direction == 1)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{boss.getName()} attacks from the left!");
                Console.ResetColor();
                stopwatch.Start();
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                stopwatch.Stop();
                char input = char.ToUpper(keyInfo.KeyChar);

                if (input == 'D' && calculateTime(stopwatch.ElapsedMilliseconds, 3000) > 0)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("You successfully dodged the attack!");
                    Console.ResetColor();
                    return true; // Return 1 for successful dodge
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("You failed to dodge the attack!");
                    Console.ResetColor();
                    // Calculate damage to player
                    player.loseLife();
                    return false; // Return 0 for failed dodge
                }
            }
            else if (direction == 2)
            {
                Console.WriteLine($"{boss.getName()} attacks from the right!");
                stopwatch.Start();
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                stopwatch.Stop();
                char input = char.ToUpper(keyInfo.KeyChar);
                if (input == 'A' && calculateTime(stopwatch.ElapsedMilliseconds, 3000) > 0)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("You successfully dodged the attack!");
                    Console.ResetColor();
                    return true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("You failed to dodge the attack!");
                    Console.ResetColor();
                    // Calculate damage to player
                    player.loseLife();
                    return false;
                }
            }
            else
            {
                Console.WriteLine($"{boss.getName()} cowardly pokes at your eyes!");
                stopwatch.Start();
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                stopwatch.Stop();
                char input = char.ToUpper(keyInfo.KeyChar);
                if (input == 'S' && calculateTime(stopwatch.ElapsedMilliseconds, 3000) > 0)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("You successfully dodged the attack!");
                    Console.ResetColor();
                    return true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("You failed to dodge the attack!");
                    Console.ResetColor();
                    // Calculate damage to player
                    player.loseLife();
                    return false;
                }
            }
        }
        // This method calculates the time allowed for a dodge based on intellect and also luck. The base 1500ms and intellect and luck with increase that amount.
        private int calculateTime(long elapsedMilliseconds, int baseTime)
        {
            int time = baseTime; // Base time in milliseconds
            time += player.getIntellect() * 10; // Increase time based on intellect
            time += player.getLuck() * 100; // Increase time based on luck
            return time - (int)elapsedMilliseconds;
        }
        // This method will have the player type a random word or phrase to attack the boss. The faster they type the more damage they do. If they type incorrectly they do no damage. Strength adds bonus damage and luck influences the random base damage of the roll. Intellect influences the amount of time you have to type the phrase. The left over amount of time is added as bonus damage.
        internal bool runTypeAttack()
        {
            Console.Clear();
            Stopwatch stopwatch = new Stopwatch();
            Console.WriteLine("Get ready to type the word, the faster the more damage!");
            Console.WriteLine("\nPress any key to start...");
            Console.ReadKey(true);

            Console.Write($"COUNTER ATTACK! Time: {calculateTime(0, 4000)}ms\n\n TYPE: ");
            Console.ForegroundColor = ConsoleColor.Green;
            string phrase = getRandomPhrase();
            Console.WriteLine($"{phrase}");
            Console.ResetColor();

            stopwatch.Start();
            string input = Console.ReadLine().Trim().ToLower();
            stopwatch.Stop();
            Console.WriteLine($"Time: {stopwatch.ElapsedMilliseconds}");

            if (input == phrase.ToLower() && calculateTime(stopwatch.ElapsedMilliseconds, 4000) > 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("You successfully typed the phrase!");
                Console.ResetColor();

                // Calculate damage to boss
                int baseDamage = random.Next(3, 9) + (int)(player.getStrength()*0.25) + player.getLuck();
                int bonusDamage = (int)(calculateTime(stopwatch.ElapsedMilliseconds, 4000) / 100); // Bonus damage based on time left
                int totalDamage = baseDamage + bonusDamage;
                boss.takeDamage(totalDamage);
                Console.WriteLine($"You dealt {totalDamage} damage to {boss.getName()}!");
                return true;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You failed to type the phrase correctly!");
                Console.ResetColor();
                // No damage to boss
                return false;
            }

        }

        private String getRandomPhrase()
        {
            int playerLevel = player.getLevel();
            string[][] phrasesByLevel = new string[][]
            {
                new string[] { }, // Level 0 (unused spacer)

                // Level 1: Single Words
                new string[10] { "Hello", "Welcome", "Running", "Jump", "Code", "Success", "Future", "Victory", "Bright", "Quest" },

                // Level 2: Two Words
                new string[10] { "Stay strong", "Keep moving", "Good luck", "Time flies", "Think big", "Work hard", "Dream on", "Fear less", "Press start", "Game over" },

                // Level 3: Three Words
                new string[10] { "Never give up", "You can win", "Make it count", "Live your life", "Practice makes perfect", "Focus on goals", "Time is money", "Learn every day", "See the world", "Find your way" },

                // Level 4: Four Words
                new string[10] { "Actions speak louder now", "Knowledge is true power", "Fortune favors the bold", "Every cloud has silver", "No pain no gain", "Time heals all wounds", "Look before you leap", "Haste makes total waste", "Birds of a feather", "Better late than never" },

                // Level 5: Five Words
                new string[10] { "The quick brown fox jumps", "All that glitters is gold", "Beauty is in the eye", "Honesty is the best policy", "Laughter is the best medicine", "Practice what you preach daily", "Rome wasn't built in day", "Two wrongs don't make right", "A picture paints thousand words", "Where there's will there's way" }
        
                // You can continue adding arrays for Levels 6 through 10 below following this exact pattern...
            };

            if (playerLevel < 1)
            {
                playerLevel = 1;
            }
            if (playerLevel >= phrasesByLevel.Length)
            {
                playerLevel = phrasesByLevel.Length - 1;
            }
            string[] availablePhrases = phrasesByLevel[playerLevel];
            int selectPhrase = random.Next(0, availablePhrases.Length);
            // Placeholder implementation - replace with actual phrase generation logic
            return availablePhrases[selectPhrase];
        }

        internal void calculateDamage()
        {

        }

        internal void checkCriticalHit()
        {
        }

    }
}