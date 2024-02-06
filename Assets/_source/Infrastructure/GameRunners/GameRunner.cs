using InfastuctureCore.GameBootstrappers;
using UnityEngine;

namespace InfastuctureCore.GameRunners
{
    /// <summary>
    /// <para>Should be placed on scenes manually</para>
    /// <para>1 GameRunner on each scene</para>
    /// </summary>
    public class GameRunner : MonoBehaviour
    {
        public GameBootstrapper BootstrapperPrefab;

        private void Awake()
        {
            if (FindObjectOfType<GameBootstrapper>() == null)
                Instantiate(BootstrapperPrefab);

            Destroy(gameObject);
        }
    }
}