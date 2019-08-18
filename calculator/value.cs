using System;

namespace calculator
{
// класс для любого значения в калькуляторе
    public class Value
    {
        private
            string stringNumber;
            double val;

        //конструкторы
        public 
            Value() : this("0")
            {
            }

            Value(string n)
            {
                stringNumber = n;

                if(n == "")
                {
                    val = 0;
                }
                else
                {
                    try
                    {
                        val = Convert.ToDouble(n);
                    }
                    catch(Exception)
                    {
                        throwingException();
                    }
                }
            }

            Value(double n)
            {
                val = n;
                stringNumber = Convert.ToString(n);
            }

        // свойства
        public double Val
        {
            set
            {
                val = value;
                stringNumber = Convert.ToString(value);
            }

            get
            {
                return val;
            }
        }

        public string StringNumber
        {
            set
            {
                stringNumber = value;

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
                        throwingException();
                    }
                }
            }

            get
            {
                return stringNumber;
            }
        }

        // методы
        // выкидывающий исключение
        private void throwingException()
        {
            val = -1;
            stringNumber = "error, please push c";
            throw new Exception();
        }

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
        public Value GetRadical(Value number, int n)
        {
            Value result = new Value();

            if (number.Val > 0)
            {
                if (n == 2)
                {
                    result.Val = Math.Sqrt(number.Val);
                }
                else
                {
                    result.Val = Math.Pow(number.Val, 1.0 / 3.0);
                }
            }

            return result;
        }

        // возведение в степень
        public Value ToPow(Value number, Value n)
        {
            return new Value(Math.Pow(number.Val, n.Val));
        }

        // факториал
        public Value Factorial(Value number)
        {
            Value result = new Value();
            int subVal = 0;

            try
            {
                subVal = Int32.Parse(number.StringNumber);
            }
            catch(Exception)
            {
                throwingException();
            }

            if (subVal > 0)
            {
                for (int i = subVal - 1; i > 0; i--)
                {
                    subVal *= i;
                }

                result.Val = subVal;
            }

            return result;
        }
    }
}
