using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

namespace UniverseTeam.Core
{
    public class SessionManager : MonoBehaviour
    {
        public enum GameState { Victory = 0, Lose = 1, InGame = 3 }

        public static event Action<GameState> GameStateChanged;

        public static SessionManager instance = null;

        [SerializeField] private CameraController cameraControllerPref;
        [SerializeField] private PlayerController playerPref;
        [SerializeField] private BlockGenerator blockGeneratorPref;

        [Space]
        [SerializeField] private float startPositionOffsetZ = 40f;

        [HideInInspector] public CameraController CameraController;
        [HideInInspector] public PlayerController PlayerController;
        [HideInInspector] public BlockGenerator BlockGeneratorPref;
        [HideInInspector] public Transform FinishTransform;

        private GameState state;
        public GameState State
        {
            get => state;
            private set
            {
                if (state == value) return;

                state = value;
                GameStateChanged?.Invoke(value);
            }
        } 

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance == this)
            {
                Destroy(gameObject);
            }

            Assert.IsNotNull(cameraControllerPref);
            Assert.IsNotNull(playerPref);
            Assert.IsNotNull(blockGeneratorPref);
        }

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            BlockGeneratorPref = Instantiate(blockGeneratorPref);
            PlayerController = Instantiate(playerPref);
            CameraController = Instantiate(cameraControllerPref);

            BlockGeneratorPref.Init();
            SetStartPosition();
            SessionStart();
        }

        public void SetFinishTransform(Transform finishTransform)
        {
            FinishTransform = finishTransform;
        }

        private void SetStartPosition()
        {
            var startPosition = BlockGeneratorPref.transform.position - Vector3.forward * startPositionOffsetZ;
            CameraController.transform.position = startPosition;
            PlayerController.transform.position = startPosition + Vector3.up / 2f;
        }

        public void SessionStart()
        {
            State = GameState.InGame;
        }

        public void VictoryBehavior()
        {
            State = GameState.Victory;

            StartCoroutine(DelayReturnToMetaScene());
        }

        public void LoseBehavior()
        {
            State = GameState.Lose;

            StartCoroutine(DelayReturnToMetaScene());
        }

        private IEnumerator DelayReturnToMetaScene()
        {
            yield return new WaitForSeconds(2f);

            instance = null;
            GameManager.instance.LoadScene(0);
        }
    }
}