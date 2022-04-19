using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainWare_Server_v2
{
    public class Utils
    {
        private static List<int> ShuffleQuestions(int _max, List<Question> _questions)
        {
            List<int> indexQuestions = new List<int>();
            for (int i = 0; i < _max; i++) //amountQuestion -> maxQuestion
            {
                _questions.Add(new Question(i));
            }

            Random rnd = new Random();
            int n = _questions.Count;
            for (int i = 0; i < n; i++)
            {
                // Use Next on random instance with an argument.
                // ... The argument is an exclusive bound.
                //     So we will not go past the end of the array.
                int r = i + rnd.Next(n - i);
                Question value = _questions[r];
                _questions[r] = _questions[i];
                _questions[i] = value;
            }

            for (int i = 0; i < _max; i++)
            {
                indexQuestions.Add(_questions[i].Number);
            }

            return indexQuestions;
        }

        public static List<int> SuffleQuestionList(int _max, List<Question> _questions)
        {
            Random rnd = new Random();
            List<int> newSuffle = ShuffleQuestions(_max, _questions);

            int n = newSuffle.Count;
            for (int i = 0; i < n; i++)
            {
                int r = i + rnd.Next(n - i);
                int value = newSuffle[r];
                newSuffle[r] = newSuffle[i];
                newSuffle[i] = value;
            }

            return newSuffle;
        }

        public static byte[] ConvertToByteArray(int[] inputElements)
        {
            byte[] myFinalBytes = new byte[inputElements.Length * 4];
            for (int cnt = 0; cnt < inputElements.Length; cnt++)
            {
                byte[] myBytes = BitConverter.GetBytes(inputElements[cnt]);
                Array.Copy(myBytes, 0, myFinalBytes, cnt * 4, 4);
            }
            return myFinalBytes;
        }


    }
}
