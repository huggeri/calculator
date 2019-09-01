using System;
using System.ComponentModel;

namespace CalcWpf
{
	internal class Number : INotifyPropertyChanged
	{
		public Number(double number = 0d)
		{
			_value = number;
			_strValue = number.ToString();
		}

		private static readonly PropertyChangedEventArgs EA_Value = new PropertyChangedEventArgs(nameof(Value));
		private static readonly PropertyChangedEventArgs EA_StrValue = new PropertyChangedEventArgs(nameof(StrValue));
		private double _value;
		private string _strValue;

		public double Value
		{
			get => _value;
			set
			{
				if(value != _value)
				{
					_value = value;
					_strValue = value.ToString();
					PropertyChanged?.Invoke(this, EA_Value);
					PropertyChanged?.Invoke(this, EA_StrValue);
				}
			}
		}

		public string StrValue
		{
			get => _strValue;
			set
			{
				if(string.IsNullOrEmpty(value))
				{
					return;
				}
				if(!value.Contains(vmCalc.Delimiter))
				{
					value = value.TrimStart('0');
				}
				if(value != _strValue
					&& double.TryParse(value, out double val))
				{
					_strValue = value;
					_value = val;
					PropertyChanged?.Invoke(this, EA_Value);
					PropertyChanged?.Invoke(this, EA_StrValue);
				}
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		internal void CopyValue(Number displayNumber)
		{
			Value = displayNumber.Value;
		}

		internal void Reset()
		{
			Value = 0;
		}
	}
}
