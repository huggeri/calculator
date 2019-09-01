using System;
using System.ComponentModel;
using System.Linq;
using System.Globalization;

namespace CalcWpf
{
	internal class vmCalc : INotifyPropertyChanged
	{
		private Operation _storedOperation = Operation.None;
		private Operation _lastOperation = Operation.None;

		private static readonly PropertyChangedEventArgs EA_DisplayNumber = new PropertyChangedEventArgs(nameof(DisplayNumber));
		private Number _displayNumber;

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

		internal void AddNumber(int num)
		{
			ChangeDisplayNumberOnImput();
			DisplayNumber.StrValue += num.ToString();
		}

		private void ChangeDisplayNumberOnImput()
		{
			switch(_storedOperation)
			{
				case Operation.Plus:
				case Operation.Minus:
				case Operation.Div:
				case Operation.Mult:
					DisplayNumber = Right;
					DisplayNumber.Value = 0;
					_lastOperation = _storedOperation;
					_storedOperation = Operation.None;
					break;
			}
		}

		public Number Left { get; private set; }

		public static readonly string Delimiter = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

		internal void AddDelimiter()
		{
			ChangeDisplayNumberOnImput();
			if(_displayNumber.StrValue.Contains(Delimiter))
			{
				return;
			}
			_displayNumber.StrValue += Delimiter;
		}

		public Number Right { get; private set; }

		public vmCalc()
		{
			DisplayNumber = Left = new Number();
			Right = new Number();
		}

		internal void Clear()
		{
			Right.Reset();
			Left.Reset();
			DisplayNumber = Left;
			_lastOperation = _storedOperation = Operation.None;
		}

		public event PropertyChangedEventHandler PropertyChanged;

		internal void StoreOperation(Operation operation)
		{
			_storedOperation = operation;
			Left.CopyValue(DisplayNumber);
			DisplayNumber.Operation = operation;
		}

		private void Calc()
		{
			Operation o = _storedOperation == Operation.None
				? _lastOperation
				: _storedOperation;
			switch(o)
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

		internal void OnCalcPressed()
		{
			DisplayNumber = Left;
			Calc();
			_storedOperation = Operation.None;
			Left.Operation = Operation.None;
			Right.Operation = Operation.None;
		}

		internal void RemoveSymbol()
		{
			if(DisplayNumber.StrValue.Length > 1)
			{
				DisplayNumber.StrValue = DisplayNumber.StrValue.Remove(DisplayNumber.StrValue.Length - 1, 1);
			}
			else
			{
				DisplayNumber.Value = 0;
			}
		}
	}
}
