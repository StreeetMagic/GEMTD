using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay.Fields.Towers.Shooters.Projectiles.ProjectileContainers;
using InfastuctureCore.ServiceLocators;
using InfastuctureCore.Services.CoroutineRunnerServices;
using InfastuctureCore.Utilities;
using Infrastructure.Services.GameFactoryServices;
using UnityEngine;

namespace Gameplay.Fields.Towers.Shooters
{
    class SingleProjectileShooterModel : IShooter
    {
        private Transform _currentTarget;
        private CoroutineDecorator _coroutine;

        public List<Transform> Targets { get; set; } = new();
        public Transform ShootingPoint { get; set; }
        public ProjectileContainerModel ProjectileContainerModel { get; set; }

        private IGameFactoryService GameFactoryService => ServiceLocator.Instance.Get<IGameFactoryService>();
        private MonoBehaviour CoroutineRunner => ServiceLocator.Instance.Get<ICoroutineRunnerService>().Instance;

        public SingleProjectileShooterModel()
        {
            _coroutine = new CoroutineDecorator(CoroutineRunner, Shooting);
            ProjectileContainerModel = new ProjectileContainerModel();
        }

        public void Shoot()
        {
            if (_currentTarget == null)
            {
                if (Targets.Count > 0)
                {
                    _currentTarget = Targets[0];
                }
            }

            if (_currentTarget != null)
            {
                if (_coroutine.IsRunning == false)
                    _coroutine.Start();
            }
            else
            {
                _coroutine.Stop();
            }
        }

        public void AddTarget(Transform otherTransform)
        {
            if (!Targets.Contains(otherTransform))
            {
                Targets.Add(otherTransform);
            }
        }

        public void RemoveTarget(Transform otherTransform)
        {
            if (Targets.Contains(otherTransform))
            {
                if (_currentTarget == otherTransform)
                {
                    _currentTarget = null;
                }

                Targets.Remove(otherTransform);
            }
        }

        private IEnumerator Shooting(Action onComplete)
        {
            while (_currentTarget != null)
            {
                yield return new WaitForSeconds(.2f);

                GameFactoryService.CreateProjectile(ShootingPoint, _currentTarget);
            }

            onComplete?.Invoke();
        }
    }
}