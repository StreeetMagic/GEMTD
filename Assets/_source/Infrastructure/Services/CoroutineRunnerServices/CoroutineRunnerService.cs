using UnityEngine;

namespace InfastuctureCore.Services.CoroutineRunnerServices
{
    public class CoroutineRunnerService : ICoroutineRunnerService
    {
        public CoroutineRunnerService(MonoBehaviour instance)
        {
            Instance = instance;
        }

        public MonoBehaviour Instance { get; }
    }
}