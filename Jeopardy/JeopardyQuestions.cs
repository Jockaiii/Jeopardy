using System;
using System.IO;
using System.Linq;

namespace Jeopardy
{
    class JeopardyQuestions
    {
        public string[] keepCategory = new string[6];
        protected string [] columns = null; 
        protected string path = @"..\..\..\jeopardy_questions\master_season1-36.tsv\master_season1-36.tsv", lines = string.Empty, keepAnswer;
        public int amountQuestions = 1;
        public int[] keepPoints = new int[10];
        readonly Random random = new Random();

        public void GetCategory(int round)
        {
            bool validLine = false;

            for (int i = 0; i < 6; i++) // Slumpar en rad 6 gånger och sparar kategorierna i de randerna och skickar dem till Program.cs för att skrivas ut.
            {
                do
                {
                    using StreamReader sr = File.OpenText(path); // Använder StreamReader för att läsa varje rad i .tsv filen
                    
                    while ((lines = sr.ReadLine()) != null) /// Fortsätter att läsa varje rad så länge raden inte innehåller null
                    {
                        columns = lines.Split("\t"); // Delar en sträng (lines) i en substrings beroende på "sträng sepereraren"("\t") som sedan lagras i element inom en array ([]columns)

                        int randomLine = random.Next(1, 359679); // Slumpar nummer mellan rad 1 och sista raden

                        for (int j = 0; j < randomLine; j++) // Hoppar över alla rader innan RandomLine.
                        {
                            sr.ReadLine();
                        }

                        if (columns[0] == round.ToString() && !keepCategory.Contains(columns[3])) // Kollar om column[0] i raden är = round och så att kategorin inte tidigare har valts.
                        {
                            keepCategory[i] = columns[3]; // Sparar kategorierna i []keepCategory
                            validLine = true;
                            break;
                        }
                        else
                        {
                            validLine = false;
                        }
                    }
                } while (validLine != true); // Fortsätter att slumpa en rad så länge den inte är till för den nuvarande rundan.
            }
        }

        public void GetPoints(int round, int[]userInput, int pos)
        {
            using StreamReader sr = File.OpenText(path);

            Array.Clear(keepPoints, 0, keepPoints.Length); // Rensar []keepPoints så att när nästa kategori väljs ligger det inte kvar poäng som valda inputs.
            amountQuestions = 0; // sätter amountQuestions till 0 så att den den inte sparar antal frågor från senast inladdade kategori.

            while ((lines = sr.ReadLine()) != null)
            {
                columns = lines.Split("\t");
                if (columns[0] == round.ToString() && columns[3] == keepCategory[userInput[pos - 1] - 1].ToString() && !keepPoints.Contains(int.Parse(columns[1]))) // Kollar så att GetPoints inte skriver ut poäng som redan skrivits ut. 
                {
                    Console.WriteLine(columns[1]);
                    keepPoints[amountQuestions] = int.Parse(columns[1]);
                    amountQuestions++;
                }
            }
        }

        public void GetQuestion(int[] userInput, int pos)
        {
            using StreamReader sr = File.OpenText(path);
            while ((lines = sr.ReadLine()) != null)
            {
                columns = lines.Split("\t");
                if (columns[3] == keepCategory[userInput[pos - 2] - 1].ToString() && userInput[pos - 1].ToString() == columns[1]) // Kollar om rad x innehåller samma kategori och poäng som användaren valde
                {
                    Console.WriteLine(columns[5]);
                    keepAnswer = columns[6];
                    break;
                }
            }
        }

        public void CheckAnswer(string answer, int[] pointsInput, JeopardyGame input, int count)
        {
            if (keepAnswer.ToLower() == answer.ToLower())
            {
                input.Score(true, pointsInput[count - 1], keepAnswer);
            }
            else
            {
                input.Score(false, pointsInput[count - 1], keepAnswer);
            }
        }
    }
}