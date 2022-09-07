using System;
using System.Collections.Generic;
using System.Linq;
using TestMazeMonsters.Core.CustomInput.Devices;
using TestMazeMonsters.Core.CustomInput.Enums;
using UnityEngine;

public class VdKeyboard : IVirtualDevice
{
    private bool _isActive;
    private Dictionary<InputCmdId, float> _inputSignals = new Dictionary<InputCmdId, float>();
    private Dictionary<InputCmdId, KeyCode> _controls;

    public VdKeyboard(Dictionary<InputCmdId, KeyCode> controls)
    {
        _controls = controls;
        foreach (var inputCmdId in Enum.GetValues(typeof(InputCmdId)).Cast<InputCmdId>().ToList())
        {
            _inputSignals.Add(inputCmdId,0);
        }
    }

    public VirtualDeviceId VirtualDeviceId => VirtualDeviceId.KeyBoard;
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
        foreach (var control in _controls)
        {
            if (Input.GetKey(control.Value))
                _inputSignals[control.Key] = 1;
            else
                _inputSignals[control.Key] = 0;
        }

    }
}
