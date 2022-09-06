using System.Collections.Generic;
using TestMazeMonsters.Core.Input.Enums;
using UnityEngine;

namespace TestMazeMonsters.Core.User
{
    public class UserData
    {
        //Todo Вместо KeyCode должен быть List<KeyCode>, чтобы назначать несколько клавишь на одно дейсвие
        private Dictionary<InputCmdId, KeyCode> _controls = new Dictionary<InputCmdId, KeyCode>();

        public Dictionary<InputCmdId, KeyCode> Controls => _controls;
    }
}
