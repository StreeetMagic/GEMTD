using Gameplay.Fields.Enemies;
using Gameplay.Fields.Towers.Shooters.Projectiles.DefaultProjectiles.Movers;
using UnityEngine;

namespace Gameplay.Fields.Towers.Shooters.Projectiles.DefaultProjectiles
{
  public class DefaultProjectileView : MonoBehaviour, IProjectileView
  {
    [field: SerializeField] public DefaultProjectileMoverView MoverView { get; set; }

    #region IProjectileView Members

    public IProjectileModel ProjectileModel { get; set; }

    public void Destroy()
    {
      //gameObject.SetActive(false);
      Destroy(gameObject);
    }

    #endregion

    public void Init(IProjectileModel projectileModel)
    {
      ProjectileModel = projectileModel;
      MoverView.Init(ProjectileModel.Mover);
      ProjectileModel.Mover.Target.Died += OnTargetDied;
      ProjectileModel.Died += OnDied;
    }

    private void OnDestroy()
    {
      ProjectileModel.Mover.Target.Died -= OnTargetDied;
      ProjectileModel.Died -= OnDied;
    }

    private void OnTargetDied(EnemyModel enemyModel)
    {
      ProjectileModel.Die();
    }

    private void OnDied()
    {
      Destroy();
    }
  }
}