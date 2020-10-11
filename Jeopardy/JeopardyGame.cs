using System;
using System.Net.Http;

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
            bool input = false;

            Console.Write("Choose category (1-4) :");
            do
            {
                try
                {
                    category = int.Parse(Console.ReadLine());
                    if (category > 0 && category < 5)
                    {
                        input = true;
                    }
                    else
                    {
                        Console.Write("Choose a number between 1-4 :");
                        input = false;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.Write("Choose a number between 1-4 :");
                    input = false;
                }
            } while (input == false);

            if (round == 1)
            {
                Console.WriteLine("100\n200\n400\n500");
            }
            else if (round == 2)
            {
                Console.WriteLine("400\n600\n800\n1000");
            }            
            Console.Write("Choose amount of points(100-1000) :");

            input = false;
            do
            {
                try
                {
                    points = int.Parse(Console.ReadLine());
                    if (round == 1 && points == 100 || round == 1 && points == 200 || round == 1 && points == 400 || round == 1 && points == 500)
                    {
                        input = true;
                    }
                    else if(round == 2 && points == 200 || round == 2 && points == 400 || round == 2 && points == 800 || round == 2 && points == 1000)
                    {
                        input = true;
                    }
                    else
                    {
                        Console.WriteLine("Choose amount of points(100-1000):");
                        input = false;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.Write("Choose amount of points(100-1000) :");
                }
            } while (input == false);

            int[] UserInput = new int[2];
            UserInput[0] = category;
            UserInput[1] = points;

            return UserInput;
         }
        static public string GetAnswer()
        {
            Console.Write("Input your answer: ");
            string answer = Console.ReadLine();
            return answer;
        }
    }
}