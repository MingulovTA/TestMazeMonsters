using System;
using TestMazeMonsters.Core.CustomInput.Enums;
using TestMazeMonsters.Core.CustomInput.Interfaces;
using TestMazeMonsters.Gameplay.Cameras;
using TestMazeMonsters.Gameplay.Characters;
using TestMazeMonsters.Gameplay.Characters.Core;
using UnityEngine;
using Zenject;

namespace TestMazeMonsters.Gameplay.Player
{
    public class PlayerController : MonoBehaviour, IInputHandler, ICharacter
    {
        [Inject] private readonly IInputController _inputController;
        
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private GameplayCamera _camera;
        
        private readonly PlayerMovement _playerMovement = new PlayerMovement();

        private int _health = 100;
        private Vector3Int _currentAreaCoords;
        private Vector3Int _areaCoords;
        private Transform _transform;
        
        public event Action<int, int> OnAreaChanged;
        public Vector3Int AreaPosision => _areaCoords;
        public Vector3 Posision => _transform.position;
        public TeamId TeamId => TeamId.Humans;
        public int Health => _health;

        public void HandleCmd(InputCmdId inputCmdId, InputActionType inputActionType, float value)
        {
            _playerMovement.HandleCmd(inputCmdId,inputActionType,value);
        }

        public bool CursorRequired => false;

        private void Awake()
        {
            _transform = transform;
            _playerMovement.Init(_characterController,_camera,_transform);
        }

        private void OnEnable()
        {
            _inputController.RegisterHandler(this);
        }

        private void OnDisable()
        {
            _inputController.UnregisterHandler(this);
        }

        private void Update()
        {
            _playerMovement.Tick();
            _areaCoords.x = Convert.ToInt32(_transform.position.x / 4);
            _areaCoords.z = Convert.ToInt32(_transform.position.z / 4);

            if (_areaCoords.x != _currentAreaCoords.x || _areaCoords.z != _currentAreaCoords.z)
            {
                _currentAreaCoords.x = _areaCoords.x;
                _currentAreaCoords.z = _areaCoords.z;
                OnAreaChanged?.Invoke(_currentAreaCoords.x,_currentAreaCoords.z);
            }
        }

        public void TakeDamage(int value)
        {
            _health -= value;
            _health = Mathf.Clamp(_health, 0, 100);
        }

        public AiSensor AiSensor { get; }
    }
}
