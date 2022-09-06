using TestMazeMonsters.Core.CustomInput.Enums;

namespace TestMazeMonsters.Core.CustomInput.Interfaces
{
    public interface IInputInjector
    {
        void Inject(InputCmdId inputCmdId, InputActionType inputActionType, float value);
    }
}
