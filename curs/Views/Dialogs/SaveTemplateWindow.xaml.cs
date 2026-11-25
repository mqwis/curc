using System.Windows;

namespace curs.Views.Dialogs;

/// <summary>
/// открывает окно сохранения шаблона
/// </summary>
public partial class SaveTemplateWindow : Window
{
    // открывает окно шаблона
    public SaveTemplateWindow()
    {
        InitializeComponent();
    }

    // закрывает окно шаблона
    private void CloseButtonClick(object sender, RoutedEventArgs e)
    {
        Close();
    }
}