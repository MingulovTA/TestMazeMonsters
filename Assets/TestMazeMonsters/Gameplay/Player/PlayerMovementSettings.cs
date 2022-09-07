namespace TestMazeMonsters.Gameplay.Player
{
    public class PlayerMovementSettings
    {
        private float walkingSpeed = 6f;
        private float runningSpeed = 10f;
        private float jumpSpeed = 6;
        private float gravity = 17;
        private float lookSpeed = 1.5f;
        private float lookXLimit = 89.9f;
        private float _inputHorisontalVelocityMax = 0.1f;
        private float _pushPower = 2.0f;

        public float PushPower => _pushPower;
        public float InputHorisontalVelocityMax => _inputHorisontalVelocityMax;
        public float LookXLimit => lookXLimit;
        public float LookSpeed => lookSpeed;
        public float Gravity => gravity;
        public float JumpSpeed => jumpSpeed;
        public float RunningSpeed => runningSpeed;
        public float WalkingSpeed => walkingSpeed;
    }
}
