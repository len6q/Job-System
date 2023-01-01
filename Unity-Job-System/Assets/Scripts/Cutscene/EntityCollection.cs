using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;

public class EntityCollection : MonoBehaviour
{
    [SerializeField] private EntitySpawner _spawner;
    [SerializeField] private int _countEntities;

    [SerializeField] private float _accelerationLimit;
    [SerializeField] private Vector3 _areaSize;

    private NativeArray<Vector3> _positions;
    private NativeArray<Vector3> _velocities;
    private NativeArray<Vector3> _accelerations;

    private TransformAccessArray _transformAccessArray;

    private void Start()
    {
        _positions = new NativeArray<Vector3>(_countEntities, Allocator.Persistent);
        _velocities = new NativeArray<Vector3>(_countEntities, Allocator.Persistent);
        _accelerations = new NativeArray<Vector3>(_countEntities, Allocator.Persistent);

        var transforms = new Transform[_countEntities];
        for(int i = 0; i < _countEntities; i++)
        {
            transforms[i] = _spawner.Get().transform;
            _velocities[i] = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
        }
        _transformAccessArray = new TransformAccessArray(transforms);
    }

    private void Update()
    {
        var boundsJob = new JBounds
        {
            Positions = _positions,
            Accelerations = _accelerations,
            AreaSize = _areaSize
        };

        var accelerationJob = new JAcceleration
        {
            Accelerations = _accelerations,
            AccelerationLimit = _accelerationLimit,
            DeltaTime = Time.deltaTime
        };

        var moveJob = new JMove
        {
            Position = _positions,
            Velocities = _velocities,
            Accelerations = _accelerations,
            DeltaTime = Time.deltaTime
        };
        var boundsHandle = boundsJob.Schedule(_countEntities, 0);
        var accelerationHandle = accelerationJob.Schedule(_countEntities, 0, boundsHandle);
        var moveHandle = moveJob.Schedule(_transformAccessArray, accelerationHandle);
        moveHandle.Complete();
    }

    private void OnDestroy()
    {
        _positions.Dispose();
        _velocities.Dispose();
        _accelerations.Dispose();
        _transformAccessArray.Dispose();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, _areaSize);
    }
}
