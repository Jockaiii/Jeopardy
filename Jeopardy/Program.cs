using System;

namespace Jeopardy
{ 
    class Program
    {
        static void Main(string[] args)
        {
            int round = 1, points = 100; // Startar på runda 1 och referar till 100 poäng columen för att det är frågan som ligger överst i varje kategori (i runda 1. I runda 2 är det 200 poäng som ligger överst)

            JeopardyGame.StartGame(round);

            JeopardyQuestions round1 = new JeopardyQuestions(); // instansierar ett nytt objekt av JeopardyQuestions()
            string[] columns = round1.GetChoices(round, points); // Tillkallar en metod från objektet och skickar med startreferenser i parametern. Tilldelar en string array värdet av metoden

            JeopardyGame input1 = new JeopardyGame(); // instansierar ett nytt objekt av JeopardyGame()
            int[] UserInput = input1.UserInput(round); // Tillkallar en metod från objeketet och skicker med en startreferens i parametern. Tilldelar en string array värdet av metoden

            round1.GetQuestion(UserInput, columns); // Tillkallar ytterligare en metod från objektet och skickar med de 2 lagrade arrayerna i parametern.

            Console.WriteLine(round1.CheckAnswer(JeopardyGame.GetAnswer(), UserInput, columns));  // Kollar ifall svaret är korrekt och tilldelar poäng ifall det stämmer. och tar bort poäng ifall svaret det är inkorrekt   
        }
    }
}
// TODO:
// Fixa så att fler än 1 runda används.
// Fixa så svar inte kräver bindestreck och apostrofer.
// Fixa så att man måste lägga till en fråga med svaret
// Fixa så att inte jag initsierar StreamReader varje gång jag använder det i en metod. Göra en StreamReader() overload som skickar tillbaks kategorier, frågor eller svar beroende på parametern som skickas in
// Fixa så att JeopardyQuestions ärver variabler från JeopardyGame så att det inte behövs skickas tillbaks och därmed mindre och snyggare kod
// Fixa så att man kan spela mer än en spelare? botar eller en annan mänsklig spelare?