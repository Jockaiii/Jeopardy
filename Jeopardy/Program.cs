using System;

namespace Jeopardy
{ 
    class Program
    {
        static void Main(string[] args)
        {
            int round = 1; // Startar på runda 1
            int[] userInput = new int[100]; // Alla lagrade inputs från användaren. kategorier och antal poäng. Antal kan variera så har 100 för säkerhets skull.

            JeopardyQuestions questions = new JeopardyQuestions(); // instansierar ett nytt objekt av JeopardyQuestions()
            JeopardyGame game = new JeopardyGame(); // instansierar ett nytt objekt av JeopardyGame()

            for (int i = 0; i <= 3; i++) // Kör all kod 3 gånger för 3 rundor
            {
                JeopardyGame.StartRound(round);

                questions.GetCategory(round); // Tillkallar en metod som slumpar och skriver ut 6 kategorier.

                do
                {
                    game.PrintCategories(questions); // Tillkallar en metod som skriver ut kategorierna
                    game.CategoryInput(userInput, questions); // Tillkallar en metod från objeketet och skicker med en startreferens i parametern. 

                    questions.GetPoints(round, userInput, game.pos); // Hämtar poängen för frågorna.
                    game.PrintPoints(questions); // Tillkallar en metod som skriver ut poängen
                    game.PointsInput(userInput, questions); // Tillkallar en metod som tar in användarens val an poäng

                    questions.GetQuestion(userInput, game.pos); // Tillkallar ytterligare en metod från objektet och skickar med inputs och en pos refererare.
                    game.PrintQuestion(questions); // Tillkallar en metod som skriver ut den hämtade frågan.
                    questions.CheckAnswer(JeopardyGame.GetAnswer(), userInput, game, game.pos);  // Kollar ifall svaret är korrekt och tilldelar poäng ifall det stämmer. och tar bort poäng ifall svaret det är inkorrekt

                } while (game.categoriesDepleted <= 6); // Forsätter be användaren om kategori, poäng, frågor och svar tills alla kategorierna är tomma på lediga frågor

                round++; // ökar round så att jag kan hämta kategorier, poäng, inputs och svar på nästa runda.
            }
        }
    }
}
// TODO Bakgrund:
// Fixa så att inte frågor från upprepande kategori skrivs ut.
// Fixa så att man måste lägga till en fråga med svaret
// Fixa så att jag inte initsierar StreamReader varje gång jag använder det i en metod. Göra en StreamReader() overload som skickar tillbaks kategorier, frågor eller svar beroende på parametern som skickas in
// Fixa så att man kan spela mer än en spelare? botar eller en annan mänsklig spelare?

//TODO Visuellt:
// Fixa så att man ser ifall frågan har valts och ifall kategorin har slut på ovalda frågor
// Fixa så svar inte kräver bindestreck, apostrofer och paranteser. Samt att man inte måste skriva t.ex "an apple". utan bara "apple".