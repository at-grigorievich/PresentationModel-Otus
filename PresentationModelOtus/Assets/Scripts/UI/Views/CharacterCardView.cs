using Core;
using UI.Elements;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{
    [RequireComponent(typeof(Canvas))]
    public class CharacterCardView: MonoBehaviour
    {
        [SerializeField] private AboutCharacterView aboutCharacterView;
        [SerializeField] private CharacterLevelView characterLevelView;
        [SerializeField] private SpecSetView specSetView;
        [Space(10)]
        [SerializeField] private BigButton levelUpBtn;
        [SerializeField] private Button closeBtn;
        
        private Canvas _canvas;
        
        private CharacterViewModel _viewModel;

        private void Awake()
        {
            _canvas = GetComponent<Canvas>();
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

        public void Init(CharacterViewModel viewModel)
        {
            _viewModel = viewModel;
            
            aboutCharacterView.Init(viewModel.CharacterDescription);
            characterLevelView.Init(viewModel.CharacterLevel);
            specSetView.Init(viewModel.SpecSet);
        }
        
        public void Show()
        {
            aboutCharacterView.Show();
            characterLevelView.Show();
            specSetView.Show();
            
            _canvas.enabled = true;
        }

        public void Hide()
        {
            aboutCharacterView.Hide();
            characterLevelView.Hide();
            specSetView.Hide();
            
            _canvas.enabled = false;
        }
    }
}