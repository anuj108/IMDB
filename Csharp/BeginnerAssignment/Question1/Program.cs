using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Question1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int sum = 0;
            
            for(int i=0; ; i++)
            {
                Console.WriteLine("ENTER A NUMBER OR TYPE \"OK\" TO EXIT");
                string response = Console.ReadLine();
                
                    if (response=="ok")
                    {
                        Console.WriteLine("THE SUM OF ENTERED NUMBERS IS : "+sum);
                        return;
                    }
                    else
                    {
                        int value = Convert.ToInt32(response);
                        sum=sum+value;
                    }
 
                
            }
           
        }
    }
}
