using TestMazeMonsters.Core.Input.Enums;

namespace TestMazeMonsters.Core.Input.Interfaces
{
    public interface IInputHandler
    {
        void HandleCmd(InputCmdId inputCmdId, InputActionType inputActionType);
    }
}
