using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] private Renderer _render;
   
    private MaterialPropertyBlock _block;

    private void Start()
    {
        _block = new MaterialPropertyBlock();
        _block.SetColor("_BaseColor", new Color(Random.value, Random.value, Random.value));
        _render.SetPropertyBlock(_block);
    }
}
