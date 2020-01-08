using System;

namespace calculator
{
    // класс для любого значения в калькуляторе
    public class Value
    {
        private
            string numberString;
            double val;

        // конструкторы
        public 
            Value() : this("0")
            {
            }

            Value(string n)
            {
                NumberString = n;
            }

            Value(double n)
            {
                Val = n;
            }

        // свойства
        public double Val
        {
            set
            {
                val = value;
                numberString = Convert.ToString(value);
            }

            get
            {
                return val;
            }
        }

        public string NumberString
        {
            set
            {
                numberString = value;

                if (value == "")
                {
                    val = 0;
                }
                else
                {
                    try
                    {
                        val = Convert.ToDouble(value);
                    }
                    catch(Exception)
                    {
                        val = 0;
                    }
                }
            }

            get
            {
                return numberString;
            }
        }

        // методы
        // сложение
        public static Value operator +(Value obj1, Value obj2)
        {
            return new Value(obj1.Val + obj2.Val);
        }

        // вычитание
        public static Value operator -(Value obj1, Value obj2)
        {
            return new Value(obj1.Val - obj2.Val);
        }

        // умножение
        public static Value operator *(Value obj1, Value obj2)
        {
            return new Value(obj1.Val * obj2.Val);
        }

        // деление
        public static Value operator /(Value obj1, Value obj2)
        {
            return new Value(obj1.Val / obj2.Val);
        }

        // взятие корней
        public Value GetRadical(Value n)
        {
            Value result = new Value();

            if (this.Val > 0 && n.Val <= 1000.0 && n.Val > 0.0)
            {
                if (Math.Truncate(n.Val) == 2.0)
                {
                    result.Val = Math.Sqrt(this.Val);
                }
                else
                {
                    result.Val = Math.Pow(this.Val, 1.0 / Math.Truncate(n.Val));
                }
            }

            return result;
        }

        // возведение в степень
        public Value ToPow(Value n)
        {
            if (n.Val > 1000.0 || n.Val < 0)
                n.Val = 0;
            return new Value(Math.Pow(this.Val, Math.Truncate(n.Val)));
        }

        // факториал
        public Value Factorial(Value number)
        {
            Value result = new Value();
            double subVal = 0;

            try
            {
                if (number.Val <= 1000.0)
                {
                    if (number.Val >= 2.0)
                    {
                        subVal = Math.Truncate(number.Val);

                        for (double i = subVal - 1.0; i > 0; i--)
                        {
                            subVal *= i;
                        }
                    }
                    else if (number.Val >= 0)
                    {
                        subVal = 1;
                    }
                }

                result.Val = subVal;
            }
            catch (Exception)
            {
                result.Val = 0;
            }

            return result;
        }
    }
}