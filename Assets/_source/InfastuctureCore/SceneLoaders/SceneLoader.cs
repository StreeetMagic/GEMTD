using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace InfastuctureCore.SceneLoaders
{
    public class SceneLoader
    {
        private readonly MonoBehaviour _coroutineRunner;
        private readonly string _initialSceneName;

        public SceneLoader(MonoBehaviour coroutineRunner, string initialSceneName)
        {
            _coroutineRunner = coroutineRunner;
            _initialSceneName = initialSceneName;
        }

        public SceneLoader(MonoBehaviour coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }

        public event Action<string> SceneLoaded;

        public void Load(string name, Action<string> onLoaded = null)
        {
            //DOTween.KillAll();
            _coroutineRunner.StartCoroutine(LoadScene(name, onLoaded));
        }

        public void Load(Action<string> onLoaded = null)
        {
            _coroutineRunner.StartCoroutine(LoadScene(_initialSceneName, onLoaded));
        }

        private IEnumerator LoadScene(string nextScene, Action<string> onLoaded)
        {
            if (SceneManager.GetActiveScene().name == nextScene)
            {
                onLoaded?.Invoke(nextScene);
                SceneLoaded?.Invoke(nextScene);
                yield break;
            }

            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(nextScene);
            asyncOperation.allowSceneActivation = true;

            while (!asyncOperation.isDone)
                yield return null;

            onLoaded?.Invoke(nextScene);
            SceneLoaded?.Invoke(nextScene);
        }
    }
}