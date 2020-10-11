using System;
using System.IO;

namespace Jeopardy
{
    class JeopardyQuestions : JeopardyGame // Tanken är att ärva fälten från JeopardyGame så att det behöves returneras något mellan klasserna.
    {
        protected string[] KeepCategory = new string[4];
        protected string [] columns = null; 
        protected string path = @"..\..\..\jeopardy_questions\master_season1-36.tsv\master_season1-36.tsv", lines = string.Empty;
        protected int count;
        Random random = new Random();

        public string[] GetChoices(int round, int points)
        {
            var hej = 1;
            for (int i = 0; i < 4; i++) // Skriver skriver ut 4 kategorier.
            {
                using (StreamReader sr = File.OpenText(path)) // Använder StreamReader för att läsa varje rad i .tsv filen
                {
                    while ((lines = sr.ReadLine()) != null) /// Fortsätter att läsa varje rad så länge inte raden innehåller null
                    {
                        columns = lines.Split("\t"); // Delar en sträng (lines) i en substring beroende på "sträng sepereraren"("\t") som sedan lagras i en array (columns)
                        do
                        {
                            hej = random.Next(0, lines.Length);
                        } while (hej % 9 == 0);

                        for (int j = 0; j < hej; j++)
                        {
                            sr.ReadLine();
                        }

                        if (columns[0] == round.ToString() && columns[1] == points.ToString()) // Kollar om column[0] i raden är = round. och ifall column[1] är = lägsta poängen för rundan & kategorin
                        {
                            Console.WriteLine(columns[3]);
                            KeepCategory[i] = columns[3]; // Sparar kategorierna i []category så att när jag tillkallar GetQuestion() kan jag leta efter kategorin som valdes av användaren.
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
                    if (columns[3] == KeepCategory[UserInput[0]-1].ToString() && columns[1] == UserInput[1].ToString()) // Kollar om rad x innehåller samma kategori och poäng som användaren valde
                    {
                        Console.WriteLine(columns[5]);
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
