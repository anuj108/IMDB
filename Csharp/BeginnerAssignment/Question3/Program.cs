using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Question3
{
    internal class Program
    {
        static void Main(string[] args)
        {


            while (true)
            {
                Console.WriteLine("PLEASE ENTER SEQUENCE OF NUMBERS : ");
                string input = Console.ReadLine();
                string[] words = input.Split(',');
                List<int> num = new List<int>();
                if (words.Length<5)
                {
                    Console.WriteLine("INVALID LIST!!\nWOULD YOU LIKE TO RETRY? TYPE YES TO RETRY OR NO TO EXIT: ");
                    string res = Console.ReadLine();
                    if (res=="YES")
                    {
                        continue;
                    }
                    else if(res=="EXIT")
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("INVALID RESPONSE EXITING...");
                        break;
                    }
                }
                else
                {
                    for (int i = 0; i<words.Length; i++)
                    {
                        num.Add(int.Parse(words[i]));
                    }
                    num.Sort();

                    Console.WriteLine("SMALLEST THREE NUMBERS ARE :");
                    Console.WriteLine("{0} {1} {2} ", num[0], num[1], num[2]);
                }
            }
            
            
            
 
            
        }
    }
}
