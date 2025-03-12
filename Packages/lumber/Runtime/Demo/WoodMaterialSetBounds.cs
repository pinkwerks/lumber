using UnityEngine;

public class WoodMaterialSetBounds : MonoBehaviour
{
    private Material _mat;
    private const string _boundsName = "_Bounds";
    private int _boundsHash = Shader.PropertyToID(_boundsName);

    // Start is called before the first frame update
    void Start()
    {
        SetBounds();
    }

    [ContextMenu("Set Bounds")]
    public void SetBounds()
    {
#if UNITY_EDITOR
        GetComponent<Renderer>().sharedMaterial.SetVector(_boundsHash, GetComponent<MeshFilter>().sharedMesh.bounds.size);
#else
        GetComponent<Renderer>().material.SetVector(_boundsHash, GetComponent<MeshFilter>().sharedMesh.bounds.size);
#endif
    }
}
