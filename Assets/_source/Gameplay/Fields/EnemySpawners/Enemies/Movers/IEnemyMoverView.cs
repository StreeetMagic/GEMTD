namespace Gameplay.Fields.EnemySpawners.Enemies.Movers
{
    public interface IEnemyMoverView
    {
        void Init(EnemyMoverModel enemyMoverModel);
        void Move();
    }
}