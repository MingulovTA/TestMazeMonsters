using System;
using System.Collections.Generic;
using System.Linq;
using TestMazeMonsters.Core.CustomInput.Enums;
using TestMazeMonsters.Core.CustomInput.Interfaces;
using TestMazeMonsters.Core.User;
using UnityEngine;
using Zenject;

namespace TestMazeMonsters.Core.CustomInput
{
    public class InputController : IInputController, IInputInjector, ITickable, IInitializable
    {
        [Inject] private readonly UserConfig _userConfig;
        
        private List<IInputHandler> _inputHandlers = new List<IInputHandler>();
        private Dictionary<InputCmdId, InputActionType> _inputActions = new Dictionary<InputCmdId, InputActionType>();
        private Vector2 _lastMousePosition;

        public void Initialize()
        {
            foreach (var inputCmdId in Enum.GetValues(typeof(InputCmdId)).Cast<InputCmdId>().ToList())
            {
                _inputActions.Add(inputCmdId,InputActionType.None);
            }

            _lastMousePosition = Input.mousePosition;
        }
    
        public void Tick()
        {
            //Todo Разбить этого монстра на классы-девайсы
            foreach (var userDataControl in _userConfig.UserData.Controls)
            {
                if (Input.GetKey(userDataControl.Value)&&
                    _inputActions[userDataControl.Key] == InputActionType.Pressed)
                {
                    TryToPushActionCmd(userDataControl.Key,InputActionType.Loop,1);
                }
                
                if (Input.GetKeyDown(userDataControl.Value))
                {
                    if (_inputActions[userDataControl.Key] == InputActionType.None)
                    {
                        TryToPushActionCmd(userDataControl.Key,InputActionType.Pressed,1);
                        _inputActions[userDataControl.Key] = InputActionType.Pressed;
                    }
                }
                
                if (Input.GetKeyUp(userDataControl.Value))
                {
                    if (_inputActions[userDataControl.Key] == InputActionType.Pressed)
                    {
                        TryToPushActionCmd(userDataControl.Key,InputActionType.Released,0);
                        _inputActions[userDataControl.Key] = InputActionType.None;
                    }
                }
            }

            if (Input.mousePosition.x > _lastMousePosition.x)
            {
                TryToPushActionCmd(InputCmdId.TurnRight, InputActionType.Loop,
                    Input.mousePosition.x - _lastMousePosition.x);
            }
            else
            {
                if (Input.mousePosition.x < _lastMousePosition.x)
                {
                    TryToPushActionCmd(InputCmdId.TurnLeft, InputActionType.Loop,
                        Input.mousePosition.x - _lastMousePosition.x);
                }
            }
            
            if (Input.mousePosition.y > _lastMousePosition.y)
            {
                TryToPushActionCmd(InputCmdId.TurnUp, InputActionType.Loop,
                    Input.mousePosition.y - _lastMousePosition.y);
            }
            else
            {
                if (Input.mousePosition.y < _lastMousePosition.y)
                {
                    TryToPushActionCmd(InputCmdId.TurnDown, InputActionType.Loop,
                        Input.mousePosition.y - _lastMousePosition.y);
                }
            }
            
            if (Input.GetMouseButtonDown(0))
            {
                TryToPushActionCmd(InputCmdId.Attack, InputActionType.Pressed, 1);
            }

            if (Input.GetMouseButton(0))
            {
                TryToPushActionCmd(InputCmdId.Attack, InputActionType.Loop, 1);
            }
            
            if (Input.GetMouseButtonUp(0))
            {
                TryToPushActionCmd(InputCmdId.Attack, InputActionType.Released, 0);
            }

            _lastMousePosition = Input.mousePosition;
        }

        public void Inject(InputCmdId inputCmdId, InputActionType inputActionType, float value)
        {
            
        }

        public void TryToPushActionCmd(InputCmdId inputCmdId, InputActionType inputActionType, float value)
        {
            if (_inputHandlers.Count == 0 || _inputHandlers[_inputHandlers.Count - 1] == null)
            {
                return;
            }
            _inputHandlers[_inputHandlers.Count - 1].HandleCmd(inputCmdId,inputActionType,value);
        }

        public void RegisterHandler(IInputHandler inputHandler)
        {
            if (inputHandler!=null)
                _inputHandlers.Add(inputHandler);
        }

        public void UnregisterHandler(IInputHandler inputHandler)
        {
            if (_inputHandlers.Contains(inputHandler))
                _inputHandlers.Remove(inputHandler);

            int i = _inputHandlers.Count - 1;
            while (i>=0)
            {
                if (_inputHandlers[i] == null)
                    _inputHandlers.RemoveAt(i);
                i--;
            }
        }
    }
}
