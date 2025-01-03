using System;
using System.Collections;
using Core.CoroutineRunner;
using UnityEngine.SceneManagement;

namespace Core.SceneLoading.Impl
{
    public class SceneLoadingService : ISceneLoadingService
    {
        private readonly ICoroutineRunner _coroutineRunner;
        
        public SceneLoadingService(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }
        
        public void LoadSceneAsync(string sceneName, Action callback = null)
        {
            _coroutineRunner.StartCoroutine(LoadScene(sceneName, callback));
        }

        private IEnumerator LoadScene(string sceneName, Action callback)
        {
            var progress = SceneManager.LoadSceneAsync(sceneName);
            
            while (!progress.isDone)
            {
                yield return null;
            }
            
            callback?.Invoke();
        }
    }
}