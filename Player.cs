using System.Text;

namespace InternOfDestiny
{
    internal class Player
    {
        private string name;
        private int level;
        private int strength;
        private int levelStrength;
        private int intellect;
        private int levelIntellect;
        private int luck;
        private int levelLuck;
        private float money;
        private float levelMoney;
        private int lives;
        private int bossesDefeated;
        private int levelBossesDefeated;
        private int trainingSessionsRemaining;
        private string title;

        public Player(string name)
        {
            this.name = name;
            this.level = 1;
            this.strength = 0;
            this.intellect = 0;
            this.luck = 0;
            this.money = 50.0f;
            this.levelMoney = this.money;
            this.lives = 3;
            this.bossesDefeated = 0;
            this.levelBossesDefeated = this.bossesDefeated;
            this.trainingSessionsRemaining = 3;
            getNewTitle();
        }

        internal void DisplayStats()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine($"\n========================================\n{name} - {title}\n========================================");
            Console.WriteLine($"{"Level:",-15} {level}");
            // Red Lives
            Console.Write($"{"Lives:",-15} ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(getHearts());
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine($"{"Strength:",-15} {strength}");
            Console.WriteLine($"{"Intellect:",-15} {intellect}");
            // Green Luck
            Console.Write($"{"Luck:",-15} ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(luck);
            Console.ResetColor();
            Console.WriteLine();
            // Yellow Money
            Console.Write($"{"Money:",-15} ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"${money}");
            Console.ResetColor();
            Console.WriteLine("\n");
            Console.WriteLine($"{"Bosses Defeated:", -15} {bossesDefeated}\n");
            Console.WriteLine($"{"Training Sessions Remaining:",-30} {trainingSessionsRemaining}\n\n");
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey(true);
        }

        internal void addStrength(int amount)
        {
            strength += amount;
        }

        internal void addIntellect(int amount)
        {
            intellect += amount;
        }

        internal void addLuck(int amount)
        {
            luck += amount;
        }

        internal void addMoney(float amount)
        {
            money += amount;
        }
        internal void spendMoney(float amount)
        {
            money -= amount;
        }
        internal void loseLife()
        {
            lives--;
        }
        internal void addLife()
        {
            lives++;
        }
        internal void resetLives()
        {
            lives = 3;
        }
        internal void useTrainingSession()
        {
            trainingSessionsRemaining--;
        }
        internal bool hasTrainingSessionsRemaining()
        {
            return trainingSessionsRemaining > 0;
        }
        internal void levelUp()
        {
            level++;
            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine("YOU HAVE LEVELED UP");
            Console.WriteLine("========================================\n");
            Console.WriteLine("You have defeated the boss.\n");
            Console.WriteLine($"Your stats have increased and you are now level {level}.\n");
            
            strength += 2;
            intellect += 2;
            luck += 1;
            trainingSessionsRemaining = 3;
            bossesDefeated++;
            // Save the current level stats for reset purposes
            levelStrength = strength;
            levelIntellect = intellect;
            levelLuck = luck;
            levelMoney = money;
            levelBossesDefeated = bossesDefeated;

            getNewTitle();
            DisplayStats();
            

        }

        private void getNewTitle()
        {
            string[] title = { "Unpaid Hero Intern", "Junior Assistant Hero", "Assistant to the Hero", "Associate Hero", "Certified Violence Technician", "Senior Hero", "Executive Hero", "Vice President of Punching", "Chief Combat Officer", "CEO Candidate", "CEO of Destiny" };
            this.title = title[this.level - 1];
        }

        internal string getHearts()
        {
            return new string('♥', lives);
        }

        internal int getStrength()
        { 
         return strength;
        }

        internal int getTrainingSessionsRemaining()
        {
            return trainingSessionsRemaining;
        }

        internal int getIntellect()
        {
            return intellect;
        }

        internal int getLevel()
        {
            return level;
        }

        internal int getHealth()
        {
            return strength * 10;
        }

        internal float getMoney()
        {
            return money;
        }

        internal int getLuck()
        {
            return luck;
        }

        internal int getLives()
        {
            return lives;
        }
        internal void playerDefeated()
        {
            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine("YOU HAVE BEEN DEFEATED");
            Console.WriteLine("========================================\n");
            Console.WriteLine("You have been defeated by the boss.\n");
            Console.WriteLine("Your stats are being reset to the beginning of your current level.\n");
            resetLives();
            trainingSessionsRemaining = 3;
            strength = levelStrength;
            intellect = levelIntellect;
            luck = levelLuck;
            money = levelMoney;
            bossesDefeated = levelBossesDefeated;
            DisplayStats();

        }

        internal string getName()
        {
            return name;
        }
    }
}