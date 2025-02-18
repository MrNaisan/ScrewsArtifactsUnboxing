using UnityEngine;

namespace Services
{
    public interface IObjectRotationService
    {
        void Rotate(Vector3 lastPos, Vector3 currentPos);
    }
}