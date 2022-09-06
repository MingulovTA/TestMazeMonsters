namespace TestMazeMonsters.Core.Input.Interfaces
{
    public interface IInputController
    {
        void RegisterHandler(IInputHandler inputHandler);
        void UnregisterHandler(IInputHandler inputHandler);
    }
}
