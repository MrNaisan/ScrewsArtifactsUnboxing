using UnityEngine;
using Views;

namespace Services.Impl
{
    public class ObjectRotationService : IObjectRotationService
    {
        private readonly ObjectView _objectView;
        private float _rotationSpeed = 10f;

        public ObjectRotationService(ObjectView objectView)
        {
            _objectView = objectView;
        }

        public void Rotate(Vector3 lastPos, Vector3 currentPos)
        {
            var deltaMouse = currentPos - lastPos;

            var rotationX = deltaMouse.x * _rotationSpeed * Time.deltaTime;
            var rotationY = deltaMouse.y * _rotationSpeed * Time.deltaTime;

            _objectView.transform.Rotate(Vector3.up, -rotationX, Space.World);
            _objectView.transform.Rotate(Vector3.right, rotationY, Space.World);
        }
    }
}