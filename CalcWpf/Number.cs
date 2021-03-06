﻿using System;
using System.ComponentModel;

namespace CalcWpf
{
    // ANY VALUE IN PROGRAM
	internal class Number : INotifyPropertyChanged
	{
        // FIELDS

		private static readonly PropertyChangedEventArgs EA_Value = new PropertyChangedEventArgs(nameof(Value));
		private static readonly PropertyChangedEventArgs EA_StrValue = new PropertyChangedEventArgs(nameof(StrValue));
		private double _value;
		private string _strValue;

		public event PropertyChangedEventHandler PropertyChanged;

        // PROPERTIES

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
				}
			}
		}

        public string StrValue
		{
			get
            {
                return _strValue;
            }
			set
			{
                double val;

                if (string.IsNullOrEmpty(value))
				{
					return;
				}
				if(!value.Contains(vmCalc.Delimiter))
				{
					value = value.TrimStart('0');
				}
				if(value != _strValue
					&& double.TryParse(value, out val))
				{
					_strValue = value;
					_value = val;
					PropertyChanged?.Invoke(this, EA_Value);
					PropertyChanged?.Invoke(this, EA_StrValue);
				}
			}
		}

        // FROM TEXTBOX INTO NUMBER
		internal void CopyValue(Number displayNumber)
		{
			Value = displayNumber.Value;
		}

        // NUMBER = 0
		internal void Reset()
		{
			Value = 0;
		}

        // CONSTRUCTOR
		public Number(double number = 0d)
		{
			_value = number;
			_strValue = number.ToString();
		}
	}
}
