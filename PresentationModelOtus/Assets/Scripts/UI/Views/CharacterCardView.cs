﻿using System;
using Core;
using TMPro;
using UI.Elements;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{
    [Serializable]
    public sealed class CharacterSpecsOutput
    {
        [SerializeField] private TMP_Text moveSpeedOutput;
        [SerializeField] private TMP_Text stamineOutput;
        [SerializeField] private TMP_Text dexterityOutput;
        [SerializeField] private TMP_Text intelligenceOutput;
        [SerializeField] private TMP_Text damageOutput;
        [SerializeField] private TMP_Text regenerationOutput;

        public void SetSpecs(ICharacterSpecs specs)
        {
            moveSpeedOutput.text = $"Move Speed: {specs.MoveSpeed}";
            stamineOutput.text = $"Stamina : {specs.Stamina}";
            dexterityOutput.text = $"Dexterity: {specs.Dexterity}";
            intelligenceOutput.text = $"Intelligence: {specs.Intelligence}";
            damageOutput.text = $"Damage: {specs.Damage}";
            regenerationOutput.text = $"Regeneration: {specs.Regeneration}";
        }
    }

    [Serializable]
    public sealed class CharacterInfoOutput
    {
        [SerializeField] private TMP_Text nicknameOutput;
        [SerializeField] private Image avatarImage;
        [SerializeField] private TMP_Text descriptionOutput;
        [SerializeField] private TMP_Text lvlOutput;

        public void SetInfo(string nickname, string description, int lvl, Sprite avatar)
        {
            nicknameOutput.text = $"@{nickname}";
            descriptionOutput.text = description;
            
            avatarImage.sprite = avatar;
            
            SetLevel(lvl);
        }

        public void SetLevel(int lvl)
        {
            lvlOutput.text = $"Level: {lvl}";
        }
    }
    
    [RequireComponent(typeof(Canvas))]
    public class CharacterCardView: MonoBehaviour, IDisposable
    {
        [SerializeField] private CharacterInfoOutput characterInfo;
        [SerializeField] private CharacterSpecsOutput characterSpecs;
        [Space(10)] 
        [SerializeField] private OutputSlider experienceSlider;
        [Space(10)]
        [SerializeField] private BigButton levelUpBtn;
        [SerializeField] private Button closeBtn;
        
        private Canvas _canvas;
        
        private CharacterViewModel _selectedCharacterViewModel;

        private void Awake()
        {
            _canvas = GetComponent<Canvas>();
            
            experienceSlider.SetPrefix("XP");
            
            Hide();
        }

        private void OnEnable()
        {
            closeBtn.onClick.AddListener(Hide);
        }

        private void OnDisable()
        {
            closeBtn.onClick.RemoveAllListeners();
        }

        public void Show(CharacterViewModel characterViewModel)
        {
            if (_selectedCharacterViewModel != null)
            {
                Dispose();
            }

            _selectedCharacterViewModel = characterViewModel;
            
            _selectedCharacterViewModel.OnExperienceChanged += OnCharacterExperienceChanged;
            _selectedCharacterViewModel.OnLevelChanged += OnCharacterLevelChanged;
            
            ShowCharacter();
            
            levelUpBtn.AddListener(OnLevelUpPressed);
            
            _canvas.enabled = true;
        }

        public void Hide()
        {
            _canvas.enabled = false;
            
            experienceSlider.Dispose();
            Dispose();
        }

        public void Dispose()
        {
            levelUpBtn.RemoveListener(OnLevelUpPressed);
            
            if(_selectedCharacterViewModel == null) return;
            
            _selectedCharacterViewModel.OnExperienceChanged -= OnCharacterExperienceChanged;
            _selectedCharacterViewModel.OnLevelChanged -= OnCharacterLevelChanged;
            
            _selectedCharacterViewModel = null;
        }
        
        private void ShowCharacter()
        {
            characterInfo.SetInfo(
                _selectedCharacterViewModel.Name,
                _selectedCharacterViewModel.Description,
                _selectedCharacterViewModel.CurrentLevel,
                _selectedCharacterViewModel.Avatar);
            
            characterSpecs.SetSpecs(_selectedCharacterViewModel.Specs);
            
            levelUpBtn.SetAvailable(_selectedCharacterViewModel.AvailableLevelUp);
            
            experienceSlider.SetValue(
                _selectedCharacterViewModel.CurrentExperience, 
                _selectedCharacterViewModel.MaxExperience,
                _selectedCharacterViewModel.PreviousMaxExperience);
        }

        private void OnCharacterExperienceChanged()
        {
            experienceSlider.SetValue(
                _selectedCharacterViewModel.CurrentExperience, 
                _selectedCharacterViewModel.MaxExperience,
                _selectedCharacterViewModel.PreviousMaxExperience);
            
            levelUpBtn.SetAvailable(_selectedCharacterViewModel.AvailableLevelUp);
        }

        private void OnCharacterLevelChanged()
        {
            characterInfo.SetLevel(_selectedCharacterViewModel.CurrentLevel);
            characterSpecs.SetSpecs(_selectedCharacterViewModel.Specs);
            
            experienceSlider.SetValue(
                _selectedCharacterViewModel.CurrentExperience, 
                _selectedCharacterViewModel.MaxExperience,
                _selectedCharacterViewModel.PreviousMaxExperience);
            
            levelUpBtn.SetAvailable(_selectedCharacterViewModel.AvailableLevelUp);
        }

        private void OnLevelUpPressed()
        {
            if(_selectedCharacterViewModel == null) return;
            _selectedCharacterViewModel.LevelUp();
        }
    }
}