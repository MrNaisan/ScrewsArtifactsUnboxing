using Core.SceneLoading;
using DG.Tweening;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Views
{
    public class WinView : MonoBehaviour
    {
        [Inject] private ISceneLoadingService _sceneLoadingService;
        
        [SerializeField] private Transform winPanel;
        [SerializeField] private Button button;

        private void Start()
        {
            button.OnClickAsObservable().Subscribe(_ => Collect()).AddTo(this);
        }

        private void Collect()
        {
            button.interactable = false;
            _sceneLoadingService.LoadSceneAsync("Menu");
        }

        public void Show()
        {
            button.interactable = false;
            winPanel.localScale = Vector3.zero;
            winPanel.gameObject.SetActive(true);
            winPanel.DOScale(1.1f, 0.5f).SetEase(Ease.InOutCubic).OnComplete(() =>
            {
                winPanel.DOScale(1f, 0.1f).SetEase(Ease.Linear).OnComplete(() => button.interactable = true);
            });
        }
    }
}