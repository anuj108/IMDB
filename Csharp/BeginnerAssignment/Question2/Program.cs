using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Question2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("ENTER SERIES OF NUMBERS SEPARATED BY COMMA: ");
            string response=Console.ReadLine();
            int ans = int.Parse(response[0].ToString());
            
            for (int i=0;i<response.Length;i++) {
                if (response[i]==',')
                {
                    int value = int.Parse(response[i-1].ToString());
                    ans=Math.Max(ans, value);
                }    
            }
            int lastValue= int.Parse(response[response.Length-1].ToString());
            ans=Math.Max(ans, lastValue);
            Console.WriteLine("MAXIMUM VALUE IS :" +ans);
        }
    }
}
