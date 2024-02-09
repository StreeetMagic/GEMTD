using System;
using System.Collections;
using UnityEngine;

namespace Infrastructure.Utilities
{
  public class CoroutineDecorator
  {
    private readonly Func<Action, IEnumerator> _coroutineFunc;
    private readonly MonoBehaviour _runner;
    private Coroutine _coroutine;

    public CoroutineDecorator(MonoBehaviour runner, Func<Action, IEnumerator> coroutineFunc)
    {
      _runner = runner;
      _coroutineFunc = coroutineFunc;
    }

    public bool IsRunning { get; private set; }

    public void Start(Action onComplete = null)
    {
      if (IsRunning && _coroutine != null)
        _runner.StopCoroutine(_coroutine);

      _coroutine = _runner.StartCoroutine(_coroutineFunc(onComplete));
      IsRunning = true;
    }

    public void Stop()
    {
      if (_coroutine == null)
        return;

      _runner.StopCoroutine(_coroutine);
      IsRunning = false;
      _coroutine = null;
    }
  }
}