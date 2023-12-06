namespace Gameplay.Towers.Shooters.Projectiles
{
    internal interface IProjectileView
    {
        IProjectileModel ProjectileModel { get; set; }
        void Destroy();
    }
}