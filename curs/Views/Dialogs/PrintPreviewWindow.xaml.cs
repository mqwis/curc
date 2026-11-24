using System.Windows;

namespace curs.Views.Dialogs;

public partial class PrintPreviewWindow : Window
{
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