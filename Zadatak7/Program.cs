using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadatak7_i_8
{
    class Program
    {
        static void Main(string[] args)
        {
            // Main method is the only method that
            // can ’t be marked with async .
            // What we are doing here is just a way for us to simulate
            // async - friendly environment you usually have with
            // other .NET application types ( like web apps , win apps etc .)
            // Ignore main method , you can just focus on
           // LetsSayUserClickedAButtonOnGuiMethod() as a
            // first method in call hierarchy .
            var t = Task.Run(() => LetsSayUserClickedAButtonOnGuiMethod());
            Console.Read();
        }

        private static async Task LetsSayUserClickedAButtonOnGuiMethod()
        {
            var result = await GetTheMagicNumber();
            Console.WriteLine(result);
        }

        private static async Task<int> GetTheMagicNumber()
        {
            var solution= await IKnowIGuyWhoKnowsAGuy();
            return solution;
        }

        private static async Task<int> IKnowIGuyWhoKnowsAGuy()
        {
            var solution1 = IKnowWhoKnowsThis(10);
            var solution2 = IKnowWhoKnowsThis(5);
            var finalSol1 = await solution1;
            var finalSol2 = await solution2;
            return finalSol1 + finalSol2;
        }

        private static async Task<int> IKnowWhoKnowsThis(int n)
        {
            var solution = await FactorialDigitSum(n);
            return solution;
        }


        private static async Task<int> FactorialDigitSum(int n)
        {
          
            Func<int, int> FactAndSumDigits = number =>
             {
                 int fact = 1;
                 for (int i = 1; i <= number; i++) fact *= i;

                 int digSum = fact.ToString().Select(i => int.Parse(i.ToString()))
                                             .Sum();
                 return digSum;
             };

            var solution = Task.Run(() => FactAndSumDigits(n));
            var finalSol = await solution;
            return finalSol;
        }
    }
}
