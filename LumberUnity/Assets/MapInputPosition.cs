using UnityEngine;

public class MapInputPosition : MonoBehaviour
{
    [Tooltip("Position at ScreenOrigin")]
    public bool localPosition = false;

    [Tooltip("Position at ScreenOrigin")]
    public Vector3 OriginPosition;

    [Tooltip("Position at ScreenOrigin + ScreenDistance")]
    public Vector3 AtDistancePosition;

    [Tooltip("Screen position where StartPosition is applied. Normalized.")]
    public Vector2 ScreenOrigin;

    [Tooltip("Distance from ScreenOrigin where EndPosition is applied")]
    public float ScreenDistance = 1;

    [SerializeField] private bool _debug;

    private DemoInputActions _demoActions;

    private void Awake()
    {
        _demoActions = new DemoInputActions();
        _demoActions.Enable();
    }

    private void Update()
    {
        var pos = _demoActions.UI.Point.ReadValue<Vector2>();
        // comensate for screen size
        pos.x /= Screen.width;
        pos.y /= Screen.height;

        var dist = Vector2.Distance(pos, ScreenOrigin);

        var mix = Mathf.InverseLerp(0, ScreenDistance, dist);

        var val = Vector3.Lerp(OriginPosition, AtDistancePosition, mix);

        if (localPosition)
        {
            transform.localPosition = val;
        }
        else
        {
            transform.position = val;
        }

        if (_debug)
        {
            Debug.Log($"pos={pos} dist={dist} mix={mix} val={val}");
        }
    }

    private void OnDestroy()
    {
        // Properly disable and dispose of the InputAction.
        _demoActions.Disable();
    }

    [ContextMenu("Set origin position")]
    public void SetOriginPosition()
    {
        if (localPosition)
        {
            OriginPosition = transform.localPosition;
        }
        else
        {
            OriginPosition = transform.position;
        }
    }

    [ContextMenu("Set at-distance position")]
    public void SetAtDistancePosition()
    {
        if (localPosition)
        {
            AtDistancePosition = transform.localPosition;
        }
        else
        {
            AtDistancePosition = transform.position;
        }
    }

    [ContextMenu("Swap start with end")]
    public void SwapStartWithEnd()
    {
        Vector3 temp = OriginPosition;
        OriginPosition = AtDistancePosition;
        AtDistancePosition = temp;
    }

    [ContextMenu("Go to origin position")]
    public void GoToStart()
    {
        transform.position = OriginPosition;
    }

    [ContextMenu("Go to distance position")]
    public void GoToEnd()
    {
        transform.position = AtDistancePosition;
    }
}
