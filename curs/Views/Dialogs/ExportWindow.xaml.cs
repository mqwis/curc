using System.Windows;

namespace curs.Views.Dialogs;

/// <summary>
/// открывает окно экспорта визитки
/// </summary>
public partial class ExportWindow : Window
{
    // открывает окно экспорта
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