using UnityEngine;

public class EntityView : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;

    private MaterialPropertyBlock _block;

    public void SetColor()
    {
        _block = new MaterialPropertyBlock();

        _block.SetColor("_BaseColor", new Color(Random.value, Random.value, Random.value));
        _renderer.SetPropertyBlock(_block);
    }

}
