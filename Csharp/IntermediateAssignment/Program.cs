using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntermediateAssignment
{
    public class Program
    {
        static void Main(string[] args)
        {
            Calculator calculator = new Calculator();
            Console.WriteLine(calculator.Add(2,6));
            Console.WriteLine(calculator.Add(2, 6,7));
            Console.WriteLine(calculator.getResult());
            Console.WriteLine(calculator.Add(2.5f, 6.6f));
            

            AdvanceCalculator obj = new AdvanceCalculator();
            double res=obj.Power(2, 3);
            Console.WriteLine(res);
            Console.WriteLine(obj.getResult());

        }
    }
}
