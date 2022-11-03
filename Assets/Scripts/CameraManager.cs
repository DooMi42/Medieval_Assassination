using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    InputManager inputManager;

    public Transform targetTransform;                       //The object the camera will follow
    public Transform cameraPivot;                           //The object the camera uses to look up and down
    private Transform cameraTransform;
    public LayerMask collisionLayers;
    private float defaultPos;
    private Vector3 cameraFollowVelocity = Vector3.zero;
    private Vector3 cameraVectorPosition;

    public float cameraCollisionOffset = 0.5f;
    public float minimumCollisionOffset = 0.2f;
    public float cameraCollisionRadius = 4;
    public float cameraFollowSpeed = 0.1f;
    public float cameraLookSpeed = .2f;
    public float cameraPivotSpeed = .2f;

    public float lookAngle;
    public float pivotAngle;
    private float minPivotAngle = -35;
    private float maxPivotAngle = 42.5f;

    private void Awake()
    {
        inputManager = FindObjectOfType<InputManager>();
        targetTransform = FindObjectOfType<PlayerManager>().transform;
        cameraTransform = Camera.main.transform;
        defaultPos = cameraTransform.localPosition.z;
    }

    public void HandleAllCameraMovement()
    {
        followPlayer();
        RotateCamera();
        HandleCameraCollisions();
    }

    private void followPlayer()
    {
        Vector3 targetPosition = Vector3.SmoothDamp(transform.position, targetTransform.position, ref cameraFollowVelocity, cameraFollowSpeed);

        transform.position = targetPosition;
    }
    
    private void RotateCamera()
    {
        Vector3 rotation;

        lookAngle = lookAngle + (inputManager.cameraInputX * cameraLookSpeed);
        pivotAngle = pivotAngle - (inputManager.cameraInputY * cameraPivotSpeed);
        pivotAngle = Mathf.Clamp(pivotAngle, minPivotAngle, maxPivotAngle);

        rotation = Vector3.zero;
        rotation.y = lookAngle;
        Quaternion targetRotation = Quaternion.Euler(rotation);
        transform.rotation = targetRotation;

        rotation = Vector3.zero;
        rotation.x = pivotAngle;
        targetRotation = Quaternion.Euler(rotation);
        cameraPivot.localRotation = targetRotation;
    }

    private void HandleCameraCollisions()
    {
        float targetPos = defaultPos;
        RaycastHit hit;
        Vector3 direction = cameraTransform.position - cameraPivot.position;
        direction.Normalize();

        if (Physics.SphereCast(cameraPivot.transform.position, cameraCollisionRadius, direction, out hit, Mathf.Abs(targetPos), collisionLayers))
        {
            float distance = Vector3.Distance(cameraPivot.position, hit.point);
            targetPos =- (distance - cameraCollisionOffset);

        }

        if (Mathf.Abs(targetPos) < minimumCollisionOffset)
        {
            targetPos = targetPos - minimumCollisionOffset;
        }

        cameraVectorPosition.z = Mathf.Lerp(cameraTransform.localPosition.z, targetPos, 0.2f);
        cameraTransform.localPosition = cameraVectorPosition;
    }
}
