using System;
using UnityEngine;
using VContainer;

namespace Core
{
    [Serializable]
    public sealed class CharacterStorageFactory
    {
        [SerializeField] private CharacterConfig[] configs;

        public void Create(IContainerBuilder builder)
        {
            builder.Register<CharactersStorage>(Lifetime.Singleton)
                .WithParameter(configs);
        }
    }
    
    public sealed class CharactersStorage
    {
        private readonly CharacterViewModel[] _characters;

        private int _currentSelectedIndex;
        
        public CharactersStorage(CharacterConfig[] configs)
        {
            _characters = new CharacterViewModel[configs.Length];

            for (int i = 0; i < configs.Length; i++)
            {
                _characters[i] = new CharacterViewModel(configs[i]);
            }

            _currentSelectedIndex = -1;
        }

        public CharacterViewModel GetNext()
        {
            UpdateSelectedIndex();
            
            return _characters[_currentSelectedIndex];
        }

        private void UpdateSelectedIndex()
        {
            if (++_currentSelectedIndex >= _characters.Length)
                _currentSelectedIndex = 0;
        }
    }
}