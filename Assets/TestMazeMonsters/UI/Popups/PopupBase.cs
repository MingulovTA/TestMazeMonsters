using System;
using System.Collections.Generic;
using DG.Tweening;
using TestMazeMonsters.Core.CustomInput.Enums;
using TestMazeMonsters.Core.CustomInput.Interfaces;
using TestMazeMonsters.UI.Popups.Core;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace TestMazeMonsters.UI.Popups
{
    public class PopupBase : MonoBehaviour,IInputHandler
    {
        [SerializeField] private List<ButtonClosePopup> _buttons;
        [SerializeField] private PopupId _popupId;

        private ButtonClosePopup _selectedButton;
        private int _selectedButtonIndex = 0;
        private IInputController _inputController;
    
        public bool CursorRequired => true;
        public PopupId PopupId => _popupId;

        private const int _hiddenOfffset = -2000;
        private const float _showHideTime = 1;
        private const Ease _showEaseType = Ease.OutBack;
        private const Ease _hideEaseType = Ease.InBack;

        private Action<PopupCloseResult> _closeResult;
        private Transform _transform;
        private Transform Transform
        {
            get
            {
                if (_transform == null)
                    _transform = transform;
                return _transform;
            }
        }

        [Inject]
        private void Construct(IInputController inputController)
        {
            _inputController = inputController;
        }

        public void Show(Action<PopupCloseResult> closeResult)
        {
            gameObject.SetActive(true);
            _closeResult = closeResult;
            DisableButtons();
            Transform.localPosition = Vector3.up*_hiddenOfffset;
            Transform.DOLocalMoveY(0, _showHideTime)
                .SetEase(_showEaseType)
                .OnComplete(EnableButtons);
        }
        
        public void Close(PopupCloseResult result)
        {
            DisableButtons();
            Hide(result);
        }

        private void OnEnable()
        {
            _inputController.RegisterHandler(this);
        }

        private void OnDisable()
        {
            _inputController.UnregisterHandler(this);
        }

        private void EnableButtons()
        {
            foreach (var btn in _buttons)
            {
                btn.EnableInteractive();
                btn.UnSelect();
                btn.Init(Close);
            }

            _selectedButtonIndex = 0;
            _selectedButton = null;
        }

        private void DisableButtons()
        {
            foreach (var button in _buttons)
            {
                button.DisableInteractive();
            }
        }

        private void Hide(PopupCloseResult result)
        {
            _transform.localPosition = Vector3.zero;
            Transform.DOLocalMoveY(_hiddenOfffset, _showHideTime)
                .SetEase(_hideEaseType)
                .OnComplete(delegate
                {
                    gameObject.SetActive(false);
                    _closeResult?.Invoke(result);
                });
        }

        private void SelectNextButton()
        {
            _selectedButtonIndex++;
            if (_selectedButtonIndex >= _buttons.Count)
                _selectedButtonIndex = 0;
            RebuildSelectedButton();
        }

        private void SelectLastButton()
        {
            _selectedButtonIndex--;
            if (_selectedButtonIndex < 0)
                _selectedButtonIndex = _buttons.Count-1;
            RebuildSelectedButton();
        }

        private void RebuildSelectedButton()
        {
            if (_selectedButton != null)
            {
                _selectedButton.UnSelect();
            }

            _selectedButton = _buttons[_selectedButtonIndex];
            _selectedButton.Select();
        }

        public void HandleCmd(InputCmdId inputCmdId, InputActionType inputActionType, float value)
        {
            if (inputCmdId == InputCmdId.TabNext && inputActionType == InputActionType.Pressed)
            {
                SelectNextButton();
            }
        
            if (inputCmdId == InputCmdId.Return && inputActionType == InputActionType.Pressed&&_selectedButton!=null)
            {
                _selectedButton.ClickHandler();
            }
        }
    }
}
