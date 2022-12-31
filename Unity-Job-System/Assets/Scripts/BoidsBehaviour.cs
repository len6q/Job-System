using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;

public class BoidsBehaviour : MonoBehaviour
{
    [SerializeField] private int _countEntities;
    [SerializeField] private GameObject _entityPrefab;
    
    [SerializeField] private float _destinationThreshold;
    [SerializeField] private Vector3 _areaSize;
    [SerializeField] private float _velocityLimit;
    [SerializeField] private Vector3 _accelerationWeights;

    private NativeArray<Vector3> _position;
    private NativeArray<Vector3> _velocities;
    private NativeArray<Vector3> _accelerations;

    private TransformAccessArray _transformAccessArray;

    private void Start()
    {
        _position = new NativeArray<Vector3>(_countEntities, Allocator.Persistent);
        _velocities = new NativeArray<Vector3>(_countEntities, Allocator.Persistent);
        _accelerations = new NativeArray<Vector3>(_countEntities, Allocator.Persistent);

        var transforms = new Transform[_countEntities];
        for(int i = 0; i < _countEntities; i++)
        {
            transforms[i] = Instantiate(_entityPrefab).transform;
            _velocities[i] = Random.insideUnitSphere;
        }
        _transformAccessArray = new TransformAccessArray(transforms);

    }

    private void Update()
    {
        var boundsJob = new BoundsJob()
        {
            Positions = _position,
            Accelerations = _accelerations,
            AreaSize = _areaSize
        };

        var accelerationJob = new AccelerationJob()
        {
            Positions = _position,
            Velocities = _velocities,
            Accelerations = _accelerations,
            Weights = _accelerationWeights,
            DestinationThreshold = _destinationThreshold
        };

        var moveJob = new MoveJob()
        {
            Positions = _position,
            Velocities = _velocities,
            Accelerations = _accelerations,
            VelocityLimit = _velocityLimit,
            DeltaTime = Time.deltaTime
        };

        JobHandle boundsHandle = boundsJob.Schedule(_countEntities, 0);
        JobHandle accelerationHandle = accelerationJob.Schedule(_countEntities, 0, boundsHandle);
        JobHandle moveHandle = moveJob.Schedule(_transformAccessArray, accelerationHandle);
        moveHandle.Complete();
    }

    private void OnDestroy()
    {
        _position.Dispose();
        _velocities.Dispose();
        _accelerations.Dispose();
        _transformAccessArray.Dispose();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(Vector3.zero, _areaSize);
    }
}
