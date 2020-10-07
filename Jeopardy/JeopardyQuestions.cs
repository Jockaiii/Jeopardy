using System;
using System.IO;

namespace Jeopardy
{
    class JeopardyQuestions
    {        
        static public void GetQuestion()
        {
            Random random = new Random();
            int season = random.Next(1, 36);
            string path = @"C:\Users\joaki\source\repos\Jeopardy\Jeopardy\jeopardy_questions\season1.tsv";
            string a = "100";

            //string st = File.ReadAllText(path);
            //Console.WriteLine(st);

            using (StreamReader sr = File.OpenText(path))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (line.Contains.(a))
                    {
                        Console.WriteLine(line);
                    }
                    
                }
            }
        }

        static public string GetAnswer()
        {
            string answer = "svar";
            return answer;
        }

        static public bool CheckAnswer(string answer)
        {
            if (answer == GetAnswer())
            {
                return true;
            }
            return false;
        }
    }
}
