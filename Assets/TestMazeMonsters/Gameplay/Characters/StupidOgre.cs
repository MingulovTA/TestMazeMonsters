using TestMazeMonsters.Gameplay.Characters.Core;
using UnityEngine;

namespace TestMazeMonsters.Gameplay.Characters
{
    public class StupidOgre : AbstractEnemy
    {
        [SerializeField] private CharacterController _characterController;
        
        protected override CharacterId CharacterId => CharacterId.StupidOgre;
        protected override int ApplyDamage(int takedDamage)
        {
            return (int) (takedDamage * 0.5f);
        }
        
        private void Update()
        {
            if (_aiSensor.IsSeeAnyEnemy)
            {
                ICharacter character = _aiSensor.GetFirstSpottedEnemyOrNull;
                if (character != null)
                {
                    Vector3 v = character.Posision - transform.position;
                    v = v.normalized;
                    _characterController.Move(v*Time.deltaTime*2);
                }
            }
        }
    }
}
