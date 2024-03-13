using MaterialDesignThemes.Wpf;
using System;
using System.Globalization;
using System.Windows.Data;

namespace KekboomKawaii.Converter
{
    public class WindowIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value is bool boolean && boolean) ? PackIconKind.WindowRestore : PackIconKind.WindowMaximize;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is PackIconKind kind && kind == PackIconKind.WindowRestore;
        }
    }
}
