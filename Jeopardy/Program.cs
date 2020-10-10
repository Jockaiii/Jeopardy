using System;

namespace Jeopardy
{
    class Program
    {
        static void Main(string[] args)
        {
            int round = 1, points = 100;

            JeopardyGame.StartGame();

            JeopardyQuestions round1 = new JeopardyQuestions(); 
            string[] columns = round1.GetChoices(round, points); // Skriver ut 4 slumpvalda kategorier med 4 olika poängval.

            JeopardyGame input1 = new JeopardyGame();
            int[] UserInput = input1.UserInput(round);

            JeopardyQuestions.GetQuestion(UserInput, columns);

            Console.WriteLine(JeopardyQuestions.CheckAnswer(JeopardyQuestions.GetAnswer(), UserInput, columns));  // Kollar ifall svaret är korrekt och tilldelar poäng ifall det stämmer. och tar bort poäng ifall svaret det är inkorrekt       
        }
    }
}
