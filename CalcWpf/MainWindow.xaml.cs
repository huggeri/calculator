using System.Windows;
using System.Windows.Input;

namespace CalcWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
			BindCommands();
		}

		private void BindCommands()
		{
			for(int i = 0; i <= 9; i++)
			{
				InputBindings.Add(new KeyBinding(Commands.NumCmd, Key.NumPad0 + i, ModifierKeys.None)
				{
					CommandParameter = i.ToString()
				});
			}
		}

        private void NumCmd_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
			int tmp;
			e.CanExecute = int.TryParse(e?.Parameter?.ToString(), out tmp);
        }

        private void StoreOperation_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = e.Parameter is Operation;
        }

        private void NumCmd_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            VM.AddNumber(int.Parse(e.Parameter.ToString()));
        }

        private void ClearCmd_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            VM.Clear();
        }

        private void DotCmd_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            VM.AddDelimiter();
        }

        private void AlwaysCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

		private void StoreOperation_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			VM.StoreOperation((Operation)e.Parameter);
		}

		private void CalcCmd_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			VM.OnCalcPressed();
		}

		private void RemoveCmd_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			VM.RemoveSymbol();
		}
	}
}
