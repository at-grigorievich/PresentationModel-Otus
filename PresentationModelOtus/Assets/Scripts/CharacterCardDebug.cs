using Core;
using UnityEngine;
using VContainer;

public sealed class CharacterCardDebug : MonoBehaviour
{
    [SerializeField] private int addedExperience;
    
    [Inject] private CharacterCardPresenter _presenter;
    [Inject] private CharactersStorage _storage;

    public void ShowNextCharacterCard()
    {
        CharacterViewModel character = _storage.GetNext();
        _presenter.OpenPopup(character);
    }

    public void OpenPopup()
    {
        _presenter.OpenPopup();
    }

    public void AddExperienceToCurrentCharacter()
    {
        _presenter.AddExpToCharacter(addedExperience);
    }
}