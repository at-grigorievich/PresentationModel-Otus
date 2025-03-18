using System;
using UI.Views;

namespace Core
{
    public sealed class CharacterCardPresenter
    {
        private readonly CharacterCardView _cardView;

        private CharacterViewModel _characterViewModel;
        
        public CharacterCardPresenter(CharacterCardView cardView)
        {
            _cardView = cardView;
        }

        public void OpenPopup()
        {
            if(_characterViewModel == null)
                throw new NullReferenceException("The character is not selected.");
            
            _cardView.Show(_characterViewModel);
        }
        
        public void OpenPopup(CharacterViewModel characterViewModel)
        {
            _characterViewModel = characterViewModel;
            _cardView.Show(_characterViewModel);
        }

        public void HidePopup()
        {
            _cardView.Hide();
        }

        public void ChangeCharacter(CharacterViewModel characterViewModel)
        {
            _characterViewModel = characterViewModel;
            _cardView.ChangeCharacter(_characterViewModel);
        }

        public void AddExpToCharacter(int experience)
        {
            if (_characterViewModel == null)
                throw new NullReferenceException("no character selected");
            
            _characterViewModel.AddExperience(experience);
        }
        
    }
}