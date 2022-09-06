using System.Collections.Generic;
using TestMazeMonsters.Core.Input.Enums;
using TestMazeMonsters.Core.Input.Interfaces;
using Zenject;

namespace TestMazeMonsters.Core.Input
{
    public class InputController : IInputController, IInputInjector, ITickable, IInitializable
    {
        private List<IInputHandler> _inputHandlers = new List<IInputHandler>();
        
        public void Initialize()
        {
            
        }
    
        public void Tick()
        {
            
        }

        public void Inject(InputCmdId inputCmdId, InputActionType inputActionType)
        {
            
        }

        public void RegisterHandler(IInputHandler inputHandler)
        {
            if (inputHandler!=null)
                _inputHandlers.Add(inputHandler);
        }

        public void UnregisterHandler(IInputHandler inputHandler)
        {
            if (_inputHandlers.Contains(inputHandler))
                _inputHandlers.Remove(inputHandler);

            int i = _inputHandlers.Count - 1;
            while (i>=0)
            {
                if (_inputHandlers[i] == null)
                    _inputHandlers.RemoveAt(i);
                i--;
            }
        }
    }
}
