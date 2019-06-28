using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathGame
{
    public class GeneratorExpression
    {
        private int a;
        private int b;
        private Random rnd;

        /// <summary>
        /// Границы для выражения от a до b
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public GeneratorExpression(int a, int b)
        {
            this.a = a;
            this.b = b;
            rnd = new Random();
        }

        /// <summary>
        /// Получить новое выражение
        /// </summary>
        /// <returns></returns>
        public Expression GetExpression()
        {
            int signN = rnd.Next(1, 5);
            switch (signN)
            {
                case 1: return Sum(a, b);
                case 2: return Subtract(a, b);
                case 3: return Multiply(b);
                case 4: return Divide(a, b);
            }
            return new Expression(0, 0, '+');
        }

        private Expression Sum(int a, int b)
        {
            return new Expression(rnd.Next(a, b), rnd.Next(a, b), '+');
        }

        private Expression Subtract(int a, int b)
        {
            int m = rnd.Next(a, b);
            int n = rnd.Next(a, b);
            if (m < n)
            {
                int temp = m;
                m = n;
                n = temp;
            }
            return new Expression(m, n, '-');
        }

        private Expression Multiply(int b)
        {
            int m, n;
            do
            {
                m = rnd.Next(2, b);
                n = rnd.Next(2, b);
            }
            while (m * n > b);
            return new Expression(m, n, '*');
        }

        private Expression Divide(int a, int b)
        {
            int m, n;
            do
                m = rnd.Next(a, b);
            while (isSimple(m));
            n = Dividers(m);
            return new Expression(m, n, '/');
        }

        private int Dividers(int m)
        {
            List<int> numbers = new List<int>();
            for (int i = 2; i < m; i++)
            {
                if (m % i == 0) numbers.Add(i);
            }
            int slag2 = rnd.Next(0, numbers.Count - 1);
            return numbers[slag2];
        }

        private bool isSimple(int N)
        {
            bool tf = false;
            //чтоб убедится простое число или нет достаточно проверить не делитсья ли 
            //число на числа до его половины
            for (int i = 2; i < (int)(N / 2); i++)
            {
                if (N % i == 0)
                {
                    tf = false;
                    break;
                }
                else
                {
                    tf = true;
                }
            }
            return tf;
        }
    }
}
