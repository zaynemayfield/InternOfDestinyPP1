namespace InternOfDestiny
{
    internal class Boss
    {
        private String name;
        private String title;
        private int maxHealth;
        private int currentHealth;
        private int timeLimitMS; // in milliseconds
        private int level;
        private float rewardMoney;
        private int rewardStrength;
        private int rewardIntellect;
        private int rewardLuck;

        public Boss(string name, string title, int maxHealth, int timeLimitMS, float rewardMoney, int rewardStrength, int rewardIntellect, int rewardLuck)
        {
            this.name = name;
            this.title = title;
            this.maxHealth = maxHealth;
            this.currentHealth = maxHealth;
            this.timeLimitMS = timeLimitMS;
            this.rewardMoney = rewardMoney;
            this.rewardStrength = rewardStrength;
            this.rewardIntellect = rewardIntellect;
            this.rewardLuck = rewardLuck;
        }

        internal int getCurrentHealth()
        {
            return currentHealth;
        }

        internal string getName()
        {
            return name;
        }

        internal long getTimeLimitMS()
        {
            return timeLimitMS;
        }

        internal object getTitle()
        {
            return title;
        }

        internal void takeDamage(int totalDamage)
        {
            currentHealth -= totalDamage;
            if (currentHealth < 0)
            {
                currentHealth = 0;
            }
        }
    }
}