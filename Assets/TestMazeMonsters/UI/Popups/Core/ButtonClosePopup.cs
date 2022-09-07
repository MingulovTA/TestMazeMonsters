using System;
using UnityEngine;
using UnityEngine.UI;

namespace TestMazeMonsters.UI.Popups.Core
{
    public class ButtonClosePopup : MonoBehaviour
    {
        [SerializeField] private PopupCloseResult _popupCloseResult;
        [SerializeField] private GameObject _select;
        [SerializeField] private Image _image;

        private Action<PopupCloseResult> _closeCallback;
        
        public void Init(Action<PopupCloseResult> closeCallback)
        {
            _closeCallback = closeCallback;
        }
        
        public void Select()
        {
            _select.SetActive(true);
        }
        
        public void UnSelect()
        {
            _select.SetActive(false);
        }

        public void EnableInteractive()
        {
            _image.raycastTarget = true;
        }
        
        public void DisableInteractive()
        {
            _image.raycastTarget = false;
        }
        
        public void ClickHandler()
        {
            _closeCallback?.Invoke(_popupCloseResult);
        }
    }
}
