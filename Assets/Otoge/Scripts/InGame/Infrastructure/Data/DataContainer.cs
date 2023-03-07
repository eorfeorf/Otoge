using Otoge.Scripts.InGame.Application.Interface;

public class DataContainer : IDataContainer
{

    /// <summary>
    /// ノーツ管理.
    /// </summary>
    public NoteContainer NoteContainer => noteContainer;
    private NoteContainer noteContainer;

    public DataContainer()
    {
        noteContainer = new NoteContainer();
    }
}
