using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TestMazeMonsters.Gameplay.Characters.Core
{
    public class AiSensor : MonoBehaviour
    {
        private ICharacter _mineCharacter;
        private List<ICharacter> _charactersInVisibleArea = new List<ICharacter>();

        public event Action<ICharacter> OnSeeCharacter;
        public event Action<ICharacter> OnLoseCharacter;
        
        //Here the concept of the enemy is relative!
        public event Action<ICharacter> OnEnemySpotted;
        public event Action<ICharacter> OnEnemySpottedLoop;
        public event Action<ICharacter> OnEnemyLosted;

        public bool IsSeeAnyEnemy => 
            _charactersInVisibleArea.Count(chiva => chiva.TeamId != _mineCharacter.TeamId) > 0;
        
        public ICharacter GetFirstSpottedEnemyOrNull=>
            _charactersInVisibleArea.FirstOrDefault(e => e.Health > 0 && e.TeamId != _mineCharacter.TeamId);

        public void Construct(ICharacter mineCharacter)
        {
            _mineCharacter = mineCharacter;
        }


        private void OnTriggerEnter(Collider other)
        {
            ICharacter character = other.GetComponent<ICharacter>();
            if (character != null&&!_charactersInVisibleArea.Contains(character))
            {
                OnSeeCharacter?.Invoke(character);
                _charactersInVisibleArea.Add(character);
                if (character.TeamId != _mineCharacter.TeamId)
                {
                    OnEnemySpotted?.Invoke(character);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            ICharacter character = other.GetComponent<ICharacter>();
            if (character != null&&_charactersInVisibleArea.Contains(character)&&character!=_mineCharacter)
            {
                OnLoseCharacter?.Invoke(character);
                _charactersInVisibleArea.Remove(character);
                if (character.TeamId !=  _mineCharacter.TeamId)
                {
                    OnEnemyLosted?.Invoke(character);
                }
            }
        }

        private void OnTriggerStay(Collider other)
        {
            ICharacter enemy = _charactersInVisibleArea.FirstOrDefault(e => e.Health > 0 && e.TeamId !=  _mineCharacter.TeamId);
            if (enemy != null)
            {
                OnEnemySpottedLoop?.Invoke(enemy);
            }
        }
    }
}
