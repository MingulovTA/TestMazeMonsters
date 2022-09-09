using TestMazeMonsters.Gameplay.Characters.Core;
using UnityEngine;
using UnityEngine.AI;

namespace TestMazeMonsters.Gameplay.Characters
{
    public class Zombie : AbstractEnemy
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;
        
        protected override CharacterId CharacterId => CharacterId.Zombie;
        protected override int ApplyDamage(int takedDamage)
        {
            return takedDamage;
        }
        
        private void Update()
        {
            if (_aiSensor.IsSeeAnyEnemy)
            {
                ICharacter character = _aiSensor.GetFirstSpottedEnemyOrNull;
                if (character != null)
                {
                    _navMeshAgent.SetDestination(character.Posision);
                }
            }
        }
    }
}
