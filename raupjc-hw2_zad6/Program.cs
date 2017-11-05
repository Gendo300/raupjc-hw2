using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace raupjc_hw2_zad6
{
    class Program
    {
        static async Task<int> FactorialDigitSumAsync(int n)
        {
            int sum=0;
            int factorial=1;
            for(int i=1; i<=n; i++)
            {
                factorial *= i;
            }
            while (factorial > 0)
            {
                sum += factorial % 10;
                factorial = factorial / 10;
            }
            return sum;
        }

        private static async Task LetsSayUserClickedAButtonOnGuiMethod()
        {
            var result = await GetTheMagicNumber();
            Console.WriteLine(result);
        }

        private static async Task<int> GetTheMagicNumber()
        {
            return await IKnowIGuyWhoKnowsAGuy();
        }

        private static async Task<int> IKnowIGuyWhoKnowsAGuy()
        {
            return await IKnowWhoKnowsThis(10) + await IKnowWhoKnowsThis(5);
        }

        private static async Task<int> IKnowWhoKnowsThis(int n)
        {
            return await FactorialDigitSumAsync(n);
        }

        static void Main(string[] args)
        {
            // Main  method  is the  only  method  that
            // can ’t be  marked  with  async.
            // What we are  doing  here is just a way  for us to  simulate
            // async -friendly  environment  you  usually  have  with
            // other .NET  application  types (like  web apps , win  apps  etc.)
            //  Ignore  main  method , you  can  just  focus on
            //LetsSayUserClickedAButtonOnGuiMethod() as a
            // first  method  in the  call  hierarchy.
            var t = Task.Run(() => LetsSayUserClickedAButtonOnGuiMethod());
            Console.Read();
        }
    }
}
