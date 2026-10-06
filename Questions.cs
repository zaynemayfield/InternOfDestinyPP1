namespace InternOfDestiny
{
    internal class Questions
    {
        public string Text { get; set; }
        public List<string> Options { get; set; }
        public string CorrectAnswer { get; set; }

        public Questions(string text, List<string> options, string correctAnswer)
        {
            Text = text;
            Options = options;
            CorrectAnswer = correctAnswer;
        }

        public void ShuffleOptions(Random rng)
        {
            int n = Options.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                string value = Options[k];
                Options[k] = Options[n];
                Options[n] = value;
            }
        }

        public static List<Questions> GetQuestionbank()
        {
            return new List<Questions>
            {
            // Your original 5 questions
            new Questions("What is the capital of France?", new List<string> { "Berlin", "Madrid", "Paris", "Rome" }, "Paris"),
            new Questions("What is 2 + 2?", new List<string> { "3", "4", "5", "6" }, "4"),
            new Questions("What is the largest planet in our solar system?", new List<string> { "Earth", "Jupiter", "Mars", "Saturn" }, "Jupiter"),
            new Questions("Who wrote 'To Kill a Mockingbird'?", new List<string> { "Harper Lee", "Mark Twain", "Ernest Hemingway", "F. Scott Fitzgerald" }, "Harper Lee"),
            new Questions("What is the chemical symbol for water?", new List<string> { "H2O", "O2", "CO2", "NaCl" }, "H2O"),

            // 25 New 10th-grade level questions
            new Questions("Who was the first President of the United States?", new List<string> { "Thomas Jefferson", "Abraham Lincoln", "George Washington", "John Adams" }, "George Washington"),
            new Questions("What is the powerhouse of the cell?", new List<string> { "Nucleus", "Mitochondria", "Ribosome", "Golgi Apparatus" }, "Mitochondria"),
            new Questions("Which gas do plants absorb from the atmosphere for photosynthesis?", new List<string> { "Oxygen", "Nitrogen", "Carbon Dioxide", "Hydrogen" }, "Carbon Dioxide"),
            new Questions("What is the square root of 144?", new List<string> { "10", "12", "14", "16" }, "12"),
            new Questions("In what year did World War II end?", new List<string> { "1918", "1939", "1941", "1945" }, "1945"),

            new Questions("Which of these is the primary author of the Declaration of Independence?", new List<string> { "George Washington", "Thomas Jefferson", "Benjamin Franklin", "Alexander Hamilton" }, "Thomas Jefferson"),
            new Questions("What is the closest star to Earth?", new List<string> { "Proxima Centauri", "The Sun", "Sirius", "Betelgeuse" }, "The Sun"),
            new Questions("Who painted the 'Mona Lisa'?", new List<string> { "Vincent van Gogh", "Pablo Picasso", "Leonardo da Vinci", "Claude Monet" }, "Leonardo da Vinci"),
            new Questions("What type of angle is exactly 90 degrees?", new List<string> { "Acute", "Obtuse", "Straight", "Right" }, "Right"),
            new Questions("What is the chemical symbol for Gold?", new List<string> { "Gd", "Ag", "Au", "Fe" }, "Au"),

            new Questions("Which play features the feuding families Capulet and Montague?", new List<string> { "Hamlet", "Macbeth", "Romeo and Juliet", "Othello" }, "Romeo and Juliet"),
            new Questions("How many continents are there on Earth?", new List<string> { "5", "6", "7", "8" }, "7"),
            new Questions("What is the process by which liquid water turns into gas?", new List<string> { "Condensation", "Evaporation", "Sublimation", "Freezing" }, "Evaporation"),
            new Questions("Who developed the theory of relativity?", new List<string> { "Isaac Newton", "Albert Einstein", "Galileo Galilei", "Nikola Tesla" }, "Albert Einstein"),
            new Questions("What is the capital of the United States?", new List<string> { "New York City", "Los Angeles", "Washington, D.C.", "Philadelphia" }, "Washington, D.C."),

            new Questions("Which ocean is the largest on Earth?", new List<string> { "Atlantic Ocean", "Indian Ocean", "Arctic Ocean", "Pacific Ocean" }, "Pacific Ocean"),
            new Questions("What is the main function of red blood cells?", new List<string> { "Fight infections", "Clot blood", "Transport oxygen", "Digest nutrients" }, "Transport oxygen"),
            new Questions("Solve for x: 3x - 5 = 10.", new List<string> { "3", "5", "15", "30" }, "5"),
            new Questions("Who is known for his famous 'I Have a Dream' speech?", new List<string> { "Malcolm X", "John F. Kennedy", "Martin Luther King Jr.", "Rosa Parks" }, "Martin Luther King Jr."),
            new Questions("Which layer of the Earth is located directly beneath the crust?", new List<string> { "Inner Core", "Outer Core", "Mantle", "Atmosphere" }, "Mantle"),

            new Questions("What is the primary language spoken in Brazil?", new List<string> { "Spanish", "Portuguese", "French", "English" }, "Portuguese"),
            new Questions("Which element has the atomic number 1?", new List<string> { "Helium", "Oxygen", "Hydrogen", "Carbon" }, "Hydrogen"),
            new Questions("Who wrote the dystopian novel '1984'?", new List<string> { "George Orwell", "Ray Bradbury", "Aldous Huxley", "J.K. Rowling" }, "George Orwell"),
            new Questions("What type of rock is formed by the cooling and solidification of magma or lava?", new List<string> { "Sedimentary", "Metamorphic", "Igneous", "Sandstone" }, "Igneous"),
            new Questions("How many states make up the United States of America?", new List<string> { "48", "49", "50", "51" }, "50")
        };
        }
    }
}