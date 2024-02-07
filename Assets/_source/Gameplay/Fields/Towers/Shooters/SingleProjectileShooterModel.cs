using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay.Fields.Enemies;
using Gameplay.Fields.Towers.Shooters.Projectiles.ProjectileContainers;
using Infrastructure.Services.GameFactories;
using Infrastructure.Utilities;
using UnityEngine;

namespace Gameplay.Fields.Towers.Shooters
{
  class SingleProjectileShooterModel : IShooter
  {
    private IGameFactoryService _gameFactoryService;
    private EnemyModel _currentTarget;
    private readonly CoroutineDecorator _coroutine;
    private readonly float _cooldown = .5f;

    public Transform ShootingPoint { get; set; }
    public ProjectileContainerModel ProjectileContainerModel { get; set; }
    public List<EnemyModel> Targets { get; set; } = new();

    private MonoBehaviour CoroutineRunner;

    public SingleProjectileShooterModel(IGameFactoryService gameFactoryService)
    {
      _gameFactoryService = gameFactoryService;
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

    public void AddTarget(EnemyModel target)
    {
      if (Targets.Contains(target) == false)
      {
        Targets.Add(target);
        target.Died += OnTargetDied;
      }
    }

    public void RemoveTarget(EnemyModel target)
    {
      if (Targets.Contains(target))
      {
        if (_currentTarget == target)
        {
          _currentTarget = null;
        }

        Targets.Remove(target);
        target.Died -= OnTargetDied;
      }
    }

    private IEnumerator Shooting(Action onComplete)
    {
      while (_currentTarget != null)
      {
        _gameFactoryService.CreateProjectile(ShootingPoint, _currentTarget);
        yield return new WaitForSeconds(_cooldown);
      }

      onComplete?.Invoke();
    }

    private void OnTargetDied(EnemyModel target)
    {
      if (_currentTarget == target)
      {
        _currentTarget = null;
      }

      if (Targets.Contains(target))
      {
      }

      {
        Targets.Remove(target);
      }
    }
  }
}