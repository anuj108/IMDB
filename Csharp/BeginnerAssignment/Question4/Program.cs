using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Question4
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("PLEASE ENTER SEQUENCE OF NUMBERS : ");
            string input = Console.ReadLine();
            string[] words = input.Split(',');
            List<int> num = new List<int>();

            for (int i = 0; i<words.Length; i++)
            {
                num.Add(int.Parse(words[i]));
            }
            num.Sort();
            num.Reverse();
            Console.WriteLine("DESCENDING ORDER OF THE SEQUENCE IS : ");
            for (int i = 0;i<num.Count;i++)
            {
                Console.Write(num[i]+" ");
            }
            Console.WriteLine();
        }

    }
}
