using TestMazeMonsters.Gameplay.Characters.Core;
using UnityEngine;

namespace TestMazeMonsters.Gameplay.Characters
{
    public class Spirit : AbstractEnemy
    {
        protected override CharacterId CharacterId => CharacterId.Spirit;
        protected override int ApplyDamage(int takedDamage)
        {
            return 0;
        }
        
        private void Update()
        {
            if (_aiSensor.IsSeeAnyEnemy)
            {
                ICharacter character = _aiSensor.GetFirstSpottedEnemyOrNull;
                if (character != null)
                    transform.position = Vector3.MoveTowards(transform.position, character.Posision, Time.deltaTime);
            }
        }
    }
}
