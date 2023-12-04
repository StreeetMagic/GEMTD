using System;
using System.Collections;
using UnityEngine;

namespace InfastuctureCore.Utilities
{
    public class CoroutineDecorator
    {
        private readonly MonoBehaviour _runner;
        private Coroutine _coroutine;
        private readonly Func<IEnumerator> _coroutineFunc;

        public CoroutineDecorator(MonoBehaviour runner, Func<IEnumerator> coroutineFunc)
        {
            _runner = runner;
            _coroutineFunc = coroutineFunc;
        }

        public bool IsRunning { get; private set; }

        public void Start()
        {
            if (IsRunning && _coroutine != null)
                _runner.StopCoroutine(_coroutine);

            _coroutine = _runner.StartCoroutine(_coroutineFunc());
            IsRunning = true;
        }

        public void Stop()
        {
            _runner.StopCoroutine(_coroutine);
            IsRunning = false;
            _coroutine = null;
        }
    }
}