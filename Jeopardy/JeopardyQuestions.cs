using System;
using System.IO;

namespace Jeopardy
{
    class JeopardyQuestions
    {
        protected string[] keepCategory = new string[6];
        protected string [] columns = null; 
        protected string path = @"..\..\..\jeopardy_questions\master_season1-36.tsv\master_season1-36.tsv", lines = string.Empty, keepAnswer;
        protected int count;
        Random random = new Random();

        public string[] GetChoices(int round, int points)
        {
            var randomLine = 1;
            bool validLine = false;

            for (int i = 0; i < 6; i++) // Skriver skriver ut 6 kategorier.
            {
                do
                {
                    using (StreamReader sr = File.OpenText(path)) // Använder StreamReader för att läsa varje rad i .tsv filen
                    {
                        while ((lines = sr.ReadLine()) != null) /// Fortsätter att läsa varje rad så länge inte raden innehåller null
                        {
                            columns = lines.Split("\t"); // Delar en sträng (lines) i en substring beroende på "sträng sepereraren"("\t") som sedan lagras i en array (columns)

                            randomLine = random.Next(0, 359679); // Slumpar numper mellan rad 0 och sista raden

                            for (int j = 0; j < randomLine; j++) // Hoppar över alla element som är mindre än RandomCategory.
                            {
                                sr.ReadLine();
                            }

                            if (columns[0] == round.ToString()) // Kollar om column[0] i raden är = round. och ifall column[1] är = lägsta poängen för rundan & kategorin
                            {
                                Console.WriteLine(columns[3]);
                                keepCategory[i] = columns[3]; // Sparar kategorierna i []category så att när jag tillkallar GetQuestion() kan jag leta efter kategorin som valdes av användaren.
                                validLine = true;
                                break;
                            }
                            else
                            {
                                validLine = false;
                            }
                        }
                    }
                } while (validLine != true);
            }
            return keepCategory;
        }

        public void GetQuestion(int[] Input, int pos)
        {
            using (StreamReader sr = File.OpenText(path))
            {
                while ((lines = sr.ReadLine()) != null)
                {
                    columns = lines.Split("\t");
                    if (columns[3] == keepCategory[Input[pos - 2]-1].ToString() && columns[1] == Input[pos - 1].ToString()) // Kollar om rad x innehåller samma kategori och poäng som användaren valde
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