using UnityEngine;

namespace Views
{
    public class PrizeView : MonoBehaviour
    {
        [SerializeField] private Rigidbody[] boxPieces;
        [SerializeField] private ParticleSystem particles;

        public void Open()
        {
            particles.Play();
            
            foreach (var boxPiece in boxPieces)
            {
                boxPiece.isKinematic = false;
                boxPiece.AddForce(boxPiece.transform.forward * 10, ForceMode.Impulse);
                boxPiece.transform.SetParent(null);
            }
        }
    }
}