using Core.SceneLoading;
using UnityEngine;
using Zenject;

public class RestartButton : MonoBehaviour
{
    [Inject] private ISceneLoadingService sceneLoadingService; 
    
    public void RestartGame()
    {
        sceneLoadingService.LoadSceneAsync("Game");
    }

    public void Menu()
    {
        sceneLoadingService.LoadSceneAsync("Menu");
    }
}