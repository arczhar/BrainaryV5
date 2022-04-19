using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainWare_Server_v2
{
    public class Question
    {
        public int Number;
        public bool Answered;

        public Question(int _number)
        {
            Number = _number;
        }
    }
}
