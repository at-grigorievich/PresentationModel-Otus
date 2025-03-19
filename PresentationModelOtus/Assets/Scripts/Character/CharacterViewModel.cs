using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class CharacterViewModel
    {
        private readonly Dictionary<int,ICharacterSpecs> _characterSpecsByLevel;
        
        public string Name { get; private set; }
        public Sprite Avatar { get; private set; }
        public string Description { get; private set; }
        public int CurrentLevel { get; private set; }
        public int CurrentExperience { get; private set; }

        public ICharacterSpecs Specs => _characterSpecsByLevel[CurrentLevel];

        public bool AvailableLevelUp => CurrentLevel < _characterSpecsByLevel.Count 
                                        && CurrentExperience >= Specs.MaxExperience;
        
        //TODO: replace with RX properties ( like ReactiveProperty<int> )
        public event Action OnExperienceChanged;
        public event Action OnLevelChanged; 
        
        public CharacterViewModel(CharacterConfig config)
        {
            Name = config.Name;
            Avatar = config.Icon;
            Description = config.Description;

            _characterSpecsByLevel = new Dictionary<int, ICharacterSpecs>(config.SpecsByLevel);

            CurrentLevel = 1;
            CurrentExperience = 0;
        }
        
        public void LevelUp()
        {
            Debug.Log("Afssaf");
            CurrentLevel = Mathf.Clamp(CurrentLevel + 1, 1, _characterSpecsByLevel.Count);
            
            Debug.Log(CurrentLevel);
            OnLevelChanged?.Invoke();
        }

        public void AddExperience(int experience)
        {
            CurrentExperience += experience;
            OnExperienceChanged?.Invoke();
        }
    }
}