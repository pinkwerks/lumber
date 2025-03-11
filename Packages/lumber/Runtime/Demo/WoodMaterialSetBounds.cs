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
        GetComponent<Renderer>().sharedMaterial.SetVector(_boundsHash, GetComponent<MeshFilter>().mesh.bounds.size);
    }
}
