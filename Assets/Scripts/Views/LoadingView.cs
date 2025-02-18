using DG.Tweening;
using UnityEngine;

namespace Views
{
    public class LoadingView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;

        public void Show()
        {
            canvasGroup.alpha = 1;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }
        
        public void Hide()
        {
            canvasGroup.alpha = 1;
            canvasGroup.DOFade(0f, 1f);
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
    }
}