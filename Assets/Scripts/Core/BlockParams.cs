using UnityEngine;
using UnityEngine.Assertions;

namespace UniverseTeam.Core
{
    [RequireComponent(typeof(MeshRenderer))]
    public class BlockParams : MonoBehaviour
    {
        [HideInInspector] [SerializeField] private MeshRenderer meshRenderer;
        [SerializeField] private float destroyDistance = 15f;
        [SerializeField] private float meshEnableDistance = 50f;

        public MeshRenderer MeshRenderer => meshRenderer;

        private Transform cameraTransform;
   
        private void OnValidate()
        {
            meshRenderer = GetComponent<MeshRenderer>();
        }

        private void Awake()
        {
            Assert.IsNotNull(meshRenderer);
        }

        private void Start()
        {
            cameraTransform = SessionManager.instance.CameraController.transform;
        }

        private void Update()
        {
            if (cameraTransform.position.z > transform.position.z + destroyDistance || transform.position.y < -10f)
            {
                Destroy(gameObject);
                return;
            }

            meshRenderer.enabled = !(transform.position.z > cameraTransform.position.z + meshEnableDistance);
        }
    }
}