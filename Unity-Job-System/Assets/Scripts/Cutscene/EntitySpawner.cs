using UnityEngine;

public class EntitySpawner : MonoBehaviour
{
    [SerializeField] private Entity _entityPrefab;

    public Entity Get()
    {
        var instance = Instantiate(_entityPrefab);
        instance.Init();
        return instance;
    }
}
