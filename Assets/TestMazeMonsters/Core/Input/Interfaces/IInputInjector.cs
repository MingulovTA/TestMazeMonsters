using TestMazeMonsters.Core.Input.Enums;

namespace TestMazeMonsters.Core.Input.Interfaces
{
    public interface IInputInjector
    {
        void Inject(InputCmdId inputCmdId, InputActionType inputActionType);
    }
}
