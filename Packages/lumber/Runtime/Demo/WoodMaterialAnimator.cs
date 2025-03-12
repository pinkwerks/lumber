using UnityEngine;

public class WoodMaterialAnimator : MonoBehaviour
{
    [SerializeField] private Vector3 _originSpeed = Vector3.zero;
    [SerializeField] private float _ringOffsetSpeed = 1;
    private Material _mat;
    private int _originHash = Shader.PropertyToID("_Origin");
    private int _ringOffsetHash = Shader.PropertyToID("_Ring_offset");

    // Start is called before the first frame update
    void Start()
    {
        // get the material
        _mat = GetComponent<Renderer>().material;
        // verify the material has the property
        if (!_mat.HasProperty(_originHash))
        {
            Debug.LogError("Material does not have the property Origin");
            enabled = false;
        }
        if (!_mat.HasProperty(_ringOffsetHash))
        {
            Debug.LogError("Material does not have the property RingOffset");
            enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        var t = Time.time;
        _mat.SetVector(_originHash, new Vector3(t * _originSpeed.x, t * _originSpeed.y, t * _originSpeed.z));
        _mat.SetFloat(_ringOffsetHash, t * _ringOffsetSpeed);
    }
}
