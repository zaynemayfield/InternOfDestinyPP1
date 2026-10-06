
using System.Numerics;

namespace InternOfDestiny
{
    internal class Game
    {
        internal void start()
        {
            Console.WriteLine("========================================");
            Console.WriteLine("INTERN OF DESTINY");
            Console.WriteLine("========================================");
            Console.WriteLine("\nCongratulations!");
            Console.WriteLine("\nDue to an unfortunate clerical error, you have\r\nbeen selected for MEGA CORP's Hero Internship\r\nProgram.");
            Console.WriteLine("\nSurvival is technically optional.");
            Console.WriteLine("\nWhat is your name?");

            // Read the player's name from the console input and if empty or whitespace, assign "Interner" as the default name
            string input = Console.ReadLine();
            string name;
            if (string.IsNullOrWhiteSpace(input))
            {
                name = "Interner";
                Console.WriteLine("I see how it is. You want to be a nameless intern.\nVery well, you shall be known as Interner.");
                Console.WriteLine("\nPress any key to return to continue...");
                Console.ReadKey(true);
            }
            else
            {
                name = input.Trim();
            }

            // Create a new Player object with the provided name
            Player player = new Player(name);
            Console.Clear();
            player.DisplayStats();
            TrainingManager trainingManager = new TrainingManager(player);
            getMenu(trainingManager, player);
        }

        private void getMenu(TrainingManager trainingManager, Player player)
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("===========================================================================");
                Console.WriteLine($"MEGA CORP Menu");
                Console.WriteLine("===========================================================================\n");
                Console.ResetColor();
                if (!player.hasTrainingSessionsRemaining())
                {
                    Console.WriteLine($"TRAINING COMPLETE {player.getName().ToUpper()}");
                    Console.WriteLine("MEGA CORP has determined that additional\r\ntraining would cut into shareholder profits.\r\n\r\nIt's time for your Performance Evaluation.\r\n");
                    Console.WriteLine("3. Shop");
                    Console.WriteLine("4. View Stats");
                    Console.WriteLine("5. Fight Boss");
                    Console.WriteLine("6. Quit");
                }
                else
                {
                    Console.WriteLine($"Choose your next step {player.getName()}");
                    Console.WriteLine("1. Study");
                    Console.WriteLine("2. Work Out");
                    Console.WriteLine("3. Shop");
                    Console.WriteLine("4. View Stats");
                    Console.WriteLine("5. Fight Boss");
                    Console.WriteLine("6. Quit");
                }
                string input = Console.ReadLine().Trim();
                if (!player.hasTrainingSessionsRemaining() && (input == "1" || input == "2"))
                {
                    input = "invalid"; // Force invalid input if training sessions are complete and player tries to select Study or Work Out
                }
                switch (input)
                {
                    case "1":
                        // Study
                        trainingManager.Study();
                        break;
                    case "2":
                        // Work Out
                        trainingManager.WorkOut();
                        break;
                    case "3":
                        // Shop
                        Console.WriteLine("Welcome to the MEGA CORP Shop! Or not!\nIt's still coming soon...");
                        Console.WriteLine("\nPress any key to return to the main menu...");
                        Console.ReadKey(true);
                        getMenu(trainingManager, player);
                        break;
                    case "4":
                        // View Stats
                        player.DisplayStats();
                        getMenu(trainingManager, player);
                        break;
                    case "5":
                        // Fight Boss
                        Boss boss = getBoss(player);
                        CombatManager combatManager = new CombatManager(player, boss);
                        combatManager.startFight();

                        break;
                    case "6":
                        // Quit
                        Console.WriteLine("Thanks for playing!");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Think you are a wise guy, huh?\nChoose the correct input next time!");
                        Console.WriteLine("\nPress any key to return to the main menu...");
                        Console.ReadKey(true);
                        getMenu(trainingManager, player);
                        break;
                }
            }
        }

        internal Boss getBoss(Player player)
        {
            Boss boss = null;
            switch (player.getLevel())
            {
                case 1:
                    boss = new Boss("Gary", "The Aggressive Goose", 100, 60000, 150, 3, 3, 1);
                    break;
                case 2:
                    boss = new Boss("Chad", "The Gym Bro", 140, 60000, 300, 3, 3, 1);
                    break;
                case 3:
                    boss = new Boss("Karen", "Destroyer of Customer Service", 175, 60000, 450, 3, 3, 1);
                    break;
                case 4:
                    boss = new Boss("Professor", "Skullsworth", 210, 60000, 600, 3, 3, 1);
                    break;
                case 5:
                    boss = new Boss("The IRS", "Audit Beast", 250, 60000, 750, 3, 3, 1);
                    break;
                case 6:
                    boss = new Boss("Cybernetic", "Abraham Lincoln", 300, 60000, 900, 3, 3, 1);
                    break;
                case 7:
                    boss = new Boss("Jeff", "Devourer of Worlds", 350, 60000, 1050, 3, 3, 1);
                    break;
                case 8:
                    boss = new Boss("The", "Moon", 425, 60000, 1200, 3, 3, 1);
                    break;
                case 9:
                    boss = new Boss("Evil", "You", player.getHealth(), 60000, player.getMoney(), 3, 3, 1);
                    break;
                case 10:
                    boss = new Boss("THE", "CEO", 600, 60000, 100000, 3, 3, 1);
                    break;
                default:
                    boss = new Boss("Unknown", "Unknown", 100, 60000, 150, 3, 3, 1);
                    break;
            }
            return boss;
        }
    }
}
