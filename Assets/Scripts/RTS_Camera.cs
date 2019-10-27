using UnityEngine;
using System.Collections;

namespace MechanicFever
{
    [RequireComponent(typeof(Camera))]
    [AddComponentMenu("RTS Camera")]
    public class RTS_Camera : Singleton<RTS_Camera>
    {
        private Camera _camera;
        public Camera Camera => _camera;

        public int lastTab = 0;

        public bool movementSettingsFoldout;
        public bool zoomingSettingsFoldout;
        public bool rotationSettingsFoldout;
        public bool heightSettingsFoldout;
        public bool mapLimitSettingsFoldout;
        public bool targetingSettingsFoldout;
        public bool inputSettingsFoldout;

        private Transform m_Transform; //camera tranform
        public bool useFixedUpdate = false; //use FixedUpdate() or Update()

        public float keyboardMovementSpeed = 5f; //speed with keyboard movement
        public float screenEdgeMovementSpeed = 3f; //spee with screen edge movement
        public float followingSpeed = 5f; //speed when following a target
        public float rotationSped = 3f;
        public float panningSpeed = 10f;
        public float mouseRotationSpeed = 10f;

        public bool autoHeight = true;
        public LayerMask groundMask = -1; //layermask of ground or other objects that affect height

        public float maxHeight = 10f; //maximal height
        public float minHeight = 15f; //minimnal height
        public float heightDampening = 5f; 
        public float keyboardZoomingSensitivity = 2f;
        public float scrollWheelZoomingSensitivity = 25f;

        private float zoomPos = 0; //value in range (0, 1) used as t in Matf.Lerp

        public bool limitMap = true;
        public float limitX = 50f; //x limit of map
        public float limitY = 50f; //z limit of map

        public Transform targetFollow;
        public Vector3 targetOffset;

        public bool FollowingTarget => targetFollow != null;

        public bool useScreenEdgeInput = true;
        public float screenEdgeBorder = 25f;

        public bool useKeyboardInput = true;
        public string horizontalAxis = "Horizontal";
        public string verticalAxis = "Vertical";

        public bool usePanning = true;
        public KeyCode panningKey = KeyCode.Mouse2;

        public bool useKeyboardZooming = true;
        public KeyCode zoomInKey = KeyCode.E;
        public KeyCode zoomOutKey = KeyCode.Q;

        public bool useScrollwheelZooming = true;
        public string zoomingAxis = "Mouse ScrollWheel";

        public bool useKeyboardRotation = true;
        public KeyCode rotateRightKey = KeyCode.X;
        public KeyCode rotateLeftKey = KeyCode.Z;

        public bool useMouseRotation = true;
        public KeyCode mouseRotationKey = KeyCode.Mouse1;

        private Vector2 KeyboardInput => useKeyboardInput ? new Vector2(Input.GetAxis(horizontalAxis), Input.GetAxis(verticalAxis)) : Vector2.zero;

        private Vector2 MouseInput => Input.mousePosition;

        private float ScrollWheel => Input.GetAxis(zoomingAxis); 

        private Vector2 MouseAxis => new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        private int ZoomDirection
        {
            get
            {
                bool zoomIn = Input.GetKey(zoomInKey);
                bool zoomOut = Input.GetKey(zoomOutKey);
                if (zoomIn && zoomOut)
                    return 0;
                else if (!zoomIn && zoomOut)
                    return 1;
                else if (zoomIn && !zoomOut)
                    return -1;
                else 
                    return 0;
            }
        }

        private int RotationDirection
        {
            get
            {
                bool rotateRight = Input.GetKey(rotateRightKey);
                bool rotateLeft = Input.GetKey(rotateLeftKey);
                if(rotateLeft && rotateRight)
                    return 0;
                else if(rotateLeft && !rotateRight)
                    return -1;
                else if(!rotateLeft && rotateRight)
                    return 1;
                else 
                    return 0;
            }
        }

        private void Start()
        {
            m_Transform = transform;
            _camera = GetComponent<Camera>();
        }

        private void Update()
        {
            if (!useFixedUpdate)
                CameraUpdate();
        }

        private void FixedUpdate()
        {
            if (useFixedUpdate)
                CameraUpdate();
        }

        private void CameraUpdate()
        {
            Move();

            HeightCalculation();
            Rotation();
            LimitPosition();
        }

        private void Move()
        {
            if (useKeyboardInput)
            {
                Vector3 desiredMove = new Vector3(KeyboardInput.x, 0, KeyboardInput.y);

                desiredMove *= keyboardMovementSpeed;
                desiredMove *= Time.deltaTime;
                desiredMove = Quaternion.Euler(new Vector3(2f, transform.eulerAngles.y, 2f)) * desiredMove;
                desiredMove = m_Transform.InverseTransformDirection(desiredMove);

                m_Transform.Translate(desiredMove, Space.Self);
            }      
        
            if(usePanning && Input.GetKey(panningKey) && MouseAxis != Vector2.zero)
            {
                Vector3 desiredMove = new Vector3(-MouseAxis.x, 0, -MouseAxis.y);

                desiredMove *= panningSpeed;
                desiredMove *= Time.deltaTime;
                desiredMove = Quaternion.Euler(new Vector3(2f, transform.eulerAngles.y, 2f)) * desiredMove;
                desiredMove = m_Transform.InverseTransformDirection(desiredMove);

                m_Transform.Translate(desiredMove, Space.Self);
            }
        }

        private void HeightCalculation()
        {
            float distanceToGround = DistanceToGround();
            if(useScrollwheelZooming)
                zoomPos += ScrollWheel * Time.deltaTime * scrollWheelZoomingSensitivity;
            if (useKeyboardZooming)
                zoomPos += ZoomDirection * Time.deltaTime * keyboardZoomingSensitivity;

            zoomPos = Mathf.Clamp01(zoomPos);

            float targetHeight = Mathf.Lerp(minHeight, maxHeight, zoomPos);
            float difference = 0; 

            if(distanceToGround != targetHeight)
                difference = targetHeight - distanceToGround;

            m_Transform.localPosition = Vector3.Lerp(m_Transform.localPosition, 
                new Vector3(m_Transform.localPosition.x, targetHeight + difference, m_Transform.localPosition.z), Time.deltaTime * heightDampening);
        }

        private void Rotation()
        {
            if(useKeyboardRotation)
                transform.Rotate(Vector3.up, RotationDirection * Time.deltaTime * rotationSped, Space.World);

            if (useMouseRotation && Input.GetKey(mouseRotationKey))
                m_Transform.Rotate(Vector3.up, -MouseAxis.x * Time.deltaTime * mouseRotationSpeed, Space.World);
        }

        private void LimitPosition()
        {
            if (!limitMap)
                return;
                
            m_Transform.localPosition = new Vector3(Mathf.Clamp(m_Transform.localPosition.x, -15, 15),
                m_Transform.localPosition.y,
                Mathf.Clamp(m_Transform.localPosition.z, -15, 15));
        }

        public void HighlightTarget(Transform highlightedTransform)
        {
            Vector3 e = new Vector3(highlightedTransform.position.x, transform.localPosition.y, highlightedTransform.position.z - transform.position.x);
            transform.localPosition = transform.InverseTransformPoint(e);
        }

        public void ResetTarget() => targetFollow = null;

        private float DistanceToGround()
        {
            Ray ray = new Ray(m_Transform.localPosition, Vector3.down);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, groundMask.value))
                return (hit.point - m_Transform.localPosition).magnitude;

            return 0f;
        }
    }
}