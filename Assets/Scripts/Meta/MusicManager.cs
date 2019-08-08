using UnityEngine;
using UnityEngine.Assertions;

namespace UniverseTeam.Meta
{
    public class MusicManager : MonoBehaviour
    {
        public static MusicManager instance = null;

        private const string key = "MUSIC_ENABLE";

        [SerializeField] private AudioSource audioSource;

        public string Key => key;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance == this)
            {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);

            Assert.IsNotNull(audioSource);
        }

        private void Start()
        {
            if (!PlayerPrefs.HasKey(Key)) return;

            if (PlayerPrefs.GetInt(Key) == 1)
            {
                EnableMusic();
            }
            else
            {
                DisableMusic();
            }
        }

        public void EnableMusic()
        {
            audioSource.mute = false;
        }

        public void DisableMusic()
        {
            audioSource.mute = true;
        }
    }
}