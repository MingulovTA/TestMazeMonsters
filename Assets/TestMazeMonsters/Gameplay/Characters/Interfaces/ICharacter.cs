using System;
using TestMazeMonsters.Gameplay.Characters.Core;
using UnityEngine;

namespace TestMazeMonsters.Gameplay.Characters
{
    public interface ICharacter
    {
        Vector3Int AreaPosision { get; }
        Vector3 Posision { get; }
        TeamId TeamId { get; }
        int Health { get; }
        void TakeDamage(int value);

        AiSensor AiSensor { get; }
        
        event Action<int, int> OnAreaChanged;
    }
}
