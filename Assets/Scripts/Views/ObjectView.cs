using System;
using DG.Tweening;
using UnityEngine;

namespace Views
{
    public class ObjectView : MonoBehaviour
    {
        public void Win(Action callback = null)
        {
            transform.DORotate(Vector3.zero, 0.6f).SetEase(Ease.InOutCubic).OnComplete(() => callback?.Invoke());
        }
    }
}