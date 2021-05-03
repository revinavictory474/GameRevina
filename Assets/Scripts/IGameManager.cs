public interface IGameManager 
{
    ManagerStatus managerStatus { get; }

    void Startup(NetworkService networkService);
}
