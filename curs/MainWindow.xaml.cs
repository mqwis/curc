using System.Windows;
using Microsoft.Win32;
using curs.Data.Models;
using curs.Services;

namespace curs;

/// <summary>
/// главное окно редактора визиток
/// </summary>
public partial class MainWindow : Window
{
    private readonly CardEditorFacade editor = new();
    private IEditorCommand? lastCommand;

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
            var command = new ResetNameCommand(viewModel);
            command.Execute();
            lastCommand = command;
        }
    }

    // создает новую пустую визитку
    private void NewCardClick(object sender, RoutedEventArgs e)
    {
        if (DataContext is MainViewModel viewModel)
        {
            viewModel.ProjectName = "Новая визитка";
            viewModel.SelectedElementName = "Имя и фамилия";
        }
    }

    // сохраняет визитку в postgres
    private async void SaveCardClick(object sender, RoutedEventArgs e)
    {
        try
        {
            if (DataContext is not MainViewModel viewModel)
            {
                return;
            }

            var card = new BusinessCard
            {
                Name = viewModel.ProjectName,
                WidthMm = 90,
                HeightMm = 50,
                BackgroundColor = "#FFFFFF"
            };

            await editor.SaveAsync(card);
            MessageBox.Show("Визитка сохранена", "Сохранение", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        catch (Exception)
        {
            MessageBox.Show("Не удалось сохранить визитку. Проверьте подключение к PostgreSQL.", "Ошибка сохранения", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    // открывает визитку по номеру
    private async void OpenCardClick(object sender, RoutedEventArgs e)
    {
        try
        {
            var answer = Microsoft.VisualBasic.Interaction.InputBox("Введите номер визитки", "Открыть визитку", "1");

            if (!int.TryParse(answer, out int cardId))
            {
                return;
            }

            var card = await editor.LoadAsync(cardId);

            if (card == null)
            {
                MessageBox.Show("Визитка не найдена", "Открытие", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (DataContext is MainViewModel viewModel)
            {
                viewModel.ProjectName = card.Name;
            }

            MessageBox.Show("Визитка загружена", "Открытие", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        catch (Exception)
        {
            MessageBox.Show("Не удалось открыть визитку. Проверьте номер и подключение к PostgreSQL.", "Ошибка открытия", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    // экспортирует холст в png
    private void ExportCardClick(object sender, RoutedEventArgs e)
    {
        var dialog = new SaveFileDialog
        {
            Filter = "Изображение PNG|*.png",
            FileName = "Визитка.png"
        };

        if (dialog.ShowDialog() == true)
        {
            try
            {
                editor.ExportPng(CardPreview, dialog.FileName);
                MessageBox.Show("Визитка экспортирована", "Экспорт", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Ошибка экспорта", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    // отменяет последнюю команду
    private void UndoClick(object sender, RoutedEventArgs e)
    {
        if (lastCommand != null)
        {
            lastCommand.Undo();
            lastCommand = null;
        }
    }
}