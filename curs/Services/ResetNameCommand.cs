namespace curs.Services;

/// <summary>
/// сбрасывает название выбранного элемента
/// </summary>
public sealed class ResetNameCommand : IEditorCommand
{
    private readonly MainViewModel viewModel;
    private readonly string oldName;

    public ResetNameCommand(MainViewModel viewModel)
    {
        this.viewModel = viewModel;
        oldName = viewModel.SelectedElementName;
    }

    // сбрасывает название к стандартному
    public void Execute()
    {
        viewModel.SelectedElementName = "Имя и фамилия";
    }

    // возвращает прежнее название
    public void Undo()
    {
        viewModel.SelectedElementName = oldName;
    }
}
