using System.Diagnostics;

namespace InternOfDestiny
{
    internal class Study
    {
        private Player player;
        private Random rng = new Random(); // Random number generator for mini-games
        private Stopwatch studyTimer = new Stopwatch(); // Stopwatch to track the total time taken for the study
        private float studyTimeLimit = 30000; // 30 seconds time limit for the study
        private float questionTimeLimit = 10000; // 10 seconds time limit for each question
        private List<Questions> questionBank = Questions.GetQuestionbank(); // Get the question bank from the Questions class
        int totalQuestionsToAsk = 5;
        int score = 0;
        int bonusTimeScore = 0;
        int bonusScore = 0;
        int currentIntellect = 0;

        public Study(Player player)
        {
            this.player = player;
            currentIntellect = player.getIntellect();
        }

        internal void startStudy()
        {
            // Start the timer for the study session
            studyTimer.Start();
            startQuiz();
            // Stop the timer after the study session is complete
            studyTimer.Stop();
            // Check if the total time taken for the study session exceeds the time limit to get a bonus intellect increase of +2
            if (studyTimer.ElapsedMilliseconds <= studyTimeLimit && score == 10)
            {
                bonusTimeScore += 2;
                player.addIntellect(2);
            }
            if (score == 10)
            {
                bonusScore += 2;
                player.addIntellect(2);
            }
            displayStudyResults();


        }
        private void displayStudyResults()
        {
            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine("STUDY COMPLETE");
            Console.WriteLine("========================================\n");
            if (score == 10)
            {
                Console.WriteLine("PERFECT SCORE!\n");
                Console.WriteLine("MEGA CORP reluctantly acknowledges\r\nthat you may actually be intelligent.\r\n");
                Console.WriteLine("+2 INTELLECT BONUS AWARDED!\n");
                if (bonusTimeScore > 0)
                {
                    Console.WriteLine($"Time Bonus: {bonusTimeScore}\n");
                }

            }
            else
            {
                Console.WriteLine($"Correct Answers: {score / 2}/{totalQuestionsToAsk}\n");

            }
            Console.WriteLine($"Total: {bonusTimeScore + bonusScore + score}\n");
            Console.WriteLine($"Intellect: {currentIntellect} -> {player.getIntellect()}\n");
            Console.WriteLine($"Training Sessions Remaining: {player.getTrainingSessionsRemaining()}\n");
            Console.WriteLine("\nPress any key to return to the main menu...");
            Console.ReadKey(true);
        }
        internal void startQuiz()
        {
            var randomQuestions = questionBank.OrderBy(q => rng.Next()).Take(totalQuestionsToAsk).ToList();
            Stopwatch timer = new Stopwatch();
            Console.WriteLine("\nYou have chosen to study. You will be asked a series of questions. Answer them correctly to increase your intellect.");
            for (int i = 0; i < randomQuestions.Count; i++)
            {
                Questions currentQuestion = randomQuestions[i];
                currentQuestion.ShuffleOptions(rng); // Shuffle the options for each question

                Console.Clear();
                Console.WriteLine($"\nQuestion {i + 1} of {totalQuestionsToAsk}");
                Console.WriteLine(currentQuestion.Text);
                Console.WriteLine();

                for (int j = 0; j < currentQuestion.Options.Count; j++)
                {
                    Console.WriteLine($"{j + 1}. {currentQuestion.Options[j]}");
                }

                timer.Restart(); // Start the timer for this question
                string input = Console.ReadLine().Trim();
                timer.Stop(); // Stop the timer after the answer is given
                if (timer.ElapsedMilliseconds > questionTimeLimit)
                {
                    Console.WriteLine("Time's up! You took too long to answer.");
                    continue; // Skip to the next question
                }
                int selectedOption;
                if (int.TryParse(input, out selectedOption) && selectedOption >= 1 && selectedOption <= currentQuestion.Options.Count)
                {
                    if (currentQuestion.Options[selectedOption - 1] == currentQuestion.CorrectAnswer)
                    {
                        Console.WriteLine("Correct!");
                        score += 2;
                        player.addIntellect(2); // Increase player's intellect for correct answer
                    }
                    else
                    {
                        Console.WriteLine($"Incorrect! The correct answer was: {currentQuestion.CorrectAnswer}");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number corresponding to the options.");
                }
            }
        }
    }
}