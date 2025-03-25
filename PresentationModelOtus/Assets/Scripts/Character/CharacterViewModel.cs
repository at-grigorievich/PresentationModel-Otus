using Core.Model;

namespace Core
{
    public sealed class CharacterViewModel
    {
        private CharacterModel _selectedModel;
        
        public readonly AboutCharacterViewModel CharacterDescription = new();
        public readonly LevelCharacterViewModel CharacterLevel = new();
        public readonly SpecSetViewModel SpecSet = new();

        public void ChangeModel(CharacterModel newModel)
        {
            if(ReferenceEquals(_selectedModel, newModel) == true) return;
            
            _selectedModel = newModel;
            
            CharacterDescription.Setup(_selectedModel);
            CharacterLevel.Setup(_selectedModel);
            SpecSet.Setup(_selectedModel.SpecByLevels[_selectedModel.CurrentLevel]);
        }
        
        public void AddExperience(int exp) => CharacterLevel.AddExperience(exp);
    }
}