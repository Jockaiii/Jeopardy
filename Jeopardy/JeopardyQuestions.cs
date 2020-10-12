using System;
using System.IO;

namespace Jeopardy
{
    class JeopardyQuestions : JeopardyGame // Tanken är att ärva fälten från JeopardyGame så att det behöves returneras något mellan klasserna.
    {
        protected string[] KeepCategory = new string[6];
        protected string [] columns = null; 
        protected string path = @"..\..\..\jeopardy_questions\master_season1-36.tsv\master_season1-36.tsv", lines = string.Empty;
        protected int count;
        Random random = new Random();

        public string[] GetChoices(int round, int points)
        {
            var RandomCategory = 1;
            for (int i = 0; i < 6; i++) // Skriver ut 6 kategorier.
            {
                using (StreamReader sr = File.OpenText(path)) // Använder StreamReader för att läsa varje rad i .tsv filen
                {
                    while ((lines = sr.ReadLine()) != null) /// Fortsätter att läsa varje rad så länge inte raden innehåller null
                    {
                        columns = lines.Split("\t"); // Delar en sträng (lines) i en substring beroende på "sträng sepereraren"("\t") som sedan lagras i en array (columns)
                        do
                        {
                            RandomCategory = random.Next(0, lines.Length);
                        } while (RandomCategory % 9 == 0); // Slumpar nummer mellan rad 0 och sista raden och kollar fortsätter till det är delbart med 9 endast. Och ifall det är det betyder det att det är en kategori

                        for (int j = 0; j < RandomCategory; j++) // Hoppar över alla element som är mindre än hej.
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
                while ((lines = sr.ReadLine()) != null)
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
                for (int i = 0; i < count; i++) // Skippar alla rader innan den som valdes på GetQuestions()
                {
                    sr.ReadLine();
                }
                columns = lines.Split("\t");
                if (columns[6].ToLower() == answer.ToLower())
                {
                    JeopardyGame.Score(UserInput[1]);
                    return true;
                }
            }
            UserInput[1] *= -1;
            JeopardyGame.Score(UserInput[1]);
            UserInput[1] *= -1;
            return false;
        }
    }
}