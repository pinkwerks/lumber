using UnityEngine;

public enum FitMethod
{
    None,
    Min,
    Max
}

public class WoodMaterialSetScale : MonoBehaviour
{
    [SerializeField] private bool invertSize = false;
    [SerializeField] private bool useLocalScale = false;
    [SerializeField] private FitMethod fitMethod = FitMethod.None;
    [SerializeField] private string parameterName = "_Scale";

    // Start is called before the first frame update
    void OnEnable()
    {
        SetBounds();
    }

    [ContextMenu("Set Bounds")]
    public void SetBounds()
    {
#if UNITY_EDITOR
        var size = GetComponent<MeshFilter>().sharedMesh.bounds.size;
#else
        var size = GetComponent<MeshFilter>().mesh.bounds.size;
#endif
        // normalize size based on scale
        if (useLocalScale)
        {
            size = new Vector3(size.x * transform.localScale.x, size.y * transform.localScale.y, size.z * transform.localScale.z);
        }
        switch (fitMethod)
        {
            case FitMethod.Min:
                var min = Mathf.Min(Mathf.Min(size.x, size.y), size.z);
                size = new Vector3(min, min, min);
                break;
            case FitMethod.Max:
                var max = Mathf.Max(Mathf.Max(size.x, size.y), size.z);
                size = new Vector3(max, max, max);
                break;
        }
        if (invertSize)
        {
            size = new Vector3(1f / size.x, 1f / size.y, 1f / size.z);
        }
#if UNITY_EDITOR
        GetComponent<Renderer>().sharedMaterial.SetVector(parameterName, size);
#else
        GetComponent<Renderer>().material.SetVector(parameterName, size);
#endif
    }
}
