using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Utils;

namespace Views
{
    public class BoltView : MonoBehaviour
    {
        [SerializeField] private EColorType colorType;
        [SerializeField] private Transform boltObj;
        
        private bool _isClicked;
        private bool _isUnscrewed;
        private Vector3 _startPos;
        private Vector3 _endPos;
        
        public bool IsUnscrewed => _isUnscrewed;
        public EColorType ColorType => colorType;
        public bool IsTweenerActive { get; private set; }

        private void Start()
        {
            _startPos = Vector3.zero;
            _endPos = Vector3.up * 0.1f;
        }

        public void UnscrewObject(Action callback = null)
        {
            if(_isClicked)
                return;

            _isClicked = true;
            IsTweenerActive = true;

            boltObj.DOLocalRotate(new(0, 180, 0), 0.2f).SetEase(Ease.InOutCubic);
            boltObj.DOLocalMove(_endPos, 0.2f).SetEase(Ease.InOutCubic).OnComplete(() =>
            {
                IsTweenerActive = false;
                boltObj.gameObject.SetActive(false);
                callback?.Invoke();
            });
        }

        public void SetBoltUnscrewed()
        {
            gameObject.SetActive(false);
        }

        public void ReturnObject(Action callback = null)
        {
            IsTweenerActive = true;
            boltObj.gameObject.SetActive(true);
            boltObj.transform.DOLocalRotate(new(0, 0, 0), 0.2f).SetEase(Ease.InOutCubic);
            boltObj.transform.DOLocalMove(_startPos, 0.2f).SetEase(Ease.InOutCubic).OnComplete(() =>
            {
                IsTweenerActive = false;
                _isClicked = false;
                callback?.Invoke();
            });
        }
    }
}