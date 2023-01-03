using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

[BurstCompile]
public struct BoundsJob : IJobParallelFor
{
    [ReadOnly] public NativeArray<Vector3> Positions;
    
    public NativeArray<Vector3> Accelerations;
    public Vector3 AreaSize;

    public void Execute(int index)
    {
        var pos = Positions[index];
        var size = AreaSize * 0.5f;
        Accelerations[index] = Accelerations[index].Compensate(pos, size);        
    }
}
