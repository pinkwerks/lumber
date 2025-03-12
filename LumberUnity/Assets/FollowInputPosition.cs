using UnityEngine;
using UnityEngine.InputSystem;

public class FollowInputNewSystem : MonoBehaviour
{
    // Set this to the desired distance from the camera along the Z axis.
    public float distanceFromCamera = 10f;

    private Camera mainCamera;
    private InputAction pointerPositionAction;

    private void Awake()
    {
        // Cache the main camera.
        mainCamera = Camera.main;

        // Create a new InputAction that reads the pointer (mouse/touch) position.
        pointerPositionAction = new InputAction("PointerPosition", binding: "<Pointer>/position");
        pointerPositionAction.Enable();
    }

    private void Update()
    {
        // Read the current pointer position as a Vector2.
        Vector2 pointerPosition = pointerPositionAction.ReadValue<Vector2>();

        // Create a Vector3 using the pointer position and the fixed z distance.
        Vector3 screenPosition = new Vector3(pointerPosition.x, pointerPosition.y, distanceFromCamera);

        // Convert the screen space position to world space.
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(screenPosition);

        // Update the object's position.
        transform.position = worldPosition;
    }

    private void OnDestroy()
    {
        // Properly disable and dispose of the InputAction.
        pointerPositionAction.Disable();
        pointerPositionAction.Dispose();
    }
}
