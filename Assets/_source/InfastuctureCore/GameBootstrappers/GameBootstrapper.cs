using Games;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace InfastuctureCore.GameBootstrappers
{
    /// <summary>
    /// <para>Main game bootstrapper</para> 
    /// <para>Do not place it on scenes manually</para> 
    /// <para>It is instantiated by GameRunner</para> 
    /// </summary>
    public class GameBootstrapper : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this);

            string initialSceneName = SceneManager.GetActiveScene().name;
            // ReSharper disable once UnusedVariable
            var game = new Game(this, initialSceneName);
        }
    }
}