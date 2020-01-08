using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CalcWpf
{
	/// <summary>
	/// Interaction logic for ucScalableCommnd.xaml
	/// </summary>
	public partial class ucScalableCommnd : UserControl
	{
		public ucScalableCommnd()
		{
			InitializeComponent();
		}

		public object CommandParameter
		{
			get { return GetValue(CommandParameterProperty); }
			set { SetValue(CommandParameterProperty, value); }
		}

		public ICommand Command
		{
			get { return (ICommand)GetValue(CommandProperty); }
			set { SetValue(CommandProperty, value); }
		}

		public string TextValue
		{
			get { return GetValue(TextValueProperty) as string; }
			set { SetValue(TextValueProperty, value); }
		}

		public static readonly DependencyProperty TextValueProperty
			= DependencyProperty.Register(
				  nameof(TextValue),
				  typeof(string),
				  typeof(ucScalableCommnd)
			  );

		public static readonly DependencyProperty CommandProperty
			= DependencyProperty.Register(
				  nameof(Command),
				  typeof(ICommand),
				  typeof(ucScalableCommnd)
			  );

		public static readonly DependencyProperty CommandParameterProperty
			= DependencyProperty.Register(
				  nameof(CommandParameter),
				  typeof(object),
				  typeof(ucScalableCommnd)
			  );
	}
}
