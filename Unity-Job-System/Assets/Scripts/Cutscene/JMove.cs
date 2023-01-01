using Unity.Burst;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Jobs;

[BurstCompile]
public struct JMove : IJobParallelForTransform
{
    public NativeArray<Vector3> Position;
    public NativeArray<Vector3> Velocities;
    public NativeArray<Vector3> Accelerations;
    public float DeltaTime;
    public Vector3 AreaSize;

    public void Execute(int index, TransformAccess transform)
    {
        Vector3 velocity = Velocities[index] + Accelerations[index] * DeltaTime;
        Vector3 direction = velocity.normalized;        

        transform.position += velocity * DeltaTime;
        transform.rotation = Quaternion.LookRotation(direction);

        Position[index] = transform.position;
        Velocities[index] = velocity;
        Accelerations[index] = Vector3.zero;
    }
}
