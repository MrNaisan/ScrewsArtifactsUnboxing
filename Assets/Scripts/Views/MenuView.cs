using Core.SceneLoading;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Views
{
    public class MenuView : MonoBehaviour
    {
        [Inject] private ISceneLoadingService _sceneLoadingService;
        
        [SerializeField] private Button startButton;
        
        private void Start()
        {
            startButton.OnClickAsObservable().Subscribe(_ => _sceneLoadingService.LoadSceneAsync("Game")).AddTo(this);
        }
    }
}