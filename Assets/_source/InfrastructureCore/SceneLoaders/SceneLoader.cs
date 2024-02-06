using System;
using System.Collections;
using InfastuctureCore.ServiceLocators;
using InfastuctureCore.Services.CoroutineRunnerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace InfastuctureCore.SceneLoaders
{
    public class SceneLoader
    {
        private readonly string _initialSceneName;

        public SceneLoader(string initialSceneName)
        {
            _initialSceneName = initialSceneName;
        }

        public event Action<string> SceneLoaded;

        private MonoBehaviour CoroutineRunner => ServiceLocator.Instance.Get<ICoroutineRunnerService>().Instance;

        public void Load(string name, Action<string> onLoaded = null)
        {
            //DOTween.KillAll();
            CoroutineRunner.StartCoroutine(LoadScene(name, onLoaded));
        }

        public void Load(Action<string> onLoaded = null)
        {
            CoroutineRunner.StartCoroutine(LoadScene(_initialSceneName, onLoaded));
        }

        private IEnumerator LoadScene(string nextScene, Action<string> onLoaded)
        {
            // if (SceneManager.GetActiveScene().name == nextScene)
            // {
            //     onLoaded?.Invoke(nextScene);
            //     SceneLoaded?.Invoke(nextScene);
            //     yield break;
            // }

            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(nextScene);
            asyncOperation.allowSceneActivation = true;

            while (!asyncOperation.isDone)
                yield return null;

            onLoaded?.Invoke(nextScene);
            SceneLoaded?.Invoke(nextScene);
        }
    }
}