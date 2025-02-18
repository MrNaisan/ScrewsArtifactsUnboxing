using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Utils;
using Views;
using Zenject;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Services.Impl
{
    public class UnscrewService : IUnscrewService, IInitializable, IDisposable
    {
        private readonly CurrentBoltView _currentBoltView;
        private readonly InventoryView _inventoryView;
        private readonly ObjectView _objectView;
        private readonly PrizeView _prizeView;
        private readonly WinView _winView;
        
        private BoltView[] _bolts;
        private BoltView _currentBolt;
        private Dictionary<EColorType, int> _colorCount = new ();
        private List<IDisposable> _disposables = new ();

        public UnscrewService(
            CurrentBoltView currentBoltView,
            InventoryView inventoryView, 
            ObjectView objectView,
            PrizeView prizeView, 
            WinView winView
        )
        {
            _currentBoltView = currentBoltView;
            _inventoryView = inventoryView;
            _objectView = objectView;
            _prizeView = prizeView;
            _winView = winView;
        }

        public void Initialize()
        {
            _bolts = Object.FindObjectsOfType<BoltView>();

            foreach (var bolt in _bolts)
            {
                var isExist = _colorCount.TryGetValue(bolt.ColorType, out var count);

                if (isExist)
                {
                    _colorCount[bolt.ColorType] = count + 1;
                }
                else
                {
                    _colorCount.Add(bolt.ColorType, 1);
                }
            }

            foreach (var box in _inventoryView.Boxes)
            {
                SetBox();
                box.Button.OnClickAsObservable().Subscribe(_ => AddScrew(box)).AddTo(_disposables);
            }
        }

        public void Unscrew(Vector3 mousePosition)
        {
            var ray = Camera.main.ScreenPointToRay(mousePosition);

            if (!Physics.Raycast(ray, out var hit)) return;
            
            if (_currentBolt != null)
            {
                ReturnBolt();
                return;
            }
            
            foreach (var bolt in _bolts)
            {
                if(!bolt.gameObject.activeSelf)
                    continue;

                if (hit.collider.gameObject != bolt.gameObject) continue;
                
                bolt.UnscrewObject(ActiveUiBolt);
                _currentBolt = bolt;
                break;
            }
        }

        private void ReturnBolt()
        {
            if(_currentBolt.IsTweenerActive)
                return;
            
            _currentBoltView.Image.gameObject.SetActive(false);
            _currentBolt.ReturnObject();
            _currentBolt = null;
        }

        private void ActiveUiBolt()
        {
            _currentBoltView.SetImageColor(GetColor(_currentBolt.ColorType));
            _currentBoltView.Image.gameObject.SetActive(true);
        }
        
        private Color GetColor(EColorType colorType)
        {
            return colorType switch
            {
                EColorType.Red => Color.red,
                EColorType.Green => Color.green,
                EColorType.Blue => Color.cyan,
                EColorType.Yellow => Color.yellow,
                _ => Color.white
            };
        }

        private void SetBox()
        {
            while (true)
            {
                var randomColor = (EColorType)Random.Range(1, 5);

                if (_colorCount.TryGetValue(randomColor, out var count) && count > 0)
                {
                    foreach (var box in _inventoryView.Boxes)
                    {
                        if (box.IsBoxActive || box.IsTweenerActive) continue;

                        if (count >= 3)
                            count = 3;

                        box.ResetBox(GetColor(randomColor), randomColor, count);
                        _colorCount[randomColor] -= count;
                        return;
                    }
                }
                else
                {
                    continue;
                }

                break;
            }
        }

        private void AddScrew(BoxView boxView)
        {
            if(_currentBolt == null || boxView == null)
                return;

            if(_currentBolt.ColorType != boxView.ColorType)
                return;
            
            if(!boxView.IsBoxActive)
                return;
            
            if(_currentBolt.IsTweenerActive)
                return;

            var isBoxFull = boxView.AddScrew();
            _currentBoltView.Image.gameObject.SetActive(false);
            _currentBolt.SetBoltUnscrewed();
            _currentBolt = null;
            
            if (isBoxFull)
            {
                var hasScrews = false;
                
                foreach(var colorCount in _colorCount.Values)
                {
                    if (colorCount > 0)
                    {
                        hasScrews = true;
                        break;
                    }
                }

                if (!hasScrews)
                {
                    var isAllBoxesFull = true;
                    
                    foreach (var box in _inventoryView.Boxes)
                    {
                        if (box.ScrewsCount > 0)
                            isAllBoxesFull = false;
                    }

                    if (!isAllBoxesFull) return;
                    
                    _objectView.Win(Win);

                    return;
                }

                SetBox();
            }
        }

        private void Win()
        {
            _prizeView.Open();
            Observable.Timer(TimeSpan.FromSeconds(1f)).Subscribe(_ => _winView.Show()).AddTo(_disposables);
        }
        
        public void Dispose()
        {
            foreach (var disposable in _disposables)
            {
                disposable.Dispose();
            }
        }
    }
}