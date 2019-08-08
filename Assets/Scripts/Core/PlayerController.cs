using UnityEngine;
using UnityEngine.Assertions;

namespace UniverseTeam.Core
{
    [RequireComponent(typeof(PlayerMovement))]
    public class PlayerController : MonoBehaviour
    {
        [HideInInspector] [SerializeField] private PlayerMovement playerMovement;

        private Vector3 lastInputPosition;
        private Vector3 currentInputPosition;

        public PlayerMovement PlayerMovement => playerMovement;

        private void OnValidate()
        {
            playerMovement = GetComponent<PlayerMovement>();
        }

        private void Awake()
        {
            Assert.IsNotNull(playerMovement);
        }

        private void Update()
        {
            if (SessionManager.instance.State != SessionManager.GameState.InGame) return;

            if (Input.GetMouseButtonDown(0))
            {
                lastInputPosition = GetViewportPointPosition();
            }

            else if (Input.GetMouseButton(0))
            {
                currentInputPosition = GetViewportPointPosition();
                playerMovement.UpdatePosition(lastInputPosition, currentInputPosition);
                lastInputPosition = currentInputPosition;
            }
        }

        private Vector3 GetViewportPointPosition()
        {
            return SessionManager.instance.CameraController.Camera.ScreenToViewportPoint(Input.mousePosition);
        }

        private void OnCollisionEnter(Collision other)
        {
            var blockParams = other.gameObject.GetComponent<BlockParams>();
            if (blockParams == null) return;

            if (blockParams.MeshRenderer.material.color != Color.white)
            {
                SessionManager.instance.LoseBehavior();
            }
        }
    }
}