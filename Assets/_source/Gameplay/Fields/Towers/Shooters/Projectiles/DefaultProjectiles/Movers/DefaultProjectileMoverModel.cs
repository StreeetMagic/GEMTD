using Gameplay.Fields.Enemies;
using UnityEngine;

namespace Gameplay.Fields.Towers.Shooters.Projectiles.DefaultProjectiles.Movers
{
  internal class DefaultProjectileMoverModel : IProjectileMoverModel
  {
    public DefaultProjectileMoverModel(EnemyModel target, IProjectileModel projectileModel)
    {
      Target = target;
      ProjectileModel = projectileModel;
    }

    public IProjectileModel ProjectileModel { get; }

    #region IProjectileMoverModel Members

    public float Speed { get; set; } = 10f;
    public EnemyModel Target { get; set; }
    public Vector3 Position { get; private set; }

    public void Move(Vector3 position)
    {
      Position = position;
    }

    #endregion
  }
}