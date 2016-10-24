using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumberToLettersApp
{
    class Program
    {
        static void Main(string[] args)
        {
            NumberToLetterConverter ntc = new NumberToLetterConverter();
            for (int i = 0; i < 1000000000; i += 124336)
            {
                Console.WriteLine("{0} : {1}", i, ntc.convertToMoney(i));
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
