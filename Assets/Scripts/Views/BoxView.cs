using DG.Tweening;
using Services;
using UnityEngine;
using UnityEngine.UI;
using Utils;
using Zenject;

namespace Views
{
    public class BoxView : MonoBehaviour
    {
        [SerializeField] private Image boxImage;
        [SerializeField] private Image[] screwsImages;
        [SerializeField] private Button button;

        public bool IsBoxActive { get; private set; }
        public EColorType ColorType { get; private set; }
        public bool IsTweenerActive { get; private set; }
        public int ScrewsCount { get; private set; }

        public Button Button => button;

        public void ResetBox(Color color, EColorType colorType, int screwsCount)
        {
            IsTweenerActive = true;
            
            gameObject.transform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InOutCubic).OnComplete(() =>
            {
                boxImage.color = color;
                ScrewsCount = screwsCount;
                ColorType = colorType;
                foreach (var screwImage in screwsImages)
                {
                    screwImage.gameObject.SetActive(false);
                    screwImage.color = color;
                }
                
                gameObject.transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.InOutCubic).OnComplete(() =>
                {
                    IsBoxActive = true;
                    IsTweenerActive = false;
                });
            });
        }

        public bool AddScrew()
        {
            ScrewsCount -= 1;
            
            foreach (var screwImage in screwsImages)
            {
                if (screwImage.gameObject.activeSelf) continue;
                
                screwImage.gameObject.SetActive(true);
                break;
            }

            if (ScrewsCount <= 0)
            {
                IsBoxActive = false;
            }
            
            return ScrewsCount <= 0;
        }
    }
}