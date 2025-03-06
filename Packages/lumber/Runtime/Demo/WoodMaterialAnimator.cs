using UnityEngine;

public class WoodMaterialAnimator : MonoBehaviour
{
    [SerializeField] private float _speed = 1;
    private Material _mat;
    private int _originHash = Shader.PropertyToID("_Origin");

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
    }

    // Update is called once per frame
    void Update()
    {
        _mat.SetVector(_originHash, new Vector3(0, 0, Time.time * _speed));
    }
}
