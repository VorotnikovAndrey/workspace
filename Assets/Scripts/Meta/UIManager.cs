using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using UniverseTeam.Core;

namespace UniverseTeam.Meta
{
    [RequireComponent(typeof(Canvas))]
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private Button playButton;
        [SerializeField] private Button settingsButton;
        [SerializeField] private Settings settingsPref;

        private void Awake()
        {
            Assert.IsNotNull(playButton);
            Assert.IsNotNull(settingsButton);
            Assert.IsNotNull(settingsPref);
        }

        public void OnClickPlay()
        {
            SetInteractableButtons(false);
            GameManager.instance.LoadScene(1);
        }

        public void OnClickSettings()
        {
            SetInteractableButtons(false);
            Instantiate(settingsPref, transform).SetCloseCallback(ReturnControll);
        }

        private void ReturnControll()
        {
            SetInteractableButtons(true);
        }

        private void SetInteractableButtons(bool state)
        {
            playButton.interactable = state;
            settingsButton.interactable = state;
        }
    }
}