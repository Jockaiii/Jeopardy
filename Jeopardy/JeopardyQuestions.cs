using System;
using System.IO;
using System.Linq;

namespace Jeopardy
{
    class JeopardyQuestions
    {
        public string[] keepCategory = new string[6];
        protected string [] columns = null;
        public int[] keepPoints = new int[5];
        public string path = @"..\..\..\jeopardy_questions\master_season1-36.tsv\master_season1-36.tsv", rows = string.Empty, keepQuestion, keepAnswer;
        public int missingQuestion;
        readonly Random random = new Random();

        public void GetCategory(int [] userInput, int round)
        {
            bool validLine = false;
            Array.Clear(userInput, 0, userInput.Length); // Rensar []userInput från tidigare rundans inputs.
            Array.Clear(keepPoints, 0, keepPoints.Length);

            for (int i = 0; i < 6; i++) // Slumpar en rad 6 gånger och sparar kategorierna från dom raderna.
            {
                do
                {
                    using StreamReader sr = File.OpenText(path); // Använder StreamReader för att läsa varje rad i .tsv filen
                    
                    while ((rows = sr.ReadLine()) != null) // Fortsätter att läsa varje rad så länge raden inte innehåller null
                    {
                        columns = rows.Split("\t"); // Delar en sträng (rows) in i substrings beroende på "sträng sepereraren"("\t") som sedan lagras i element inom en array ([]columns)

                        int randomRow = random.Next(1, 359679); // Slumpar nummer mellan rad 1 och sista raden

                        for (int j = 0; j < randomRow; j++) // Hoppar över alla rader innan randomLine.
                        {
                            sr.ReadLine();
                        }

                        if (columns[0] == round.ToString() && !keepCategory.Contains(columns[3])) // Kollar om column[0] i raden är = round och så att kategorin inte tidigare har valts. 
                        {
                            keepCategory[i] = columns[3]; // Sparar kategorierna i []keepCategory
                            validLine = true;
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

            int count = 0;
            missingQuestion = 0;

            while ((rows = sr.ReadLine()) != null)
            {
                columns = rows.Split("\t");
                if (columns[0] == round.ToString() && columns[3] == keepCategory[userInput[pos - 1] - 1].ToString() && !keepPoints.Contains(int.Parse(columns[1])) && keepPoints[4] == 0) // Kollar så att GetPoints inte skriver ut poäng som redan skrivits ut. och att ifall en femte fråga ha lagrats så ska inga mer lagras.
                {
                    keepPoints[count] = int.Parse(columns[1]);
                    count++;
                }
            }

            for (int i = 0; i < keepPoints.Length; i++) // Ifall en kategori har mindre än 5 frågor så måste jag ha något som tar bort antal frågor som förvantas i en kateogri i JeopardyGame.cs
            {
                if (keepPoints[i] == 0)
                    missingQuestion++;
            }
        }
            
        public void GetQuestion(int[] userInput, int pos)
        {
            using StreamReader sr = File.OpenText(path);
            while ((rows = sr.ReadLine()) != null)
            {
                columns = rows.Split("\t");
                if (columns[3] == keepCategory[userInput[pos - 2] - 1].ToString() && userInput[pos - 1].ToString() == columns[1]) // Kollar om rad x innehåller samma kategori och poäng som användaren valde
                {
                    keepQuestion = columns[5];
                    keepAnswer = columns[6];
                    break;
                }
            }
        }

        public void CheckAnswer(string answer, int[] pointsInput, JeopardyGame game, int pos)
        {
            if (keepAnswer.ToLower() == answer.ToLower())
            {
                game.Score(true, pointsInput[pos - 1], keepAnswer);
            }
            else
            {
                game.Score(false, pointsInput[pos - 1], keepAnswer);
            }
        }
    }
}