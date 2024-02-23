namespace IntermediateAssignment
{
    public interface ICalculator
    {
        double Add(int a,int b);
        double Add(int a, int b,int c);
        double Add(float a, float b);
    }
    public class Calculator : ICalculator
    {
        private double _result;
        public double ResultProp
        {
            get
            {
                return _result;
            }
            set
            {
                _result = value;
            }
        }
        public double Add(int a,int b)
        {
            return _result=a + b;
        }
        public double Add(int a, int b,int c)
        {
            return _result=a+b+c;
        }
        public double Add(float a, float b)
        {
            return _result=(a+b);
        }

        public virtual double getResult()
        {
            return _result;
        }
    }

}
