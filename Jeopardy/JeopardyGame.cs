using System;
using System.Linq;

namespace Jeopardy
{
    class JeopardyGame
    {
        protected int category, points, score = 0;
        public int pos = 0, maxQuestions, categoriesDepleted;
        static string playerName;

        public static void StartRound(int round, JeopardyGame game)
        {
            game.categoriesDepleted = 0; // Sätter categoriesDeplted till 0 vid start av ny runda.

            if (round == 1)
            {
                Console.WriteLine("Welcome to jeopardy! The game where you answer with questions!");
                Console.Write("Enter playername: ");
                playerName = Console.ReadLine();
                Console.Clear();

                Console.WriteLine("--------------------------------------------------------------");
                Console.WriteLine("Please select your category and amount of points.");
                Console.WriteLine("--------------------------------------------------------------");
            }
            else if (round == 2)
            {
                game.Score(true, 0, null);
                Console.WriteLine("Round 2 Lets go again!");
            }
            else
            {
                game.Score(true, 0, null);
                Console.WriteLine("Round 3!");
            }
        }

        public void PrintCategories(JeopardyQuestions questions)
        {
            for (int i = 0; i < questions.keepCategory.Length; i++)
            {
                Console.WriteLine(i + 1 + ": " + questions.keepCategory[i]);
            }

            Console.WriteLine("--------------------------");
            Console.Write("Select a category (1-6): ");
        }

        public void CategoryInput(int[] userInput, JeopardyQuestions questions)
        {
            bool inputCheck;

            do
            {
                try
                {
                    maxQuestions = 0;
                    category = int.Parse(Console.ReadLine());

                    for (int i = 0; i < userInput.Length; i += 2)
                    {
                        if (userInput[i] == category) // Kollar om kategorin redan finns lagrad i []Input. Och om den gör det så ökar maxQuestions +1. 
                        {
                            maxQuestions++;
                        }
                    }

                    if (questions.keepCategory[category - 1] == "Depleted")
                    {
                        Console.WriteLine("Please select a non depleted category");
                        Console.Write("Select a number between 1-6: ");
                        inputCheck = false;
                    }
                    else if (maxQuestions == 5 - questions.missingQuestion)
                    {
                        Console.WriteLine("Please select a non depleted category");
                        Console.Write("Select a number between 1-6: ");
                        categoriesDepleted++;
                        questions.keepCategory[category - 1] = "Depleted";
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

            Console.Clear();
            Console.WriteLine("--------------------------");
            Console.WriteLine("Category: " + questions.keepCategory[category - 1]);
            Console.WriteLine("--------------------------");

            pos++; // Räkna antalet gånger som metoden kallas så jag vet vilken position jag ska lagra inputs i []Input
        }

        public void PrintPoints(JeopardyQuestions questions, int[] userInput)
        {
            for (int i = 0; i < questions.keepPoints.Length; i++)
            {
                //for (int k = 0; k < questions.keepPoints.Length; k++) // Tar bort frågor som redan är valda. Men fungerar inte riktigt eftersom många kategorier har samma poängval.
                //{                                                     // Hade fungerat om jag sparade en userInput för varje kategori.
                //    for (int j = 1; j < userInput.Length; j += 2)
                //    {
                //        if (userInput[j - 1] == category && questions.keepPoints[k] == userInput[j])
                //        {
                //            questions.keepPoints[k] = 123;
                //            break;
                //        }
                //    }
                //}

                if (questions.keepPoints[i] == 0)
                {

                }
                else
                {
                    Console.WriteLine(questions.keepPoints[i]);
                }
            }

            Console.WriteLine("--------------------------");
            Console.Write("Select amount of points: ");
        }

        public void PointsInput(int[] userInput, JeopardyQuestions questions)
        {
            bool inputCheck;

            do
            {
                try
                {
                    points = int.Parse(Console.ReadLine());

                    for (int i = 0; i < pos; i++)
                    {
                        if (userInput[i] == category && userInput[i + 1] == points) // kollar igenom []userInput efter ifall category && points redan finns lagrad.
                        {
                            Console.WriteLine("Please select a question that hasn't already been selected");
                            points = 0; // Lite fulhack men sätter points till 0 så att else körs och man måste välja om poäng
                            break;
                        }
                    }

                    if (points == 0)
                    {
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
                        Console.WriteLine("Please select a valid number");
                        Console.Write("Select amount of points: ");
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

            Console.Clear();
            pos++;
        }

        public void PrintQuestion(JeopardyQuestions questions)
        {
            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine("Question for " + points + " points: " + questions.keepQuestion);
            Console.WriteLine("--------------------------------------------------------------");
        }

        static public string AnswerInput()
        {
            Console.Write("Input your answer: ");
            string answer = Console.ReadLine();
            return answer == string.Empty ? AnswerInput() : answer;
        }

        public void Score(bool checkAnswer, int nextScore, string keepAnswer)
        {
            Console.Clear();
            if (checkAnswer)
            {
                Console.WriteLine("Correct!");
                score += nextScore;
            }
            else
            {
                Console.WriteLine("Incorrect!");
                Console.WriteLine(playerName + " the correct answer is: " + keepAnswer);
                score -= nextScore;
                Console.WriteLine("Press 'Enter' to continue");
                Console.ReadLine();
                Console.Clear();
            }
            Console.WriteLine("--------------------------");
            Console.WriteLine(playerName + " your score is: " + score);
            Console.WriteLine("--------------------------");
        }
    }
}