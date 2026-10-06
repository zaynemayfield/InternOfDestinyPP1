using System.Diagnostics;
using System.Text;

namespace InternOfDestiny
{
    internal class Workout
    {
        private Player player; //Reference to the player object to update their stats
        private Random random = new Random(); // Random number generator for mini-games
        private Stopwatch workoutTimer = new Stopwatch(); // Stopwatch to track the total time taken for the workout
        private float workoutTimeLimit = 10000; // 10 seconds time limit for the mini-games
        private float buttonRushTimeLimit = 3000; // 3 seconds time limit for the button mash mini-game
        private float precisionTimeLimit = 3000; // 1.5 seconds time limit for the precision mini-game
        private int precisionScore = 0; // Score for the precision mini-game
        private int buttonRushScore = 0; // Score for the button rush mini-game
        private int reflexScore = 0; // Score for the reflex mini-game
        private int bonusPoints = 0; // Bonus points for completing the workout within the time limit
        private int currentStrength = 0; // Current strength of the player before the workout session

        public Workout(Player player)
        {
            this.player = player;
            this.currentStrength = player.getStrength();
        }

        internal void startWorkout()
        {
            // Start the workout timer
            workoutTimer.Start();
            // Run a mini game where the user has to type in the word that is given, but they cannot make a mistake, if they type a wrong letter, then they get no points and runButtonRush() is called next. There is also a time limit on it as well and must type it within the time limit.
            runPrecision();
            // Run a mini game where the user has to press space bar a specific amount of times within a time limit, if they press it too many or too few times, then they get no points and runButtonRush() is called next.
            runButtonRush();
            // Run a mini game where the user has to press a specific key when prompted, if they press the wrong key, then they get no points and the workout ends.
            runReflex();
            // Stop the workout timer
            workoutTimer.Stop();
            // run method to check if the total time was less than the time limit and if so award 2 bonus points.
            if (workoutTimer.ElapsedMilliseconds <= workoutTimeLimit)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n[✓] GREAT! You completed the workout session within the time limit! 2+ bonus!");
                Console.ResetColor();
                player.addStrength(2);
                bonusPoints += 2;
                Thread.Sleep(1000);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n[!] You completed the workout session, but exceeded the time limit. No bonus points awarded.");
                Console.ResetColor();
                Thread.Sleep(1000);
            }
            // Display the total score for the workout session
            displayWorkoutResults();

        }

        private void displayWorkoutResults()
        { 
            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine("WORKOUT COMPLETE");
            Console.WriteLine("========================================\n");

            Console.WriteLine($"Precision: {precisionScore}");
            Console.WriteLine($"Button Rush: {buttonRushScore}");
            Console.WriteLine($"Reflex: {reflexScore}");
            Console.WriteLine($"Speed Bonus: {bonusPoints}\n");
            int totalScore = precisionScore + buttonRushScore + reflexScore + bonusPoints;
            Console.WriteLine($"TOTAL: {totalScore}\n");
            Console.WriteLine($"Strength: {currentStrength} -> {player.getStrength()}\n");
            Console.WriteLine($"Training Sessions Remaining: {player.getTrainingSessionsRemaining()}\n");
            Console.WriteLine("\nPress any key to return to the main menu...");
            Console.ReadKey(true);
        }

