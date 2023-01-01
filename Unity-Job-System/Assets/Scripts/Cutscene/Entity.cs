using UnityEngine;

public class Entity : MonoBehaviour
{    
    [SerializeField] private EntityView _view;

    public void Init()
    {
        _view.SetColor();
    }
}
