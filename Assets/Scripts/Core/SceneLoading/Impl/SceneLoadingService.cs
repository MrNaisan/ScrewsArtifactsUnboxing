using System;
using System.Collections;
using Core.CoroutineRunner;
using UnityEngine.SceneManagement;
using Views;

namespace Core.SceneLoading.Impl
{
    public class SceneLoadingService : ISceneLoadingService
    {
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly LoadingView _loadingView;
        
        public SceneLoadingService(
            ICoroutineRunner coroutineRunner,
            LoadingView loadingView
        )
        {
            _coroutineRunner = coroutineRunner;
            _loadingView = loadingView;
        }
        
        public void LoadSceneAsync(string sceneName, Action callback = null)
        {
            _coroutineRunner.StartCoroutine(LoadScene(sceneName, callback));
        }

        private IEnumerator LoadScene(string sceneName, Action callback)
        {
            _loadingView.Show();
            var progress = SceneManager.LoadSceneAsync(sceneName);
            
            while (!progress.isDone)
            {
                yield return null;
            }
            
            callback?.Invoke();
            _loadingView.Hide();
        }
    }
}