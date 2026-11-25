using System.Windows;
using Microsoft.EntityFrameworkCore;
using curs.Data;
using curs.Data.Models;

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
            var options = new DbContextOptionsBuilder<BusinessCardDbContext>()
                .UseNpgsql("Host=localhost;Port=5432;Database=business_cards;Username=postgres;Password=123")
                .Options;

            using var context = new BusinessCardDbContext(options);
            var service = new CardService(context);

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

            await service.SaveAsync(card);
            MessageBox.Show("Визитка сохранена", "Сохранение", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        catch (Exception exception)
        {
            MessageBox.Show(exception.Message, "Ошибка сохранения", MessageBoxButton.OK, MessageBoxImage.Error);
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

            var options = new DbContextOptionsBuilder<BusinessCardDbContext>()
                .UseNpgsql("Host=localhost;Port=5432;Database=business_cards;Username=postgres;Password=123")
                .Options;

            using var context = new BusinessCardDbContext(options);
            var service = new CardService(context);
            var card = await service.LoadAsync(cardId);

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
        catch (Exception exception)
        {
            MessageBox.Show(exception.Message, "Ошибка открытия", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}