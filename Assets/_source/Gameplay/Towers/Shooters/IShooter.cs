using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Towers.Shooters
{
    public interface IShooter
    {
        Stack<Transform> Targets { get; set; }
        Transform ShootingPoint { get; set; }
        void Shoot();
    }
}