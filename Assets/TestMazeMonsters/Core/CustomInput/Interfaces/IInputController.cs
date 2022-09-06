namespace TestMazeMonsters.Core.CustomInput.Interfaces
{
    public interface IInputController
    {
        void RegisterHandler(IInputHandler inputHandler);
        void UnregisterHandler(IInputHandler inputHandler);
    }
}
