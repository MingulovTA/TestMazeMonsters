using System;
using System.Collections.Generic;
using System.Linq;
using TestMazeMonsters.Core.CustomInput.Enums;
using UnityEngine;

namespace TestMazeMonsters.Core.CustomInput.Devices
{
    public class VdMouse:IVirtualDevice
    {
        private bool _isActive;
        private Dictionary<InputCmdId, float> _inputSignals = new Dictionary<InputCmdId, float>();
        private float _horisontal;
        private float _vertical;

        public VdMouse()
        {
            foreach (var inputCmdId in Enum.GetValues(typeof(InputCmdId)).Cast<InputCmdId>().ToList())
            {
                _inputSignals.Add(inputCmdId,0);
            }
        }

        public VirtualDeviceId VirtualDeviceId => VirtualDeviceId.Mouse;
        public Dictionary<InputCmdId, float> InputActions => _inputSignals;
        public bool IsAcitve => _isActive;
        
        public void Enable()
        {
            _isActive = true;
        }

        public void Disable()
        {
            _isActive = false;
        }

        public void Tick()
        {
            _horisontal = Input.GetAxis("Mouse X");
            _vertical = Input.GetAxis("Mouse Y");

            _inputSignals[InputCmdId.TurnRight] = _horisontal>0?_horisontal:0;
            _inputSignals[InputCmdId.TurnLeft] = _horisontal<0?-_horisontal:0;
            _inputSignals[InputCmdId.TurnUp] = _vertical>0?_vertical:0;
            _inputSignals[InputCmdId.TurnDown] = _vertical<0?-_vertical:0;
            _inputSignals[InputCmdId.Attack] = Input.GetMouseButton(0)?1:0;
            _inputSignals[InputCmdId.CameraZoomIn] = Input.mouseScrollDelta.y > 0 ? 1 : 0;
            _inputSignals[InputCmdId.CameraZoomOut] = Input.mouseScrollDelta.y < 0 ? -1 : 0;
        }
    }
}
