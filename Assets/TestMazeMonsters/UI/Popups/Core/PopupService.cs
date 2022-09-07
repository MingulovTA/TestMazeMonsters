using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TestMazeMonsters.UI.Popups.Core
{
    public class PopupService : MonoBehaviour, IPopupService
    {
        [SerializeField] private GameObject _bg;
        [SerializeField] private List<PopupBase> _popups;

        private Action<PopupCloseResult> _popupCloseCallback;

        public T TryToOpenPopup<T>(PopupId popupId, Action<PopupCloseResult> popupCloseCallback) where T : class
        {
            _popupCloseCallback = popupCloseCallback;
            PopupBase popup = _popups.FirstOrDefault(pb => pb.PopupId == popupId);
            if (popup != null)
            {
                popup.Show(PopupCloseHanlder);
                _bg.SetActive(true);
            }

            return popup as T;
        }

        public void TryToOpenPopup(PopupId popupId, Action<PopupCloseResult> popupCloseCallback)
        {
            _popupCloseCallback = popupCloseCallback;
            TryToOpenPopup<PopupBase>(popupId, popupCloseCallback);
        }
        
        public void TryToOpenPopup(PopupId popupId)
        {
            TryToOpenPopup<PopupBase>(popupId, null);
        }

        private void Awake()
        {
            foreach (var popupBase in _popups)
            {
                popupBase.gameObject.SetActive(false);
            }
        }

        private void PopupCloseHanlder(PopupCloseResult result)
        {
            _bg.SetActive(false);
            if (_popupCloseCallback != null)
            {
                _popupCloseCallback?.Invoke(result);
                _popupCloseCallback = null;
            }
        }
    }
}
