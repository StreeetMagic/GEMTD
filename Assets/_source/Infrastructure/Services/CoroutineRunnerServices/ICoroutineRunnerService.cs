using UnityEngine;

namespace Infrastructure.Services.CoroutineRunnerServices
{
    public interface ICoroutineRunnerService : IService
    {
        MonoBehaviour Instance { get; }
    }
}