using UnityEngine;

public class PlayerCamera : MonoBehaviour {

    [Header("Settings")]
    [SerializeField] float mouseSensitivity = 3.0f;
    [SerializeField] float distanceFromTarget = 3.5f;
    [SerializeField] float cameraHeightToPlayer = 1.5f;
    [SerializeField] float rotationSmoothTime = 8.0f;
    [SerializeField] Transform cameraTarget;
    [SerializeField] Vector2 pitchMinMax = new Vector2(-40, 85);
    [SerializeField] bool lockCursor;

    float yaw, pitch;
    Vector3 rotationSmoothVelocity, currentRotation;

    [Header("Collision Variables")]

    [Header("Transparency")]
    public bool changeTransparency = true;
    public SkinnedMeshRenderer bodyRenderer;
    public SkinnedMeshRenderer jointsRenderer;

    [Header("Camera Speeds")]
    public float cameraMoveSpeed = 5.0f;
    public float returnToOriginalSpeed = 9.0f;
    public float wallPush = 0.7f;

    [Header("Distances")]
    public float closeDistanceToPlayer = 2.0f;
    public float closestDistanceToPlayer = 1.0f;

    [Header("Mask")]
    public LayerMask collisionMask;

    private bool pitchLock = false;

    private void Start()
    {
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void LateUpdate()
    {
        CameraRotation();
    }

    void CameraRotation()
    {
        CollisionCheck(cameraTarget.position - transform.forward * distanceFromTarget);
        WallCheck();

        if (!pitchLock)
        {
            yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
            pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
            pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);
            currentRotation = Vector3.Lerp(currentRotation, new Vector3(pitch, yaw), rotationSmoothTime * Time.deltaTime);
        }
        else
        {
            yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
            pitch = pitchMinMax.y;
            currentRotation = Vector3.Lerp(currentRotation, new Vector3(pitch, yaw), rotationSmoothTime * Time.deltaTime);
        }
        
        transform.eulerAngles = currentRotation;

        Vector3 e = transform.eulerAngles;
        e.x = 0;
        cameraTarget.eulerAngles = e;
    }

    private void WallCheck()
    {
        Ray ray = new Ray(cameraTarget.position, -cameraTarget.forward);
        RaycastHit hit;

        if(Physics.SphereCast(ray, 0.5f, out hit, 0.7f, collisionMask))
        {
            pitchLock = true;
        }
        else
        {
            pitchLock = false;
        }
    }

    private void CollisionCheck(Vector3 returnPoint)
    {
        RaycastHit hit;

        if(Physics.Linecast(cameraTarget.position, returnPoint, out hit, collisionMask))
        {
            Vector3 norm = hit.normal * wallPush;
            Vector3 p = hit.point + norm;

            TransparencyCheck();

            if(Vector3.Distance(Vector3.Lerp(transform.position, p, cameraMoveSpeed * Time.deltaTime), cameraTarget.position) <= closestDistanceToPlayer)
            {

            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, p, cameraMoveSpeed * Time.deltaTime);
            }

            return;
        }

        FullTransparency();

        transform.position = Vector3.Lerp(transform.position, returnPoint, returnToOriginalSpeed * Time.deltaTime);
        pitchLock = false;
    }

    private void TransparencyCheck()
    {
        if(changeTransparency)
        {
            if(Vector3.Distance(transform.position, cameraTarget.position) <= closeDistanceToPlayer)
            {
                Color bodyTemp = bodyRenderer.sharedMaterial.color;
                Color jointsTemp = jointsRenderer.sharedMaterial.color;

                bodyTemp.a = Mathf.Lerp(bodyTemp.a, 0.2f, cameraMoveSpeed * Time.deltaTime);
                jointsTemp.a = Mathf.Lerp(jointsTemp.a, 0.2f, cameraMoveSpeed * Time.deltaTime);

                bodyRenderer.sharedMaterial.color = bodyTemp;
                jointsRenderer.sharedMaterial.color = jointsTemp;
            }
            else
            {
                if(bodyRenderer.sharedMaterial.color.a <= 0.99f || jointsRenderer.sharedMaterial.color.a <= 0.99f)
                {
                    Color bodyTemp = bodyRenderer.sharedMaterial.color;
                    Color jointsTemp = jointsRenderer.sharedMaterial.color;

                    bodyTemp.a = Mathf.Lerp(bodyTemp.a, 1, cameraMoveSpeed * Time.deltaTime);
                    jointsTemp.a = Mathf.Lerp(jointsTemp.a, 1, cameraMoveSpeed * Time.deltaTime);

                    bodyRenderer.sharedMaterial.color = bodyTemp;
                    jointsRenderer.sharedMaterial.color = jointsTemp;
                }
            }
        }
    }

    private void FullTransparency()
    {
        if(changeTransparency)
        {
            if (bodyRenderer.sharedMaterial.color.a <= 0.99f || jointsRenderer.sharedMaterial.color.a <= 0.99f)
            {
                Color bodyTemp = bodyRenderer.sharedMaterial.color;
                Color jointsTemp = jointsRenderer.sharedMaterial.color;

                bodyTemp.a = Mathf.Lerp(bodyTemp.a, 1, cameraMoveSpeed * Time.deltaTime);
                jointsTemp.a = Mathf.Lerp(jointsTemp.a, 1, cameraMoveSpeed * Time.deltaTime);

                bodyRenderer.sharedMaterial.color = bodyTemp;
                jointsRenderer.sharedMaterial.color = jointsTemp;
            }
        }
    }
}
