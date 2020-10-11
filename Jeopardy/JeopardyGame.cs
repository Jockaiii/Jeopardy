using System;

namespace Jeopardy
{
    class JeopardyGame
    {
        protected int category, points;
        public static void StartGame()
        {
            Console.WriteLine("Welcome to jeopardy! The game wher you answer with questions!");
            Console.WriteLine("Please choose your category and amount of points");
        }
         public int[] UserInput(int round)
         {
            Console.Write("Choose category (1-4) :");
            category = int.Parse(Console.ReadLine());

            if (round == 1)
            {
                Console.WriteLine("100\n200\n400\n500");
            }
            else if (round == 2)
            {
                Console.WriteLine("400\n600\n800\n1000");
            }            
            Console.Write("Choose amount of points(100-1000) :");
            points = int.Parse(Console.ReadLine());

            int[] UserInput = new int[2];
            UserInput[0] = category;
            UserInput[1] = points;

            return UserInput;
         }
        static public string GetAnswer()
        {
            Console.Write("Ange ditt svar :");
            string answer = Console.ReadLine();
            return answer;
        }
    }
}
