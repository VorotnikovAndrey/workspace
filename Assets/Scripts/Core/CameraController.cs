using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace UniverseTeam.Core
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Camera camera;
        [Space]
        [SerializeField] private Color inGameScreenColor;
        [SerializeField] private Color victoryScreenColor;
        [SerializeField] private Color loseScreenColor;

        public Camera Camera => camera;

        private void Awake()
        {
            SessionManager.GameStateChanged += BackgroundAnimation;

            Assert.IsNotNull(camera);
        }

        private void BackgroundAnimation(SessionManager.GameState state)
        {
            StopAllCoroutines();

            switch (state)
            {
                case SessionManager.GameState.Victory:
                    StartCoroutine(ColorLerpAnimation(Camera.backgroundColor, victoryScreenColor));
                    break;
                case SessionManager.GameState.Lose:
                    StartCoroutine(ColorLerpAnimation(Camera.backgroundColor, loseScreenColor));
                    break;
                case SessionManager.GameState.InGame:
                    StartCoroutine(ColorLerpAnimation(Camera.backgroundColor, inGameScreenColor));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }

        private IEnumerator ColorLerpAnimation(Color from, Color to)
        {
            var progress = 0f;

            while (progress <= 1f)
            {
                Camera.backgroundColor = Color.Lerp(from, to, progress);
                progress += Time.deltaTime;

                yield return null;
            }
        }

        private void OnDestroy()
        {
            SessionManager.GameStateChanged -= BackgroundAnimation;
        }
    }
}