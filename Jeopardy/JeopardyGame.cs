using System;

namespace Jeopardy
{
    class JeopardyGame
    {
        protected int category, points;
        protected int score { get; set; } = 0;
        public int count = 0, maxQuestions;

        public static void StartGame(int round)
        {
            if (round == 1)
            {
                Console.WriteLine("--------------------------------------------------------------");
                Console.WriteLine("Welcome to jeopardy! The game where you answer with questions!");
                Console.WriteLine("Please choose your category and amount of points.");
                Console.WriteLine("--------------------------------------------------------------");
            }
            else if(round == 2)
            {
                Console.WriteLine("Round 2 Lets go again!");
            }
        }
        public int[] CategoryInput(int round, int[] Input)
        {
            bool input;
            Console.WriteLine("------------------------------");
            Console.Write("Select a category (1-6): ");

            do
            {
                try
                {
                    maxQuestions = 0;
                    category = int.Parse(Console.ReadLine());

                    for (int i = 0; i < Input.Length; i++)
                    {
                        if (Input[i] == category)
                        {
                            maxQuestions++;
                        }
                    }

                    if (maxQuestions == 4)
                    {
                        Console.WriteLine("Please choose a category with non selected questions");
                        Console.Write("Choose a number between 1-6: ");
                        category = 7;
                        input = false;
                    }
                    else if (category > 0 && category <= 6)
                    {
                        input = true;
                    }
                    else
                    {
                        Console.Write("Select a number between 1-6: ");
                        input = false;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.Write("Choose a number between 1-6: ");
                    input = false;
                }
            } while (input == false);

            Input[count] = category;

            count++; // Räkna antalet gånger som metoden kallas så jag vet vilken position jag ska lagra inputs i []Input
            return Input;
        }

        public int[] PointsInput(int round, int[] Input)
        {
            bool input;
            Console.WriteLine("------------------------------");
            Console.Write("Choose amount of points(100-1000): ");

            do
            {
                try
                {
                    points = int.Parse(Console.ReadLine());
                    for (int i = 0; i < count + 2; i++)
                    {
                        if (Input[i] == category && Input[i + 1] == points) // kollar igenom []Input efter ifall category && points redan finns lagrad.
                        {
                            Console.WriteLine("Please choose a question that hasn't already been selected");
                            points = 0; // Lite fulhack men sätter points till 0 så att else körs och man måste välja om poäng
                        }
                    }

                    if (round == 1)
                    {
                        Input[count] = points;
                        input = true;
                    }
                    else if (round == 2)
                    {
                        Input[count] = points;
                        input = true;
                    }
                    else
                    {
                        Console.WriteLine("Please choose a valid number");
                        Console.Write("Choose amount of points: ");
                        input = false;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.Write("Choose amount of points: ");
                    input = false;
                }
            } while (input == false);
            count++;
            return Input;
        }

        static public string GetAnswer()
        {
            Console.Write("Input your answer: ");
            string answer = Console.ReadLine();
            return answer == string.Empty ? GetAnswer() : answer; 
        }

        public void Score(bool CheckAnswer, int nextScore)
        {
            Console.Clear();
            if (CheckAnswer)
            {
                Console.WriteLine("Correct!");
                score += nextScore;
            }
            else
            {
                Console.WriteLine("Incorrect!");
                score -= nextScore;
            }
            Console.WriteLine("Your score is: " + score);
        }
    }
}