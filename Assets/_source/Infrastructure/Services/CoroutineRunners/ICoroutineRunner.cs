using System.Collections;
using UnityEngine;

namespace Infrastructure.Services.CoroutineRunners
{
  public interface ICoroutineRunner
  {
    Coroutine StartCoroutine(IEnumerator coroutine);
  }
}