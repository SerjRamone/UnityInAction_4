public interface IGameManager
{
    ManagerStatus status { get; } //перечисление, которое нужно определить

    void Startup();
}
