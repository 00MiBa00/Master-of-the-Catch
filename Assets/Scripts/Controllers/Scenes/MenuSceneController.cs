using Models.Scenes;
using Types;
using UnityEngine;
using Views.General;
using Views.Menu;

namespace Controllers.Scenes
{
    public class MenuSceneController : AbstractSceneController
    {
        [SerializeField] private MainPanel _mainPanel;
        [SerializeField] private PrivacyPanel _privacyPanel;

        private MenuSceneModel _model;
        
        protected override void OnSceneEnable()
        {
            OpenMainPanel();
        }

        protected override void OnSceneStart()
        {
            
        }

        protected override void OnSceneDisable()
        {
            
        }

        protected override void Initialize()
        {
            
        }

        protected override void Subscribe()
        {
            _model = new MenuSceneModel();
        }

        protected override void Unsubscribe()
        {
            
        }

        private void OpenMainPanel()
        {
            _mainPanel.OnPressBtnAction += OnReceiveAnswerMainPanel;
            OpenPanel(_mainPanel);
        }

        private void OpenPrivacyPanel()
        {
            _privacyPanel.OnPressBtnAction += OnReceiveAnswerPrivacyPanel;
            OpenPanel(_privacyPanel);
        }

        private void OnReceiveAnswerMainPanel(int answer)
        {
            _mainPanel.OnPressBtnAction -= OnReceiveAnswerMainPanel;

            if (answer < 4)
            {
                _model.SetMeal(answer);
                base.LoadScene(SceneType.ChooseDishScene);
            }
            else
            {
                ClosePanel(_mainPanel);
                OpenPrivacyPanel();
            }
        }

        private void OnReceiveAnswerPrivacyPanel(int answer)
        {
            _privacyPanel.OnPressBtnAction += OnReceiveAnswerPrivacyPanel;
            ClosePanel(_privacyPanel);
            OpenMainPanel();
        }

        private void OpenPanel(PanelView view)
        {
            view.Open();
        }

        private void ClosePanel(PanelView view)
        {
            view.Close();
        }
    }
}