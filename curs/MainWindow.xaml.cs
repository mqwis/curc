using System.Windows;

namespace curs;

/// <summary>
/// главное окно редактора визиток
/// </summary>
public partial class MainWindow : Window
{
    // открывает главное окно и viewmodel
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

    // сбрасывает название выбранного элемента
    private void ResetElementNameClick(object sender, RoutedEventArgs e)
    {
        if (DataContext is MainViewModel viewModel)
        {
            viewModel.SelectedElementName = "Имя и фамилия";
        }
    }
}