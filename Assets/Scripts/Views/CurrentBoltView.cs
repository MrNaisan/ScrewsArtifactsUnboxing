using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    public class CurrentBoltView : MonoBehaviour
    {
        [SerializeField] private Image image;

        public Image Image => image;

        public void SetImageColor(Color color)
        {
            image.color = color;
        }
    }
}