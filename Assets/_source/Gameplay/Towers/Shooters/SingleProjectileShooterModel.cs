using System;
using System.Collections;
using System.Collections.Generic;
using InfastuctureCore.ServiceLocators;
using InfastuctureCore.Services.StateMachineServices;
using InfastuctureCore.Services.StaticDataServices;
using InfastuctureCore.Utilities;
using Infrastructure.GameStateMachines;
using Infrastructure.Services.GameFactoryServices;
using UnityEngine;

namespace Gameplay.Towers.Shooters
{
    class SingleProjectileShooterModel : IShooter
    {
        private Transform _currentTarget;
        private CoroutineDecorator _coroutine;

        public Stack<Transform> Targets { get; set; } = new();
        public Transform ShootingPoint { get; set; }

        private IGameFactoryService GameFactoryService => ServiceLocator.Instance.Get<IGameFactoryService>();
        private MonoBehaviour CoroutineRunner => ServiceLocator.Instance.Get<IStateMachineService<GameStateMachineData>>().Data.CoroutineRunner;

        public SingleProjectileShooterModel()
        {
            _coroutine = new CoroutineDecorator(CoroutineRunner, Shooting);
        }

        public void Shoot()
        {
            if (_currentTarget == null)
            {
                Debug.Log("Цели нет");

                if (Targets.Count > 0)
                {
                    Debug.Log("назначаю цель");
                    _currentTarget = Targets.Pop();
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

        private IEnumerator Shooting()
        {
            while (true)
            {
                yield return new WaitForSeconds(.2f);

                GameFactoryService.CreateProjectile();
            }
        }
    }
}