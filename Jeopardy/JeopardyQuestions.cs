using System;
using System.IO;
using System.Threading;
using System.Xml.Serialization;

namespace Jeopardy
{
    class JeopardyQuestions : JeopardyGame
    {
        protected string[] choices = new string[4];
        protected string [] columns = null; 
        protected string path = @"..\..\..\jeopardy_questions\master_season1-36.tsv\master_season1-36.tsv", lines = string.Empty;
        protected int count;
        protected Random random = new Random();
        
        public string[] GetChoices(int round, int points)
        {
            var category = 0;
            for (int i = 0; i < 4; i++)
            {
                using (StreamReader sr = File.OpenText(path))
                {
                    while ((lines = sr.ReadLine()) != null)
                    {
                        columns = lines.Split("\t"); // Delar en sträng (lines) i en substring beroende på "sträng sepereraren"("\t") som sedan lagras i en array (columns)
                        //do
                        //{
                        //    category = random.Next(0, lines.Length);
                        //} while (category % 10 == 0);

                        if (columns[0] == round.ToString() && columns[1] == points.ToString())
                        {
                            Console.WriteLine(columns[3]);
                            choices[i] = columns[3];
                            break;
                        }
                    }
                }
            }
            return columns;
        }

        public void GetQuestion(int[] UserInput, string[] columns)
        {
            using (StreamReader sr = File.OpenText(path))
            {
                while ((lines =sr.ReadLine()) != null)
                {
                    columns = lines.Split("\t");
                    if (columns[3] == choices[UserInput[0]-1].ToString() && columns[1] == UserInput[1].ToString())
                    {
                        Console.WriteLine(points);
                        Console.WriteLine(columns[5]);
                        //rememberRow = int.Parse(lines);
                        break;
                    }
                    count++;
                }
            }
        }

        public bool CheckAnswer(string answer, int[] UserInput, string[] columns)
        {
            using (StreamReader sr = File.OpenText(path))
            {
                for (int i = 0; i < count; i++) // Skippar alla rader innan den som valdes på GetQuestions
                {
                    sr.ReadLine();
                }
                columns = lines.Split("\t");
                if (columns[6] == answer)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
