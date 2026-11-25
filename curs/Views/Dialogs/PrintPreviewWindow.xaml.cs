using System.Windows;

namespace curs.Views.Dialogs;

/// <summary>
/// показывает окно печати визитки
/// </summary>
public partial class PrintPreviewWindow : Window
{
    // открывает окно печати
    public PrintPreviewWindow()
    {
        InitializeComponent();
    }

    // закрывает окно печати
    private void CloseButtonClick(object sender, RoutedEventArgs e)
    {
        Close();
    }
}