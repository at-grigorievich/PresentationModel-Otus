﻿using System;
using UnityEngine;

namespace Core
{
    //for potential decorators
    public interface ICharacterSpecs
    {
        int MaxExperience { get; }
        int MoveSpeed { get; }
        int Stamina { get; }
        int Dexterity { get; }
        int Intelligence { get; }
        int Damage { get; }
        int Regeneration { get; }
    }

    [Serializable]
    public sealed class CharacterSpecsDataFactory
    {
        [SerializeField] private int maxExperience;
        [SerializeField, Range(10f,100f)] private int moveSpeed;
        [SerializeField, Range(10f,100f)] private int stamina;
        [SerializeField, Range(10f,100f)] private int dexterity;
        [SerializeField, Range(10f,100f)] private int intelligence;
        [SerializeField, Range(10f,100f)] private int damage;
        [SerializeField, Range(10f,100f)] private int regeneration;
        
        public ICharacterSpecs Get() =>
            new CharacterSpecsData(maxExperience, moveSpeed, stamina, dexterity, 
                intelligence, damage, regeneration);
    }
    
    public sealed class CharacterSpecsData: ICharacterSpecs
    {
        public int MaxExperience { get; }
        public int MoveSpeed { get; }
        public int Stamina { get; }
        public int Dexterity { get; }
        public int Intelligence { get; }
        public int Damage { get; }
        public int Regeneration { get; }

        public CharacterSpecsData(int maxExp, int moveSpeed, int stamina, int dexterity, 
            int intelligence, int damage, int regeneration)
        {
            MaxExperience = maxExp;
            MoveSpeed = moveSpeed;
            Stamina = stamina;
            Dexterity = dexterity;
            Intelligence = intelligence;
            Damage = damage;
            Regeneration = regeneration;
        }
    }
}