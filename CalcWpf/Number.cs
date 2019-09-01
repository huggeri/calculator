using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

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
		private static readonly PropertyChangedEventArgs EA_Text = new PropertyChangedEventArgs(nameof(Text));
		private double _value;
		private string _strValue;

		public double Value
		{
			get
			{
				return _value;
			}
			set
			{
				if(value != _value)
				{
					_value = value;
					_strValue = value.ToString();
					PropertyChanged?.Invoke(this, EA_Value);
					PropertyChanged?.Invoke(this, EA_StrValue);
					PropertyChanged?.Invoke(this, EA_Text);
				}
			}
		}

		private Operation _operation;
		public Operation Operation
		{
			get
			{
				return _operation;
			}
			set
			{
				if(_operation == value)
				{
					return;
				}
				_operation = value;
				PropertyChanged?.Invoke(this, EA_Text);
			}
		}

		public string Text
		{
			get
			{
				string text = _strValue;
				if(_operation == Operation.None)
				{
					return text;
				}
				char last = _strValue.Last();
				if(last.ToString() == vmCalc.Delimiter)
				{
					if(_value % 1 == 0)
					{
						text = "0";
					}
					else
					{
						text += "0";
					}
				}
				return text += GetOperationStr(_operation);
			}
		}

		public static string GetOperationStr(Operation operation)
		{
			string operationStr = string.Empty;
			switch(operation)
			{
				case Operation.Plus:
				case Operation.Minus:
				case Operation.Div:
				case Operation.Mult:
					operationStr = _operationChars[operation].ToString();
					break;
			}
			return operationStr;
		}

		private static Dictionary<Operation, char> _operationChars = new Dictionary<Operation, char>
		{
			{Operation.Div, '/' },
			{Operation.Plus, '+' },
			{Operation.Minus, '-' },
			{Operation.Mult, '*' }
		};

		public string StrValue
		{
			get
			{
				return _strValue;
			}
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
				double val;
				if(value != _strValue
					&& double.TryParse(value, out val))
				{
					_strValue = value;
					_value = val;
					PropertyChanged?.Invoke(this, EA_Value);
					PropertyChanged?.Invoke(this, EA_StrValue);
					PropertyChanged?.Invoke(this, EA_Text);
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
			_value = 0;
			_strValue = "0";
			_operation = Operation.None;
			PropertyChanged?.Invoke(this, EA_Value);
			PropertyChanged?.Invoke(this, EA_StrValue);
			PropertyChanged?.Invoke(this, EA_Text);
		}
	}
}
