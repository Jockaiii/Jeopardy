using System;

namespace Jeopardy
{ 
    class Program
    {
        static void Main(string[] args)
        {
            int round = 1; // Startar på runda 1 och referar till 100 poäng columen för att det är frågan som ligger överst i varje kategori (i runda 1. I runda 2 är det 200 poäng som ligger överst)
            int[] Input = new int[100];

            JeopardyQuestions questions = new JeopardyQuestions(); // instansierar ett nytt objekt av JeopardyQuestions()
            JeopardyGame game = new JeopardyGame(); // instansierar ett nytt objekt av JeopardyGame()

            for (int i = 0; i <= 3; i++)
            {
                JeopardyGame.StartGame(round);

                string[] keepCategory = questions.GetData(round); // Tillkallar en metod från objektet och skickar med startreferenser i parametern. Tilldelar []keepCategory det som retuneras av metoden

                do
                {
                    for (int j = 0; j < keepCategory.Length; j++)
                    {
                        Console.WriteLine(keepCategory[j]);
                    }

                    int[] keepCategoryInput = game.CategoryInput(round, Input, questions); // Tillkallar en metod från objeketet och skicker med en startreferens i parametern. Tilldelar en string array värdet av metoden
                    Input = keepCategoryInput; // ser till så att input i metoden innehåller de tidigare lagrade inputsen.

                    questions.GetPoints(round, Input, game.pos);

                    int[] keepPointsInput = game.PointsInput(round, Input, questions);
                    Input = keepPointsInput;

                    questions.GetQuestion(Input, game.pos); // Tillkallar ytterligare en metod från objektet och skickar med de 2 lagrade arrayerna i parametern.

                    questions.CheckAnswer(JeopardyGame.GetAnswer(), keepPointsInput, game, game.pos);  // Kollar ifall svaret är korrekt och tilldelar poäng ifall det stämmer. och tar bort poäng ifall svaret det är inkorrekt
                } while (game.categoriesDepleted < 6);

                round++;
            }
        }
    }
}
// TODO Bakgrund:
// Fixa så att man måste lägga till en fråga med svaret
// Fixa så att inte jag initsierar StreamReader varje gång jag använder det i en metod. Göra en StreamReader() overload som skickar tillbaks kategorier, frågor eller svar beroende på parametern som skickas in
// Fixa så att JeopardyQuestions ärver variabler från JeopardyGame så att det inte behövs skickas tillbaks och därmed mindre och snyggare kod
// Fixa så att man kan spela mer än en spelare? botar eller en annan mänsklig spelare?

//TODO Visuellt:
// Fixa så att man ser ifall frågan har valts och ifall kategorin har slut på ovalda frågor
// Fixa så svar inte kräver bindestreck och apostrofer.
// Försöka göra texten enklare att läsa
// Fixa så att när man väljer en kategori så svarar consolen med du valde den här kategorin