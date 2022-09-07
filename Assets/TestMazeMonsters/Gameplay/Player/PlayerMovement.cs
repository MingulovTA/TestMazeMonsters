using System;
using System.Collections.Generic;
using TestMazeMonsters.Core.CustomInput.Enums;
using TestMazeMonsters.Gameplay.Cameras;
using UnityEngine;

namespace TestMazeMonsters.Gameplay.Player
{
    public class PlayerMovement
    {
        private Transform _playerTransform;
        private CharacterController _characterController;
        private GameplayCamera _camera;
        private PlayerMovementSettings _pmSettings = new PlayerMovementSettings();
        private Dictionary<InputCmdId,float> _actionStates = new Dictionary<InputCmdId,float> ();
        
        ////////// VARIABLES //////////
        private Vector3 _moveDirection = Vector3.zero;
        private float _rotationX = 0;
        private Vector3 _lastVelocity;
        private bool _unCrouchAvailiable;
        private bool _uncrouchChechTime;
        private bool _isCrouch;
        private float _inputHorisontalVelocity = 0;
        private Vector3 _posInLastFrame;
        private bool _inputJumpPressed;
        
        public void Init(CharacterController characterController, GameplayCamera camera, Transform playerTransform)
        {
            _playerTransform = playerTransform;
            _characterController = characterController;
            _camera = camera;
            
            foreach (var actionStr in Enum.GetNames(typeof(InputCmdId)))
            {
                InputCmdId action = (InputCmdId)Enum.Parse(typeof(InputCmdId), actionStr);
                _actionStates.Add(action,0);
            }
        }
        
        public void HandleCmd(InputCmdId inputCmdId, InputActionType inputActionType, float value)
        {
            _actionStates[inputCmdId] = value;
            if (inputActionType == InputActionType.Released)
                _actionStates[inputCmdId] = 0;

            if (inputCmdId == InputCmdId.CameraZoomIn)
            {
                CameraZoom(1);
            }
            else if (inputCmdId == InputCmdId.CameraZoomOut)
            {
                CameraZoom(-1);
            }
        }

        private void CameraZoom(int dZ)
        {
            _camera.CameraTransform.localPosition =
                Vector3.forward * Mathf.Clamp(_camera.CameraTransform.localPosition.z + dZ, -6, 0);
        }

        public void Tick()
        {

            float dMoveX = _actionStates[InputCmdId.MoveRight] - _actionStates[InputCmdId.MoveLeft];
            float dMoveY = _actionStates[InputCmdId.MoveForward] - _actionStates[InputCmdId.MoveBackward];


            //CalcInput();
            Vector2 v = new Vector2(dMoveX, dMoveY);
            float ioMovePower = Mathf.Clamp01(Vector2.Distance(Vector2.zero, v));
            v = v.normalized;
            _inputHorisontalVelocity = v.magnitude * ioMovePower * 0.1f;

            Vector3 forward = _playerTransform.TransformDirection(Vector3.forward);
            Vector3 right = _playerTransform.TransformDirection(Vector3.right);
            float curSpeedX = _pmSettings.RunningSpeed * v.y;
            float curSpeedY = _pmSettings.RunningSpeed * v.x;
            float movementDirectionY = _moveDirection.y;

            if (_characterController.isGrounded)
            {
                _moveDirection = (forward * curSpeedX *
                                  (_inputHorisontalVelocity / _pmSettings.InputHorisontalVelocityMax)) +
                                 (right * curSpeedY *
                                  (_inputHorisontalVelocity / _pmSettings.InputHorisontalVelocityMax));
                if (_isCrouch)
                    _moveDirection = _moveDirection * 0.5f;
                _lastVelocity = _moveDirection;
            }
            else
            {
                Vector3 moveAirCorrection =
                    (forward * curSpeedX * (_inputHorisontalVelocity / _pmSettings.InputHorisontalVelocityMax)) +
                    (right * curSpeedY * (_inputHorisontalVelocity / _pmSettings.InputHorisontalVelocityMax));
                _moveDirection = _lastVelocity + moveAirCorrection * 0.25f;
            }

            _moveDirection.y = movementDirectionY;


            _characterController.center = Vector3.up * _characterController.height / 2;
            if (!_characterController.isGrounded)
            {
                _moveDirection.y -= _pmSettings.Gravity * Time.deltaTime;
            }

            _characterController.Move(_moveDirection * Time.deltaTime);

            float dLookX = _actionStates[InputCmdId.TurnRight] - _actionStates[InputCmdId.TurnLeft];
            float dLookY = _actionStates[InputCmdId.TurnUp] - _actionStates[InputCmdId.TurnDown];

            _rotationX += -dLookY * _pmSettings.LookSpeed;
            _rotationX = Mathf.Clamp(_rotationX, -_pmSettings.LookXLimit, _pmSettings.LookXLimit);
            _camera.RootTransform.localRotation = Quaternion.Euler(_rotationX, 0, 0);
            _playerTransform.rotation *= Quaternion.Euler(0, dLookX * _pmSettings.LookSpeed, 0);
            _posInLastFrame = _playerTransform.position;
        }
    }
}
