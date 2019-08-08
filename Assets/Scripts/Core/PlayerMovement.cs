using UnityEngine;

namespace UniverseTeam.Core
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] [Range(0f, 100f)] private float moveSpeed = 5;
        [SerializeField] [Range(0f, 100f)] private float sensitivity = 10f;
        [SerializeField] private float offsetX = 9f;
        [SerializeField] private float offsetForward = 20f;
        [SerializeField] private float offsetBack = 7f;

        private Transform finishTransform;

        private void Update()
        {
            if (SessionManager.instance.State != SessionManager.GameState.InGame) return;

            ApplyConstantMotion();
            FinishBehavior();
        }

        private void FinishBehavior()
        {
            if (finishTransform == null)
            {
                finishTransform = SessionManager.instance.FinishTransform;
            }
            else if (transform.position.z > finishTransform.position.z)
            {
                SessionManager.instance.VictoryBehavior();
            }
        }

        public void UpdatePosition(Vector3 startInputPosition, Vector3 currentInputPosition)
        {
            var result = (currentInputPosition - startInputPosition) * sensitivity;
            var position = transform.position;
            var cameraPosition = SessionManager.instance.CameraController.transform.position;

            position = new Vector3(Mathf.Clamp(position.x + result.x,
                    -offsetX,
                    offsetX),
                position.y,
                Mathf.Clamp(position.z + result.y,
                    cameraPosition.z - offsetBack,
                    cameraPosition.z + offsetForward));

            transform.position = position;
        }

        private void ApplyConstantMotion()
        {
            var step = Vector3.forward * moveSpeed * Time.deltaTime;
            transform.position += step;
            SessionManager.instance.CameraController.transform.position += step;
        }
    }
}