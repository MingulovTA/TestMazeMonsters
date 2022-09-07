using System.Collections.Generic;
using TestMazeMonsters.Core.CustomInput.Devices;
using TestMazeMonsters.Core.CustomInput.Enums;
using UnityEngine;

namespace TestMazeMonsters.Core.User
{
    public class UserData
    {
        //Todo Вместо KeyCode должен быть List<KeyCode>, чтобы назначать несколько клавишь на одно дейсвие
        private Dictionary<InputCmdId, KeyCode> _controls = new Dictionary<InputCmdId, KeyCode>();
        private Dictionary<VirtualDeviceId, bool> _inputDevices = new Dictionary<VirtualDeviceId, bool>();
        public Dictionary<InputCmdId, KeyCode> Controls => _controls;
        public Dictionary<VirtualDeviceId, bool> InputDevices => _inputDevices;
    }
}
