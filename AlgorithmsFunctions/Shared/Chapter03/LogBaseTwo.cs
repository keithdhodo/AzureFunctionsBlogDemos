using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsFunctions.Shared.Chapter03
{
    // class is originally called LogTable and is Program 3.1
    public class LogBaseTwo
    {
        public int NumerToFindLogarithmFor { get; private set; }

        public int Log(int iterations)
        {
            NumerToFindLogarithmFor = iterations;

            int i = 0;
            while (NumerToFindLogarithmFor > 0)
            {
                i++;
                NumerToFindLogarithmFor /= 2;
            }
            return i;
        }
    }
}
