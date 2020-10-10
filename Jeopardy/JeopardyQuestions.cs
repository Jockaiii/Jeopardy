using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.ComponentModel;
using System.Threading;
using System.Data.Common;

namespace Jeopardy
{
    class JeopardyQuestions
    {
         public string[] GetChoices(int round, int points)
        {
            Random random = new Random();          
            string path = @"..\..\..\jeopardy_questions\master_season1-36.tsv\master_season1-36.tsv", lines ="";
            string[] columns = null;
            var category = 0;
         
            for (int i = 0; i < 4; i++)
            {
                using (StreamReader sr = File.OpenText(path))
                {
                    while (( lines = sr.ReadLine()) != null)
                    {
                        columns = lines.Split("\t"); // Delar en sträng (row) i en substring beroende på "sträng sepereraren"("\t") som sedan lagras i en array (columns)
                        //do
                        //{
                        //    category = random.Next(0, lines.Length);
                        //} while (category % 10 == 0);

                        if (columns[0] == round.ToString() && columns[1] == points.ToString())
                        {
                            Console.WriteLine(columns[3]);
                            break;
                        }
                    }
                }
            }
            return columns;
        }      

        static public void GetQuestion(int[]UserInput, string[] columns)
        {
            string path = @"..\..\..\jeopardy_questions\master_season1-36.tsv\master_season1-36.tsv", row;

            using (StreamReader sr = File.OpenText(path))
            {
                while ((row = sr.ReadLine()) != null)
                {
                    if (columns[3] == UserInput[0].ToString() && columns[1] == UserInput[1].ToString())
                    {
                        Console.WriteLine(columns[4]);
                    }
                }
            }
        }

        static public string GetAnswer()
        {
            Console.Write("Ange ditt svar :");
            string answer = Console.ReadLine();
            return answer;
        }

        static public bool CheckAnswer(string answer, int[] UserInput, string[] columns)
        {
            string path = @"..\..\..\jeopardy_questions\master_season1-36.tsv\master_season1-36.tsv", row;

            using (StreamReader sr = File.OpenText(path))
            {
                while ((row = sr.ReadLine()) != null)
                {
                    if (answer == columns[5])
                    {
                        return true;
                    }
                    return false;
                }
                return false;
            }
        }
    }
}
