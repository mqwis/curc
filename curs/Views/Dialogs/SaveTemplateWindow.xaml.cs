using System.Windows;

namespace curs.Views.Dialogs;

public partial class SaveTemplateWindow : Window
{
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