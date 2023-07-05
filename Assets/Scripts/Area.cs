using UnityEngine;


public class Area : MonoBehaviour
{
    [SerializeField] public float MinX;
    [SerializeField] public float MaxX;
    [SerializeField] public float MinZ;
    [SerializeField] public float MaxZ;

    public Vector3 RandomPoint => new Vector3(Random.Range(MinX, MaxX), 0, Random.Range(MinZ, MaxZ));

    private void OnDrawGizmosSelected()
    {
        Vector3 center = new Vector3((MinX + MaxX) / 2, 0.5f, (MinZ + MaxZ) / 2);
        Vector3 size = new Vector3((-MinX + MaxX), 1f, (-MinZ + MaxZ));

        Gizmos.DrawCube(center, size);
    }

    private void OnValidate()
    {
        float x1 = MinX;
        float x2 = MaxX;
        MinX = Mathf.Min(x1, x2);
        MaxX = Mathf.Max(x1, x2);

        float z1 = MinZ;
        float z2 = MaxZ;
        MinZ = Mathf.Min(z1, z2);
        MaxZ = Mathf.Max(z1, z2);
    }
}
