using System.ComponentModel;
using System.Globalization;

namespace CalcWpf
{
    internal class vmCalc : INotifyPropertyChanged
    {
        private Operation _operation = Operation.None;

        private static readonly PropertyChangedEventArgs EA_CurrentNumber = new PropertyChangedEventArgs(nameof(CurrentNumber));
        private Number _currentNumber;

        public Number CurrentNumber
        {
            get => _currentNumber;
            private set
            {
                if (_currentNumber != value)
                {
                    _currentNumber = value;
                    PropertyChanged?.Invoke(this, EA_CurrentNumber);
                }
            }
        }

        internal void AddNumber(int num)
        {
            CurrentNumber.StrValue += num.ToString();
        }

        public Number Left { get; private set; }

        public static readonly string Delimiter = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

        internal void AddDelimiter()
        {
            if (_currentNumber.StrValue.Contains(Delimiter))
            {
                return;
            }
            _currentNumber.StrValue += Delimiter;
        }

        public Number Right { get; private set; }

        public vmCalc()
        {
            CurrentNumber = Left = new Number();
            Right = new Number();
        }

        internal void Clear()
        {
            Right.Value = 0;
            Left.Value = 0;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
