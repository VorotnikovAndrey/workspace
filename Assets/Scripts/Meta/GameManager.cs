using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;
using UniverseTeam.Libraries;

namespace UniverseTeam.Core
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance = null;

        [Header("Libraries")]
        public FiguresLibrary figuresLibrary;

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

            Assert.IsNotNull(figuresLibrary);
        }

        public void LoadScene(int index)
        {
            SceneManager.LoadScene(index);
        }
    }
}