﻿using System;

namespace IntermediateAssignment
{
    public interface IAdvanceCalculator
    {
        double Power(int b, int e);
    }
    public class AdvanceCalculator : Calculator
    {
        
        public double Power(int b,int exp)
        {
            return ResultProp=Math.Pow(b, exp);
        }

        public override double getResult()
        {
            return ResultProp*Math.Pow(10,6);
        }

    }

}
