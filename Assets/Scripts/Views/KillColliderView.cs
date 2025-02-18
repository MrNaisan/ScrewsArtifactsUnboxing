using UnityEngine;

namespace Views
{
    public class KillColliderView : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            other.gameObject.SetActive(false);
        }
    }
}