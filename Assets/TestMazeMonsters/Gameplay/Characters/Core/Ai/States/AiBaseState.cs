namespace TestMazeMonsters.Gameplay.Characters.Core.Ai.States
{
    public abstract class AiBaseState
    {
        private AiStateMachine _aiSm;
        private AbstractEnemy _character;
        
        protected AiStateMachine AiSm => _aiSm;
        protected AbstractEnemy Character => _character;

        public void Init(AiStateMachine aiSm, AbstractEnemy character)
        {
            _aiSm = aiSm;
            _character = character;
        }

        public abstract void Enter();
        public abstract void Exit();
        public abstract void Update();
    }
}
