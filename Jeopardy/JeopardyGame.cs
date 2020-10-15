using System;
using System.Linq;

namespace Jeopardy
{
    class JeopardyGame
    {
        protected int category, points, score = 0;
        public int pos = 0, maxQuestions, categoriesDepleted;

        public static void StartRound(int round)
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
                Console.Clear();
                Console.WriteLine("Round 2 Lets go again!");
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Round 3!");
            }
        }

        public void PrintCategories(JeopardyQuestions questions) 
        {
            for (int i = 0; i < questions.keepCategory.Length; i++)
            {
                Console.WriteLine(i + 1 + ": " + questions.keepCategory[i]);
            }
        }

        public void PrintPoints(JeopardyQuestions questions)
        {
            for (int i = 0; i < questions.keepPoints.Length; i++)
            {
                Console.WriteLine(questions.keepPoints[i]);
            }
        }

        public void PrintQuestion(JeopardyQuestions questions)
        {
            Console.WriteLine(questions.keepAnswer);
        }

        public void CategoryInput(int[] userInput, JeopardyQuestions questions)
        {
            bool inputCheck;
            Console.WriteLine("------------------------------");
            Console.Write("Select a category (1-6): ");

            do
            {
                try
                {
                    maxQuestions = 0;
                    category = int.Parse(Console.ReadLine());

                    for (int i = 0; i < userInput.Length; i++)
                    {
                        if (userInput[i] == category) // Kollar om kategorin redan finns lagrad i []Input. Och om den gör det så ökar maxQuestions +1. 
                        {
                            maxQuestions++;
                        }
                    }

                    if (maxQuestions == questions.amountQuestions) // Om kategorin finns sparad i []Input lika många gånger som det finns frågor så betyder det att alla frågor är valda och kategorin är tömd på fira frågor.
                    {                                              // Då får användaren helt enkelt välja en annan kategori. Om categoriesDepleted når 7 kommer rundan avslutas i Program.cs
                        Console.WriteLine("Please select a non depleted category");
                        Console.Write("Select a number between 1-6: ");
                        categoriesDepleted++;
                        category = 7;
                        inputCheck = false;
                    }
                    else if (category > 0 && category <= 6)
                    {
                        userInput[pos] = category;
                        inputCheck = true;
                    }
                    else
                    {
                        Console.Write("Select a number between 1-6: ");
                        inputCheck = false;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.Write("Select a number between 1-6: ");
                    inputCheck = false;
                }
            } while (inputCheck == false);

            Console.WriteLine("Category: " + questions.keepCategory[category - 1]);
            pos++; // Räkna antalet gånger som metoden kallas så jag vet vilken position jag ska lagra inputs i []Input
        }

        public void PointsInput(int[] userInput, JeopardyQuestions questions)
        {
            bool inputCheck;
            Console.WriteLine("------------------------------");
            Console.Write("Select amount of points: ");

            do
            {
                try
                {
                    points = int.Parse(Console.ReadLine());

                    for (int i = 0; i < pos; i++)
                    {
                        if (userInput[i] == category && userInput[i+1] == points) // kollar igenom []Input efter ifall category && points redan finns lagrad.
                        {
                            Console.WriteLine("Please select a question that hasn't already been selected");
                            points = 0; // Lite fulhack men sätter points till 0 så att else körs och man måste välja om poäng
                        }
                    }

                    if (points == 0)
                    {
                        Console.WriteLine("Please select a valid number");
                        Console.Write("Select amount of points: ");
                        inputCheck = false;
                    }
                    else if (questions.keepPoints.Contains(points))
                    {
                        userInput[pos] = points;
                        inputCheck = true;
                    }
                    else
                    {
                        inputCheck = false;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.Write("Select amount of points: ");
                    inputCheck = false;
                }
            } while (inputCheck == false);
            
            pos++;
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