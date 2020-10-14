using System;
using System.IO;
using System.Linq;

namespace Jeopardy
{
    class JeopardyQuestions
    {
        protected string[] keepCategory = new string[6];
        protected string [] columns = null; 
        protected string path = @"..\..\..\jeopardy_questions\master_season1-36.tsv\master_season1-36.tsv", lines = string.Empty, keepAnswer;
        Random random = new Random();

        public string[] GetData(int round, int points)
        {
            var randomLine = 1;
            bool validLine = false;

            for (int i = 0; i < 6; i++) // Skriver ut en kategori 6 gånger.
            {
                do
                {
                    using (StreamReader sr = File.OpenText(path)) // Använder StreamReader för att läsa varje rad i .tsv filen
                    {
                        while ((lines = sr.ReadLine()) != null) /// Fortsätter att läsa varje rad så länge raden int innehåller null
                        {
                            columns = lines.Split("\t"); // Delar en sträng (lines) i en substring beroende på "sträng sepereraren"("\t") som sedan lagras i en array (columns)

                            randomLine = random.Next(1, 359679); // Slumpar nummer mellan rad 1 och sista raden

                            for (int j = 0; j < randomLine; j++) // Hoppar över alla rader innan RandomLine.
                            {
                                sr.ReadLine();
                            }

                            if (columns[0] == round.ToString() && !keepCategory.Contains(columns[3])) // Kollar om column[0] i raden är = round och så att kategorin inte tidigare har valts.
                            {
                                Console.WriteLine(columns[3]);
                                keepCategory[i] = columns[3]; // Sparar kategorierna i []keepCategory så att när jag tillkallar GetQuestion() kan jag leta efter kategorin som valdes av användaren.
                                validLine = true;
                                break;
                            }
                            else
                            {
                                validLine = false;
                            }
                        }
                    }
                } while (validLine != true); // Fortsätter att slumpa en rad så länge den inte är till för den nuvarande rundan.
            }
            return keepCategory;
        }

        public void GetPoints(int[]Input, int pos)
        {
            using (StreamReader sr = File.OpenText(path))
            {
                while ((lines = sr.ReadLine()) != null)
                {
                    columns = lines.Split("\t");

                    if (columns[3] == keepCategory[Input[pos - 2] - 1].ToString())
                    {
                        Console.WriteLine(columns[1]);
                    }
                }
            }
        }

        public void GetQuestion(int[] Input, int count)
        {
            using (StreamReader sr = File.OpenText(path))
            {
                while ((lines = sr.ReadLine()) != null)
                {
                    columns = lines.Split("\t");
                    if (columns[3] == keepCategory[Input[count - 2]-1].ToString() && columns[1] == Input[count - 1].ToString()) // Kollar om rad x innehåller samma kategori och poäng som användaren valde
                    {
                        Console.WriteLine(columns[5]);
                        keepAnswer = columns[6];
                        break;
                    }
                }
            }
        }

        public void CheckAnswer(string answer, int[] Input, JeopardyGame input, int count)
        {
            if (keepAnswer.ToLower() == answer.ToLower())
            {
                input.Score(true, Input[count - 1]);
            }
            else
            {
                input.Score(false, Input[count - 1]);
            }
        }
    }
}