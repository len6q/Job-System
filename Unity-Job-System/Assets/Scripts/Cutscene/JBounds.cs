using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

[BurstCompile]
public struct JBounds : IJobParallelFor
{
    [ReadOnly] public NativeArray<Vector3> Positions;

    public NativeArray<Vector3> Accelerations;
    public Vector3 AreaSize;

    public void Execute(int index)
    {
        Vector3 pos = Positions[index];
        Vector3 size = AreaSize * 0.5f;
        Accelerations[index].Compensate(pos, size);
    }
}
