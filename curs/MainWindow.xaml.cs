using System.Windows;

namespace curs;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainViewModel();
    }

    // показывает или скрывает заголовки
    private void ShowHeadersCheckBoxChanged(object sender, RoutedEventArgs e)
    {
        if (ShowHeadersCheckBox.IsChecked == true)
        {
            HeaderProjectLabel.Visibility = Visibility.Visible;
            EditorTitleLabel.Visibility = Visibility.Visible;
        }
        else
        {
            HeaderProjectLabel.Visibility = Visibility.Collapsed;
            EditorTitleLabel.Visibility = Visibility.Collapsed;
        }
    }
}