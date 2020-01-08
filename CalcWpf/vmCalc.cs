using System;
using System.ComponentModel;
using System.Globalization;

namespace CalcWpf
{
	internal class vmCalc : INotifyPropertyChanged
	{
        // FIELDS

		private Operation _storedOperation = Operation.None;
		private Operation _lastOperation = Operation.None;
		private static readonly PropertyChangedEventArgs EA_DisplayNumber = new PropertyChangedEventArgs(nameof(DisplayNumber));
		private Number _displayNumber;

		public static readonly string Delimiter = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
		public event PropertyChangedEventHandler PropertyChanged;

        // PROPERTIES

		public Number Left { get; private set; }
		public Number Right { get; private set; }

		public Number DisplayNumber
		{
			get
            {
                return _displayNumber;
            }
			private set
			{
				if(_displayNumber != value)
				{
					_displayNumber = value;
					PropertyChanged?.Invoke(this, EA_DisplayNumber);
				}
			}
		}

        // METHODS

		private void ChangeDisplayNumberOnImput()
		{
            _lastOperation = Operation.None;
			switch(_storedOperation)
			{
				case Operation.Plus:
				case Operation.Minus:
				case Operation.Div:
				case Operation.Mult:
					DisplayNumber = Right;
					break;
			}
		}

		private void Calc()
		{
			Operation o = _storedOperation == Operation.None
				? _lastOperation
				: _storedOperation;

            DisplayNumber = Left;

            switch (o)
			{
				case Operation.Plus:
					DisplayNumber.Value = Left.Value + Right.Value;
					break;
				case Operation.Minus:
					DisplayNumber.Value = Left.Value - Right.Value;
					break;
				case Operation.Div:
					DisplayNumber.Value = Left.Value / Right.Value;
					break;
				case Operation.Mult:
					DisplayNumber.Value = Left.Value * Right.Value;
					break;
				default:
					return;
			}
            _lastOperation = o;
		}

		internal void AddNumber(int num)
		{
			ChangeDisplayNumberOnImput();
			DisplayNumber.StrValue += num.ToString();
		}

		internal void AddDelimiter()
		{
			ChangeDisplayNumberOnImput();
			if(_displayNumber.StrValue.Contains(Delimiter))
			{
				return;
			}
			_displayNumber.StrValue += Delimiter;
		}

		internal void OnCalcPressed()
		{
			Calc();
			_storedOperation = Operation.None;
        }

		internal void Clear()
		{
			Right.Reset();
			Left.Reset();
			DisplayNumber = Left;
			_lastOperation = _storedOperation = Operation.None;
		}

		internal void StoreOperation(Operation parameter)
		{
            if (_storedOperation == Operation.None)
            {
                _storedOperation = parameter;
            }

			if(ReferenceEquals(_displayNumber, Right))
			{
				Calc();

                if (_storedOperation == _lastOperation)
                {
                    _storedOperation = parameter;
                    Right.Reset();
                }
            }
			else
			{
				Left.CopyValue(DisplayNumber);
                Right.Reset();
            }
		}

        // CONSTRUCTOR

        public vmCalc()
		{
			DisplayNumber = Left = new Number();
			Right = new Number();
		}
	}
}
