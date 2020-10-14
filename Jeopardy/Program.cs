using System;

namespace Jeopardy
{ 
    class Program
    {
        static void Main(string[] args)
        {
            int round = 1, points = 100; // Startar på runda 1 och referar till 100 poäng columen för att det är frågan som ligger överst i varje kategori (i runda 1. I runda 2 är det 200 poäng som ligger överst)
            int[] Input = new int[24];

            JeopardyQuestions questions = new JeopardyQuestions(); // instansierar ett nytt objekt av JeopardyQuestions()
            JeopardyGame game = new JeopardyGame(); // instansierar ett nytt objekt av JeopardyGame()

            JeopardyGame.StartGame(round);

            for (int i = 0; i <= 3; i++)
            {
                string[] keepCategory = questions.GetData(round, points); // Tillkallar en metod från objektet och skickar med startreferenser i parametern. Tilldelar []keepCategory det som retuneras av metoden

                for (int j = 0; j <= 16; j++)
                {
                    if (j > 0) // ser till så att kategorierna skrivs ut varje gång man väljer en ny fråga.(första gången skrivs de ut i JeopardyQuestions)
                    {
                        for (int k = 0; k < keepCategory.Length; k++)
                        {
                            Console.WriteLine(keepCategory[k]);
                        }
                    }


                    int[] keepCategoryInput = game.CategoryInput(round, Input); // Tillkallar en metod från objeketet och skicker med en startreferens i parametern. Tilldelar en string array värdet av metoden
                    Input = keepCategoryInput; // ser till så att input i metoden innehåller de tidigare lagrade inputsen.

                    questions.GetPoints(round ,Input, game.count);

                    int[] keepPointsInput = game.PointsInput(round, Input);
                    Input = keepPointsInput;

                    questions.GetQuestion(Input, game.count); // Tillkallar ytterligare en metod från objektet och skickar med de 2 lagrade arrayerna i parametern.

                    questions.CheckAnswer(JeopardyGame.GetAnswer(), keepPointsInput, game, game.count);  // Kollar ifall svaret är korrekt och tilldelar poäng ifall det stämmer. och tar bort poäng ifall svaret det är inkorrekt
                }
                round++;
                points += 100;
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