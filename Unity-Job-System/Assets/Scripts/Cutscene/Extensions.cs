using Unity.Mathematics;
using UnityEngine;

public static class Extensions
{
    private const float THRESHOLD = 3f;
    private const float MULTIPLIER = 100f;

    public static Vector3 Compensate(this Vector3 vector, Vector3 pos, Vector3 size)
    {
        vector +=
            Compensate(-size.x - pos.x, Vector3.right) +
            Compensate(size.x - pos.x, Vector3.left) +
            Compensate(-size.z - pos.z, Vector3.forward) +
            Compensate(size.z - pos.z, Vector3.back);
        return vector;
    }

    private static Vector3 Compensate(float delta, Vector3 direction)
    {
        delta = math.abs(delta);
        if (delta > THRESHOLD)
        {
            return Vector3.zero;
        }
        return direction;
    }
}
