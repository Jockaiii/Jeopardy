namespace Jeopardy
{
    class Program
    {
        static void Main(string[] args)
        {
            int round = 1; // Startar på runda 1.
            int[] userInput = new int[60]; // Lagrar val av kategori och poäng från användaren.

            JeopardyQuestions questions = new JeopardyQuestions(); // instansierar ett nytt objekt av JeopardyQuestions().
            JeopardyGame game = new JeopardyGame(); // instansierar ett nytt objekt av JeopardyGame().

            for (int i = 0; i <= 2; i++) // Kör all kod 2 gånger för 2 rundor. Struntar i 3:e rundan eftersom den ändå inte delar ut poäng. och det finns knappt några frågor för runda 3.
            {
                JeopardyGame.StartRound(round, game);

                questions.GetCategory(userInput, round); // Tillkallar en metod som slumpar och skriver ut 6 kategorier.

                while (game.categoriesDepleted < 6) // Forsätter be användaren om kategori, poäng, frågor och svar tills alla kategorierna är tomma på lediga frågor.
                {
                    game.PrintCategories(questions); // Tillkallar en metod som skriver ut kategorierna.
                    game.CategoryInput(userInput, questions); // Tillkallar en metod som hämtar användarens val av kategori.

                    questions.GetPoints(round, userInput, game.pos); // Hämtar poängen för frågorna.
                    game.PrintPoints(questions, userInput); // Tillkallar en metod som skriver ut poängen.
                    game.PointsInput(userInput, questions); // Tillkallar en metod som tar in användarens val av poäng.

                    questions.GetQuestion(userInput, game.pos); // Tillkallar en metod som hämtar frågan beroende på vad användaren har valt för kategori och poäng.
                    game.PrintQuestion(questions); // Tillkallar en metod som skriver ut den hämtade frågan.
                    questions.CheckAnswer(JeopardyGame.AnswerInput(), userInput, game, game.pos);  // Kollar ifall svaret är korrekt och tilldelar poäng beroende på det.

                    game.CategoryDepleted(userInput, questions); // Tillkallar för att se till så att man inte kan välja en depleted kategori.
                } 

                round++; // ökar round så att jag kan hämta kategorier, poäng, inputs och svar på nästa runda.
            }
        }
    }
}
// TODO Bakgrund:
// Fixa så att man måste lägga till en fråga med svaret
// Fixa så att om 2 frågor i samma kategori har samma poäng så måste jag kolla på "daily double" för att hämta rätt fråga och inte bara en fråga med samma poäng.
// Fixa så att jag inte initsierar StreamReader varje gång jag använder det i en metod. Göra en StreamReader() overload som skickar tillbaks kategorier, frågor eller svar beroende på parametern som skickas in
// Fixa så att man kan spela mer än en spelare? botar eller en annan mänsklig spelare?

//TODO Visuellt:
// Fixa så att man ser ifall frågan har valts och ifall kategorin har slut på ovalda frågor
// Fixa så svar inte kräver bindestreck, apostrofer och paranteser. Samt att man inte måste skriva t.ex "an apple". utan bara "apple".
// Array för att hålla koll på spelares resultat
// Variabel för att hålla koll på tur