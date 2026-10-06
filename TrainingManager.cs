namespace InternOfDestiny
{
    internal class TrainingManager
    {
        private Player player;

        public TrainingManager(Player player)
        {
            this.player = player;
        }

        internal void Study()
        {   
            if (player.hasTrainingSessionsRemaining())
            {
                player.useTrainingSession();
                Study study = new Study(player);
                study.startStudy();
                // Maybe add a ranom trigger event later that can happen after studying, like a random encounter or a mini-game.
            }
            else
            {
                Console.WriteLine("\nYou have no training sessions remaining. You must challenge the boss to earn more sessions.");
            }
        }

        internal void WorkOut()
        {
            if (player.hasTrainingSessionsRemaining())
            {
                player.useTrainingSession();
                Workout workout = new Workout(player);
                workout.startWorkout();
                // Maybe add a ranom trigger event later that can happen after working out, like a random encounter or a mini-game.
            }
            else
            {
                Console.WriteLine("\nYou have no training sessions remaining. You must challenge the boss to earn more sessions.");
            }
        }
    }
}