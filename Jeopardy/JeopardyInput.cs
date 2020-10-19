using System;
using System.IO;
using System.Linq;

namespace Jeopardy
{
    class JeopardyInput
    {
        private string path = @"..\..\..\jeopardy_questions\master_season1-36.tsv\master_season1-36.tsv", rows = string.Empty;
        private string[] category = new string[6]; 
        private string[] columns;
        readonly Random random = new Random();

        public JeopardyInput()
        {
            bool categoryDepleted, isAnswered;
            string category;
            int points;
        }

        public void GetData(int round)
        {
            bool validLine = false;

            using StreamReader sr = File.OpenText(path);
            columns = rows.Split("\t");

            for (int i = 0; i < 6; i++) 
            {
                do
                {
                    while ((rows = sr.ReadLine()) != null)
                    {
                        int randomRow = random.Next(1, 359679);

                        for (int j = 0; j < randomRow; j++)
                        {
                            sr.ReadLine();
                        }

                        if (columns[0] == round.ToString() && !category.Contains(columns[3])) 
                        {
                            category[i] = columns[3];
                            validLine = true;
                        }
                        else
                        {
                            validLine = false;
                        }
                    }
                } while (validLine != true);
            }
        }
    }
}    
