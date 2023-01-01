using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

[BurstCompile]
public struct JAcceleration : IJobParallelFor
{
    public NativeArray<Vector3> Accelerations;
    public float AccelerationLimit;
    public float DeltaTime;

    public void Execute(int index)
    {
        Vector3 acceleration = Accelerations[index];
        acceleration += acceleration * DeltaTime;        

        Accelerations[index] = acceleration;
    }
}
