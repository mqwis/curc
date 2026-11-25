using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace curs;

/// <summary>
/// хранит данные главного окна
/// </summary>
public sealed class MainViewModel : INotifyPropertyChanged
{
    private string projectName = "Новый проект";
    private string selectedElementName = "Имя и фамилия";

    public event PropertyChangedEventHandler? PropertyChanged;

    public string ProjectName
    {
        get => projectName;
        set
        {
            projectName = value;
            OnPropertyChanged();
        }
    }

    public string SelectedElementName
    {
        get => selectedElementName;
        set
        {
            selectedElementName = value;
            OnPropertyChanged();
        }
    }

    public string CardSize => "Визитка 90 × 50 мм";

    // сообщает окну об изменении свойства
    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
