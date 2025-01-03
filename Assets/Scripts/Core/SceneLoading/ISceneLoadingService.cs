using System;

namespace Core.SceneLoading
{
    public interface ISceneLoadingService
    {
        public void LoadSceneAsync(string sceneName, Action callback = null);
    }
}