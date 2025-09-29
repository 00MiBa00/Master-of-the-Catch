using System;
using System.Collections.Generic;
using UnityEngine;
using Views.General;

namespace Views.ChooseDish.Panels
{
    public class AddDishPanel : PanelView
    {
        [SerializeField] private TileBody _tileBody;
        [SerializeField] private IconBody _iconBody;
        [SerializeField] private IngredientsBody _ingredientsBody;
        [SerializeField] private DirectionsBody _directionsBody;
        [SerializeField] private AnimationPanel _notificationPanel;

        public event Action<string> OnSetTileAction;
        public event Action<Sprite> OnSetSpriteAction;
        public event Action<List<string>> OnSetIngredientsAction;
        public event Action<string> OnSetDirectionsActions; 
        
        protected override void OnPanelEnable()
        {
            OpenTileBody();
        }

        protected override void OnPanelDisable()
        {
            
        }

        private void OpenTileBody()
        {
            _tileBody.OnPressNextBtnAction += OnReceiveAnswerTileBody;
            OpenBody(_tileBody);
        }

        private void OpenIconBody()
        {
            _iconBody.OnPressNextBtnAction += OnReceiveAnswerIconBody;
            OpenBody(_iconBody);
        }

        private void OpenIngredientsBody()
        {
            _ingredientsBody.OnPressNextBtnAction += OnReceiveAnswerIngredientsBody;
            OpenBody(_ingredientsBody);
        }

        private void OpenDirectionsBody()
        {
            _directionsBody.OnPressNextBtnAction += OnReceiveAnswerDirectionsBody;
            OpenBody(_directionsBody);
        }

        private void OnReceiveAnswerTileBody(bool isCompleted, string text)
        {
            if (isCompleted)
            {
                _tileBody.OnPressNextBtnAction -= OnReceiveAnswerTileBody;
                CloseBody(_tileBody);
                OpenIconBody();
                
                OnSetTileAction?.Invoke(text);
            }
            else
            {
                OpenNotificationPanel(text);
            }
        }

        private void OnReceiveAnswerIconBody(Sprite sprite, string text)
        {
            if (sprite == null)
            {
                OpenNotificationPanel(text);
            }
            else
            {
                _iconBody.OnPressNextBtnAction -= OnReceiveAnswerIconBody;
                CloseBody(_iconBody);
                OpenIngredientsBody();
                
                OnSetSpriteAction?.Invoke(sprite);
            }
        }

        private void OnReceiveAnswerIngredientsBody(List<string> ingredients, string text)
        {
            if (ingredients == null)
            {
                OpenNotificationPanel(text);
            }
            else
            {
                _ingredientsBody.OnPressNextBtnAction -= OnReceiveAnswerIngredientsBody;
                CloseBody(_ingredientsBody);
                OpenDirectionsBody();
                
                OnSetIngredientsAction?.Invoke(ingredients);
            }
        }

        private void OnReceiveAnswerDirectionsBody(bool isCompleted, string text)
        {
            if (isCompleted)
            {
                _directionsBody.OnPressNextBtnAction -= OnReceiveAnswerDirectionsBody;
                CloseBody(_directionsBody);
                
                OnSetDirectionsActions?.Invoke(text);
            }
            else
            {
                OpenNotificationPanel(text);
            }
        }

        private void OpenBody(BodyAnimation body)
        {
            body.Show();
        }

        private void CloseBody(BodyAnimation body)
        {
            body.Hide();
        }

        private void OpenNotificationPanel(string value)
        {
            _notificationPanel.SetText(value);
            _notificationPanel.PlayAnimNotification();
        }
    }
}