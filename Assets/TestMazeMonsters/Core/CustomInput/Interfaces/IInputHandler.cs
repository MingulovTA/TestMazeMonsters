using TestMazeMonsters.Core.CustomInput.Enums;

namespace TestMazeMonsters.Core.CustomInput.Interfaces
{
    public interface IInputHandler
    {
        void HandleCmd(InputCmdId inputCmdId, InputActionType inputActionType, float value);
    }
}
