using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Views;
using Zenject;

namespace Services.Impl
{
    public class InputService : IInputService, IInitializable, IDisposable
    {
        private readonly InputView _inputView;
        private readonly IObjectRotationService _objectRotationService;
        private readonly IUnscrewService _unscrewService;

        private List<IDisposable> _disposables = new();
        private Vector3? _startMousePosition;
        private Vector3? _lastMousePosition;

        public bool IsClicked { get; private set; }
        public Vector3? MousePosition { get; private set; }

        public InputService(
            InputView inputView,
            IObjectRotationService objectRotationService,
            IUnscrewService unscrewService
        )
        {
            _inputView = inputView;
            _objectRotationService = objectRotationService;
            _unscrewService = unscrewService;
        }

        public void Initialize()
        {
            _inputView.IsClicked.Subscribe(ChangeClickState).AddTo(_disposables);
            _inputView.MousePosition.Subscribe(ChangeMousePosition).AddTo(_disposables);
        }

        private void ChangeClickState(bool value)
        {
            if(IsClicked == value) return;

            IsClicked = value;

            if (IsClicked || _startMousePosition == null || MousePosition == null) return;
            
            var swipeDistance = Vector3.Distance(_startMousePosition.Value, MousePosition.Value);
            _startMousePosition = null;
            
            if (swipeDistance < 10f && _inputView.ClickTime < 0.2f)
            {
                UnscrewObject();
            }
        }

        private void ChangeMousePosition(Vector3? value)
        {
            if (value != null && _startMousePosition == null)
            {
                _startMousePosition = value;
            }
            
            _lastMousePosition = _lastMousePosition == null ? value : MousePosition;
            MousePosition = value;

            if (IsClicked && _lastMousePosition != null && MousePosition != null)
            {
                RotateObject();
            }
        }

        private void RotateObject()
        {
            _objectRotationService.Rotate(_lastMousePosition.Value, MousePosition.Value);
        }
        
        private void UnscrewObject()
        {
            _unscrewService.Unscrew(MousePosition.Value);
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