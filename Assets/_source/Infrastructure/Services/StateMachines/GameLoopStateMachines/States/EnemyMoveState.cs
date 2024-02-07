using Infrastructure.Services.CurrentDatas;

namespace Infrastructure.Services.StateMachines.GameLoopStateMachines.States
{
  public class EnemyMoveState : IGameLoopState
  {
    private readonly ICurrentDataService _currentDataService;

    public EnemyMoveState(ICurrentDataService currentDataService)
    {
      _currentDataService = currentDataService;
    }

    public void Enter()
    {
      _currentDataService.FieldModel.EnemySpawnerModel.Spawn();
    }

    public void Exit()
    {
    }
  }
}