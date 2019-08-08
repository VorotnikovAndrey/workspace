using UnityEngine;
using UniverseTeam.Core;

namespace UniverseTeam.Meta
{
    public class Preload : MonoBehaviour
    {
        [SerializeField] private GameManager gameManager;
        [SerializeField] private MusicManager musicManager;

        private void Awake()
        {
            if (GameManager.instance == null)
            {
                Instantiate(gameManager);
            }

            if (MusicManager.instance == null)
            {
                Instantiate(musicManager);
            }
        }
    }
}