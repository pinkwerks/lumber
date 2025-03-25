using UnityEngine;

public class MapInputRotation : MonoBehaviour
{
    [Tooltip("Rotation at ScreenOrigin")]
    public float OriginRotation;
    [Tooltip("Rotation at ScreenOrigin + ScreenDistance")]
    public float AtDistanceRotation;
    [Tooltip("Screen position where StartRotation is applied. Normalized.")]
    public Vector2 ScreenOrigin;
    [Tooltip("Distance from ScreenOrigin where EndRotation is applied")]
    public float ScreenDistance = 1;

    private Quaternion _original;

    public enum AxisType
    {
        X,
        Y,
        Z
    }

    public AxisType AxisTypeValue;

    [SerializeField] private bool _debug;

    private DemoInputActions _demoActions;

    private void Awake()
    {
        _demoActions = new DemoInputActions();
        _demoActions.Enable();

        Vector3 axis;
        float angle;
        transform.rotation.ToAngleAxis(out angle, out axis);
        _original = transform.localRotation;
    }

    private void OnEnable()
    {
        SetAxisFromCurrent();
    }

    private void Update()
    {
        var pos = _demoActions.UI.Point.ReadValue<Vector2>();
        // compensate for screen size
        pos.x /= Screen.width;
        pos.y /= Screen.height;

        var dist = Vector2.Distance(pos, ScreenOrigin);

        var mix = Mathf.InverseLerp(ScreenDistance, 0, dist);

        var angle = Mathf.Lerp(AtDistanceRotation, OriginRotation, mix);

        if (_debug)
        {
            Debug.Log($"pos={pos} dist={dist} mix={mix} angle={angle}");
        }

        Vector3 axis = Vector3.zero;
        switch (AxisTypeValue)
        {
            case AxisType.X:
                axis = Vector3.right;
                break;
            case AxisType.Y:
                axis = Vector3.up;
                break;
            case AxisType.Z:
                axis = Vector3.forward;
                break;
        }

        transform.localRotation = _original * Quaternion.AngleAxis(angle, axis);
    }

    private void OnDestroy()
    {
        // Properly disable and dispose of the InputAction.
        _demoActions.Disable();
    }

    [ContextMenu("Set axis from current")]
    private void SetAxisFromCurrent()
    {
        Vector3 axis;
        float angle;
        transform.rotation.ToAngleAxis(out angle, out axis);
    }

    [ContextMenu("Set origin rotation from current local")]
    private void SetOriginRotationFromCurrent()
    {
        transform.localRotation.ToAngleAxis(out float angle, out _);
        OriginRotation = angle;
    }
}
