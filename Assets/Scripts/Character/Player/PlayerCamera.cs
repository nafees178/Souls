using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public static PlayerCamera instance;
    public PlayerManager player;
    public Camera cameraObject;
    [SerializeField] Transform cameraPivotTransform;

    [Header("Camera Settings")]
    private float cameraSmoothSpeed = 1f;
    [SerializeField] float leftAndRightRotationSpeed = 5;
    [SerializeField] float upAndDownRotationSpeed = 5;
    [SerializeField] float minimumPivot = -30;
    [SerializeField] float maximumPivot = 60;
    [SerializeField] float cameraCollisonRadius = 0.2f;
    [SerializeField] LayerMask collideWithLayers;




    [Header("Camera Values")]
    private Vector3 cameraVelocity;
    private Vector3 cameraObjectPosition;
    [SerializeField] float leftAndRightLookAngle;
    [SerializeField] float upAndDownLookAngle;
    private float cameraZposition;
    private float targetCameraZposition;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        cameraZposition = cameraObject.transform.localPosition.z;

    }

    public void HandleAllCameraActions()
    {
        if(player != null)
        {
            HandleFollowTarget();
            HandleRotations();
            HandleCollisions();
        }
    }

    private void HandleFollowTarget()
    {
        Vector3 targetCameraPosition = Vector3.SmoothDamp(transform.position, player.transform.position, ref cameraVelocity, cameraSmoothSpeed * Time.deltaTime);
        transform.position = targetCameraPosition;
    }

    private void HandleRotations()
    {
        leftAndRightLookAngle += (PlayerInputManager.instance.cameraHorizontalInput * leftAndRightRotationSpeed) * Time.deltaTime;
        upAndDownLookAngle -= (PlayerInputManager.instance.cameraVerticalInput * upAndDownRotationSpeed) * Time.deltaTime;
        upAndDownLookAngle = Mathf.Clamp(upAndDownLookAngle,minimumPivot,maximumPivot);

        Vector3 cameraRotation = Vector3.zero;
        Quaternion targetRotaion;

        cameraRotation.y = leftAndRightLookAngle;
        targetRotaion = Quaternion.Euler(cameraRotation);
        transform.rotation = targetRotaion;

        cameraRotation = Vector3.zero;
        cameraRotation.x = upAndDownLookAngle;
        targetRotaion = Quaternion.Euler(cameraRotation);
        
        cameraPivotTransform.localRotation = targetRotaion;
    }

    private void HandleCollisions()
    {
        targetCameraZposition = cameraZposition;
        RaycastHit hit;
        Vector3 direction = cameraObject.transform.position - cameraPivotTransform.position;
        direction.Normalize();

        if(Physics.SphereCast(cameraPivotTransform.position, cameraCollisonRadius, direction, out hit, Mathf.Abs(targetCameraZposition), collideWithLayers))
        {
            float distanceFromHitObject = Vector3.Distance(cameraPivotTransform.position, hit.point);
            targetCameraZposition = -(distanceFromHitObject - cameraCollisonRadius);
        }

        if(Mathf.Abs(targetCameraZposition) < cameraCollisonRadius)
        {
            targetCameraZposition = -cameraCollisonRadius;
        }

        cameraObjectPosition.z = Mathf.Lerp(cameraObject.transform.localPosition.z, targetCameraZposition, 0.2f);
        cameraObject.transform.localPosition = cameraObjectPosition;
    }
}
