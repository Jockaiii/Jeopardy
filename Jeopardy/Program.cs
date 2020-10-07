using System;

namespace Jeopardy
{
    class Program
    {
        static void Main(string[] args)
        {
            JeopardyGame.StartGame();
            JeopardyQuestions.GetQuestion();
            JeopardyQuestions.CheckAnswer();
        }
    }
}
