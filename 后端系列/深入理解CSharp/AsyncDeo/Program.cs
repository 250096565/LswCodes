using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncDeo
{
    class Program
    {
        static void Main(string[] args)
        {
            int a = 0;
            int d= a = -1;
            SumCharactersAsync(new List<char>(5)).Wait();
        }

        static async Task<int> SumCharactersAsync(IEnumerable<char> text)
        {
            int total = 0;
            foreach (var ch in text)
            {
                int unicode = ch;
                await Task.Delay(unicode);
                total += unicode;
            }

            await Task.Delay(50);
            return total;

        }
    }
}
