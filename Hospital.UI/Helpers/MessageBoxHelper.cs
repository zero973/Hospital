using System.Windows;

namespace Hospital.UI.Helpers;

internal static class MessageBoxHelper
{

    public static void InfoMessage(string text, string caption = "Уведомление")
    {
        MessageBox.Show(text, caption, MessageBoxButton.OK, MessageBoxImage.Information);
    }

    public static void ErrorMessage(string text, string caption = "Ошибка")
    {
        MessageBox.Show(text, caption, MessageBoxButton.OK, MessageBoxImage.Error);
    }

    public static void ErrorMessage(IEnumerable<string> errors, string caption = "Ошибка")
    {
        MessageBox.Show(string.Join("; ", errors), caption, MessageBoxButton.OK, MessageBoxImage.Error);
    }

}