using System;
using UnityEngine;

namespace TestMazeMonsters.Gameplay.Characters.Core
{
    public abstract class AbstractEnemy : MonoBehaviour, ICharacter
    {
        [SerializeField] protected AiSensor _aiSensor;
        private int _health = 100;
        private Transform _transform;
        private Vector3Int _currentAreaCoords;
        
        private Vector3Int _areaCoords;
        protected abstract CharacterId CharacterId { get; }
        public Vector3Int AreaPosision => _areaCoords;
        public Vector3 Posision => _transform.position;
        public TeamId TeamId => TeamId.Monsters;
        public int Health => _health;
        
        public AiSensor AiSensor => _aiSensor;
        
        
        public void TakeDamage(int value)
        {
            _health -= ApplyDamage(value);
        }

        

        public event Action<int, int> OnAreaChanged;

        protected abstract int ApplyDamage(int takedDamage);

        private void Awake()
        {
            _transform = transform;
            _aiSensor.Construct(this);
        }

        private void OnEnable()
        {
            _aiSensor.OnEnemySpotted += EnemySpottedHandler;
            _aiSensor.OnEnemyLosted += EnemyLostedHandler;
        }

        private void EnemyLostedHandler(ICharacter obj)
        {
            
        }

        private void EnemySpottedHandler(ICharacter obj)
        {
            
        }

        private void OnDisable()
        {
            _aiSensor.OnEnemySpotted -= EnemySpottedHandler;
            _aiSensor.OnEnemyLosted -= EnemyLostedHandler;
        }
        
        private void Update()
        {
            _areaCoords.x = Convert.ToInt32(_transform.position.x / 4);
            _areaCoords.z = Convert.ToInt32(_transform.position.z / 4);

            if (_areaCoords.x != _currentAreaCoords.x || _areaCoords.z != _currentAreaCoords.z)
            {
                _currentAreaCoords.x = _areaCoords.x;
                _currentAreaCoords.z = _areaCoords.z;
                OnAreaChanged?.Invoke(_currentAreaCoords.x,_currentAreaCoords.z);
            }
        }
    }
}
