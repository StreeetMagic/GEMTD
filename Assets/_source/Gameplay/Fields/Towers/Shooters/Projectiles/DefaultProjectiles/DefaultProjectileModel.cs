using System;
using Gameplay.Fields.Enemies;
using Gameplay.Fields.Towers.Shooters.Projectiles.DefaultProjectiles.Movers;

namespace Gameplay.Fields.Towers.Shooters.Projectiles.DefaultProjectiles
{
  internal class DefaultProjectileModel : IProjectileModel
  {
    public DefaultProjectileModel(EnemyModel target)
    {
      Mover = new DefaultProjectileMoverModel(target, this);
    }

    #region IProjectileModel Members

    public event Action Died;

    public IProjectileMoverModel Mover { get; }
    public float Damage { get; set; } = 4f;

    public void Die()
    {
      Died?.Invoke();
    }

    #endregion
  }
}