        private void runReflex()
        {
            // Run a mini game where the user has to press a specific key when prompted 5 different times there should be no delay between presses, So once the correct or incorrect key has been pressed the next one is given. if they press the wrong key, then they get no point is given for that 1, once all five have been attempted then it will calculate the total and the workout ends.
            Console.Clear();
            // Display the button rush mini-game instructions
            Console.WriteLine("Reflex Training Mini-Game: Press the correct Key when prompted,\nyou get 5 chances, type as quickly as you can!\nPress enter to start.");
            ConsoleKeyInfo startKey = Console.ReadKey(true);
            if (startKey.Key != ConsoleKey.Enter)
            {
                Console.WriteLine("\nYou didn't press Enter! Game canceled... Just kidding! Let's try that again");
                // Wait for 1 second before restarting the mini-game
                Thread.Sleep(1000);
                runReflex();
            }
            else
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();
                int totalScore = 0;

                // Loop 5 times to prompt the user to press a random key
                for (int i = 0; i < 5; i++)
                {
                    // Generate a random key from A-Z
                    char randomKey = (char)random.Next('a', 'z' + 1);
                    Console.WriteLine($"\nPress the '{randomKey}' key!");
                    ConsoleKeyInfo pressedKey = Console.ReadKey(true);

                    // Check if the pressed key matches the random key
                    if (pressedKey.KeyChar == randomKey)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("[✓] Correct!");
                        Console.ResetColor();
                        totalScore++;
                    }

                    // If the pressed key does not match the random key, display an error message and continue to the next prompt
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"[X] Incorrect! You pressed '{pressedKey.KeyChar}' instead of '{randomKey}'.");
                        Console.ResetColor();
                    }
                }
                timer.Stop();
                Console.WriteLine($"\nYour total score is: {totalScore}/5");
                // If total time is less an 3 seconds then give a 2 point bonus only if they got all 5 correct, if they got less than 5 correct then no bonus is given.
                if (totalScore == 5 && timer.ElapsedMilliseconds <= 3000)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("[✓] PERFECT! You completed the reflex mini-game in record time! 2+ bonus!");
                    Console.ResetColor();
                    player.addStrength(totalScore+2);
                    reflexScore += totalScore+2;
                    Thread.Sleep(1000);
                }
                // If they got all 5 correct but took longer than 3 seconds then give them the normal 5 points.
                else if (totalScore == 5)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("[✓] GREAT! You completed the reflex mini-game!");
                    Console.ResetColor();
                    player.addStrength(totalScore);
                    reflexScore += totalScore;
                    Thread.Sleep(1000);
                }
                // If they got less than 5 correct but more than 0 then give them the points they earned.
                else if (totalScore > 0)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"[!] You completed the reflex mini-game with some errors. But still got {totalScore} strength.");
                    Console.ResetColor();
                    player.addStrength(totalScore);
                    reflexScore += totalScore;
                    Thread.Sleep(1000);
                }
                // If they got 0 correct then give them no points.
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("[X] You did not complete the reflex mini-game successfully. No strength gained.");
                    Console.ResetColor();
                    Thread.Sleep(1000);
                }

            }
        }

        private void runButtonRush()
        {
            // Run a mini game where the user has to press space bar a specific amount of times within a time limit, if they press it too many or too few times, then they get no points and runReflex() is called next.
            Console.Clear();
            // Display the button rush mini-game instructions
            Console.WriteLine("Button Rush Mini-Game: Press Spacebar a specific amount of times within the time limit! Press space to start.");
            ConsoleKeyInfo startKey = Console.ReadKey(true);
            if (startKey.Key != ConsoleKey.Spacebar)
            {
                Console.WriteLine("\nYou didn't press Spacebar! Game canceled... Just kidding! Let's try that again");
                // Wait for 1 second before restarting the mini-game
                Thread.Sleep(1000);
                runButtonRush();
            }
            else
            {
                // Generate a random number of times the user has to press the space bar
                int targetPresses = random.Next(5, 15);
                Console.WriteLine($"Press the Spacebar {targetPresses} times within {buttonRushTimeLimit / 1000} seconds!");
                Console.WriteLine("Start pressing now!");
                int pressCount = 0;
                Stopwatch timer = new Stopwatch();
                timer.Start();
                while (timer.ElapsedMilliseconds < buttonRushTimeLimit)
                {
                    if (Console.KeyAvailable)
                    {
                        ConsoleKeyInfo pressedKey = Console.ReadKey(true);
                        if (pressedKey.Key == ConsoleKey.Spacebar)
                        {
                            pressCount++;
                        }
                    }
                }
                timer.Stop();
                Console.WriteLine($"\nYou pressed the Spacebar {pressCount} times.");
                if (pressCount == targetPresses)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("[✓] PERFECT! You pressed the correct number of times.");
                    Console.ResetColor();
                    player.addStrength(2);
                    buttonRushScore += 2;
                    Thread.Sleep(1000);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"[X] MISTAKE! You needed to press {targetPresses} times, but you pressed {pressCount} times.");
                    Console.ResetColor();
                    Console.WriteLine("Workout ended. You get 0 points.");
                    return;
                }
            }
        }

        private void runPrecision()
        {
            // Implement the precision mini-game logic here. Introduce the user to the game, set up the timing, and handle user input. If the user presses the wrong key, they get no points and the workout ends.
            Console.Clear();
            // Display the precision mini-game instructions
            Console.WriteLine("Precision Mini-Game: Type the word exactly! No Mistakes! Press space to start.");
            ConsoleKeyInfo startKey = Console.ReadKey(true);
            if (startKey.Key != ConsoleKey.Spacebar)
            {
                Console.WriteLine("\nYou didn't press Spacebar! Game canceled... Just kidding! Let's try that again");
                // Wait for 1 second before restarting the mini-game
                Thread.Sleep(1000);
                runPrecision();
            }
            else
            {

                // Generate a random word from the list of workout-related words
                string[] workoutWords = new string[]
                    {
                        "PUSHUP",   "MUSCLE",   "DUMBBELL", "SQUAT",    "POWER",
                        "FITNESS",  "BURPEE",   "BARBELL",  "STRENGTH", "CARDIO",
                        "RUN",      "LUNGE",    "PLANK",    "AGILITY",  "STAMINA",
                        "ENDURANCE","STRETCH",  "WARMUP",   "HEALTH",   "ENERGY"
                    };

                // Randomly select a word from the list
                int randomIndex = random.Next(0, workoutWords.Length);
                string targetWord = workoutWords[randomIndex];
                Console.WriteLine($"TYPE THIS WORD: {targetWord}");
                Console.Write("Your input: ");
                Stopwatch timer = new Stopwatch();
                timer.Start();

                // Loop through each character in the target word and check if the user types it correctly
                for (int i = 0; i < targetWord.Length; i++)
                {
                    ConsoleKeyInfo pressedKey = Console.ReadKey(true);
                    char typedChar = pressedKey.KeyChar;
                    typedChar = char.ToUpper(typedChar);
                    if (typedChar != targetWord[i])
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"\n\n[X] MISTAKE! You typed '{pressedKey.KeyChar}' instead of '{targetWord[i]}'.");
                        Console.ResetColor();
                        Console.WriteLine("Workout ended. You get 0 points.");
                        return;
                    }
                    else
                    {
                        Console.Write(typedChar);
                    }
                }
                timer.Stop();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.OutputEncoding = Encoding.UTF8;
                Console.WriteLine($"\n\n[✓] PERFECT! Word completed successfully in {timer.ElapsedMilliseconds} ms.");
                Console.ResetColor();
                if (timer.ElapsedMilliseconds <= precisionTimeLimit)
                {
                    Console.WriteLine("You completed the precision mini-game within the time limit! You earn 3 Strength points.");
                    player.addStrength(3);
                    precisionScore += 3;
                    Thread.Sleep(1000);
                }
                else
                {
                    int timeExceeded = (int)((timer.ElapsedMilliseconds - precisionTimeLimit) / 500);
                    Console.WriteLine($"You completed the precision mini-game, but exceeded the time limit by {timeExceeded} ms. You earn {5 - timeExceeded} Strength points.");
                    player.addStrength(5 - timeExceeded);
                    precisionScore += 5 - timeExceeded;
                    Thread.Sleep(1000);

                }


            }
        }
    }
}