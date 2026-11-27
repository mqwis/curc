namespace curs.Services;

/// <summary>
/// описывает команду редактора
/// </summary>
public interface IEditorCommand
{
    // выполняет команду
    void Execute();

    // отменяет команду
    void Undo();
}
