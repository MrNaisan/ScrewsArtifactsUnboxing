using UniRx;
using UnityEngine;

namespace Services
{
    public interface IInputService
    {
        bool IsClicked { get; }
        Vector3? MousePosition { get; }
    }
}