using UnityEngine;

namespace Views
{
    public class InventoryView : MonoBehaviour
    {
        [SerializeField] private BoxView[] boxes;
        
        public BoxView[] Boxes => boxes;
    }
}