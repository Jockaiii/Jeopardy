using System;
using System.Linq;

namespace Jeopardy
{
    class JeopardyGame
    {
        protected int category, points;
        protected int score { get; set; } = 0;
        public int pos = 0, maxQuestions, categoriesDepleted;

        public static void StartGame(int round)
        {
            if (round == 1)
            {
                Console.WriteLine("--------------------------------------------------------------");
                Console.WriteLine("Welcome to jeopardy! The game where you answer with questions!");
                Console.WriteLine("Please select your category and amount of points.");
                Console.WriteLine("--------------------------------------------------------------");
            }
            else if(round == 2)
            {
                Console.WriteLine("Round 2 Lets go again!");
            }
            else
            {
                Console.WriteLine("Round 3!");
            }
        }
        public int[] CategoryInput(int round, int[] Input, JeopardyQuestions questions)
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

                    if (maxQuestions == questions.amountQuestions)
                    {
                        Console.WriteLine("Please select a non depleted category");
                        Console.Write("Select a number between 1-6: ");
                        categoriesDepleted++;
                        category = 7;
                        input = false;
                    }
                    else if (category > 0 && category <= 6)
                    {
                        Input[pos] = category;
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
                    Console.Write("Select a number between 1-6: ");
                    input = false;
                }
            } while (input == false);

            pos++; // Räkna antalet gånger som metoden kallas så jag vet vilken position jag ska lagra inputs i []Input
            return Input;
        }

        public int[] PointsInput(int round, int[] Input, JeopardyQuestions questions)
        {
            bool input;
            Console.WriteLine("------------------------------");
            Console.Write("Select amount of points: ");

            do
            {
                try
                {
                    points = int.Parse(Console.ReadLine());
                    for (int i = 0; i < pos; i++)
                    {
                        if (Input[i] == category && Input[i+1] == points) // kollar igenom []Input efter ifall category && points redan finns lagrad.
                        {
                            Console.WriteLine("Please select a question that hasn't already been selected");
                            points = 0; // Lite fulhack men sätter points till 0 så att else körs och man måste välja om poäng
                        }
                    }

                    if (points == 0)
                    {
                        Console.WriteLine("Please select a valid number");
                        Console.Write("Select amount of points: ");
                        input = false;
                    }
                    else if (questions.keepPoints.Contains(points))
                    {
                        Input[pos] = points;
                        input = true;
                    }
                    else
                    {
                        input = false;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.Write("Select amount of points: ");
                    input = false;
                }
            } while (input == false);
            
            pos++;
            return Input;
        }

        static public string GetAnswer()
        {
            Console.Write("Input your answer: ");
            string answer = Console.ReadLine();
            return answer == string.Empty ? GetAnswer() : answer; 
        }

        public void Score(bool CheckAnswer, int nextScore, string keepAnswer)
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
                Console.WriteLine("The correct answer is: " + keepAnswer);
                score -= nextScore;
            }
            Console.WriteLine("Your score is: " + score);
        }
    }
}