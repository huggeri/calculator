using System.Windows.Input;

namespace CalcWpf
{
    internal static class Commands
    {
        public static RoutedCommand NumCmd = new RoutedCommand(nameof(NumCmd), typeof(Commands));
        public static RoutedCommand DotCmd = new RoutedCommand(nameof(DotCmd), typeof(Commands));
        public static RoutedCommand ClearCmd = new RoutedCommand(nameof(ClearCmd), typeof(Commands));
        public static RoutedCommand StoreOperation = new RoutedCommand(nameof(StoreOperation), typeof(Commands));
        public static RoutedCommand CalcCmd = new RoutedCommand(nameof(CalcCmd), typeof(Commands));
    }
}
