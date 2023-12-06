using System.Collections.Generic;

namespace Gameplay.Towers.Shooters.Projectiles.ProjectileContainers
{
    public class ProjectileContainerModel
    {
        public List<IProjectileModel> Projectiles { get; set; } = new();
    }
}