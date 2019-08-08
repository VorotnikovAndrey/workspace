using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace UniverseTeam.Meta
{
    public class Settings : MonoBehaviour
    {
        [SerializeField] private Button exitButton;
        [SerializeField] private Button onMusicButton;
        [SerializeField] private Button offMusicButton;
        [Space]
        [SerializeField] private Sprite defaultButtonOn;
        [SerializeField] private Sprite colorButtonOn;
        [SerializeField] private Sprite defaultButtonOff;
        [SerializeField] private Sprite colorButtonOff;

        private Action closeCallBack;

        private void Awake()
        {
            Assert.IsNotNull(exitButton);
            Assert.IsNotNull(onMusicButton);
            Assert.IsNotNull(offMusicButton);
            Assert.IsNotNull(defaultButtonOn);
            Assert.IsNotNull(colorButtonOn);
            Assert.IsNotNull(defaultButtonOff);
            Assert.IsNotNull(colorButtonOff);
        }

        private void Start()
        {
            UpdateMusicButtonColor();
        }

        public void SetCloseCallback(Action callback)
        {
            closeCallBack = callback;
        }

        public void OnClickExitButton()
        {
            closeCallBack?.Invoke();
            Destroy(gameObject);
        }

        public void OnClickMusicOnButton()
        {
            PlayerPrefs.SetInt(MusicManager.instance.Key, 1);
            UpdateMusicButtonColor();
        }

        public void OnClickMusicOffButton()
        {
            PlayerPrefs.SetInt(MusicManager.instance.Key, 0);
            UpdateMusicButtonColor();
        }

        private void UpdateMusicButtonColor()
        {
            if (!PlayerPrefs.HasKey(MusicManager.instance.Key))
            {
                PlayerPrefs.SetInt(MusicManager.instance.Key, 1);
            }

            var state = PlayerPrefs.GetInt(MusicManager.instance.Key);

            onMusicButton.image.sprite = state == 1 ? colorButtonOn : defaultButtonOn;
            offMusicButton.image.sprite = state == 0 ? colorButtonOff : defaultButtonOff;

            if (state == 1)
            {
                MusicManager.instance.EnableMusic();
            }
            else
            {
                MusicManager.instance.DisableMusic();
            }
        }
    }
}