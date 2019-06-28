using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathGame
{
    public class Expression //Выражение вида a sign b = c; с - вычисляется автоматически
    {
        public int a { get; }
        public int b { get; }
        public int c { get; }
        public char sign { get; }

        /// <summary>
        /// Формируем выражение и получаем итоговое значение
        /// </summary>
        /// <param name="а">Первое число</param>
        /// <param name="b">Второе число</param>
        /// <param name="sign">Сигнатура</param>
        public Expression(int a, int b, char sign)
        {
            this.a = a;
            this.b = b;
            if (sign != '+' && sign != '-' && sign != '/' && sign != '*') throw new ArgumentException("Такой операции не существует");
            this.sign = sign;
            switch (sign)
            {
                case '+': c = a + b; break;
                case '-': c = a - b; break;
                case '*': c = a * b; break;
                case '/': c = a / b; break;
            }
        }

        public override string ToString()
        {
            return a + " " + sign.ToString() + " " + b;
        }
    }

}
