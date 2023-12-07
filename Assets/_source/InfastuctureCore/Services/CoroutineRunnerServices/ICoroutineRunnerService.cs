using UnityEngine;

namespace InfastuctureCore.Services.CoroutineRunnerServices
{
    public interface ICoroutineRunnerService : IService
    {
        MonoBehaviour Instance { get; }
    }
}