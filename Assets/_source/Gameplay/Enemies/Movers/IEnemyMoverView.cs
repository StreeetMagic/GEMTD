namespace Gameplay.Enemies.Movers
{
    public interface IEnemyMoverView
    {
        void Init(EnemyMoverModel enemyMoverModel);
        void Move();
    }
}