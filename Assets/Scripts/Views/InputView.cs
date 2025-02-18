using System;
using UniRx;
using UnityEngine;

namespace Views
{
    public class InputView : MonoBehaviour
    {
        [NonSerialized] public ReactiveProperty<bool> IsClicked = new ();
        [NonSerialized] public ReactiveProperty<Vector3?> MousePosition = new ();
        public float ClickTime { get; private set; }
        
        private void FixedUpdate()
        {
            if (Input.GetMouseButton(0))
            {
                if (!IsClicked.Value)
                {
                    ClickTime = 0;
                    MousePosition.Value = null;
                    IsClicked.Value = true;
                }

                ClickTime += Time.deltaTime;
                MousePosition.Value = Input.mousePosition;
            }
            else if(IsClicked.Value)
            {
                IsClicked.Value = false;
            }
        }
    }
}