using System.Windows;

namespace curs.Views.Dialogs;

public partial class ExportWindow : Window
{
    public ExportWindow()
    {
        InitializeComponent();
    }

    // закрывает окно экспорта
    private void CloseButtonClick(object sender, RoutedEventArgs e)
    {
        Close();
    }
}