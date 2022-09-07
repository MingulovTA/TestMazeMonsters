using System;
using System.Collections.Generic;
using System.Linq;
using TestMazeMonsters.Core.CustomInput.Devices;
using TestMazeMonsters.Core.CustomInput.Enums;
using TestMazeMonsters.Core.CustomInput.Interfaces;
using TestMazeMonsters.Core.User;
using UnityEngine;
using Zenject;

namespace TestMazeMonsters.Core.CustomInput
{
    public class InputController : IInputController, IInputInjector, ITickable, IInitializable
    {
        private const float  FLOAT_TOLERANCE = 0.001f;
        
        [Inject] private readonly UserConfig _userConfig;
        
        private List<IInputHandler> _inputHandlers = new List<IInputHandler>();
        private List<InputCmdId> _inputCmdIds = new List<InputCmdId>();
        private Dictionary<InputCmdId, float> _lastCommonSignals = new Dictionary<InputCmdId, float>();
        private Dictionary<InputCmdId, float> _commonSignals = new Dictionary<InputCmdId, float>();
        private Vector2 _lastMousePosition;
        private List<IVirtualDevice> _virtualDevices = new List<IVirtualDevice>();

        public void Initialize()
        {
            foreach (var inputCmdId in Enum.GetValues(typeof(InputCmdId)).Cast<InputCmdId>().ToList())
            {
                _inputCmdIds.Add(inputCmdId);
                _commonSignals.Add(inputCmdId,0);
                _lastCommonSignals.Add(inputCmdId,0);
            }
            
            _virtualDevices.Add(new VdMouse());
            _virtualDevices.Add(new VdKeyboard(_userConfig.UserData.Controls));
            _virtualDevices.Add(new VdTouchScreen());
            
            foreach (var userDataInputDevice in _userConfig.UserData.InputDevices)
            {
                IVirtualDevice vd = _virtualDevices.FirstOrDefault(v => v.VirtualDeviceId == userDataInputDevice.Key);
                if (vd != null)
                {
                    if (userDataInputDevice.Value)
                    {
                        vd.Enable();
                    }
                    else
                    {
                        vd.Disable();
                    }
                }
            }
        }

    
        public void Tick()
        {
            foreach (var virtualDevice in _virtualDevices)
            {
                if (virtualDevice.IsAcitve)
                    virtualDevice.Tick();
            }

            foreach (var cmdId in _inputCmdIds)
            {
                _commonSignals[cmdId] = 0;
                foreach (var virtualDevice in _virtualDevices)
                {
                    if (virtualDevice.IsAcitve)
                        _commonSignals[cmdId] += virtualDevice.InputActions[cmdId];
                }

                if (Math.Abs(_lastCommonSignals[cmdId]) < FLOAT_TOLERANCE && Math.Abs(_commonSignals[cmdId]) > FLOAT_TOLERANCE)
                {
                    TryToPushActionCmd(cmdId,InputActionType.Pressed,_commonSignals[cmdId]);
                }
                
                if (Math.Abs(_lastCommonSignals[cmdId]) > FLOAT_TOLERANCE && Math.Abs(_commonSignals[cmdId]) > FLOAT_TOLERANCE)
                {
                    TryToPushActionCmd(cmdId,InputActionType.Loop,_commonSignals[cmdId]);
                }
                
                if (Math.Abs(_lastCommonSignals[cmdId]) > FLOAT_TOLERANCE && Math.Abs(_commonSignals[cmdId]) < FLOAT_TOLERANCE)
                {
                    TryToPushActionCmd(cmdId,InputActionType.Released,_commonSignals[cmdId]);
                }

                _lastCommonSignals[cmdId] = _commonSignals[cmdId];
            }   
        }

        public void Inject(InputCmdId inputCmdId, InputActionType inputActionType, float value)
        {
            
        }

        public void RegisterHandler(IInputHandler inputHandler)
        {
            Debug.Log($"RegisterHandler {inputHandler.CursorRequired}");
            if (inputHandler != null)
            {
                _inputHandlers.Add(inputHandler);
                SetActiveHandler();
            }
        }

        public void UnregisterHandler(IInputHandler inputHandler)
        {
            if (_inputHandlers.Contains(inputHandler))
            {
                _inputHandlers.Remove(inputHandler);
            }

            int i = _inputHandlers.Count - 1;
            while (i>=0)
            {
                if (_inputHandlers[i] == null)
                    _inputHandlers.RemoveAt(i);
                i--;
            }
            SetActiveHandler();
        }
        
        private void TryToPushActionCmd(InputCmdId inputCmdId, InputActionType inputActionType, float value)
        {
            if (_inputHandlers.Count == 0 || _inputHandlers[_inputHandlers.Count - 1] == null)
            {
                return;
            }
            _inputHandlers[_inputHandlers.Count - 1].HandleCmd(inputCmdId,inputActionType,value);
        }

        private void SetActiveHandler()
        {
            if (_inputHandlers.Count==0||
                _inputHandlers[_inputHandlers.Count-1]==null||
                _inputHandlers[_inputHandlers.Count-1].CursorRequired)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }
}
